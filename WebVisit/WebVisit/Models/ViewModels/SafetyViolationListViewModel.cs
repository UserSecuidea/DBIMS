namespace WebVisit.Models
{
    public class SafetyViolationListViewModel
    {
        // public IEnumerable<SafetyViolation> SafetyViolations { get; set; } = new List<SafetyViolation>();        
        public IEnumerable<dynamic> SafetyViolations { get; set; } = new List<dynamic>();        
        public SafetyViolationGridData CurrentRoute { get; set; } = new SafetyViolationGridData();
        public SafetyViolationGridData SearchRoute { get; set; } = new SafetyViolationGridData();

        // 위반사유 리스트 
        public IEnumerable<SafetyViolationReason> SafetyViolationReasons { get; set; } = new List<SafetyViolationReason>();
        /// SafetyViolationStatus 안전수칙위반상태: 0 신규발행, 1 방지대책등록, 2 방지대책승인, 3 방지대책재등록요청    
        public List<CommonCode> CodeSafetyViolationStatus { get; set; } = new(); 

        
        
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}