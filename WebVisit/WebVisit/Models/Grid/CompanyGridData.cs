using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class CompanyGridData: GridData
    {
        // public int OrderNo = 0;
        public string SearchCompanyStatus { get; set; } = "-1";
        public string SearchCompanyName { get; set; } = string.Empty;

        public CompanyGridData()
        {
            SortField = nameof(Company.CompanyName);
            SortDirection = "desc";
            PageSize = 10;
        }

        /// <summary>
        /// determines where the company are currently sorted by compnay name
        /// </summary>
        [JsonIgnore]
        public bool IsSortByCompanyName => SortField.EqualsNoCase(nameof(Company.CompanyName));
        public override CompanyGridData Clone() => (CompanyGridData)MemberwiseClone();

        public override Dictionary<string, string> ToDictionary()  =>
            new()
            {
                { nameof(PageNumber), PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() },
                { nameof(SortField), SortField },
                { nameof(SortDirection), SortDirection },
                { nameof(SearchCompanyStatus), SearchCompanyStatus.ToString() },
                { nameof(SearchCompanyName), SearchCompanyName.ToString() },
            };
            // { nameof(OrderNo), OrderNo.ToString() },
    }
}