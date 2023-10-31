namespace WebVisit.Models
{
    public class SafetyViolationDetailViewModel
    {
        public IEnumerable<SafetyViolationPerson> SafetyViolationPersons { get; set; } = new List<SafetyViolationPerson>();
        public IEnumerable<SafetyViolationReason> SafetyViolationReasons { get; set; } = new List<SafetyViolationReason>();
        
        public SafetyViolation SafetyViolation { get; set; } = new SafetyViolation();

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new();

        //안전수칙위반상태	SafetyViolationStatus : 0 신규발행, 1 방지대책등록, 2 방지대책승인, 3 방지대책재등록요청        
        public List<CommonCode> CodeSafetyViolationStatus { get; set; } = new();
    }
}