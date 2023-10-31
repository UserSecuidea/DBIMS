namespace WebVisit.Models
{
    public class NotebookSelfApprovalListViewModel
    {
        public IEnumerable<NotebookSelfApproval> NotebookSelfApprovals { get; set; } = new List<NotebookSelfApproval>();        
        public NotebookSelfApprovalGridData CurrentRoute { get; set; } = new NotebookSelfApprovalGridData();
        public NotebookSelfApprovalGridData SearchRoute { get; set; } = new NotebookSelfApprovalGridData();

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation{ get; set; } = new(); 

        
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}