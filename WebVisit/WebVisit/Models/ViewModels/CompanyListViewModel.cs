namespace WebVisit.Models
{
    public class CompanyListViewModel
    {
        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
        public CompanyGridData CurrentRoute { get; set; } = new CompanyGridData();
        public List<CommonCode>? CodeCompanyStatus;
        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
    }
}