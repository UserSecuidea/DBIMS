namespace WebVisit.Models
{
    public class SafetyEduListViewModel
    {
        public IEnumerable<SafetyEdu> SafetyEdus { get; set; } = new List<SafetyEdu>();        
        public SafetyEduGridData CurrentRoute { get; set; } = new SafetyEduGridData();
        public SafetyEduGridData SearchRoute { get; set; } = new SafetyEduGridData();

        // EducationCompleteStatus 교육이수상태: 미이수(0)/이수(1)     
        public List<CommonCode> CodeEducationCompleteStatus { get; set; } = new();

        
        
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}