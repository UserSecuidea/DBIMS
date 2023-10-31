using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class SafetyEduGridData:GridData
    {
        // searchEducationCompleteStatus searchCompanyName searchStartEduDate searchEndEduDate searchName
        public int SearchEducationCompleteStatus { get; set; } = -1; // 교육 이수 상태: 미이수(0), 이수(1) SafetyEduApply > EduCompleteStatus
        public string SearchCompanyName { get; set; } = "+"; // VisitApplyPerson > CompanyName
        public string SearchStartEduDate { get; set; } = "+"; // 교육이수일시: SafetyEduApply > EduDate
        public string SearchEndEduDate { get; set; } = "+"; // 교육이수일시: SafetyEduApply > EduDate
        public string SearchName { get; set; } = "+"; // VisitApplyPerson > Name or SafetyEduApply > Name
        public SafetyEduGridData()
        {
            SortField = nameof(SafetyEdu.EduDate);
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
                { nameof(SearchEducationCompleteStatus), SearchEducationCompleteStatus.ToString() },
                { nameof(SearchCompanyName), SearchCompanyName },
                { nameof(SearchStartEduDate), SearchStartEduDate },
                { nameof(SearchEndEduDate), SearchEndEduDate },
                { nameof(SearchName), SearchName },
            };
    }
}