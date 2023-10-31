using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class CommonCode
    {
        /* req: CommonCodeID, GroupNo, CodeNameKor, CodeNameEng, OrderNo, IsActive, IsSys
        no req: SubCodeID, 
        
        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommonCodeID { get; set; }

        public int GroupNo { get; set; } //insert PK
        public int? SubCodeID { get; set; }
         
        [MaxLength(50)]
        public string? SubCodeDesc { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Please enter a code name.")]
        [MaxLength(100)]
        public string CodeNameKor { get; set; } = string.Empty;

        // [Required(ErrorMessage = "Please enter a code name.")]
        [MaxLength(100)]
        public string? CodeNameEng { get; set; } = string.Empty;

        public int OrderNo { get; set; } = 0;
        
        [Column(TypeName = "char(1)")]
        public string IsActive { get; set; } = "Y";

        [Column(TypeName = "char(1)")]
        public string IsSys { get; set; } = "N";
         
        [MaxLength(300)]
        public string? Memo { get; set; } = string.Empty; 

        //등록자: 사번, 이름, 부서ID, 부서명
        [Required(ErrorMessage = "Please enter a employee no.")]
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
        public DateTime? UpdateDate { get; set; } = DateTime.Now;

        public DateTime InsertDate { get; set; }  = DateTime.Now;
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = "N";

        public CommonCode(){
            // To-do
        }
        public CommonCode Clone() => (CommonCode)MemberwiseClone();

    }}