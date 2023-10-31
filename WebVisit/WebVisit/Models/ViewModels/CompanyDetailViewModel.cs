namespace WebVisit.Models
{
    public class CompanyDetailViewModel
    {
        public Company Company { get; set; } = new Company();
        public Person Person { get; set; } = new Person();
        public List<CommonCode> CodeCompanyStatus = new(); 
        public List<CommonCode> CodeCompanyType = new (); 

        public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        public bool IsMaster { get; set; } = false;
        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}