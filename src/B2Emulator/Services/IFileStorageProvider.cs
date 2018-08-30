using System.IO;
using System.Threading.Tasks;

namespace B2Emulator.Services
{
    /// <summary>
    /// A service that facilitates storage and retrieval of file binary data.
    /// </summary>
    public interface IFileStorageProvider
    {
        Task<Dtos.File> RetrieveFileAsync(string fileId);

        Task<bool> StoreFileAsync(Dtos.File file);
    }
}