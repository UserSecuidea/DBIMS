namespace WebVisit.Models
{
    public class SafetyViolationRegViewModel
    {
        // 위반사유 리스트 
        public IEnumerable<SafetyViolationReason> SafetyViolationReasons { get; set; } = new List<SafetyViolationReason>();
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation;         
        public PersonDTO Person { get; set; } = new PersonDTO();

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}