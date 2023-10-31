using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVisit.Utilities;

namespace WebVisit.Models
{
    public class CarryItemDTO
    {
        public int? Idx { get; set; } // 방문객 입력 순서, 0 부터 시작
        // CarryItemApply: ImportDate ExportDate Location ImportPurposeCodeID PlaceCodeID 
        public CarryItemApply CarryItemApply { get; set; } = new ();
        public CarryItemApplyHistory CarryItemApplyHistory { get; set; } = new();

        public List<CarryItemInfo>? CarryItemInfoList { get => Items ; set => Items = value; }
        public List<CarryItemInfo>? Items { get; set; } // form name
        // public List<CarryItemInfoHistory>? CarryItemInfoHistoryList { get; set; }
        // public List<CarryItemInfo> CarryItemInfoList { get; set; } = new ();
        // public List<CarryItemDetail>? Items { get; set; }
        public void SetCarryItemApply(int VisitorType, int VisitPersonID, string? VisitorSabun = "", string? Name="", string? Mobile="", string? CompanyName="", string? GradeName="", string? Tel=""){
          // Utils.WriteLog("CarryItemDTO SetCarryItemApply: 1> "+ImportPurposeCodeID+" / "+ContactPersonOrgID); "DA20001"
          CarryItemApply = new()
          {
            ImportDate = ImportDate,
            ExportDate = ExportDate,
            Location = Location??"",
            ImportPurposeCodeID = string.IsNullOrEmpty(ImportPurposeCodeID)? 0:int.Parse(ImportPurposeCodeID),
            PlaceCodeID = PlaceCodeID == null? 0:int.Parse(PlaceCodeID),
            // VisitApplyPersonID = VisitPersonID,
            Sabun = VisitorSabun,
            Name = Name,
            Mobile = Mobile,
            CompanyName = CompanyName,
            GradeName = GradeName,
            Tel = Tel,
            ContactSabun = ContactPersonSabun,
            ContactName = ContactPersonName,
            ContactOrgID = ContactPersonOrgID,
            ContactOrgNameKor = ContactPersonOrgNameKor,
            ContactOrgNameEng = ContactPersonOrgNameEng,
            ContactMobile = ContactPersonMobile,
          };
          CarryItemApplyHistory = new()
          {
            ImportDate = ImportDate,
            ExportDate = ExportDate,
            Location = Location??"",
            ImportPurposeCodeID = string.IsNullOrEmpty(ImportPurposeCodeID)? 0:int.Parse(ImportPurposeCodeID),
            PlaceCodeID = PlaceCodeID == null? 0:int.Parse(PlaceCodeID),
            // VisitApplyPersonID = VisitPersonID,
            Sabun = VisitorSabun,
            Name = Name,
            Mobile = Mobile,
            CompanyName = CompanyName,
            ContactSabun = ContactPersonSabun,
            ContactName = ContactPersonName,
            ContactOrgID = ContactPersonOrgID,
            ContactOrgNameKor = ContactPersonOrgNameKor,
            ContactOrgNameEng = ContactPersonOrgNameEng,
            ContactMobile = ContactPersonMobile,
          };
        }

        public string? ImportDate { get; set; }
        public string? ExportDate { get; set; }
        public string? Location { get; set; } // Location
        public string? ImportPurposeCodeID { get; set; }
        public string? PlaceCodeID { get; set; } // PlaceCodeID

        // 담당자 정보: ContactPersonName ContactPersonTel
        public string? ContactPersonSabun { get; set; }
        public string? ContactPersonName { get; set; }
        public string? ContactPersonTel { get; set; }
        public string? ContactPersonMobile { get; set; }        
        public string? ContactPersonOrgID { get; set; }
        public string? ContactPersonOrgNameKor { get; set; }
        public string? ContactPersonOrgNameEng { get; set; }

        // public CarryItemDetail[]? Items { get; set; }
        // 휴대물품 정보: CarryItemCodeID ItemName ItemSN Quantity Unit

        public CarryItemDTO(){}
    }

    public class CarryItemDetail {
        public string? CarryItemCodeID { get; set; }
        public string? ItemName { get; set; }
        public string? ItemSN { get; set; }
        public string? Quantity { get; set; }
        public string? Unit { get; set; }
        //2023.10.16 dsyoo 보안조치 추가
        public string? IsVaccineUpdated { get; set; }
        public string? IsVirusScanned { get; set; }
        public string? QuarantineConfirmationGate { get; set; }
        public string? QuarantineConfirmationContact { get; set; }

    }
}
/*
{
  "idx": 0,
  "items": [
    {
      "CarryItemCodeID": "0",
      "ItemName": "",
      "ItemSN": "",
      "Quantity": "",
      "Unit": ""
    },
    {
      "CarryItemCodeID": "0",
      "ItemName": "",
      "ItemSN": "",
      "Quantity": "",
      "Unit": ""
    }
  ],
  "ImportDate": "",
  "ExportDate": "",
  "Location": "1000",
  "ImportPurposeCodeID": "0",
  "PlaceCodeID": "0",
  "ContactPersonName": "",
  "ContactPersonTel": ""
}
*/