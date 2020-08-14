using Ecommerce.Models;
using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    // All our magic to manage the uploading of blob.
    public class Blob : IImage
    {
        private StoreDbContext _context;
        private IConfiguration _config;
        public CloudStorageAccount CloudStorageAccount { get; set; }
        public CloudBlobClient CloudBlobClient { get; set; }

        //Connect to account
        public Blob( IConfiguration configuration , StoreDbContext context)
        {
            _context = context;
            _config = configuration;
            var storagecredentials = new StorageCredentials(_config["BlobAccountName"], _config["BlobKey"]);
            CloudStorageAccount = new CloudStorageAccount(storagecredentials, true);
            CloudBlobClient = CloudStorageAccount.CreateCloudBlobClient();

        }


        /// <summary>
        /// Gets container from the client
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public async Task<CloudBlobContainer> GetContainer(string containerName)
        {
            // creating a cloud referencs to the existing container name
            CloudBlobContainer cbc = CloudBlobClient.GetContainerReference(containerName.ToLower());
            await cbc.CreateIfNotExistsAsync();

            await cbc.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            return cbc;
        }
      
        // gets blob from the container in azure
        public async Task<CloudBlob> GetBlob(string imageName, string containerName)
        {
            var container = await GetContainer(containerName);
            CloudBlob cb = container.GetBlobReference(imageName);

            return cb;
        }

        // sends image into the blob
        public async Task UploadImageFile( string containerName, string fileName, byte[] image, string contentType)
        {
            
            var container = await GetContainer(containerName);
            var blobReference = container.GetBlockBlobReference(fileName);
            blobReference.Properties.ContentType = contentType;
        
            await blobReference.UploadFromByteArrayAsync(image, 0, image.Length);

           
          
           
        }
       
    }
}
