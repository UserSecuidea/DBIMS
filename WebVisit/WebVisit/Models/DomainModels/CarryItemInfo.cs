using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class CarryItemInfo
    {
        /* 
        req: CarryItemInfoID, CarryItemApplyID, CarryItemCodeID, ItemName, 
        no req: ItemSN, Quantity, Unit, ImportCnt, ExportCnt

        [common]
        req: InsertDate, IsDelete
        no req: UpdateDate, 
        */
        [Key]
        public int CarryItemInfoID { get; set; }
        public int CarryItemApplyID { get; set; }
        public int CarryItemCodeID { get; set; }

        [MaxLength(100)]
        public string ItemName { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? ItemSN { get; set; } = string.Empty;

        public int? Quantity { get; set; } = 0;

        [MaxLength(50)]
        public string? Unit { get; set; } = string.Empty;
        public int? ExportCnt { get; set; } = 0;
        public int? RemainCnt { get; set; } = 0;

        //갱신일, 생성일, 삭제여부
        public DateTime? UpdateDate { get; set; } = DateTime.Now;

        public DateTime InsertDate { get; set; } = DateTime.Now;
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = "N";
        
        //2023.10.05 dsyoo CarryItemInfo 에 노트북 바이러스 관련 컬럼 추가
        [Column(TypeName = "char(1)")]
        public string IsVaccineUpdated { get; set; } = "N";

        [Column(TypeName = "char(1)")]
        public string IsVirusScanned { get; set; } = "N";

        [Column(TypeName = "char(1)")]
        public string QuarantineConfirmationGate { get; set; } = "N";

        [Column(TypeName = "char(1)")]
        public string QuarantineConfirmationContact { get; set; } = "N";

        public CarryItemInfo(){
            //
        }
        

    }
}
