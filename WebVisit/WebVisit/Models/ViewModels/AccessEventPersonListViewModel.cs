using WebVisit.Models.DomainModels.Passport;

namespace WebVisit.Models
{
    public class AccessEventPersonListViewModel
    {
        public IEnumerable<ViewAccesseventList> ViewAccesseventLists { get; set; } = new List<ViewAccesseventList>();

        public AccessEventPersonGridData CurrentRoute { get; set; } = new AccessEventPersonGridData();
        public AccessEventPersonGridData SearchRoute { get; set; } = new AccessEventPersonGridData();

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new(); 
        
        // // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
        // public List<CommonCode> CodeCardIssueType { get; set; } = new(); 

        // // CodeCardApplyStatus 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
        // public List<CommonCode> CodeCardApplyStatus { get; set; } = new(); 

        // // CardType 출입증구분 0: 일반, 1: 방문증
        // public List<CommonCode> CodeCardType { get; set; } = new(); 

        // // CardIssueStatus 출입증발급상태: 미발급(0)/발급(1)
        // public List<CommonCode> CodeCardIssueStatus { get; set; } = new(); 
       
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}