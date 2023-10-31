namespace WebVisit.Models
{
    public class CardIssueApplyListViewModel
    {
        public IEnumerable<CardIssueApply> CardIssueApplys { get; set; } = new List<CardIssueApply>();
        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
        public CardIssueApplyGridData CurrentRoute { get; set; } = new CardIssueApplyGridData();
        public CardIssueApplyGridData SearchRoute { get; set; } = new CardIssueApplyGridData();

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new(); 
        
        // PersonType 회원구분: 임직원(0)/상주직원(1)/비상주관리자(3)/비상주직원(4)/방문객(5)
        public List<CommonCode> CodePersonType { get; set; } = new(); 

        // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
        public List<CommonCode> CodeCardIssueType { get; set; } = new(); 

        // CodeCardApplyStatus 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
        public List<CommonCode> CodeCardApplyStatus { get; set; } = new(); 

        // CardType 출입증구분 0: 일반, 1: 방문증
        public List<CommonCode> CodeCardType { get; set; } = new(); 

        // CardIssueStatus 출입증발급상태: 미발급(0)/발급(1)
        public List<CommonCode> CodeCardIssueStatus { get; set; } = new(); 
        

        
        
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}