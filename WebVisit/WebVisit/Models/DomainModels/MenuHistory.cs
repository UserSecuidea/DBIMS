using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class MenuHistory
    {
        /* req: MenuHistoryID, MenuID, GroupNo, Depth, OrderNo, MenuNameKor, IsDisplay
        no req: MenuNameEng, URL, Params, 
        
        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng 
        */
        [Key]
        public int MenuHistoryID { get; set; }
        public int MenuID { get; set; }
        public int GroupNo { get; set; }
        public int Depth { get; set; }
        public int OrderNo { get; set; }
        
        [Required(ErrorMessage = "Please enter a menu name.")]
        [MaxLength(100)]
        public string MenuNameKor { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter a menu name.")]
        [MaxLength(100)]
        public string? MenuNameEng { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter a menu url.")]
        [MaxLength(200)]
        public string? URL { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? IconImg { get; set; } = string.Empty;

        [MaxLength(1)]
        public string IsDisplay { get; set; } = string.Empty;



        //등록&변경자: 사번, 이름, 부서ID, 부서명
        [Required]
        [MaxLength(50)]
        public string UpdateSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? UpdateName { get; set; } = string.Empty;

        // public int? UpdateOrgID {get; set;}
        [MaxLength(50)]
        public string? UpdateOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? UpdateOrgNameKor { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? UpdateOrgNameEng { get; set; } = string.Empty;

        //생성(갱신)일
        public DateTime InsertUpdateDate { get; set; } 

        public MenuHistory(){
            //
        }
    }
}