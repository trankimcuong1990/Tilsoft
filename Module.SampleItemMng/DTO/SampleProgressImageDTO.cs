namespace Module.SampleItemMng.DTO
{
    public class SampleProgressImageDTO
    {
        public int SampleProgressImageID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }

        public bool HasChanged { get; set; }
        public string NewFile { get; set; }
    }
}
