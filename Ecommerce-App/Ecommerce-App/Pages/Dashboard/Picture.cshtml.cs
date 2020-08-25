using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
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
        private readonly IImage _image;
        private readonly IProduct _productservice;

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

        /// <summary>
        /// OnPost - When the administrator posts to this page, operations take place between the Azure blob storage account and this website that in the end results in a product image being associated with a certain product.
        /// </summary>
        /// <returns>The task complete, a new product image is sent to the cloud storage account and associated with a product rendered to the site. That product image is now tied to the product and will be rendered alongside it where applicable.</returns>
        public async Task OnPost()
        {
            string ext = Path.GetExtension(Image.FileName);
            
            // Convert image into a stream

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
                // Gets url from blob
                string imageUri = blobReference.Uri.AbsoluteUri;

                Product product = await _productservice.GetProductByName(Name);
                product.Image = imageUri;

                await _productservice.Update(product);
            }
        }
    }
}
