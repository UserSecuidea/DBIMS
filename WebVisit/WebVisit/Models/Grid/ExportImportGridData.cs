using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class ExportImportGridData:GridData
    {
        public int SearchExportLocation { get; set; } = -1;
        public int SearchImportLocation { get; set; } = -1;
        public int SearchExportImportStatus { get; set; } = -1;
        public int? SearchExportImportApplyStatus{ get; set; } = null;  //2023.10.06 ���ξ� ������ ����Ʈ�� ����Ϸ� ����
        public int SearchExportType { get; set; } = -1;
        public string SearchStartInsertDate { get; set; } = "+";
        public string SearchEndInsertDate { get; set; } = "+";
        public string SearchInsertName { get; set; } = "+";
        public string SearchName { get; set; } = "+";
        public string SearchExportImportID { get; set; } = "+";
        public string SearchInsertOrgNameKor { get; set; } = "+";

        public ExportImportGridData()
        {
            SortField = nameof(ExportImport.ExportImportNo);
            SortDirection = "desc";
            PageSize = 10;
        }

        // [JsonIgnore]
        // public bool IsSortByCompanyID => SortField.EqualsNoCase(nameof(Person.CompanyID));
        public override Dictionary<string, string> ToDictionary()  =>
            new()
            {
                { nameof(PageNumber), PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() },
                { nameof(SortField), SortField },
                { nameof(SortDirection), SortDirection },
                { nameof(SearchExportLocation), SearchExportLocation.ToString() },
                { nameof(SearchImportLocation), SearchImportLocation.ToString() },
                { nameof(SearchExportImportStatus), SearchExportImportStatus.ToString() },
                { nameof(SearchExportImportApplyStatus), SearchExportImportApplyStatus.ToString() },
                { nameof(SearchExportType), SearchExportType.ToString() },
                { nameof(SearchStartInsertDate), SearchStartInsertDate.ToString() },
                { nameof(SearchEndInsertDate), SearchEndInsertDate.ToString() },
                { nameof(SearchInsertName), SearchInsertName.ToString() },
                { nameof(SearchName), SearchName.ToString() },
                { nameof(SearchExportImportID), SearchExportImportID.ToString() },
                { nameof(SearchInsertOrgNameKor), SearchInsertOrgNameKor.ToString() },
            };
    }
}