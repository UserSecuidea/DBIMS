using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class NoticeGridData: GridData
    {
        public int OrderNo = 0;

        public NoticeGridData()
        {
            SortField = nameof(Notice.NoticeID);
            SortDirection = "desc";
            PageSize = 10;
        }
        
        /// <summary>
        /// determines where the company are currently sorted by compnay name
        /// </summary>
        [JsonIgnore]
        public bool IsSortByNoticeID => SortField.EqualsNoCase(nameof(Notice.NoticeID));

        public override Dictionary<string, string> ToDictionary()  =>
            new()
            {
                { nameof(PageNumber), PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() },
                { nameof(SortField), SortField },
                { nameof(SortDirection), SortDirection },
                { nameof(SearchField), SearchField },
                { nameof(SearchKeyword), SearchKeyword },
            };
    }
}