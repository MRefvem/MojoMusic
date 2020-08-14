using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IImage
    {
        Task<CloudBlobContainer> GetContainer(string containerName);
        Task<CloudBlob> GetBlob(string imageName, string containerName);
        Task UploadImageFile(string containerName, string fileName, byte[] image, string contentType);

    }
}
