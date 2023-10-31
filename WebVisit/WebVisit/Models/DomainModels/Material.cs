using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class Material
    {
        /* 
        req: MaterialID
        no req: DIV_CODE, STOCK_CODE, STOCK_NM, STOCK_RULE, STOCK_UNIT
        
        [common]
        req: W_DATE, 
        */
        [Key]
        public int MaterialID { get; set; }

        
        [MaxLength(50)] // Location 
        public string? DIV_CODE { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string? STOCK_CODE { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? STOCK_NM { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? STOCK_RULE { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? STOCK_UNIT { get; set; } = string.Empty;


        //생성일, 삭제여부 

        [Column(TypeName = "char(10)")]
        public string? W_DATE { get; set; } = string.Empty;

        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = string.Empty;

        public Material(){
            //
        }
    }
}
