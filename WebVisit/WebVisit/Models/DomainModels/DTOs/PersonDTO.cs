using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVisit.Models
{
    public class PersonDTO
    {
        public int PersonID { get; set; } = -1;
        public int? PID { get; set; } = -1;
        public string Sabun { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty; // 사업장
        public string? LocationName { get; set; } = string.Empty;
        public int? CompanyID { get; set; } = -1;
        // 임직원(0)/상주직원(1)/비상주관리자(2)/비상주직원(3)
        public int? PersonTypeID { get; set; } = 3;
        public string? OrgID { get; set; } = string.Empty;
        public string? OrgNameKor { get; set; } = string.Empty;
        public string? OrgNameEng { get; set; } = string.Empty;
        public int LevelCodeID { get; set; } = -1;
        public string? GradeName { get; set; } = string.Empty;
        public string? Tel { get; set; } = string.Empty;
        public string? Mobile { get; set; } = string.Empty;
        public int? CarAllowCnt { get; set; } = 0;
        public int? CarRegCnt { get; set; } = 0;
        
        public string? InsertOrgNameKor { get; set; } = string.Empty;
        public string? InsertOrgNameEng { get; set; } = string.Empty;

        public string? PorteID { get; set; } = string.Empty; // V_USER_INFO
        public string? SapDeptCode { get; set; } = string.Empty;
        // public int? GradeID { get; set; }
        // public string? GradeName { get; set; } = string.Empty;
        // public string? BirthDate { get; set; } = string.Empty;
        // public int? Gender { get; set; }
        // public string? Mobile { get; set; } = string.Empty;
        // public string? Tel { get; set; } = string.Empty;
        // public string? Email { get; set; } = string.Empty;
        // public int? PersonStatusID { get; set; }
        // public string? HomeAddr { get; set; } = string.Empty;
        // public string? HomeDetailAddr { get; set; } = string.Empty;
        // public string? HomeZipcode { get; set; } = string.Empty;
        // public int? IsForeigner { get; set; }
        // public string? Nationality { get; set; } = string.Empty;
        // public int? ImmStatusCodeID { get; set; }
        // public string? ImmStartDate { get; set; } = string.Empty;
        // public string? ImmEndDate { get; set; } = string.Empty;
        // public int? WorkTypeCodeID { get; set; }

        public PersonDTO() { }

        public PersonDTO(Person person){
            PersonID = person.PersonID;
            PID = person.PID;
            Sabun = person.Sabun;
            Name = person.Name;
            Location = person.Location;
            CompanyID = person.CompanyID;
            PersonTypeID = person.PersonTypeID;
            OrgID = person.OrgID;
            OrgNameKor = person.OrgNameKor;
            OrgNameEng = person.OrgNameEng;
            LevelCodeID = person.LevelCodeID;
            GradeName = person.GradeName;
            InsertOrgNameKor = person.InsertOrgNameKor;
            InsertOrgNameEng = person.InsertOrgNameEng;
            PorteID = person.PorteID;
            SapDeptCode = person.SapDeptCode;
            Tel = person.Tel;
            Mobile = person.Mobile;
            CarAllowCnt = person.CarAllowCnt;
            CarRegCnt = person.CarRegCnt;
        }
    }
}