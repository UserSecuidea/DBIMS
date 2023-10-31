using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class MenuLevelHistory
    {
        /* req: MenuLevelHistoryID, MenuLevelID, MenuID, LevelID, MenuNameKor, IsAccess, IsDisplay
        
        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng 
        */
        [Key]
        public int MenuLevelHistoryID { get; set; }
        public int MenuLevelID { get; set; }

        public int MenuID { get; set; }
        public int LevelID { get; set; }
        
        [Required(ErrorMessage = "Please enter a menu name.")]
        [MaxLength(100)]
        public string MenuNameKor { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter a menu name.")]
        [MaxLength(100)]
        public string MenuNameEng { get; set; } = string.Empty;
        

        [Column(TypeName = "char(1)")]
        public string IsAccess { get; set; } = string.Empty;
        [Column(TypeName = "char(1)")]
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

        public MenuLevelHistory(){
            //
        }
    }
}