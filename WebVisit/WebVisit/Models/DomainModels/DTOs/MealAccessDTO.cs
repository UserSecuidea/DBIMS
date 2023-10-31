using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class MealAccessDTO
    {
        public string Location { get; set; } = string.Empty;
        public string OrgNameKor { get; set; } = string.Empty;
        public string OrgNameEng { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string GradeName { get; set; } = string.Empty;
        public string EMPLOYEE_TYPE { get; set; } = string.Empty;
        public string PersonTypeID { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        // MealAccess
        public string Sabun { get; set; } = string.Empty;
        public int MealAccessID { get; set; }

        public int? MealGB1 { get; set; }
        public int? MealGB2 { get; set; }
        public int? MealGB3 { get; set; }
        public int? MealGB4 { get; set; }
        public int? MealGB5 { get; set; }
        public int? MealGB6 { get; set; }

        //갱신일, 생성일
        public DateTime? UpdateDate { get; set; } 

        public DateTime InsertDate { get; set; }

        public MealAccess Clone() => (MealAccess)MemberwiseClone();

    }
}