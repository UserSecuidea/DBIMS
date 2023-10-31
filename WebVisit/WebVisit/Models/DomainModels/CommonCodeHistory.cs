using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebVisit.Models
{
    public class CommonCodeHistory
    {
        /* req: CommonCodeHistoryID, CommonCodeID, GroupNo, CodeNameKor, CodeNameEng, OrderNo, IsActive, IsSys
        no req: SubCodeID, 
        
        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng
        */
        [Key]
        public int CommonCodeHistoryID { get; set; }
        public int CommonCodeID { get; set; }

        public int GroupNo { get; set; }
        
        [Required(ErrorMessage = "Please enter a code name.")]
        [MaxLength(100)]
        public string CodeNameKor { get; set; } = string.Empty;

        // [Required(ErrorMessage = "Please enter a code name.")]
        [MaxLength(100)]
        public string? CodeNameEng { get; set; } = string.Empty;

        public int? SubCodeID { get; set; }
         
        [MaxLength(50)]
        public string? SubCodeDesc { get; set; } = string.Empty; 

        public int OrderNo {get; set;}
        
        [Column(TypeName = "char(1)")]
        public string IsActive { get; set; } = string.Empty;

        [Column(TypeName = "char(1)")]
        public string IsSys { get; set; } = string.Empty;
         
        [MaxLength(300)]
        public string? Memo { get; set; } = string.Empty; 


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

        public CommonCodeHistory(){
            // To-do
        }

    }}