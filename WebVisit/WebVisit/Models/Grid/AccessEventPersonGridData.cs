using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class AccessEventPersonGridData:GridData
    {
        public int TabIdx { get; set; } = 1;
        public int SearchStartHour { get; set; } = -1;
        public int SearchStartMinute { get; set; } = -1;
        public int SearchEndHour { get; set; } = -1;
        public int SearchEndMinute { get; set; } = -1;
        public int SearchLocation { get; set; } = -1;
        public string SearchStartDate { get; set; } = "+";
        public string SearchEndDate { get; set; } = "+";
        public string SearchOrgNameKor { get; set; } = "+";
        public string SearchName { get; set; } = "+";

        public AccessEventPersonGridData()
        {
            SortField = nameof(CardIssueApply.CardIssueApplyID);
            SortDirection = "desc";
            PageSize = 10;
        }

        // [JsonIgnore]
        // public bool IsSortByCompanyID => SortField.EqualsNoCase(nameof(Person.CompanyID));
        public virtual AccessEventPersonGridData Clone() => (AccessEventPersonGridData)MemberwiseClone();
        public override Dictionary<string, string> ToDictionary()  =>
            new Dictionary<string, string> {
                { nameof(TabIdx), TabIdx.ToString() },
                { nameof(PageNumber), PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() },
                { nameof(SortField), SortField },
                { nameof(SortDirection), SortDirection },
                { nameof(SearchStartHour), SearchStartHour.ToString() },
                { nameof(SearchStartMinute), SearchStartMinute.ToString() },
                { nameof(SearchEndHour), SearchEndHour.ToString() },
                { nameof(SearchEndMinute), SearchEndMinute.ToString() },
                { nameof(SearchLocation), SearchLocation.ToString() },
                { nameof(SearchStartDate), SearchStartDate.ToString() },
                { nameof(SearchEndDate), SearchEndDate.ToString() },
                { nameof(SearchOrgNameKor), SearchOrgNameKor.ToString() },
                { nameof(SearchName), SearchName.ToString() },
            };

    }
}