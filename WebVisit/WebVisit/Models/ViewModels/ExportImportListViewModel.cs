
namespace WebVisit.Models
{
    public class ExportImportListViewModel
    {
        public IEnumerable<ExportImport> ExportImports { get; set; } = new List<ExportImport>();        
        public ExportImportGridData CurrentRoute { get; set; } = new ExportImportGridData();        
        public ExportImportGridData SearchRoute { get; set; } = new ExportImportGridData();

        // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
        public List<CommonCode> CodeLocation { get; set; } = new();  

        // ExportType 반출구분: 0 외부업체, 1 공장간이동, 2 외부업체경유공장간이동
        public List<CommonCode> CodeExportType { get; set; } = new();  

        // 반출입목적 ExportImportPurposeType: 0 수리, 1 리클레임 2 판매 3 세정(재생) 4 교환  5 이체(이동) 6 기타-(X)  7 분석  8 Demo 9 우수협력업체-(X) 10 Warranty 11 반환  12 태블릿 
        public List<CommonCode> CodeExportImportPurposeType { get; set; } = new();  
        
        //2023.09.14 dsyoo  ExportImportApplyStatus 반출입신청상태: 승인대기(0)/결재완료(1)/반려(2)/결재중(3)/승인(4)/미승인(5),상신(64),임시저장(256),승인보류(512),삭제(1024
        public List<CommonCode> CodeExportImportApplyStatus = new();        
        // ExportImportStatus 반출입상태: 승인대기(0)/승인완료(1)/반려(2)/결재중(3)/결재완료(4)
        public List<CommonCode> CodeExportImportStatus { get; set; } = new();  

        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}