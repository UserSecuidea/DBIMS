using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class BlackListGridData:GridData
    {
        public int SearchBlackListStatus { get; set; } = -1;
        public int SearchBlackListType { get; set; } = -1;
        public string SearchName { get; set; } = "+";
        public string SearchBirthDate { get; set; } = "+";
        public string SearchCompanyName { get; set; } = "+";
        public string SearchStartInsertDate { get; set; } = "+";
        public string SearchEndInsertDate { get; set; } = "+";

        public BlackListGridData()
        {
            SortField = nameof(BlackList.BlackListID);
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
                { nameof(SearchBlackListStatus), SearchBlackListStatus.ToString() },
                { nameof(SearchBlackListType), SearchBlackListType.ToString() },
                { nameof(SearchName), SearchName.ToString() },
                { nameof(SearchBirthDate), SearchBirthDate.ToString() },
                { nameof(SearchCompanyName), SearchCompanyName.ToString() },
                { nameof(SearchStartInsertDate), SearchStartInsertDate.ToString() },
                { nameof(SearchEndInsertDate), SearchEndInsertDate.ToString() },
            };

    }
}