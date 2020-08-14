using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Dashboard
{
    [Authorize(Policy = "AdminOnly" )]
    public class PictureModel : PageModel
    {
        private IImage _image;
        private IProduct _productservice;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }
       

        public PictureModel(IImage image , IProduct productservice)
        {
            _image = image;
            _productservice = productservice;
        }

        public void OnGet()
        {
        }

        public async Task OnPost()
        {
            string ext = Path.GetExtension(Image.FileName);
            
            // convert Image into a stream

            if (Image != null)
            {
                using (var stream = new MemoryStream())
                {
                    await Image.CopyToAsync(stream);
                    var bytes = stream.ToArray();
                    await _image.UploadImageFile("productimages", $"{Name}{ext}", bytes, Image.ContentType);
                
                }
                // Gets the blob 
                var blobReference = await _image.GetBlob($"{Name}{ext}", "productimages");
                // gets url from blob
                string imageUri = blobReference.Uri.AbsoluteUri;

                Product product = await _productservice.GetProductByName(Name);
                product.Image = imageUri;
                await _productservice.Update(product);

            }
            
        }
    }
}
