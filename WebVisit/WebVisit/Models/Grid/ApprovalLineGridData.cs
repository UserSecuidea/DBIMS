using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class ApprovalLineGridData: GridData
    {
        public int OrderNo = 0;

        // necessary, search filed

        public ApprovalLineGridData()
        {
            SortField = nameof(ApprovalLine.ApprovalLineID);
            SortDirection = "desc";
            PageSize = 10;
        }
        
        /// <summary>
        /// determines where the company are currently sorted by compnay name
        /// </summary>
        [JsonIgnore]
        public bool IsSortByApprovalID => SortField.EqualsNoCase(nameof(ApprovalLine.ApprovalLineID));


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