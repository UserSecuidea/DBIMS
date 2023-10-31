namespace WebVisit.Models
{
    public class CarCardIssueApplyDetailViewModel
    {
        public IEnumerable<CarCardIssueApplyHistory> CarCardIssueApplyHistory { get; set; } = new List<CarCardIssueApplyHistory>();
        
        public CarCardIssueApply CarCardIssueApply { get; set; } = new();
        public Person Person { get; set; } = new Person();

        public List<CommonCode> CodeLocation { get; set; } = new(); 
      
        
        // PersonType 회원구분: 임직원(0)/상주직원(1)/비상주관리자(3)/비상주직원(4)/방문객(5)
        public List<CommonCode> CodePersonType { get; set; } = new();

        // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
        public List<CommonCode> CodeCardIssueType { get; set; } = new();

        // CodeCardApplyStatus 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
        public List<CommonCode> CodeCardApplyStatus { get; set; } = new();

        // CodeCardType 출입증구분 0: 일반, 1: 방문증 
        public List<CommonCode> CodeCardType { get; set; } = new();
        public List<CommonCode> CodeCarType { get; set; } = new();

        // CodeCardIssueStatus 출입증발급상태: 미발급(0)/발급(1)
        public List<CommonCode> CodeCardIssueStatus { get; set; } = new();

        public List<CommonCode> CodeGenderType { get; set; } = new();
        public List<CommonCode> CodePersonStatus { get; set; } = new();
        
        

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}