using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class CarCardGridData:GridData
    {
        public int SearchLocation { get; set; } = -1;
        public int SearchCarTypeID { get; set; } = -1;
        public int SearchCardIssueStatus { get; set; } = -1;
        public string SearchCarNo { get; set; } = "+";
        public string SearchName { get; set; } = "+";
        public string SearchSabun { get; set; } = "+";

        public CarCardGridData()
        {
            SortField = nameof(CarCardIssueApply.CarCardIssueApplyID);
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
                { nameof(SearchLocation), SearchLocation.ToString() },
                { nameof(SearchCarTypeID), SearchCarTypeID.ToString() },
                { nameof(SearchCardIssueStatus), SearchCardIssueStatus.ToString() },
                { nameof(SearchCarNo), SearchCarNo.ToString() },
                { nameof(SearchName), SearchName.ToString() },
                { nameof(SearchSabun), SearchSabun.ToString() },
            };

    }
}