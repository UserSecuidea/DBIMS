namespace WebVisit.Models
{
    public class SafetyViolationReasonListViewModel
    {
        public IEnumerable<SafetyViolationReason> SafetyViolationReasons { get; set; } = new List<SafetyViolationReason>();        
        public SafetyViolationReasonGridData CurrentRoute { get; set; } = new SafetyViolationReasonGridData();

        /// SafetyViolationStatus 안전수칙위반상태: 0 신규발행, 1 방지대책등록, 2 방지대책승인, 3 방지대책재등록요청    
        // public List<CommonCode> CodeSafetyViolationStatus;

        
        
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}