using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class WorkApplyGridData:GridData
    {
        public int SearchWorkApplyStatus { get; set; } = -1; // 승인구분: 전체(-1), 승인대기(0), 승인완료(1), 반려(2)
        public string SearchWorkName { get; set; } = "+"; // 공사명
        public string SearchWorkStartDate { get; set; } = "+"; // 공사기간
        public string SearchWorkEndDate { get; set; } = "+"; // 공사기간
        public string SearchWorkApplyID { get; set; } = "+"; // 공사번호
        public string SearchContactName { get; set; } = "+"; // 담당자명
        // public int OrderNo = 0;

        public WorkApplyGridData()
        {
            SortField = nameof(WorkApply.WorkName);
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
                { nameof(SearchWorkApplyStatus), SearchWorkApplyStatus.ToString() },
                { nameof(SearchWorkName), SearchWorkName },
                { nameof(SearchWorkStartDate), SearchWorkStartDate },
                { nameof(SearchWorkEndDate), SearchWorkEndDate },
                { nameof(SearchWorkApplyID), SearchWorkApplyID },
                { nameof(SearchContactName), SearchContactName },
                // { nameof(OrderNo), OrderNo.ToString() },
            };
    }
}