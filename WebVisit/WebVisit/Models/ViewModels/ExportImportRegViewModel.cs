using WebVisit.Models.DomainModels.Passport;
namespace WebVisit.Models
{
    public class ExportImportRegViewModel
    {
        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new(); 
        
        //반출입구분 ExportImportType 0 자산, 1 수리, 2 노트북 
        public List<CommonCode> CodeExportImportType { get; set; } = new();
        
        //반출구분 ExportType : 0 외부업체, 1 공장간이동, 2 외부업체경유공장간이동
        public List<CommonCode> CodeExportType { get; set; } = new(); 

        // 반출입목적 ExportImportPurposeType: 0 수리, 1 리클레임 2 판매 3 세정(재생) 4 교환  5 이체(이동) 6 기타-(X)  7 분석  8 Demo 9 우수협력업체-(X) 10 Warranty 11 반환  12 태블릿 
        public List<CommonCode> CodeExportImportPurposeType { get; set; } = new(); 
        
        // 반입구분 ImportType :0 반입요  1 반입불요 2 매각 3 반입불요(무상) 4 대체품반입요 5 대체품선입고     
        public List<CommonCode> CodeImportType { get; set; } = new(); 
        
        public List<CommonCode> CodeUnit { get; set; } = new(); 
        
        // 반출물전달방법 DeliveryMethodType : 0	방문수령, 1	우편/택배, 2	대리인수령, 3	핸드캐리, 4	물류차량
        public List<CommonCode> CodeDeliveryMethodType { get; set; } = new();
        public ExportImport ExportImport { get; set; } = new ExportImport();
        public List<ApprovalPerson> ApprovalPeople { get; set; } = new();
    }
}