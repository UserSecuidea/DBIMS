namespace WebVisit.Models
{
    public class MealManualRegViewModel
    {
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation; 
        
        //방문자구분 VisitorType 0 임직원, 1 방문자 
        public List<CommonCode> CodeVisitorType; 
        public List<CommonCode> CodeMealType; 

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}