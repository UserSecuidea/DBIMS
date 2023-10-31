using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVisit.Utilities;

namespace WebVisit.Models
{
    public class SafetyViolationDTO
    {
        // mode ContactSabun ContactOrgID ContactOrgNameKor ContactOrgNameEng
        // ContactMobile personID ViolationDate InsertName InsertOrgNameKor 
        // WorkOrderNo IsWorkOrder ContactName Location ViolationLocation SafetyViolationReasonID DetailReason
        public string Mode { get; set; } = "A";
        public string? ContactName { get; set; }
        public int ContactSabun { get; set; }
        public string? ContactOrgID { get; set; }
        public string? ContactOrgNameKor { get; set; }
        public string? ContactOrgNameEng { get; set; }
        public string? ContactMobile { get; set; }
        public string? ViolationDate { get; set; }
        public string? InsertName { get; set; }
        public string? InsertOrgNameKor { get; set; }
        public string? WorkOrderNo { get; set; }
        public string? IsWorkOrder { get; set; } = "N"; // WorkOrderNo 없음 체크박스
        public string? Location { get; set; }
        public string? ViolationLocation { get; set; }
        public string? SafetyViolationReasonID { get; set; }
        public string? DetailReason { get; set; }

        public List<SafetyViolationPerson?> SafetyViolationPersonList { get; set; } = new ();
        public string[]? Sabun { get; set; }
        public string[]? Name { get; set; }
        public string[]? CompanyName { get; set; }
        public string[]? BirthDate { get; set; }
        public string[]? Mobile { get; set; }

        public void SetSafetyViolationPerson()
        {
            if(SafetyViolationPersonList != null){
                if(Name != null) {
                    for (var i = 0; i < Name.Length; i++)
                    {
                        SafetyViolationPerson safetyViolationPerson = new();
                        SafetyViolationPersonHistory safetyViolationPersonHistory = new();
                        safetyViolationPerson.Name = Name[i];
                        safetyViolationPerson.Sabun = Sabun?[i]??"";
                        safetyViolationPerson.CompanyName = CompanyName?[i]??"";
                        safetyViolationPerson.BirthDate = BirthDate?[i]??"";
                        safetyViolationPerson.Mobile = Mobile?[i]??"";
                        
                        safetyViolationPersonHistory.Name = Name[i];
                        safetyViolationPersonHistory.CompanyName = CompanyName?[i]??"";
                        safetyViolationPersonHistory.BirthDate = BirthDate?[i]??"";
                        safetyViolationPersonHistory.Mobile = Mobile?[i]??"";

                        safetyViolationPerson.SafetyViolationPersonHistory = safetyViolationPersonHistory;
                        SafetyViolationPersonList.Add(safetyViolationPerson);
                    }
                }
            }
        }

    }
}