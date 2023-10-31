namespace WebVisit.Models
{
    public class WorkVisitApplyDetailViewModel
    {
        public WorkVisitApply WorkVisitApply { get; set; } = new WorkVisitApply();
        public WorkApply WorkApply { get; set; } = new WorkApply();
        
        // public IEnumerable<WorkVisitPersonApply> WorkVisitPersonApplys { get; set; } = new List<WorkVisitPersonApply>();
        

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation; 

        // WorkVisitApplyStatus 공사출입신청상태: 승인대기(0)/승인완료(1)/반려(2)
        public List<CommonCode> CodeWorkVisitApplyStatus; 
        public List<CommonCode> CodeGenderType; 

        
        

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}