namespace WebVisit.Models
{
    public class ApprovalLineListViewModel
    {
        public IEnumerable<Approval> Approval { get; set; } = new List<Approval>();
        public IEnumerable<ApprovalLine> ApprovalLine { get; set; } = new List<ApprovalLine>();


        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new(); 
        // 결재유형 ApprovalType-0: 사원추가, 1: 카드추가, 2: 반출입신청, 3: 공사신청, 4: 안전교육신청
        public List<CommonCode> CodeApprovalType { get; set; } = new();  
        
        // 결재자유형 ApprovalSysType-0: 통문관리시스템, 1: ERP, 2: 사용자설정
        public List<CommonCode> CodeApprovalSysType { get; set; } = new();  
        
        public ApprovalLineGridData CurrentRoute { get; set; } = new ApprovalLineGridData();
        public int TotalPages { get; set; }

        public int MaxSubCodeID { get; set; }
    }
}