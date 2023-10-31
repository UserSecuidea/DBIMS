using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebVisit.Models
{
    public class NoticeListViewModel
    {
        public IEnumerable<Notice> Notices { get; set; } = new List<Notice>();
        public NoticeGridData CurrentRoute { get; set; } = new NoticeGridData();
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool IsMaster { get; set; } = false;
        public string? SearchField { get; set; }
        public string? SearchKeyword { get; set; }
        public List<SelectListItem> SearchFieldsKor { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Title", Text = "제목" },
            new SelectListItem { Value = "Contents", Text = "내용" },
        };        
        public List<SelectListItem> SearchFieldsEng { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Title", Text = "Title" },
            new SelectListItem { Value = "Contents", Text = "Contents" },
        };        
    }
}