using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IImage
    {
        /// <summary>
        /// GetContainer - Method gets container from the Azure client.
        /// </summary>
        /// <param name="containerName">The name of the azure container associated with that administrator.</param>
        /// <returns>The reference to the administrator's cloud storage account.</returns>
        Task<CloudBlobContainer> GetContainer(string containerName);


        /// <summary>
        /// GetBlob - Method that gets blob from the container in azure.
        /// </summary>
        /// <param name="imageName">The name of the image.</param>
        /// <param name="containerName">The name of the container.</param>
        /// <returns>The cloud blob object searched for in the request.</returns>
        Task<CloudBlob> GetBlob(string imageName, string containerName);

        /// <summary>
        /// UploadImageFile - Method sends image into the blob.
        /// </summary>
        /// <param name="containerName">The name of the container.</param>
        /// <param name="fileName">The file name of the image.</param>
        /// <param name="image">The image.</param>
        /// <param name="contentType">The typing of the content.</param>
        /// <returns>The completed task: a new file was uploaded to the blob and associated with a product.</returns>
        Task UploadImageFile(string containerName, string fileName, byte[] image, string contentType);
    }
}
