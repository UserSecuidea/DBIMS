using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class Menu
    {
        /*req: MenuID, GroupNo, Depth, OrderNo, MenuNameKor, IsDisplay
        no req: MenuNameEng, URL, Params,

        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int MenuID { get; set; }
        public int GroupNo { get; set; }
        public int Depth { get; set; } = 0;
        public int OrderNo { get; set; } = 1;
        
        [Required(ErrorMessage = "Please enter a menu name.")]
        [MaxLength(100)]
        public string MenuNameKor { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? MenuNameEng { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? URL { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? IconImg { get; set; } = string.Empty;

        [MaxLength(1)]
        public string IsDisplay { get; set; } = "Y";



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
        public string IsDelete { get; set; } = "N";


        public Menu(){
            //
        }

        public Menu Clone() => (Menu)MemberwiseClone();

    }
}
