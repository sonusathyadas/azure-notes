using AzureBlobStorageDemo.Helpers;
using System;
using System.Threading.Tasks;

namespace AzureBlobStorageDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=sonustorageaccount2020;AccountKey=1zuB0Ov+SkeWDLlQmRgodY0MkKDEbPe/SWpqNx8yKcja/MiZUw4OfNMGu9VUKFGE1dP5w5juabXI8DCKj+8JVg==;EndpointSuffix=core.windows.net";

            StorageBlobHelper blobHelper = new StorageBlobHelper(storageConnectionString);

            await blobHelper.CreateBlobContainer("myfiles");

            var blobUrl= await blobHelper.UploadBlob(@"C:\Users\sonus\Desktop\1.png", "myfiles");

            Console.WriteLine($"File uploaded to {blobUrl}");

            Console.ReadLine();
        }
    }
}
