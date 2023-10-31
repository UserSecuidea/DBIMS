using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class MealPrice
    {
        [Key]
        public int MealPriceID { get; set; }

        [MaxLength(50)]
        public string? Location { get; set; } = string.Empty;

        public int Price { get; set; }

        [MaxLength(50)]
        public string? UpdateIP { get; set; } = string.Empty;

        //등록자: 사번, 이름, 부서ID, 부서명        
        [MaxLength(50)]
        public string? UpdateSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? InsertName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? InsertOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? InsertOrgNameKor { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? InsertOrgNameEng { get; set; } = string.Empty;


        //갱신일, 생성일, 삭제여부 
        public DateTime? UpdateDate { get; set; } 

        public DateTime InsertDate { get; set; } 
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = "N";


        public MealPrice(){
            //
        }
        public MealPrice Clone() => (MealPrice)MemberwiseClone();
        

    }
}

