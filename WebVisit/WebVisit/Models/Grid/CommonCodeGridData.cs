using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class CommonCodeGridData: GridData
    {
        public int OrderNo = 0;

        // necessary, search filed

        public CommonCodeGridData()
        {
            SortField = nameof(CommonCode.CommonCodeID);
            SortDirection = "desc";
            PageSize = 3;
        }
        
        /// <summary>
        /// determines where the company are currently sorted by compnay name
        /// </summary>
        [JsonIgnore]
        public bool IsSortByGroupNo => SortField.EqualsNoCase(nameof(CommonCode.GroupNo));
        [JsonIgnore]
        public bool IsSortByCommonCodeID => SortField.EqualsNoCase(nameof(CommonCode.CommonCodeID));
        [JsonIgnore]
        public bool IsSortByCodeNameKor => SortField.EqualsNoCase(nameof(CommonCode.CodeNameKor));


        public override Dictionary<string, string> ToDictionary()  =>
            new Dictionary<string, string> {
                { nameof(PageNumber), PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() },
                { nameof(SortField), SortField },
                { nameof(SortDirection), SortDirection },
                { nameof(OrderNo), OrderNo.ToString() },
            };
    }
}