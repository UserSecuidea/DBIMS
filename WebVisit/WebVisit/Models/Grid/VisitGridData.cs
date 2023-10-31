using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class VisitGridData:GridData
    {
        public int SearchVisitApplyType { get; set; } = -1;
        public int? SearchVisitApplyStatus { get; set; } = null;  //2023.09.26 신인아
        public int SearchVisitStatus { get; set; } = -1;
        public string SearchName { get; set; } = "+";
        public string SearchBirthDate { get; set; } = "+";
        public string SearchCompanyName { get; set; } = "+";
        public string SearchStartInsertDate { get; set; } = "+";
        public string SearchEndInsertDate { get; set; } = "+";
        public string SearchContactName { get; set; } = "+"; // 2023.10.05 신인아, 담당자검색 참고
        public string SearchInsertName { get; set; } = "+";  // 2023.10.05 신인아, 담당자검색
        public string SearchCardNo { get; set; } = "+";
        public string SearchCarNo { get; set; } = "+";

        public VisitGridData()
        {
            SortField = nameof(VisitApplyPerson.Name);
            SortDirection = "desc";
            PageSize = 10;
        }

        [JsonIgnore]
        public bool IsSortByName => SortField.EqualsNoCase(nameof(VisitApplyPerson.Name));

        // [JsonIgnore]
        // public bool IsSortByCompanyID => SortField.EqualsNoCase(nameof(Person.CompanyID));
        public override Dictionary<string, string> ToDictionary()  =>
            new()
            {
                { nameof(PageNumber), PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() },
                { nameof(SortField), SortField },
                { nameof(SortDirection), SortDirection },
                { nameof(SearchVisitApplyType), SearchVisitApplyType.ToString() },
                { nameof(SearchVisitApplyStatus), SearchVisitApplyStatus.ToString() },
                { nameof(SearchVisitStatus), SearchVisitStatus.ToString() },
                { nameof(SearchName), SearchName.ToString() },
                { nameof(SearchBirthDate), SearchBirthDate.ToString() },
                { nameof(SearchCompanyName), SearchCompanyName.ToString() },
                { nameof(SearchStartInsertDate), SearchStartInsertDate.ToString() },
                { nameof(SearchEndInsertDate), SearchEndInsertDate.ToString() },
                { nameof(SearchContactName), SearchContactName.ToString() }, // 2023.10.05 신인아, 담당자검색 참고
                { nameof(SearchInsertName), SearchInsertName.ToString() },   // 2023.10.05 신인아, 담당자검색
                { nameof(SearchCardNo), SearchCardNo.ToString() },
                { nameof(SearchCarNo), SearchCarNo.ToString() },
            };
    }
}