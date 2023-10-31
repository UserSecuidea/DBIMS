namespace WebVisit.Models
{
    public class SafetyEduDetailViewModel
    {
        // public IEnumerable<CarryItemInfo> CarryItemInfos { get; set; } = new List<CarryItemInfo>();
        
        public SafetyEduApply SafetyEduApply { get; set; } = new SafetyEduApply();
        public SafetyEdu SafetyEdu { get; set; } = new SafetyEdu();
        public WorkApply WorkApply { get; set; } = new WorkApply();

        // EducationCompleteStatus 교육이수상태: 미이수(0)/이수(1)     
        public List<CommonCode> CodeEducationCompleteStatus { get; set; } = new();
        public List<CommonCode> CodeGenderType { get; set; } = new();
    }
}