using File = fileProcessor.Models.File;

namespace fileProcessor.models
{
    public class Country
    {
        public int Idcountry { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Color { get; set; }
        public int Idfile { get; set; }
        public File File { get; set; }
    }
}
