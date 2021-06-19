using Azure.Storage.Blobs;
using Ilex.Server.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ilex.Server.Services
{
    public class BlobStorage : IBlobStorage
    {
        private readonly string connectionString = "";
        private readonly string containerName = "";



        public async Task<string> SaveImageAsync(IFormFile image)
        {
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);


            string fileName = Path.GetFileNameWithoutExtension(image.FileName);
            string extension = Path.GetExtension(image.FileName);

            string file = fileName + DateTime.Now.ToString("ddmmyyss") + extension;

            BlobClient blob = container.GetBlobClient(file);

            await blob.UploadAsync(image.OpenReadStream());

            return file;
        }


        public async Task<bool> DeleteBlobAsync(string ImageName)
        {
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

            var response = await container.DeleteBlobIfExistsAsync(ImageName);

            return true;
        }

    }
}
