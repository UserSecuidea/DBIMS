using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVisit.Utilities;

namespace WebVisit.Models
{
    public class VisitDTO
    {
        // [VisitApply] String Location, String VisitStartDate, String VisitEndDate, int PlaceCodeID, int VisitPurposeCodeID, 
        //             String ContactSabun, String ContactName, int ContactOrgID, String ContactOrgNameKor, String ContactOrgNameEng, int RegVisitorType,
        // [VisitApply|VisitApplyPerson] int VisitorType, int VisitApplyType, int VisitPersonID, 
        // [VisitApplyPerson] String ckVisitorEdu, String ckCleanEdu, String IsSafetyEdu, String SafetyEduDate, String ckIsTermsPrivarcy, String ckIsVIP, int VipTypeCodeID,
        // [VisitApplyPerson|VisitPerson] String Name, String BirthDate, int Gender, String Mobile, String CompanyName, String CarNo, 
        // [VisitPerson] String GradeName, String Tel)
        // VisitApply
        public VisitApply? VisitApply { get; set; } = new VisitApply();
        public VisitApplyHistory? VisitApplyHistory { get; set; } = new VisitApplyHistory();
        public void SetVisitApply(){
            if (VisitApply != null && VisitApplyHistory != null){
                if (Location != null){
                    VisitApply.Location = Location;
                    VisitApplyHistory.Location = Location;
                }
                if (VisitStartDate != null){
                    VisitApply.VisitStartDate = VisitStartDate;
                    VisitApplyHistory.VisitStartDate = VisitStartDate;
                }
                if (VisitEndDate != null){
                    VisitApply.VisitEndDate = VisitEndDate;
                    VisitApplyHistory.VisitEndDate = VisitEndDate;
                }
                if (PlaceCodeID != null){
                    VisitApply.PlaceCodeID = PlaceCodeID??0;
                    VisitApplyHistory.PlaceCodeID = PlaceCodeID??0;
                }
                if (VisitPurposeCodeID != null){
                    VisitApply.VisitPurposeCodeID = VisitPurposeCodeID;
                    VisitApplyHistory.VisitPurposeCodeID = VisitPurposeCodeID;
                }
                if (ContactName != null){
                    VisitApply.ContactName = ContactName;
                    VisitApplyHistory.ContactName = ContactName;
                }
                if (ContactSabun != null){
                    VisitApply.ContactSabun = ContactSabun;
                    VisitApplyHistory.ContactSabun = ContactSabun;
                }
                if (ContactOrgID != null){
                    VisitApply.ContactOrgID = ContactOrgID;
                    VisitApplyHistory.ContactOrgID = ContactOrgID;
                }
                if (ContactOrgNameKor != null){
                    VisitApply.ContactOrgNameKor = ContactOrgNameKor;
                    VisitApplyHistory.ContactOrgNameKor = ContactOrgNameKor;
                }
                if (ContactOrgNameEng != null){
                    VisitApply.ContactOrgNameEng = ContactOrgNameEng;
                    VisitApplyHistory.ContactOrgNameEng = ContactOrgNameEng;
                }
                if (RegVisitorType != null){
                    VisitApply.RegVisitorType = RegVisitorType??0; // 방문자구분(등록자): 회원 = 0 / 방문자 = 1 
                    VisitApplyHistory.RegVisitorType = RegVisitorType??0; // 방문자구분(등록자): 회원 = 0 / 방문자 = 1 
                }
                if (VisitApplyType != null){
                    VisitApply.VisitApplyType = VisitApplyType??0;
                    VisitApplyHistory.VisitApplyType = VisitApplyType??0;
                }
            }
        }
        public string Mode { get; set; } = "A"; // AW 공사, AH 홈 화면(방문자), A 방문객 등록
        public int InsertVisitorType { get; set; } = 0; // 방문자구분(등록자): 회원 = 0 / 방문자 = 1 
        public string? Location { get; set; }
        public string? VisitStartDate { get; set; }
        public string? VisitEndDate { get; set; }
        public int? PlaceCodeID { get; set; }
        public int? VisitPurposeCodeID { get; set; }
        public string? ContactName { get; set; }
        public string? ContactSabun { get; set; }
        public string? ContactOrgID { get; set; }
        public string? ContactOrgNameKor { get; set; }
        public string? ContactOrgNameEng { get; set; }
        public int? RegVisitorType { get; set; }
        public int? VisitApplyType { get; set; } // 방문신청자구분(등록자): 임직원(0) / 방문자(1)
        public List<VisitApplyPerson?> VisitApplyPersonList { get; set; } = new ();
        // public List<VisitApplyPersonHistory?> VisitApplyPersonHistoryList { get; set; } = new();
        // public List<CarryItemDTO> CarryItemDTOList { get; set; } = new ();
        public void SetVisitApplyPerson()
        {
            if (VisitApplyPersonList != null)
            {
                if (Name != null){
                    var IsVisitorEdus = CkVisitorEdus?.Split(",");
                    var IsCleanEdus = CkCleanEdus?.Split(",");
                    var IsVIPs = CkIsVIPs?.Split(",");
                    var IsSafetyEdus= CkSafetyEdus?.Split(",");
                    for (var i = 0; i < Name.Length; i++){
                        VisitApplyPerson visitApplyPerson = new();
                        VisitApplyPersonHistory visitApplyPersonHistory = new();
                        // VisitorType VisitPersonID VisitorSabun Name Mobile CompanyName
                        // 방문객 등록 페이지: OrgID OrgNameKor OrgNameEng 정보 없음.
                        if (VisitorSabun != null)
                            visitApplyPerson.Sabun = VisitorSabun[i];
                        visitApplyPerson.Name = Name[i];
                        visitApplyPerson.BirthDate = BirthDate?[i];
                        visitApplyPerson.Gender = Gender?[i];
                        visitApplyPerson.Mobile = Mobile?[i];
                        visitApplyPerson.CompanyName = CompanyName?[i];
                        visitApplyPerson.CarNo = CarNo?[i];

                        if (VisitorSabun != null)
                            visitApplyPersonHistory.Sabun = VisitorSabun[i];
                        visitApplyPersonHistory.Name = Name[i];
                        visitApplyPersonHistory.BirthDate = BirthDate?[i];
                        visitApplyPersonHistory.Gender = Gender?[i];
                        visitApplyPersonHistory.Mobile = Mobile?[i];
                        visitApplyPersonHistory.CompanyName = CompanyName?[i];
                        visitApplyPersonHistory.CarNo = CarNo?[i];
                        
                        int vType = 0; // 방문자구분(방문자): 임직원(0, 비상주업체관리자 또는 비상주사원) / 방문자(1)
                        if(VisitorType != null && VisitorType.Length > i){
                            if (!string.IsNullOrEmpty(VisitorType[i])){
                                vType = int.Parse(VisitorType[i]);
                            }
                            visitApplyPerson.VisitorType =  vType;
                            visitApplyPersonHistory.VisitorType = vType;
                        }
                        int vPersonID = 0; // 방문객회원ID: "방문자구분(방문자) 중 방문자(1)" 일때 방문자회원의 VisitPersonID
                        if(VisitPersonID != null && VisitPersonID.Length > i){
                            if (!string.IsNullOrEmpty(VisitPersonID[i])){
                                vPersonID = int.Parse(VisitPersonID[i]);
                            }
                            visitApplyPerson.VisitPersonID = vPersonID;
                            visitApplyPersonHistory.VisitPersonID = vPersonID;
                        }
                        visitApplyPerson.Sabun = VisitorSabun?[i] ?? "";
                        visitApplyPersonHistory.Sabun = VisitorSabun?[i] ?? "";
                        visitApplyPerson.IsSafetyEdu = VisitorSabun?[i] ?? "";
                        visitApplyPersonHistory.IsSafetyEdu = VisitorSabun?[i] ?? "";
                        if(SafetyEduDate != null){
                            visitApplyPerson.SafetyEduDate = SafetyEduDate?[i] == null ? null : Convert.ToDateTime(SafetyEduDate?[i]);
                            visitApplyPersonHistory.SafetyEduDate = SafetyEduDate?[i] == null ? null : Convert.ToDateTime(SafetyEduDate?[i]);
                        }
                        visitApplyPerson.IsTermsPrivarcy = CkIsTermsPrivarcy?[i] ?? "N";
                        visitApplyPersonHistory.IsTermsPrivarcy = CkIsTermsPrivarcy?[i] ?? "N";
                        visitApplyPerson.VipTypeCodeID = VipTypeCodeID?[i];
                        visitApplyPersonHistory.VipTypeCodeID = VipTypeCodeID?[i];
                        // 방문자 교육
                        visitApplyPerson.IsVisitorEdu = IsVisitorEdus?[i];
                        visitApplyPersonHistory.IsVisitorEdu = IsVisitorEdus?[i];
                        if (Mode.Equals("AH") && IsVisitorEdus != null && i<IsVisitorEdus.Length && IsVisitorEdus[i].Equals("Y")){
                            visitApplyPerson.VisitorEduDate = DateTime.Now;
                            visitApplyPersonHistory.VisitorEduDate = DateTime.Now;
                        }
                        // 클린룸 교육
                        visitApplyPerson.IsCleanEdu = IsCleanEdus?[i];
                        visitApplyPersonHistory.IsCleanEdu = IsCleanEdus?[i];
                        if (Mode.Equals("AH") && IsCleanEdus != null && i<IsCleanEdus.Length && IsCleanEdus[i].Equals("Y")){
                            visitApplyPerson.CleanEduDate = DateTime.Now;
                            visitApplyPersonHistory.CleanEduDate = DateTime.Now;
                        }
                        // 안전 교육
                        visitApplyPerson.IsSafetyEdu = IsSafetyEdus?[i];
                        visitApplyPersonHistory.IsSafetyEdu = IsSafetyEdus?[i];
                        // VIP 여부
                        visitApplyPerson.IsVIP = IsVIPs?[i];
                        visitApplyPersonHistory.IsVIP = IsVIPs?[i];

                        visitApplyPerson.VisitApplyPersonHistory = visitApplyPersonHistory;
                        VisitPerson visitPerson = new()
                        {
                            Name = Name[i],
                            BirthDate = BirthDate?[i] ?? "",
                            Gender = Gender == null ? 0 : Gender[i],
                            Mobile = Mobile?[i] ?? "",
                            CompanyName = CompanyName?[i] ?? "",
                            CarNo = CarNo?[i],
                            GradeName = GradeName?[i],
                            Tel = Tel?[i],
                            VisitorEduLastDate = visitApplyPerson.VisitorEduDate,
                            CleanEduLastDate = visitApplyPerson.CleanEduDate,                            
                        };
                        visitApplyPerson.VisitPerson = visitPerson;

                        var m = CarryItems?[i];
                        if (m != null)
                        {
                            // Utils.WriteLog("visitDTO CarryItems: 1> " + m);
                            var carryItemDTO = Utils.Des<CarryItemDTO>(m);
                            if (carryItemDTO != null && VisitorSabun != null && VisitorSabun.Length > i
                                 && Name != null && Name.Length > i && Mobile != null && Mobile.Length > i  && CompanyName != null && CompanyName.Length > i)
                            {
                                // VisitorType VisitPersonID VisitorSabun Name Mobile CompanyName
                                carryItemDTO.SetCarryItemApply(vType, vPersonID, VisitorSabun[i], Name[i], Mobile[i], CompanyName[i], GradeName?[i], Tel?[i]);
                                visitApplyPerson.CarryItemDTO = carryItemDTO;
                                // Utils.WriteLog("visitDTO carryItemDTO: 3> " + Utils.Dump(carryItemDTO));
                            }
                        }
                        VisitApplyPersonList.Add(visitApplyPerson);
                    }
                }
            }
        }
        // VisitApply | VisitApplyPerson
        public string[]? VisitorType { get; set; } // 방문자구분(방문자): 임직원(0, 비상주업체관리자 또는 비상주사원) / 방문자(1)
        public string[]? VisitPersonID { get; set; } // 방문객회원ID: "방문자구분(방문자) 중 방문자(1)" 일때 방문자회원의 VisitPersonID
        // VisitApplyPerson
        public string[]? VisitorSabun { get; set; } // 회원사번(방문자): "방문자구분(방문자) 중 임직원(0, 비상주업체관리자 또는 비상주사원)" 일때 임직원의 Sabun

        // public string[]? CkVisitorEdu { get; set; } // 사용안함
        // public string[]? CkCleanEdu { get; set; } // 사용안함
        // public string[]? CkIsVIP { get; set; } // 사용안함
        public string[]? IsSafetyEdu { get; set; }
        public string[]? SafetyEduDate { get; set; }
        public string[]? CkIsTermsPrivarcy { get; set; }
        public int[]? VipTypeCodeID { get; set; } // selectbox

        public string? CkVisitorEdus { get; set; } // 사용 - 쉼표로 구분된 array
        public string? CkCleanEdus { get; set; } // 사용 - 쉼표로 구분된 array
        public string? CkIsVIPs { get; set; } // 사용 - 쉼표로 구분된 array

        public string? CkSafetyEdus { get; set; } // 사용 - 쉼표로 구분된 array

        // VisitApplyPerson|VisitPerson
        public string[]? Name { get; set; }
        public string[]? BirthDate { get; set; }
        public int[]? Gender { get; set; } // select box
        public string[]? Mobile { get; set; }
        public string[]? CompanyName {get; set;}
        public string[]? CarNo {get; set;}

        // VisitPerson
        public string[]? GradeName { get; set; }
        public string[]? Tel { get; set; }

        // public void SetCarryItems(){
        //     if(CarryItems!=null){
        //         foreach(string m in CarryItems) {
        //             Utils.WriteLog("visitDTO CarryItems:" + m);
        //             var carryItemDTO = Utils.Des<CarryItemDTO>(m);
        //             if (carryItemDTO != null)
        //                 CarryItemDTOList.Add(carryItemDTO);
        //             Utils.WriteLog("visitDTO CarryItems: des 2 " + Utils.Dump(carryItemDTO));
        //         }
        //     }
        // }
        // CarryItem 휴대물품
        // public string[]? CkCarryItem { get; set; } // deprecated 사용안함
        // public string? CkCarryItems { get; set; } // 사용안함

        public string[]? CarryItems {get;set;}

        public VisitDTO() { }

        public int TotalCount(){
            if (Name == null) {
                return 0;
            }
            return Name.Length;
        }
    }
}