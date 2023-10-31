namespace WebVisit.Models
{
    public class MealLogInvalidListViewModel 
    {
        public IEnumerable<MealLog> MealLogs { get; set; } = new List<MealLog>();
        public IEnumerable<MealLogDTO> MealLogDTOs { get; set; } = new List<MealLogDTO>();
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new (); 

        public MealInvalidGridData CurrentRoute { get; set; } = new MealInvalidGridData();
        public MealInvalidGridData SearchRoute { get; set; } = new MealInvalidGridData();
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
    }
}