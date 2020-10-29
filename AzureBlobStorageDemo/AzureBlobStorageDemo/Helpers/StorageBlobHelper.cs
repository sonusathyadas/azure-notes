using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace AzureBlobStorageDemo.Helpers
{
    public class StorageBlobHelper
    {
        private readonly BlobServiceClient serviceClient;

        public StorageBlobHelper(string storageConnectionString)
        {
            serviceClient = new BlobServiceClient(storageConnectionString);
            
        }

        public async Task<bool> CreateBlobContainer(string containerName)
        {
            try
            {
                var containerClient = serviceClient.GetBlobContainerClient(containerName);
                await containerClient.CreateIfNotExistsAsync();                
                Dictionary<string, string> metadata = new Dictionary<string, string>()
                {
                    {"author", "Sonu Sathyadas" },
                    { "project", "LTI Project demo" },
                    { "creationDate", DateTime.Now.ToString() }
                };
                await containerClient.SetMetadataAsync(metadata);
                await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }

        public async Task<string> UploadBlob(string filePath, string containerName)
        {
            var blobName= Path.GetFileName(filePath);
            var containerClient = serviceClient.GetBlobContainerClient(containerName);
            var blobClient=   containerClient.GetBlobClient(blobName);
            var response = await blobClient.UploadAsync(filePath,true);
            return blobClient.Uri.AbsoluteUri;
        }


    }
}
