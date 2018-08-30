using System;

namespace B2Emulator.Dtos
{
    public class B2UploadCompleteDetails
    {
        public string FileId { get; set; }

        public string FileName { get; set; }

        public string AccountId { get; set; }

        public string BucketId { get; set; }

        public long ContentLength { get; set; }

        public string ContentSha1 { get; set; }

        public string ContentType { get; set; }

        public object FileInfo { get; set; } = null;

        public string Action { get; set; } = "upload";

        public long UploadTimestamp { get; set; } = Utils.DateTimeNowMs();
    }
}