using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace WebVisit.Models
{
    public class QueryOptions<T>
    {
        // public properties for sorting, filtering, and paging
        public Expression<Func<T, Object>> OrderBy { get; set; } = null!;
        public Dictionary<Expression<Func<T, Object>>, string> MultipleOrderBy = new();

        public Expression<Func<T, Object>> GroupBy { get; set; } = null!;
        public Expression<Func<T, Object>> Max { get; set; } = null!;
        public Expression<Func<T, bool>> Where { get; set; } = null!;
        public List<Expression<Func<T, bool>>> MultipleWhere = new();
        public string OrderByDirection { get; set; } = "asc";  // default
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        /* Code for working with Include strings */
        private string[] includes = Array.Empty<string>();
        /// <summary>
        /// public write-only property for Include strings – accepts a string, 
        /// converts it to a string array, and stores in private string array field
        /// </summary>
        public string Includes {
            set => includes = value.Replace(" ", "").Split(',');
        }

        /// <summary>
        /// public get method for Include strings - returns private string array, or
        /// public get method for Include strings - returns private string array, or
        /// </summary>
        /// <returns></returns>
        public string[] GetIncludes() => includes;

        // read-only properties 
        public bool HasWhere => Where != null || MultipleWhere.Count > 0;
        public bool HasOrderBy => OrderBy != null || MultipleOrderBy.Count > 0;
        public bool HasGroupBy => GroupBy != null;
        public bool HasMax => Max != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;

    }
}