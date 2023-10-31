using WebVisit.Models.DomainModels.Passport;

namespace WebVisit.Models
{
    public class CardIssueApplyRegViewModel
    {
        public CardIssueApply CardIssueApply { get; set; } = new();
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new();

        // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
        public List<CommonCode> CodeCardIssueType { get; set; } = new();
        public List<ViewDuty> ContactPersonList { get; set; } = new();

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}