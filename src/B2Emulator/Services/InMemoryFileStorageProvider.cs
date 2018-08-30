using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace B2Emulator.Services
{
    /// <summary>
    /// Stores data in memory.
    /// </summary>
    public class InMemoryFileStorageProvider : IFileStorageProvider
    {
        /// <summary>
        /// Contains the data until the application process exits.
        /// </summary>
        private IDictionary<string, Dtos.InMemoryFile> _data = new ConcurrentDictionary<string, Dtos.InMemoryFile>();

        public async Task<Dtos.File> RetrieveFileAsync(string fileId)
        {
            // Validate

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            Dtos.InMemoryFile memFile;

            if (!_data.TryGetValue(fileId, out memFile))
            {
                // Doesn't exist.
                return null;
            }

            return new Dtos.File
            {
                Id = memFile.Id,
                Data = new MemoryStream(memFile.Data),
                Metadata = memFile.Metadata,
            };
        }

        public async Task<bool> StoreFileAsync(Dtos.File file)
        {
            // Validate:

            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            if (file.Id == null)
            {
                throw new ArgumentNullException("file.Id");
            }

            if (file.Data == null)
            {
                throw new ArgumentNullException("file.Data");
            }

            // (file.Metadata can be null)

            // Continue:

            var fileData = file.Data;

            // Copy to local memory stream and get raw bytes to store
            var memStream = new MemoryStream();
            await fileData.CopyToAsync(memStream);
            var rawData = memStream.ToArray();

            try
            {
                _data.TryAdd(file.Id, new Dtos.InMemoryFile
                {
                    Id = file.Id,
                    Data = rawData,
                    Metadata = file.Metadata,
                });

                return true;
            }
            catch (OverflowException e)
            {
                // The dictionary is full.
                return false;
            }
        }
    }
}