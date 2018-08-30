using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using B2Emulator.Services;
using Xunit;

namespace B2Emulator.Tests
{
    public class InMemoryFileStorageProviderTests
    {
        [Fact]
        public async void Able_To_Store_File()
        {
            var provider = new InMemoryFileStorageProvider();

            var testFile = new Dtos.File
            {
                Id = Guid.NewGuid().ToString(),
                Data = TestDataStream(),
                Metadata = TestMetadata(),
            };

            var success = await provider.StoreFileAsync(testFile);

            Assert.NotNull(success);
            Assert.IsType<bool>(success);
            Assert.True(success);
        }

        [Fact]
        public async void Able_To_Retrieve_Data_By_Id()
        {
            // Store so that we can retrieve
            var provider = new InMemoryFileStorageProvider();

            var testFile = new Dtos.File
            {
                Id = Guid.NewGuid().ToString(),
                Data = TestDataStream(),
                Metadata = TestMetadata(),
            };

            var success = await provider.StoreFileAsync(testFile);

            Assert.NotNull(success);
            Assert.IsType<bool>(success);
            Assert.True(success);

            // Now, proceed to retrieve.
            var retrievedFile = await provider.RetrieveFileAsync(testFile.Id);

            Assert.NotNull(retrievedFile);
            Assert.NotEmpty(retrievedFile.Metadata); // expect 1 entry
            Assert.True(retrievedFile.Data.Length > 0);
        }

        private static string TestId()
        {
            return Guid.NewGuid().ToString();
        }
        private static Stream TestDataStream()
        {
            var testString = "Hello, World!";

            var testData = Encoding.UTF8.GetBytes(testString);

            return new MemoryStream(testData);
        }

        private static IDictionary<string, string> TestMetadata()
        {
            var metadata = new Dictionary<string, string>();

            metadata.Add("creation date", "today, perhaps");

            return metadata;
        }
    }
}
