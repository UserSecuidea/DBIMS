namespace WebVisit.Models
{
    public class PersonRegViewModel
    {
        public Person Person = new();
        public IEnumerable<Level> Levels { get; set; } = new List<Level>();
        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
        public List<CommonCode> CodeLocation = new();
        public List<CommonCode> CodeGenderType = new();
        public List<CommonCode> CodeForeignerType = new();
        public List<CommonCode> CodeWorkTypeCode = new();
        public List<CommonCode> CodeImmStatusCode = new();
        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}