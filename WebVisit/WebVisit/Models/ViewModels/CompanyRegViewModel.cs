using WebVisit.Models.DomainModels.Passport;
namespace WebVisit.Models
{
    public class CompanyRegViewModel
    {
        public Company company { get; set; } = new Company();
        public List<ApprovalPerson> ApprovalPeople { get; set; } = new();
       
    }
}