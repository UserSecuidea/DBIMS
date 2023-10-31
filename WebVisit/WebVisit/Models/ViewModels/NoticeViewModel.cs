namespace WebVisit.Models
{
    public class NoticeViewModel
    {
        public Notice Notice { get; set; } = new Notice();
        public NoticeGridData CurrentRoute { get; set; } = new NoticeGridData();
        public bool IsMaster { get; set; } = false;
        
    //     public int TotalPages { get; set; }
    //     public int TotalCount { get; set; }
    }
}