namespace WebVisit.Models
{
    public class TempCardIssueApplyListViewModel
    {
        public IEnumerable<TempCardIssueApply> TempCardIssueApply { get; set; } = new List<TempCardIssueApply>();
        public TempCardIssueApplyGridData CurrentRoute { get; set; } = new TempCardIssueApplyGridData();
        public TempCardIssueApplyGridData SearchRoute { get; set; } = new TempCardIssueApplyGridData();

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new(); 

        
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}