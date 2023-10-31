using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class PersonGridData:GridData
    {
        // Search Properties
        public string SearchPersonTypeID { get; set; } = "-1";//0 임직원, 1 상주, 비상주(2 관리자/3 사원)
        public string SearchPersonStatusID  { get; set; } = "0";// 재직 상태 0 재직, 1 휴직, 2 퇴직
        public string SearchOrgName  { get; set; } = "+";
        public string SearchName { get; set; } = "+";
        public string SearchCompanyName { get; set; } = "+";

        public PersonGridData()
        {
            SortField = nameof(Person.Name);
            SortDirection = "desc";
            PageSize = 10;
        }

        [JsonIgnore]
        public bool IsSortByName => SortField.EqualsNoCase(nameof(Person.Name));

        // [JsonIgnore]
        // public bool IsSortByCompanyID => SortField.EqualsNoCase(nameof(Person.CompanyID));

        public override PersonGridData Clone() => (PersonGridData)MemberwiseClone();

        public Dictionary<string, string> SearchDictionary = new();
        //PersonTypeID CompanyID PersonStatusID OrgName  Name
        public override Dictionary<string, string> ToDictionary()  =>
            new()
            {
                { nameof(PageNumber), PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() },
                { nameof(SortField), SortField },
                { nameof(SortDirection), SortDirection },
                { nameof(SearchPersonTypeID), SearchPersonTypeID.ToString() },
                { nameof(SearchPersonStatusID), SearchPersonStatusID.ToString() },
                { nameof(SearchOrgName), SearchOrgName.ToString() },
                { nameof(SearchName), SearchName.ToString() },
                { nameof(SearchCompanyName), SearchCompanyName.ToString() },
            };
    }
}