using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class SafetyViolationReasonGridData:GridData
    {
        public int OrderNo = 0;

        public SafetyViolationReasonGridData()
        {
            SortField = nameof(SafetyViolationReason.SafetyViolationReasonID);
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
                { nameof(OrderNo), OrderNo.ToString() },
            };
    }
}