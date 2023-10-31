namespace WebVisit.Models
{
    public class WorkApplyListViewModel
    {
        public IEnumerable<WorkApply> WorkApplys { get; set; } = new List<WorkApply>();        
        public WorkApplyGridData CurrentRoute { get; set; } = new WorkApplyGridData();
        public WorkApplyGridData SearchRoute { get; set; } = new WorkApplyGridData();

        // WorkApplyStatus 공사신청상태: 승인대기(0)/승인완료(1)/반려(2)        
        public List<CommonCode> CodeWorkApplyStatus { get; set; } = new();
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new();
        
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}