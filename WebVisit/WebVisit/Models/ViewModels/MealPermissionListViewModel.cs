namespace WebVisit.Models
{
    public class MealPermissionListViewModel
    {
        public IEnumerable<Person> Person { get; set; } = new List<Person>();
        public IEnumerable<MealAccessDTO> MealAccessDTOs { get; set; } = new List<MealAccessDTO>();

        public MealPermissionGridData CurrentRoute { get; set; } = new MealPermissionGridData();
        public MealPermissionGridData SearchRoute { get; set; } = new MealPermissionGridData();

        
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation{ get; set; } = new();  
        

        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
       
    }
}