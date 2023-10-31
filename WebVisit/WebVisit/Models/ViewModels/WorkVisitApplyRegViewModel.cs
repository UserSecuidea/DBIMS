namespace WebVisit.Models
{
    public class WorkVisitApplyRegViewModel
    {
        public WorkApply WorkApply { get; set; } = new WorkApply();
        // CodeLocation CodeImportPurpose CodePlace CodeCarryItem
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation  { get; set; } = new(); 
        public List<CommonCode> CodeImportPurpose  { get; set; } = new(); 
        public List<CommonCode> CodePlace  { get; set; } = new(); 
        public List<CommonCode> CodeCarryItem  { get; set; } = new(); 
        public List<CommonCode> CodeGenderType { get; set; } = new();
        // 단위 Unit : 0: 개, 1: 대, 2: EA, 3: SIK
        public List<CommonCode> CodeUnit = new();
        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}