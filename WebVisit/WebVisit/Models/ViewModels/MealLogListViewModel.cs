namespace WebVisit.Models
{
    public class MealLogListViewModel
    {
        public IEnumerable<MealLog> MealLogs { get; set; } = new List<MealLog>();
        public IEnumerable<MealLogDTO> MealLogDTOs { get; set; } = new List<MealLogDTO>();
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new (); 

        public MealLogGridData CurrentRoute { get; set; } = new MealLogGridData();
        public MealLogGridData SearchRoute { get; set; } = new MealLogGridData();
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
    }
}