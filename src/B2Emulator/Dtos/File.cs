using System.Collections.Generic;
using System.IO;

namespace B2Emulator.Dtos
{
    public class File
    {
        public string Id { get; set; }

        public Stream Data { get; set; }

        public IDictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    }
}