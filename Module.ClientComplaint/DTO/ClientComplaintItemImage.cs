namespace Module.ClientComplaint.DTO
{
    public class ClientComplaintItemImage
    {
        public int? ClientComplaintItemImageID { get; set; }
        public int? ClientComplaintItemID { get; set; }
        public string ImageFile { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }

        //support image field
        public bool? ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
    }
}
