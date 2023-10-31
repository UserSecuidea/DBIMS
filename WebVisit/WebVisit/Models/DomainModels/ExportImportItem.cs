using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class ExportImportItem
    {
        /* 
        req: ExportImportItemID, ExportImportID,
        no req: PRNo, MaterialsCode, MaterialsName, Quantity, Unit, Standard, Memo

        [common]
        req: InsertDate, IsDelete
        no req: UpdateDate, 
        */
        [Key]
        public int ExportImportItemID { get; set; }
        public int ExportImportID { get; set; }

        [MaxLength(100)]
        public string? PRNo { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? MaterialsCode { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? MaterialsName { get; set; } = string.Empty;

        public float? Quantity { get; set; }
        // public int? Quantity { get; set; } // 2023.09.08 SAP(RFC)의 자재 수량이 decimal 인 경우가 존재.

        [MaxLength(50)]
        public string? Unit { get; set; } = string.Empty;
        public float? ImportCnt { get; set; } = 0.0f; // 2023.09.08 SAP(RFC)의 자재 수량이 decimal 인 경우가 존재.
        public float? RemainCnt { get; set; } = 0.0f; // 2023.09.08 SAP(RFC)의 자재 수량이 decimal 인 경우가 존재.

        [MaxLength(100)]
        public string? Standard { get; set; } = string.Empty;
        
        [MaxLength(300)]
        public string? Memo { get; set; } = string.Empty;
        



        //갱신일, 생성일, 삭제여부 
        public DateTime? UpdateDate { get; set; } 

        public DateTime InsertDate { get; set; } 
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = "N";

        public ExportImportItem(){
            //
        }
        

    }
}
