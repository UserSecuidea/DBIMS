namespace WebVisit.Models
{
    public class WorkApplyDetailViewModel
    {
        public IEnumerable<WorkApplyAttachFile> WorkApplyAttachFiles { get; set; } = new List<WorkApplyAttachFile>();
        
        public WorkApply WorkApply { get; set; } = new WorkApply();

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation; 
        public List<CommonCode> CodeWorkApplyStatus; 
                

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}