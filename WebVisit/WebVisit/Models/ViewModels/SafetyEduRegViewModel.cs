namespace WebVisit.Models
{
    public class SafetyEduRegViewModel
    {
        public WorkApply WorkApply { get; set; } = new WorkApply();
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new();
        public List<CommonCode> CodeWorkTypeCode { get; set; } = new();
        public List<CommonCode> CodeGenderType { get; set; } = new(); 
    }
}