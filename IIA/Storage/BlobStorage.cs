using System;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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
        /// Reads a file
        /// </summary>
        /// <param name="conatinerName">prefix to be added</param>
        /// <param name="filepath">file</param>
        /// <returns>url of the file</returns>
        public static BinaryData GetFile(string conatinerName, string filepath)
        {
            string connectionString = Environment.GetEnvironmentVariable("BlobContainerEndpoint");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(conatinerName);
            BlobClient blobClient = containerClient.GetBlobClient(filepath);
            BlobDownloadResult response = blobClient.DownloadContent();
            return response.Content;
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

        /// <summary>
        /// Stores a file
        /// </summary>
        /// <param name="file">file</param>
        /// <returns>url of the file</returns>
        public static string UploadExcelFile(byte[] file)
        {
            string extension = ".xlsx";
            string fileName = Guid.NewGuid().ToString() + extension;
            string connectionString = Environment.GetEnvironmentVariable("BlobContainerEndpoint");

            var container = new BlobContainerClient(connectionString, "adminexcels");
            var blockBlob = container.GetBlobClient(fileName);

            using (Stream stream = new MemoryStream(file))
            {
                blockBlob.Upload(stream);

                return Environment.GetEnvironmentVariable("BlobContainerPath") + "adminexcels" + '/' + fileName;
            }
        }

        /// <summary>
        /// Stores a file
        /// </summary>
        /// <param name="file">file</param>
        /// <returns>url of the file</returns>
        public static string UploadExcelFileForPayment(byte[] file)
        {
            string extension = ".csv";
            string fileName = Guid.NewGuid().ToString() + extension;
            string connectionString = Environment.GetEnvironmentVariable("BlobContainerEndpoint");

            var container = new BlobContainerClient(connectionString, "invoiceexcel");
            var blockBlob = container.GetBlobClient(fileName);

            using (Stream stream = new MemoryStream(file))
            {
                blockBlob.Upload(stream);

                return Environment.GetEnvironmentVariable("BlobContainerPath") + "invoiceexcel" + '/' + fileName;
            }
        }

        /// <summary>
        /// Stores a file
        /// </summary>
        /// <param name="file">file</param>
        /// <returns>url of the file</returns>
        public static string UploadExcelFileForMembers(byte[] file)
        {
            string extension = ".xlsx";
            string fileName = Guid.NewGuid().ToString() + extension;
            string connectionString = Environment.GetEnvironmentVariable("BlobContainerEndpoint");

            var container = new BlobContainerClient(connectionString, "memberexcel");
            var blockBlob = container.GetBlobClient(fileName);

            using (Stream stream = new MemoryStream(file))
            {
                blockBlob.Upload(stream);

                return Environment.GetEnvironmentVariable("BlobContainerPath") + "memberexcel" + '/' + fileName;
            }
        }
    }
}
