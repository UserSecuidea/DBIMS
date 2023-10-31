using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class CarListGridData:GridData
    {
        public string SearchDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        public CarListGridData()
        {
            SortField = nameof(CardIssueApply.CardIssueApplyID);
            SortDirection = "desc";
            PageSize = 10;
        }

        // [JsonIgnore]
        // public bool IsSortByCompanyID => SortField.EqualsNoCase(nameof(Person.CompanyID));
        public virtual CarListGridData Clone() => (CarListGridData)MemberwiseClone();
        public override Dictionary<string, string> ToDictionary()  =>
            new()
            {
                { nameof(PageNumber), PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() },
                { nameof(SortField), SortField },
                { nameof(SortDirection), SortDirection },
                { nameof(SearchDate), SearchDate },
            };

    }
}