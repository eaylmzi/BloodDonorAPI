using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace DonorAPI.Services.Blob
{
    public class BlobService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobService()
        {
            var blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=bloodbankphoto;AccountKey=pxhVFaiA7EpwjAiecue94W3YH+lr5givQYAz3jttjBIJdPUTQm0S9WwLxO7ucckEUtZY0vR6ANnH+AStnMkfeQ==;EndpointSuffix=core.windows.net");
            _containerClient = blobServiceClient.GetBlobContainerClient("images");
        }

        public async Task<string> UploadPhotoAsync(IFormFile photo, string name, string surname, int id)
        {
            var fileName = $"{name}{surname}{id}{Path.GetExtension(photo.FileName)}";
            var blobClient = _containerClient.GetBlobClient(fileName);

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = "image/jpeg" // İçerik türünü ayarla
                }
            };

            using (var stream = photo.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, httpHeaders: new BlobHttpHeaders { ContentType = "image/jpeg" });
            }

            return blobClient.Uri.ToString();
        }
    }
}
