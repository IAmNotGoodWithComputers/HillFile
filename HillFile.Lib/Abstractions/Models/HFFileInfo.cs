using System;

namespace HillFile.Lib.Abstractions.Models
{
    public class HFFileInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public long ByteSize { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime AccessDate { get; set; }
        
    }
}