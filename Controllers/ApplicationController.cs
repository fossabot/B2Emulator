using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using B2Emulator.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2Emulator.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        // GET api/values
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

            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();

                if (body.Length == 0)
                {
                    errors.Add("Uploaded file must be at least one byte.");
                    return BadRequest(errors);
                }

                // Success. Emulate B2-generated values.
                return Ok(new B2UploadCompleteDetails
                {
                    FileId = Guid.NewGuid().ToString(),
                    FileName = xBzFileName,
                    AccountId = Environment.GetEnvironmentVariable("B2_CLOUD_STORAGE_ACCOUNT_ID"),
                    BucketId = Environment.GetEnvironmentVariable("B2_CLOUD_STORAGE_BUCKET_ID"),
                    ContentLength = body.Length,
                    ContentSha1 = xBzContentSha1,
                    ContentType = contentType,
                });
            }
        }
    }
}
