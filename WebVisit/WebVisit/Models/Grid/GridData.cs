using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVisit.Models
{
    /// <summary>
    /// custom route - page_sort 와 함께 사용
    /// page_sort: {controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}
    /// </summary>
    public abstract class GridData
    {
        // model binding properties
        // route parameter: pagenumber, pagesize, sortfield, sortdirection
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortDirection { get; set; } = "desc"; // asc | desc
        public string SortField { get; set; } = string.Empty;

        public string SearchField { get; set; } = string.Empty;
        public string SearchKeyword { get; set; } = string.Empty;

        /// <summary>
        /// general purpose methods for paging and sorting 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public int GetTotalPages(int count) => (count + PageSize - 1) / PageSize;

        /// <summary>
        /// determines the sort order of the items
        /// </summary>
        /// <param name="newSortField"></param>
        /// <param name="current"></param>
        public void SetSortAndDirection(string newSortField, GridData current)
        {
            // set sort field
            SortField = newSortField;

            // set sort direction based on comparison of new and current sort field.  
            // If sort field is same as current, toggle between ascending and descending. 
            // if it's different, should always be ascending.
            if (current.SortField.EqualsNoCase(newSortField) && current.SortDirection == "asc")
                SortDirection = "desc";
            else
                SortDirection = "asc";
        }

        /// <summary>
        /// make copy of self
        /// 현재 객체의 copy 를 반환
        /// </summary>
        /// <returns></returns>
        public virtual GridData Clone() => (GridData)MemberwiseClone();

        /// <summary>
        /// return dictionary of route segments for URL building 
        /// {controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> ToDictionary() =>
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