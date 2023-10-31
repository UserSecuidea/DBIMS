using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class MealLogGridData: GridData
    {
        // {tabIdx?}/{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/
        // {searchlocation?}/{searchcompanyname?}/{searchorgnamekor?}/{searchname?}
        // /{searchstartmealymd?}/{searchendmealymd?}
        public int TabIdx { get; set;} = 1;
        public int SearchLocation { get; set; } = -1;
        public string SearchCompanyName { get; set; } = "+";
        public string SearchOrgNameKor { get; set; } = "+";
        public string SearchName { get; set; } = "+";
        public string SearchStartMealYMD { get; set; } = "+";
        public string SearchEndMealYMD { get; set; } = "+";

        public MealLogGridData()
        {
            SortField = nameof(MealLog.MealLogID);
            SortDirection = "desc";
            PageSize = 10;
        }
        
        /// <summary>
        /// determines where the company are currently sorted by compnay name
        /// </summary>
        [JsonIgnore]
        public bool IsSortByMealLogID => SortField.EqualsNoCase(nameof(MealLog.MealLogID));
        public virtual MealLogGridData Clone() => (MealLogGridData)MemberwiseClone();

        public override Dictionary<string, string> ToDictionary()  =>
            new Dictionary<string, string> {
                { nameof(TabIdx), TabIdx.ToString() },
                { nameof(PageNumber), PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() },
                { nameof(SortField), SortField },
                { nameof(SortDirection), SortDirection },
                { nameof(SearchLocation), SearchLocation.ToString() },
                { nameof(SearchCompanyName), SearchCompanyName },
                { nameof(SearchOrgNameKor), SearchOrgNameKor },
                { nameof(SearchName), SearchName },
                { nameof(SearchStartMealYMD), SearchStartMealYMD },
                { nameof(SearchEndMealYMD), SearchEndMealYMD },
            };
    }
}