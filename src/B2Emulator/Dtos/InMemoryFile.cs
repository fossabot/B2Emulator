using System.Collections.Generic;

namespace B2Emulator.Dtos
{
    public class InMemoryFile
    {
        public string Id { get; set; }

        public byte[] Data { get; set; }

        public IDictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    }
}