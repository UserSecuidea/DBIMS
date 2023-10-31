namespace WebVisit.Models
{
    public class HolidayListViewModel
    {
        public IEnumerable<Holiday> Holiday { get; set; } = new List<Holiday>();        
        public HolidayGridData CurrentRoute { get; set; } = new HolidayGridData();        
        public HolidayGridData SearchRoute { get; set; } = new HolidayGridData();
        
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}