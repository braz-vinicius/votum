using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Votus.Application;

namespace Votus.Infrastructure.Storage.Providers
{
    public class AzureStorageClient : IStorageClient
    {
        private readonly BlobServiceClient _blobService;
        private readonly BlobContainerClient _blobContainerClient;

        public AzureStorageClient(BlobServiceClient blobService)
        {
            _blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
            _blobContainerClient = blobService.GetBlobContainerClient("profile-images");
        }

        public async Task UploadFileAsync(string fileName, byte[] binaryData)
        {
            await _blobContainerClient.DeleteBlobIfExistsAsync(fileName);
            await _blobContainerClient.UploadBlobAsync(fileName, new BinaryData(binaryData));
        }
    }
}
