using WebVisit.Models.DomainModels.Passport;

namespace WebVisit.Models
{
    public class PersonDetailViewModel
    {
        public IEnumerable<Level> Levels { get; set; } = new List<Level>();
        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
        public Person Person { get; set; } = new Person();
        public Company Company { get; set; } = new Company();
        
        public List<CommonCode> CodeLocation = new(); 
        public List<CommonCode> CodeGenderType = new();
        public List<CommonCode> CodePersonStatus = new(); 
        
        public List<CommonCode> CodeForeignerType = new(); 
        public List<CommonCode> CodeWorkTypeCode = new(); 
        public List<CommonCode> CodeImmStatusCode = new(); 

        public PersonGridData CurrentRoute { get; set; } = new PersonGridData();
        public ViewPerson ViewPerson { get; set; } = new();
        public ViewPersonCard ViewPersonCard { get; set; } = new();
       
    }
}