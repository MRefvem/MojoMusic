using Ecommerce_App.Models;
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
        private readonly IConfiguration _config;
        public CloudStorageAccount CloudStorageAccount { get; set; }
        public CloudBlobClient CloudBlobClient { get; set; }

        //Connect to account
        public Blob( IConfiguration configuration)
        {
            _config = configuration;
            var storagecredentials = new StorageCredentials(_config["BlobAccountName"], _config["BlobKey"]);
            CloudStorageAccount = new CloudStorageAccount(storagecredentials, true);
            CloudBlobClient = CloudStorageAccount.CreateCloudBlobClient();
        }


        /// <summary>
        /// GetContainer - Method gets container from the Azure client.
        /// </summary>
        /// <param name="containerName">The name of the azure container associated with that administrator.</param>
        /// <returns>The reference to the administrator's cloud storage account.</returns>
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


        /// <summary>
        /// GetBlob - Method that gets blob from the container in azure.
        /// </summary>
        /// <param name="imageName">The name of the image.</param>
        /// <param name="containerName">The name of the container.</param>
        /// <returns>The cloud blob object searched for in the request.</returns>
        public async Task<CloudBlob> GetBlob(string imageName, string containerName)
        {
            var container = await GetContainer(containerName);
            CloudBlob cb = container.GetBlobReference(imageName);

            return cb;
        }


        /// <summary>
        /// UploadImageFile - Method sends image into the blob.
        /// </summary>
        /// <param name="containerName">The name of the container.</param>
        /// <param name="fileName">The file name of the image.</param>
        /// <param name="image">The image.</param>
        /// <param name="contentType">The typing of the content.</param>
        /// <returns>The completed task: a new file was uploaded to the blob and associated with a product.</returns>
        public async Task UploadImageFile( string containerName, string fileName, byte[] image, string contentType)
        {
            var container = await GetContainer(containerName);
            var blobReference = container.GetBlockBlobReference(fileName);
            blobReference.Properties.ContentType = contentType;
        
            await blobReference.UploadFromByteArrayAsync(image, 0, image.Length);  
        }  
    }
}
