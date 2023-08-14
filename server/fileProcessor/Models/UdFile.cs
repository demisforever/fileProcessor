namespace fileProcessor.Models
{
    public class UdFile
    {
        public int idfile { get; set; }
        public DateTime timestamp { get; set; }
        public IFormFile ffile { get; set; }
    }
}
