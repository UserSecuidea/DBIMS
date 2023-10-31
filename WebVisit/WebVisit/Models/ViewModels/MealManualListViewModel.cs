namespace WebVisit.Models
{
    public class MealManualListViewModel
    {
        public IEnumerable<MealLog> MealLog { get; set; } = new List<MealLog>();        
        public MealManualGridData CurrentRoute { get; set; } = new MealManualGridData();
        public MealManualGridData SearchRoute { get; set; } = new MealManualGridData();

        
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation{ get; set; } = new(); 
        
        //방문자구분 VisitorType 0 임직원, 1 방문자 
        public List<CommonCode> CodeVisitorType{ get; set; } = new(); 
        public List<CommonCode> CodeMealType{ get; set; } = new();  

        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
       
    }
}