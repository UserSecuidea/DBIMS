using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class MealSchedule
    {
        [Key]
        public int MealScheduleID { get; set; }
        //하계(0)/동계(1)구분
        public int Term { get; set; }

        // 조식(1),중식(2),석식(3),야식(4)
        public int MealGB { get; set; }

        
        [Column(TypeName = "char(4)")]
        public string StartTime { get; set; } = string.Empty;
        [Column(TypeName = "char(4)")]
        public string EndTime { get; set; } = string.Empty;

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

        public MealSchedule(){
            //
        }
        
        public MealSchedule Clone() => (MealSchedule)MemberwiseClone();
        

    }
}

