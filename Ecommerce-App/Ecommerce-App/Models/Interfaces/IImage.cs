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
        /// Gets container from the client
        /// </summary>
        /// <param name="containerName"> name of the azure container</param>
        /// <returns> the reference to our container</returns>
        Task<CloudBlobContainer> GetContainer(string containerName);


        /// <summary>
        /// gets blob from the container in azure
        /// </summary>
        /// <param name="imageName">the name of the image</param>
        /// <param name="containerName">the name of the container</param>
        /// <returns> cloud blob object</returns>
        Task<CloudBlob> GetBlob(string imageName, string containerName);

        /// <summary>
        /// sends image into the blob
        /// </summary>
        /// <param name="containerName">the name of the container</param>
        /// <param name="fileName"> filename of the image</param>
        /// <param name="image">the image</param>
        /// <param name="contentType"></param>
        /// <returns>task completion</returns>
        Task UploadImageFile(string containerName, string fileName, byte[] image, string contentType);

    }
}
