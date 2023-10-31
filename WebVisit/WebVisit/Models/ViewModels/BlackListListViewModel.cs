namespace WebVisit.Models
{
    public class BlackListListViewModel
    {
        // VisitApplyPersons
        public IEnumerable<BlackList> BlackListLists { get; set; } = new List<BlackList>();        
        public BlackListGridData CurrentRoute { get; set; } = new BlackListGridData();
        public BlackListGridData SearchRoute { get; set; } = new BlackListGridData();


        // 블랙리스트 등록사유구분 BlackListType : 0 퇴직임직원 , 1 안전수칙위반, 2 보안수칙위반, 3 기타
        public List<CommonCode> CodeBlackListType{ get; set; } = new(); 

        // 블랙리스트 상태 BlackListStatus : 0 등록요청, 1 등록, 2 해제 
        public List<CommonCode> CodeBlackListStatus{ get; set; } = new(); 

        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}