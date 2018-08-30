using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using B2Emulator.Dtos;
using B2Emulator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace B2Emulator.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IFileStorageProvider _fileStorageProvider;

        public ApplicationController(IFileStorageProvider fileStorageProvider)
        {
            _fileStorageProvider = fileStorageProvider;
        }

        [HttpPost("b2_upload_file")]
        public async Task<ActionResult<B2UploadCompleteDetails>> UploadFile()
        {
            var errors = new List<string>();

            // Check for presence of required headers
            var headers = Request.Headers;

            var contentType = Utils.GetHeader(headers, "content-type", errors);
            var authorization = Utils.GetHeader(headers, "authorization", errors);
            var xBzFileName = Utils.GetHeader(headers, "x-bz-file-name", errors);
            var xBzContentSha1 = Utils.GetHeader(headers, "x-bz-content-sha1", errors);

            if (errors.Count != 0)
            {
                return BadRequest(errors);
            }

            // Store the file

            var fileId = Guid.NewGuid().ToString();

            var fileStoreSuccess = await _fileStorageProvider.StoreFileAsync(new Dtos.File
            {
                Id = fileId,
                Data = Request.Body,
                Metadata = null,
            });

            if (!fileStoreSuccess)
            {
                return BadRequest();
            }

            // Success. Emulate B2-generated values.
            return Ok(new B2UploadCompleteDetails
            {
                FileId = fileId,
                FileName = xBzFileName,
                AccountId = Environment.GetEnvironmentVariable("B2_CLOUD_STORAGE_ACCOUNT_ID"),
                BucketId = Environment.GetEnvironmentVariable("B2_CLOUD_STORAGE_BUCKET_ID"),
                ContentLength = 123,
                ContentSha1 = xBzContentSha1,
                ContentType = contentType,
            });
        }

        [HttpGet("b2_download_file_by_id")]
        public async Task<ActionResult<Stream>> DownloadFileById([FromQuery] string fileId)
        {
            var retrievedFile = await _fileStorageProvider.RetrieveFileAsync(fileId);

            if (retrievedFile == null)
            {
                return NotFound();
            }

            return Ok(retrievedFile.Data);
        }
    }
}
