namespace WebVisit.Models
{
    public class CarryItemDetailViewModel
    {
        public IEnumerable<CarryItemInfo> CarryItemInfos { get; set; } = new List<CarryItemInfo>();
        
        public CarryItemApply CarryItemApply { get; set; } = new CarryItemApply();
        public IEnumerable<CarryItemApplyHistory> CarryItemApplyHistoryList { get; set; }= new List<CarryItemApplyHistory>();

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new();
        
        //반입목적 ImportPurpose: 0 Set Up, 1 장비교체, 2 데모, 3 납품, 4 기타
        public List<CommonCode> CodeImportPurpose { get; set; } = new();

        // 반입장소 Place: 0 공통-통제구역-적색-클린룸, 1 공통-제한구역-황색-건물내부, 2 공통-일반구역-청색-건물외부/접견실/납품 ....... 
        public List<CommonCode> CodePlace { get; set; } = new();

        // 휴대물품 구분 CarryItem : 0 노트북, 1 PC, 2 카메라, 3 기타 
        public List<CommonCode> CodeCarryItem { get; set; } = new();

        // 단위 Unit : 0: 개, 1: 대, 2: EA, 3: SIK
        public List<CommonCode> CodeUnit = new();

        // 휴대물품신청상태  CarryItemApplyStatus: 0 승인대기, 1 승인완료, 2 반려, 3 결재중, 4 결재완료
        public List<CommonCode> CodeCarryItemApplyStatus { get; set; } = new();
        public List<CommonCode> CodeCarryItemStatus { get; set; } = new();

        // 휴대물품반입구분 CarryItemImportType 0 반입대기, 1 반입처리, 2 반출요, 3 반출불가, 4 반출불필요(유상), 5 반출불필요(무상)
        public List<CommonCode> CodeCarryItemImportType { get; set; } = new();

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();

        public CarryItemDetailViewModel(){

        }       
    }
}