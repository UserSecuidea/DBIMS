using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVisit.Models
{
    public static class VisitEnum
    {
        public enum LevelType 
        {
            Master                      = 1, // 마스터관리자
            GeneralManager              = 2, // 일반관리자
            Employee                    = 3, // 임직원(일반)
            EmployeeHR                  = 4, // 임직원(인사)
            EmployeeESH                 = 5, // 임직원(ESH)
            EmployeeDietitian           = 6, // 임직원(영양사)
            PartnerNonresident          = 7, // 비상주 (사원)
            Security                    = 8, // 보안실
            PartnerNonresidentManager   = 9, // 비상주업체 (관리자)
            EmployeePurchasing          = 10, // 구매
        }

        public enum CommonCode
        {
            Location                        =            1 , // 사업장
            CompanyType                     =            6 , // 업체구분
            WorkTypeCode                    =           10 , // 공종코드
            ImmStatusCode                   =           16 , // 체류자격코드
            CarType                         =           26 , // 차량구분코드
            VisitPurpose                    =           33 , // 방문목적코드
            ImportPurpose                   =           39 , // 반입목적코드
            ExportImportPurposeType         =           45 , // 반출입목적
            ExportImportType                =           60 , // 반출입구분
            ExportType                      =           64 , // 반출구분
            ImportType                      =           70 , // 반입구분
            ImportWayType                   =           77 , // 반입자구분
            DeliveryMethodType              =           80 , // 반출물전달방법
            Place                           =           86 , // 장소코드
            CarryItem                       =          102 , // 휴대물품구분코드
            CardIssueType                   =          107 , // 출입증발급구분
            VisitApplyType                  =          110 , // 방문신청구분
            VipType                         =          116 , // VIP구분코드
            SMSType                         =          118 , // SMS발송구분
            CompanyStatus                   =          120 , // 업체상태
            CardIssueStatus                 =          124 , // 출입증발급상태
            CardApplyStatus                 =          127 , // 출입증신청상태
            TempCardIssueStatus             =          131 , // 임시증발급상태
            SafetyViolationStatus           =          136 , // 안전수칙위반상태
            BlackListStatus                 =          141 , // 블랙리스트상태
            VisitApplyStatus                =          144 , // 방문신청상태
            VisitStatus                     =          150 , // 방문상태
            EducationApplyStatus            =          156 , // 교육신청상태
            EducationCompleteStatus         =          159 , // 교육이수상태
            ExportImportStatus              =          162 , // 반출입상태
            CarryItemStatus                 =          168 , // 휴대물품반입반출상태
            CarryItemApplyStatus            =          177 , // 휴대물품신청상태
            MealType                        =          183 , // 식수구분
            TermType                        =          189 , // 계절구분
            MealMenualType                  =          192 , // 수기등록구분
            ForeignerType                   =          195 , // 외국인구분
            GenderType                      =          198 , // 성별구분
            VisitorType                     =          201 , // 방문자구분
            PersonStatus                    =          204 , // 재직상태
            ApprovalType                    =          208 , // 결재유형
            ApprovalSysType                 =          214 , // 결재자유형
            PersonType                      =          218 , // 회원구분
            WorkApplyStatus                 =          225, // 공사신청상태
            WorkVisitApplyStatus            =          229, // 공사출입신청상태
            VisitManualPurpose              =          233, // 방문수기등록방문목적
            BlackListType                   =          235, // 등록사유구분
            CarryItemImportType             =          241, // 휴대물품반입구분
            CardType                        =          249, // 출입증구분
            Unit                            =          254, // 단위
            ExportImportApplyStatus         =          277, // 반출입신청상태
        }
    }
}