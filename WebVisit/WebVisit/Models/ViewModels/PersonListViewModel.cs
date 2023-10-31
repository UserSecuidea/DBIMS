namespace WebVisit.Models
{
    public class PersonListViewModel
    {
        public IEnumerable<Person> Persons { get; set; } = new List<Person>();
        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
        public PersonGridData CurrentRoute { get; set; } = new PersonGridData();
        public PersonGridData SearchRoute { get; set; } = new PersonGridData();
        // 공통 코드
        public List<CommonCode>? CodePersonStatus;
        public List<CommonCode>? CodePersonType;
        public List<CommonCode>? CodeLocation;

        public int TotalPages { get; set; }
        public int TotalCnt { get; set; }
        
    }
}