namespace HillFile.Lib.Abstractions.Models
{
    public enum ListingType
    {
        File,
        Directory
    }
    
    public class HFListing
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public ListingType Type { get; set; }
    }
}