using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class Holiday
    {
        /*
        req: HolidayID, HolidayDate, HolidayName, IsManual

        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int HolidayID { get; set; }
        
        [Column(TypeName = "char(10)")]
        public string HolidayDate { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter a holiday name.")]
        [MaxLength(100)]
        public string HolidayName { get; set; } = string.Empty;

        [MaxLength(1)]
        public string IsManual { get; set; } = string.Empty;



        //등록자: 사번, 이름, 부서ID, 부서명
        [Required]
        [MaxLength(50)]
        public string InsertSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? InsertName { get; set; } = string.Empty;

        // public int? InsertOrgID {get; set;}
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
        public string IsDelete { get; set; } = string.Empty;


        public Holiday(){
            //
        }
    }
}
