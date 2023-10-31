using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class CarryItemInfoHistory
    {
        /* 
        CarryItemInfoHistoryID	CarryItemInfoID	CarryItemApplyID	CarryItemCodeID	ItemName	ItemSN	Quantity	Unit	ImportCnt	ExportCnt	RemainCnt	Memo	InsertUpdateDate


        req: CarryItemInfoHistoryID, CarryItemInfoID, CarryItemApplyID, CarryItemCodeID, ItemName, 
        no req: ItemSN, Quantity, Unit, ImportCnt, ExportCnt, RemainCnt, Memo

        [common]
        req: InsertUpdateDate

        */
        [Key]
        public int CarryItemInfoHistoryID { get; set; }
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
        
        [MaxLength(300)]
        public string? Memo { get; set; } = string.Empty;


        //생성(갱신)일 
        public DateTime InsertUpdateDate { get; set; }

        //2023.10.05 dsyoo CarryItemInfo 에 노트북 바이러스 관련 컬럼 추가
        [Column(TypeName = "char(1)")]
        public string? IsVaccineUpdated { get; set; } = "N";

        [Column(TypeName = "char(1)")]
        public string? IsVirusScanned { get; set; } = "N";

        [Column(TypeName = "char(1)")]
        public string? QuarantineConfirmationGate { get; set; } = "N";

        [Column(TypeName = "char(1)")]
        public string? QuarantineConfirmationContact { get; set; } = "N";

        public CarryItemInfoHistory(){
            //
        }
        

    }
}
