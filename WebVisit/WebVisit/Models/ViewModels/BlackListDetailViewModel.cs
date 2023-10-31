namespace WebVisit.Models
{
    public class BlackListDetailViewModel
    {
        public IEnumerable<BlackListHistory> BlackListHistory { get; set; } = new List<BlackListHistory>();
        
        public BlackList BlackList { get; set; } = new BlackList();
        
        // 블랙리스트 등록사유구분 BlackListType : 0 퇴직임직원 , 1 안전수칙위반, 2 보안수칙위반, 3 기타
        public List<CommonCode> CodeBlackListType;

        // 블랙리스트 상태 BlackListStatus : 0 등록요청, 1 등록, 2 해제 
        public List<CommonCode> CodeBlackListStatus; 
        public List<CommonCode> CodeGenderType; 

        // public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        // public IEnumerable<Company> Companies { get; set; } = new List<Company>();
       
    }
}