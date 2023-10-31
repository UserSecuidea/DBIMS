using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class TempCardIssueApplyGridData:GridData
    {
        public string SearchStartInsertDate { get; set; } = "+";
        public string SearchEndInsertDate { get; set; } = "+";
        public string SearchCardNo { get; set; } = "+";
        public string SearchName { get; set; } = "+";
        public string SearchSabun { get; set; } = "+";

        public TempCardIssueApplyGridData()
        {
            SortField = nameof(TempCardIssueApply.TempCardIssueApplyID);
            SortDirection = "desc";
            PageSize = 10;
        }

        // [JsonIgnore]
        // public bool IsSortByCompanyID => SortField.EqualsNoCase(nameof(Person.CompanyID));
        public override Dictionary<string, string> ToDictionary()  =>
            new Dictionary<string, string> {
                { nameof(PageNumber), PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() },
                { nameof(SortField), SortField },
                { nameof(SortDirection), SortDirection },
                { nameof(SearchStartInsertDate), SearchStartInsertDate.ToString() },
                { nameof(SearchEndInsertDate), SearchEndInsertDate.ToString() },
                { nameof(SearchCardNo), SearchCardNo.ToString() },
                { nameof(SearchName), SearchName.ToString() },
                { nameof(SearchSabun), SearchSabun.ToString() },
            };
    }
}