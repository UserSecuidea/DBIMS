using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class SafetyViolationGridData:GridData
    {
        public int SearchSafetyViolationStatus { get; set; } = -1;
        public int SearchSafetyViolationReasonID { get; set; } = -1;
        public string SearchName { get; set; } = "+";
        // public string SearchBirthDate { get; set; } = "+";
        public string SearchCompanyName { get; set; } = "+";
        public string SearchStartViolationDate { get; set; } = "+";
        public string SearchEndViolationDate { get; set; } = "+";

        public SafetyViolationGridData()
        {
            SortField = nameof(SafetyViolation.SafetyViolationID);
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

                { nameof(SearchSafetyViolationStatus), SearchSafetyViolationStatus.ToString() },
                { nameof(SearchSafetyViolationReasonID), SearchSafetyViolationReasonID.ToString() },
                { nameof(SearchName), SearchName.ToString() },
                // { nameof(SearchBirthDate), SearchBirthDate.ToString() },
                { nameof(SearchCompanyName), SearchCompanyName.ToString() },
                { nameof(SearchStartViolationDate), SearchStartViolationDate.ToString() },
                { nameof(SearchEndViolationDate), SearchEndViolationDate.ToString() },
            };
    }
}