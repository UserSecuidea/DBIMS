using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class CarryItemGridData:GridData
    {
        // public int OrderNo = 0;
        public int SearchCarryItemApplyStatus { get; set; } = -1;
        public int SearchCarryItemStatus { get; set; } = -1;
        public int SearchCarryItemImportType { get; set; } = -1;
        public string SearchName { get; set; } = "+";
        public string SearchStartInsertDate { get; set; } = "+";
        public string SearchEndInsertDate { get; set; } = "+";
        public string SearchContactName { get; set; } = "+";
        // public string SearchCardNo { get; set; } = "+";
            

        public CarryItemGridData()
        {
            SortField = nameof(CarryItemApply.Name);
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
                { nameof(SearchCarryItemApplyStatus), SearchCarryItemApplyStatus.ToString() },
                { nameof(SearchCarryItemStatus), SearchCarryItemStatus.ToString() },
                { nameof(SearchCarryItemImportType), SearchCarryItemImportType.ToString() },
                { nameof(SearchName), SearchName.ToString() },
                { nameof(SearchStartInsertDate), SearchStartInsertDate.ToString() },
                { nameof(SearchEndInsertDate), SearchEndInsertDate.ToString() },
                { nameof(SearchContactName), SearchContactName.ToString() },
                // { nameof(SearchCardNo), SearchCardNo.ToString() },
            };

    }
}