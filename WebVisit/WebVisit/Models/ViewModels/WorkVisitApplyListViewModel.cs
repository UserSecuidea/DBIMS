namespace WebVisit.Models
{
    public class WorkVisitApplyListViewModel
    {
        public IEnumerable<WorkVisitApply> WorkVisitApplys { get; set; } = new List<WorkVisitApply>();        
        public WorkVisitApplyGridData CurrentRoute { get; set; } = new WorkVisitApplyGridData();
        public WorkVisitApplyGridData SearchRoute { get; set; } = new WorkVisitApplyGridData();

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new(); 

        // WorkVisitApplyStatus 공사출입신청상태: 승인대기(0)/승인완료(1)/반려(2)       
        public List<CommonCode> CodeWorkVisitApplyStatus{ get; set; } = new(); 

        // WorkApplyStatus 공사신청상태: 승인대기(0)/승인완료(1)/반려(2)       
        public List<CommonCode> CodeWorkApplyStatus{ get; set; } = new(); 

        // VisitApplyStatus 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)       
        public List<CommonCode> CodeVisitApplyStatus{ get; set; } = new(); 
        
        // VisitStatus 방문상태: 승인대기(0)/승인완료(1)/반려(2)
        public List<CommonCode> CodeVisitStatus{ get; set; } = new(); 
        
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}