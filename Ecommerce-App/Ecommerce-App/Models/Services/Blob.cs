using Ecommerce_App.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    // All our magic to manage the uploading of blob.
    public class Blob : IImage
    {
        public Task UploadImage()
        {
            throw new NotImplementedException();
        }
    }
}
