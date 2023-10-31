namespace WebVisit.Models
{
    public class MealInfoListViewModel
    {
        public IEnumerable<MealTerm> MealTerm { get; set; } = new List<MealTerm>();
        public IEnumerable<MealSchedule> MealSchedule { get; set; } = new List<MealSchedule>();
        public IEnumerable<MealPrice> MealPrice { get; set; } = new List<MealPrice>();

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation{ get; set; } = new();  
        // 계절구분  TermType : 0 하계, 1 동계
        public List<CommonCode> CodeTermType{ get; set; } = new();  
        // 식수구분  MealType : 0 이상식수, 1 조식, 2 중식, 3 석식, 4 야식 
        public List<CommonCode> CodeMealType{ get; set; } = new();   
        
        // public CardIssueApplyGridData CurrentRoute { get; set; } = new CardIssueApplyGridData();

        
        
        // // PersonType 회원구분: 임직원(0)/상주직원(1)/비상주관리자(3)/비상주직원(4)/방문객(5)
        // public List<CommonCode> CodePersonType;

        // // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
        // public List<CommonCode> CodeCardIssueType;

        // // CodeCardApplyStatus 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
        // public List<CommonCode> CodeCardApplyStatus;

        // // CardType 출입증구분 0: 일반, 1: 방문증
        // public List<CommonCode> CodeCardType;

        // // CardIssueStatus 출입증발급상태: 미발급(0)/발급(1)
        // public List<CommonCode> CodeCardIssueStatus;
        

        
        
        // public int TotalPages { get; set; }
        // public int TotalCnt { get; set; }
        
    }
}