namespace WebVisit.Models
{
    public class VisitDetailViewModel
    {
        public IEnumerable<VisitApplyPerson> VisitApplyPersons { get; set; } = new List<VisitApplyPerson>();
        
        public VisitApply VisitApply { get; set; } = new VisitApply();
        public List<CommonCode> CodeLocation; 
        public List<CommonCode> CodePlace; 
        public List<CommonCode> CodeVisitPurpose; 
        public List<CommonCode> CodeVisitManualPurpose; 
        
        public List<CommonCode> CodeGenderType; 
        public List<CommonCode> CodeVipType; 
        public List<CommonCode> CodeImportPurpose;
        public List<CommonCode> CodeCarryItem; 
         
        // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
        public List<CommonCode> CodeVisitApplyStatus;
        
        // 방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4) 
        public List<CommonCode> CodeVisitStatus;

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}