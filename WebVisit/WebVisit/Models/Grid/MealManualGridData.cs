using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class MealManualGridData:GridData
    {
        public int SearchLocation { get; set; } = -1;
        public int SearchIsVisitor { get; set; } = -1;
        public string SearchStartMealYMD { get; set; } = "+";
        public string SearchEndMealYMD { get; set; } = "+";
        public string SearchVisitantCompany { get; set; } = "+";
        public string SearchOrgNameKor { get; set; } = "+";
        public string SearchVisitantName { get; set; } = "+";
        public string SearchMealGB1 { get; set; } = "1";
        public string SearchMealGB2 { get; set; } = "1";
        public string SearchMealGB3 { get; set; } = "1";
        public string SearchMealGB4 { get; set; } = "1";
        
        public MealManualGridData()
        {
            SortField = nameof(MealLog.MealLogID);
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
                { nameof(SearchLocation), SearchLocation.ToString() },
                { nameof(SearchIsVisitor), SearchIsVisitor.ToString() },
                { nameof(SearchStartMealYMD), SearchStartMealYMD.ToString() },
                { nameof(SearchEndMealYMD), SearchEndMealYMD.ToString() },
                { nameof(SearchVisitantCompany), SearchVisitantCompany.ToString() },
                { nameof(SearchOrgNameKor), SearchOrgNameKor.ToString() },
                { nameof(SearchVisitantName), SearchVisitantName.ToString() },
                { nameof(SearchMealGB1), SearchMealGB1.ToString() },
                { nameof(SearchMealGB2), SearchMealGB2.ToString() },
                { nameof(SearchMealGB3), SearchMealGB3.ToString() },
                { nameof(SearchMealGB4), SearchMealGB4.ToString() },
            };
    }
}