using System;
using System.IO;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace IIABackend
{
    /// <summary>
    /// Class to manange all Cosmos DB functions.
    /// </summary>
    public static class BlobStorage
    {
        /// <summary>
        /// Stores a file
        /// </summary>
        /// <param name="file">prefix to be added</param>
        /// <param name="fileDirectory">file</param>
        /// <param name="name">name</param>
        /// <returns>url of the file</returns>
        public static string UploadFile(IFormFile file, string fileDirectory, string name)
        {
            string extension = Path.GetExtension(file.FileName);
            string fileName = Guid.NewGuid().ToString() + "_" + name + extension;
            string connectionString = Environment.GetEnvironmentVariable("BlobContainerEndpoint");

            var container = new BlobContainerClient(connectionString, fileDirectory);
            var blockBlob = container.GetBlobClient(fileName);

            using (var fileStream = file.OpenReadStream())
            {
                blockBlob.Upload(fileStream);

                return Environment.GetEnvironmentVariable("BlobContainerPath") + fileDirectory + "/" + fileName;
            }
        }

        /// <summary>
        /// Stores a file
        /// </summary>
        /// <param name="file">file</param>
        /// <returns>url of the file</returns>
        public static string UploadInvoice(byte[] file)
        {
            string extension = ".pdf";
            string fileName = Guid.NewGuid().ToString() + extension;
            string connectionString = Environment.GetEnvironmentVariable("BlobContainerEndpoint");

            var container = new BlobContainerClient(connectionString, "invoice");
            var blockBlob = container.GetBlobClient(fileName);

            using (Stream stream = new MemoryStream(file))
            {
                blockBlob.Upload(stream);

                return Environment.GetEnvironmentVariable("BlobContainerPath") + "invoice" + '/' + fileName;
            }
        }
    }
}
