using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class CardIssueApplyGridData:GridData
    {    
        public int SearchLocation { get; set; } = -1;
        public int SearchCompanyID { get; set; } = -1;
        public int SearchCardIssueType { get; set; } = -1;
        public int SearchCardApplyStatus { get; set; } = -1;
        public int SearchPersonTypeID { get; set; } = -1;
        public string SearchStartInsertDate { get; set; } = "+";
        public string SearchEndInsertDate { get; set; } = "+";
        public string SearchName { get; set; } = "+";

        public CardIssueApplyGridData()
        {
            SortField = nameof(CardIssueApply.CardIssueApplyID);
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
                { nameof(SearchCompanyID), SearchCompanyID.ToString() },
                { nameof(SearchCardIssueType), SearchCardIssueType.ToString() },
                { nameof(SearchCardApplyStatus), SearchCardApplyStatus.ToString() },
                { nameof(SearchPersonTypeID), SearchPersonTypeID.ToString() },
                { nameof(SearchStartInsertDate), SearchStartInsertDate.ToString() },
                { nameof(SearchEndInsertDate), SearchEndInsertDate.ToString() },
                { nameof(SearchName), SearchName.ToString() },
            };

    }
}