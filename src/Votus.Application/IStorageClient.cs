using System.Threading.Tasks;

namespace Votus.Application
{
    public interface IStorageClient
    {
        Task UploadFileAsync(string fileName, byte[] binaryData);
    }
}