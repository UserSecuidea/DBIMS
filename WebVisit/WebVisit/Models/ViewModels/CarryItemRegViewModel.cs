using WebVisit.Models.DomainModels.Passport;
namespace WebVisit.Models
{
    public class BaseViewModel{

    }

    public class CarryItemRegViewModel:BaseViewModel
    {
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation = new();
        
        //반입목적 ImportPurpose: 0 Set Up, 1 장비교체, 2 데모, 3 납품, 4 기타
        public List<CommonCode> CodeImportPurpose = new();

        // 반입장소 Place: 0 공통-통제구역-적색-클린룸, 1 공통-제한구역-황색-건물내부, 2 공통-일반구역-청색-건물외부/접견실/납품 ....... 
        public List<CommonCode> CodePlace = new();

        // 휴대물품 구분 CarryItem : 0 노트북, 1 PC, 2 카메라, 3 기타 
        public List<CommonCode> CodeCarryItem = new();
        
        // 단위 Unit : 0: 개, 1: 대, 2: EA, 3: SIK
        public List<CommonCode> CodeUnit = new();

        public List<ApprovalPerson> ApprovalPeople { get; set; } = new();

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}