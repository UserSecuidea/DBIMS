using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class NotebookSelfApprovalGridData:GridData
    {
        public int SearchLocation { get; set; } = -1;
        public string SearchName { get; set; } = "+";
        public string SearchCompanyName { get; set; } = "+";
        public string SearchOrgNameKor { get; set; } = "+";

            
        public NotebookSelfApprovalGridData()
        {
            SortField = nameof(NotebookSelfApproval.NotebookSelfApprovalID);
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
                { nameof(SearchLocation), SearchLocation.ToString() },
                { nameof(SearchName), SearchName },
                { nameof(SearchCompanyName), SearchCompanyName },
                { nameof(SearchOrgNameKor), SearchOrgNameKor },
            };
    }
}