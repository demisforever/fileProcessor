using fileProcessor.models;

namespace fileProcessor.Models
{
    public class File
    {
        public int Idfile { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public ICollection<Country> Countries { get; set; }
    }
}
