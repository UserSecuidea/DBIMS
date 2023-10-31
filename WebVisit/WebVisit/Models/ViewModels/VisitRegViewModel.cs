namespace WebVisit.Models
{
    public class VisitRegViewModel
    {
        
        public VisitPerson VisitPerson { get; set; } = new VisitPerson();
        public List<CommonCode> CodeLocation { get; set; } = new(); 
        public List<CommonCode> CodeVisitPurpose { get; set; } = new();
         public List<CommonCode> CodePlace { get; set; } = new(); 
         public List<CommonCode> CodeVisitManualPurpose { get; set; } = new();          
         public List<CommonCode> CodeGenderType { get; set; } = new(); 
         public List<CommonCode> CodeVipType { get; set; } = new(); 
         public List<CommonCode> CodeImportPurpose { get; set; } = new(); 
         public List<CommonCode> CodeCarryItem { get; set; } = new();
        
        // 단위 Unit : 0: 개, 1: 대, 2: EA, 3: SIK
        public List<CommonCode> CodeUnit = new();

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
        public VisitRegViewModel() { }
    }
}