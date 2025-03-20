namespace FinalProject.Models
{
    public class AttachmentViewModel
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime LastModifiedTimestamp { get; set; }
        public int ProjectId { get; set; }
    }
}
