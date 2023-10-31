namespace WebVisit.Models
{
    public class CommonCodeListViewModel
    {
        public IEnumerable<CommonCode> CommonCodes { get; set; } = new List<CommonCode>();
        public CommonCodeGridData CurrentRoute { get; set; } = new CommonCodeGridData();
        public int TotalPages { get; set; }

        public int MaxSubCodeID { get; set; }
    }
}