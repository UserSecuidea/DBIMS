using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class VisitManualGridData:GridData
    {
        // public int SearchVisitApplyType { get; set; } = -1;
        public int SearchVisitApplyStatus { get; set; } = -1;
        public int SearchVisitStatus { get; set; } = -1;
        public string SearchName { get; set; } = "+";
        // public string SearchBirthDate { get; set; } = "+";
        // public string SearchCompanyName { get; set; } = "+";
        public string SearchStartInsertDate { get; set; } = "+";
        public string SearchEndInsertDate { get; set; } = "+";
        public string SearchContactName { get; set; } = "+";
        public string SearchCardNo { get; set; } = "+";
        // public string SearchCarNo { get; set; } = "+";

        public VisitManualGridData()
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
                // { nameof(SearchVisitApplyType), SearchVisitApplyType.ToString() },
                { nameof(SearchVisitApplyStatus), SearchVisitApplyStatus.ToString() },
                { nameof(SearchVisitStatus), SearchVisitStatus.ToString() },
                { nameof(SearchName), SearchName.ToString() },
                // { nameof(SearchBirthDate), SearchBirthDate.ToString() },
                // { nameof(SearchCompanyName), SearchCompanyName.ToString() },
                { nameof(SearchStartInsertDate), SearchStartInsertDate.ToString() },
                { nameof(SearchEndInsertDate), SearchEndInsertDate.ToString() },
                { nameof(SearchContactName), SearchContactName.ToString() },
                { nameof(SearchCardNo), SearchCardNo.ToString() },
                // { nameof(SearchCarNo), SearchCarNo.ToString() },
            };
    }
}