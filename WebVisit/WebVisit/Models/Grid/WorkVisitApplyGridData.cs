using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class WorkVisitApplyGridData:GridData
    {
        public int SearchWorkVisitApplyStatus { get; set; } = -1;
        public int SearchVisitStatus { get; set; } = -1;
        public string SearchWorkName { get; set; } = "+";
        public string SearchWorkStartDate { get; set; } = "+";
        public string  SearchWorkEndDate { get; set; } = "+";
        public string SearchContactName { get; set; } = "+";
        public string SearchCardNo { get; set; } = "+";

        public WorkVisitApplyGridData()
        {
            SortField = nameof(WorkVisitApply.WorkDate);
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
                { nameof(SearchWorkVisitApplyStatus), SearchWorkVisitApplyStatus.ToString() },
                { nameof(SearchVisitStatus), SearchVisitStatus.ToString() },
                { nameof(SearchWorkName), SearchWorkName },
                { nameof(SearchWorkStartDate), SearchWorkStartDate },
                { nameof(SearchWorkEndDate), SearchWorkEndDate },
                { nameof(SearchContactName), SearchContactName },
                { nameof(SearchCardNo), SearchCardNo },
            };
    }
}