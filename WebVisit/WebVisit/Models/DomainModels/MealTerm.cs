using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class MealTerm
    {
        [Key]
        public int MealTermID { get; set; }
        //하계(0)/동계(1)구분
        public int Term { get; set; }
        
        [Column(TypeName = "varchar(4)")]
        public string StartDate { get; set; } = string.Empty;
        
        [Column(TypeName = "varchar(4)")]
        public string EndDate { get; set; } = string.Empty;

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


        public MealTerm(){
            //
        }
        
        public MealTerm Clone() => (MealTerm)MemberwiseClone();       

    }
}

