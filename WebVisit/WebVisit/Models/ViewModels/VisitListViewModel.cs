namespace WebVisit.Models
{
    public class VisitListViewModel
    {
        // VisitApplyPersons
        public IEnumerable<VisitApplyPerson> VisitApplyPersons { get; set; } = new List<VisitApplyPerson>();        
        public VisitGridData CurrentRoute { get; set; } = new VisitGridData();
        public VisitGridData SearchRoute { get; set; } = new VisitGridData();
        public VisitManualGridData CurrentManualRoute { get; set; } = new VisitManualGridData();
        public VisitManualGridData SearchManualRoute { get; set; } = new VisitManualGridData();
        
        // 방문신청구분: 방문신청(0)/공사출입인원신청(1)/안전교육(2)/방문객수기등록(3)/택배(4)
        public List<CommonCode> CodeVisitApplyType { get; set; } = new(); 

        // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
        public List<CommonCode> CodeVisitApplyStatus { get; set; } = new(); 
        
        // 방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4) 
        public List<CommonCode> CodeVisitStatus { get; set; } = new(); 

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new(); 
        public List<CommonCode> CodeVisitPurpose { get; set; } = new(); 
        public List<CommonCode> CodeVisitManualPurpose { get; set; } = new(); 
        
        
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}