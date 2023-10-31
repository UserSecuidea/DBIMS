namespace WebVisit.Models
{
    public class IndexViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserPwd { get; set; } = string.Empty;

        public VisitPerson VisitPerson { get; set; } = new VisitPerson();
        public List<CommonCode> CodeLocation { get; set; } = new();
        public List<CommonCode> CodeVisitPurpose { get; set; } = new();
        public List<CommonCode> CodePlace { get; set; } = new();
         public List<CommonCode> CodeVisitManualPurpose { get; set; } = new(); 
        public List<CommonCode> CodeGenderType { get; set; } = new();
         public List<CommonCode> CodeVipType { get; set; } = new(); 
        public List<CommonCode> CodeImportPurpose { get; set; } = new();
        public List<CommonCode> CodeCarryItem { get; set; } = new();
        public List<CommonCode> CodeUnit = new();
    }
}
