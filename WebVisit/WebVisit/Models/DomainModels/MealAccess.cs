using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class MealAccess
    {
        [Key]
        public int MealAccessID { get; set; }

        //식사자: 사번
        [MaxLength(50)]
        public string Sabun { get; set; } = string.Empty;

        public int? MealGB1 { get; set; }
        public int? MealGB2 { get; set; }
        public int? MealGB3 { get; set; }
        public int? MealGB4 { get; set; }
        public int? MealGB5 { get; set; }
        public int? MealGB6 { get; set; }

        //갱신일, 생성일
        public DateTime? UpdateDate { get; set; } 

        public DateTime InsertDate { get; set; }

        public MealAccess(){
            //
        }
        public MealAccess Clone() => (MealAccess)MemberwiseClone();
        

    }
}

