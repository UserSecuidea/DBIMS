using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class PorteLog
    {
        /* req: PorteLogID	BusinessID	ResponseData	IsSuccess InsertDate	IsDelete
        
        [common]
        req: BusinessID, ResponseData, IsSuccess
        */
        [Key]
        public int PorteLogID { get; set; }
        [MaxLength(20)]
        public string BusinessID { get; set; } = string.Empty;
        [MaxLength(2000)]
        public string ResponseData { get; set; } = string.Empty;

        
        [Column(TypeName = "char(1)")]
        public string IsSuccess { get; set; } = "N";

        //생성일, 삭제여부 

        public DateTime InsertDate { get; set; } 
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = "N";

        public PorteLog(){
            //
        }
        public PorteLog Clone() => (PorteLog)MemberwiseClone();

    }
}
