namespace WebVisit.Models
{
    public class CarryItemListViewModel
    {
        // VisitApplyPersons
        public IEnumerable<CarryItemApply> CarryItemApplys { get; set; } = new List<CarryItemApply>();        
        public CarryItemGridData CurrentRoute { get; set; } = new CarryItemGridData();
        public CarryItemGridData SearchRoute { get; set; } = new CarryItemGridData();

        // CarryItemApplyStatus 휴대물품신청상태: 승인대기(0)/승인완료(1)/반려(2)/결재중(3)/결재완료(4)
        public List<CommonCode> CodeCarryItemApplyStatus { get; set; } = new(); 

        // CarryItemStatus 휴대물품반입반출상태: 반입대기(0), 반출대기(1), 확인대기(2), 반출완료(3), 미반입(4), 미반출(5), 반입완료(6), 확인완료(7)
        public List<CommonCode> CodeCarryItemStatus { get; set; } = new(); 

        // CarryItemImportType 휴대물품반입구분: 0 반입대기, 1 반입처리, 2 반출요, 3 반출불가, 4 반출불필요(유상), 5 반출불필요(무상)
        public List<CommonCode> CodeCarryItemImportType { get; set; } = new(); 

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new(); 
        
        //반입목적 ImportPurpose: 0 Set Up, 1 장비교체, 2 데모, 3 납품, 4 기타
        public List<CommonCode> CodeImportPurpose { get; set; } = new(); 
        
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}