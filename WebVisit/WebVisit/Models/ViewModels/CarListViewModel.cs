using WebVisit.Models.DomainModels.LPR;
namespace WebVisit.Models
{
    public class CarListViewModel
    {
        // public IEnumerable<Approval> Approval { get; set; } = new List<Approval>();
        public List<PassedVehicleLog> PassedVehicleLogs {get;set;} = new();
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        // public List<CommonCode> CodeLocation { get; set; } = new(); 
        public CarListGridData CurrentRoute { get; set; } = new CarListGridData();
        public CarListGridData SearchRoute { get; set; } = new CarListGridData();
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
    }
}