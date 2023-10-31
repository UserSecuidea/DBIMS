using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class ApprovalGridData: GridData
    {
        public int SearchLocation { get; set; } = -1;
        public int SearchApprovalType { get; set; } = -1;

        // necessary, search filed

        public ApprovalGridData()
        {
            SortField = nameof(Approval.ApprovalID);
            SortDirection = "desc";
            PageSize = 6;
        }
        
        /// <summary>
        /// determines where the company are currently sorted by compnay name
        /// </summary>
        [JsonIgnore]
        public bool IsSortByApprovalID => SortField.EqualsNoCase(nameof(Approval.ApprovalID));
        // [JsonIgnore]
        // public bool IsSortByCommonCodeID => SortField.EqualsNoCase(nameof(CommonCode.CommonCodeID));
        // [JsonIgnore]
        // public bool IsSortByCodeNameKor => SortField.EqualsNoCase(nameof(CommonCode.CodeNameKor));


        public override Dictionary<string, string> ToDictionary()  =>
            new Dictionary<string, string> {
                { nameof(PageNumber), PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() },
                { nameof(SortField), SortField },
                { nameof(SortDirection), SortDirection },
                { nameof(SearchLocation), SearchLocation.ToString() },
                { nameof(SearchApprovalType), SearchApprovalType.ToString() },
            };
    }
}