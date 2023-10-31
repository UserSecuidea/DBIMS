using WebVisit.Models.DomainModels.Passport;

namespace WebVisit.Models
{
    public class ApprovalExportImportViewModel
    {
        public IEnumerable<ExportImportItem> ExportImportItems { get; set; } = new List<ExportImportItem>();
        
        public IEnumerable<ExportImportHistory> ExportImportHistory { get; set; } = new List<ExportImportHistory>();
        
        public ExportImport ExportImport { get; set; } = new();

        // public AccessEventPersonGridData CurrentRoute { get; set; } = new AccessEventPersonGridData();
        // public AccessEventPersonGridData SearchRoute { get; set; } = new AccessEventPersonGridData();

        // public List<CommonCode> CodeLocation { get; set; } = new(); 
        
        // public int TotalPages { get; set; }
        // public int TotalCnt { get; set; }
        
    }
}