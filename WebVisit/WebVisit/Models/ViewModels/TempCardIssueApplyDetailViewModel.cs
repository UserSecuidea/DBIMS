namespace WebVisit.Models
{
    public class TempCardIssueApplyDetailViewModel
    {

        // TempCardIssueStatus 임시증발급상태: 승인대기(0)/승인완료(1)/반려(2)/회수(3)
        public List<CommonCode> CodeTempCardIssueStatus; 
        public List<CommonCode> CodeLocation; 
        
        public TempCardIssueApply TempCardIssueApply { get; set; } = new TempCardIssueApply();

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}