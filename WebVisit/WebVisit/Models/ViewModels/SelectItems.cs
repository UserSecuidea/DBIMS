//2023.09.11 dsyoo 
//Select Item 목록의 모델을 분리한다.
namespace WebVisit.Models.ViewModels
{
    public class SelectItems
    {


        //entity.HasData(new { LevelID = 1, LevelName = "마스터관리자", InsertSabun="1"});
        //entity.HasData(new { LevelID = 2, LevelName = "일반관리자", InsertSabun="1"});
        //entity.HasData(new { LevelID = 3, LevelName = "임직원(일반)", InsertSabun = "1" });
        //entity.HasData(new { LevelID = 4, LevelName = "임직원(인사)", InsertSabun = "1" });
        //entity.HasData(new { LevelID = 5, LevelName = "임직원(ESH)", InsertSabun = "1" });
        //entity.HasData(new { LevelID = 6, LevelName = "임직원(영양사)", InsertSabun = "1" });
        //entity.HasData(new { LevelID = 7, LevelName = "비상주", InsertSabun = "1" });
        //entity.HasData(new { LevelID = 8, LevelName = "보안실", InsertSabun = "1" });
        //entity.HasData(new { LevelID = 9, LevelName = "비상주업체", InsertSabun = "1" });

        //-----------------------------------------------------------------------------------------------------------------
        //       휴대물품, 반출입 코드표
        //-----------------------------------------------------------------------------------------------------------------
        //출입관리 Text  출입관리코드 전자결재코드                      전자결재Text 설명
        //   신청	          0	                                                                                  최초 신청상태(상신전)
        //   결재완료 	      1	             8	         일반결재 완료 : (전결자, 대결자, 결재자, 1인결재자)	  결재가 완료된상태(기존 승인완료)
        //   반려      	      2	             16	         반려 : (결재자, 협조자, 전결자, 대결자) 신청이 거부된 상태
        //   결재중 	          3	             32	         이중결재 신청부서완료 : (신청부서 최종결재자 완결 시)	  이중결재 신청부서 완료된상태
        //   승인완료	      4	             128	     이중결재 주관부서 완료 : (주관부서 최종결재자 완결 시)	  이중결재 주관부서 완료된상태
        //   미승인 	          5		         	                                                                  휴대물품 반입신청기간내에 승인이 이루어지지 않았음
        //   상신	          64	         64	         이중결재 주관부서 상신 : (주관부서 기안자 상신 시)	      이중결재 주관부서 상신 : (주관부서 기안자 상신 시)
        //   임시저장	      256	         256	     임시저장 : (기안자가 임시저장 시)	                      임시저장 : (기안자가 임시저장 시)
        //   승인보류	      512	         512	     승인보류 : (결재자가 승인보류 시)	                      승인보류 : (결재자가 승인보류 시)
        //   삭제	          1024	         1024	     삭제 : (휴지통에 있는 상신이전의 문서 삭제 시)	          삭제 : (휴지통에 있는 상신이전의 문서 삭제 시)

        #region ElApprovalOptions 전자결재 진행 옵션목록-전자결재 상태값에 따름. (자산반출입, 임직원 휴대물품신청)
        public static List<OptionModel> ElApprovalOptions(string Culture,
                                                        List<CommonCode> CodeApplyStatus,
                                                        int ApplyStatus)
        {
            List<OptionModel> options = new List<OptionModel>();
            options.Add(new OptionModel { Selected = 0, Id = ApplyStatus.ToString(), Text = GetCodeName(GetCodeByValue(ApplyStatus, CodeApplyStatus), Culture) }); //
            return options;
        }
        #endregion
        #region CarryItemApprovalOptions 내방객 휴대물품 승인을 위한 옵션 목록
        public static List<OptionModel> CarryItemApprovalOptions(string Culture,
                                                        List<CommonCode> CodeApplyStatus,
                                                        int ApplyStatus)
        {
            List<OptionModel> options = new List<OptionModel>();
            switch (ApplyStatus)
            {
                case 0: //신청상태이면 신청
                    {
                        options.Add(new OptionModel { Selected = 0, Id = "0", Text = GetCodeName(GetCodeByValue(0, CodeApplyStatus), Culture) }); //신청
                        options.Add(new OptionModel { Selected = 0, Id = "1", Text = GetCodeName(GetCodeByValue(1, CodeApplyStatus), Culture) }); //승인
                        options.Add(new OptionModel { Selected = 0, Id = "2", Text = GetCodeName(GetCodeByValue(2, CodeApplyStatus), Culture) }); //반려
                    }
                    break;
                default:
                    {
                        options.Add(new OptionModel { Selected = 0, Id = ApplyStatus.ToString(), Text = GetCodeName(GetCodeByValue(ApplyStatus, CodeApplyStatus), Culture) }); //
                    }
                    break;
            }
            return options;
        }
        #endregion
        #region ExportImportOptions 반출입 옵션
        
         //---반출입상태---
         //   0		신청
         //   1		반출상신
         //   2		결재완료
         //   3		반려
         //   4		반출대기
         //   5		반출확인
         //   6		정문반송
         //   7		반출완료   --반출입 업무가 모두 종료된 반출(리클레임, 판매, 반환)
         //   8		반입상신
         //   9		반입대기
         //   10		반입확인
         //   11		반입완료   --반출입 업무가 모두 종료된 반입

        
        //  ---반출입목적---
        //    0	   	수리
        //    1	   	리클레임
        //    2	   	판매
        //    3	   	세정(재생)
        //    4	   	교환
        //    5	   	이체(이동)
        //    6	   	기타
        //    7	   	분석
        //    8	   	Demo
        //    9	   	우수협력업체
        //    10	   	Warranty
        //    11	   	반환
        //    12	    태블릿
        //--반출구분 Export Type: 0 외부업체, 1 공장간이동, 2 외부업체경유공장간이동
        //--반입구분 ImportType : 0 반입요  , 1 반입불요  , 2 매각,3 반입불요(무상),4 대체품반입요,5 대체품선입고
        // *
        public static List<OptionModel> ExportImportOptions(string Culture,
                                                            List<CommonCode> CodeExportImportStatus,
                                                            int? ExportImportPurposeType,
                                                            int ExportImportApplyStatus,
                                                            int ExportImportStatus,
                                                            int ExportType, //0 외부업체, 1 공장간이동, 2 외부업체경유공장간이동
                                                            int? ImportType  //0 반입요  , 1 반입불요  , 2 매각,3 반입불요(무상),4 대체품반입요,5 대체품선입고
                                                            )
        {
            List<OptionModel> options = new List<OptionModel>();

            if (ExportImportApplyStatus == 1)  //반출입승인상태
            {
                switch (ExportImportStatus)
                {
                    case 4: //반출대기(4)
                        {
                            options.Add(new OptionModel { Selected = 1, Id = "4", Text = GetCodeName(GetCodeByValue(4, CodeExportImportStatus), Culture) }); //반출대기
                            if (ImportType != 0) // 0 반입요  , 1 반입불요  , 2 매각,3 반입불요(무상),4 대체품반입요,5 대체품선입고
                            {
                                options.Add(new OptionModel { Selected = 0, Id = "5", Text = GetCodeName(GetCodeByValue(5, CodeExportImportStatus), Culture) }); //반출확인
                            }
                            else
                            {
                                options.Add(new OptionModel { Selected = 0, Id = "7", Text = GetCodeName(GetCodeByValue(7, CodeExportImportStatus), Culture) }); //반출완료
                            }
                            options.Add(new OptionModel { Selected = 0, Id = "6", Text = GetCodeName(GetCodeByValue(6, CodeExportImportStatus), Culture) }); //정문반송
                        }
                        break;
                    /*
                case 6: //정문반송(6) 정문반송은 업무 종료료처리
                    {
                        options.Add(new OptionModel { Selected = 1, Id = "6", Text = GetCodeName(GetCodeByValue(6, CodeExportImportStatus), Culture) }); //정문반송
                        if (ImportType != 0) // 0 반입요  , 1 반입불요  , 2 매각,3 반입불요(무상),4 대체품반입요,5 대체품선입고
                        {
                            options.Add(new OptionModel { Selected = 0, Id = "5", Text = GetCodeName(GetCodeByValue(5, CodeExportImportStatus), Culture) }); //반출확인
                        }
                        else
                        {
                            options.Add(new OptionModel { Selected = 0, Id = "7", Text = GetCodeName(GetCodeByValue(7, CodeExportImportStatus), Culture) }); //반출완료
                        }
                    }
                    break;
                    */
                    case 7: //반출확인(7) 
                        {
                            if (ImportType == 0 || ImportType == 4 || ImportType == 5)
                            {
                                options.Add(new OptionModel { Selected = 1, Id = "9", Text = GetCodeName(GetCodeByValue(9, CodeExportImportStatus), Culture) }); //반입대기
                                options.Add(new OptionModel { Selected = 0, Id = "11", Text = GetCodeName(GetCodeByValue(11, CodeExportImportStatus), Culture) }); //반입완료
                            }
                            else
                            {
                                options.Add(new OptionModel { Selected = 0, Id = "7", Text = GetCodeName(GetCodeByValue(7, CodeExportImportStatus), Culture) }); //반출확인
                            }
                        }
                        break;
                    case 9: //반입대기(9)
                        {
                            if(ImportType==0 || ImportType==4 || ImportType==5 || ImportType==-1) //0 반입요  , 1 반입불요  , 2 매각,3 반입불요(무상),4 대체품반입요,5 대체품선입고, -1:노트북, 공장간이동
                            {
                                options.Add(new OptionModel { Selected = 1, Id = "9", Text = GetCodeName(GetCodeByValue(9, CodeExportImportStatus), Culture) }); //반입대기
                                options.Add(new OptionModel { Selected = 0, Id = "11", Text = GetCodeName(GetCodeByValue(11, CodeExportImportStatus), Culture) }); //반입완료
                            }
                            else
                            {
                                options.Add(new OptionModel { Selected = 0, Id = "9", Text = GetCodeName(GetCodeByValue(9, CodeExportImportStatus), Culture) }); //반출완료
                            }
                        }
                        break;
                    case 10: //반입확인(10)
                        {
                            options.Add(new OptionModel { Selected = 1, Id = "10", Text = GetCodeName(GetCodeByValue(10, CodeExportImportStatus), Culture) }); //반입확인
                            options.Add(new OptionModel { Selected = 0, Id = "11", Text = GetCodeName(GetCodeByValue(11, CodeExportImportStatus), Culture) }); //반입완료
                        }
                        break;
                    default:
                        {
                            options.Add(new OptionModel { Selected = 0, Id = ExportImportStatus.ToString(), Text = GetCodeName(GetCodeByValue(ExportImportStatus, CodeExportImportStatus), Culture) }); //반출완료
                        }
                        break;
                }
            }
            else
            {
                options.Add(new OptionModel { Selected = 1, Id = "", Text = "" }); //반출입상태값 없음
            }
            return options;
        }
        #endregion

        #region CarryItemApplyOptions 2023.09.20 dsyoo 휴대물품 승인 옵션
        public static List<OptionModel> CarryItemApplyOptions(string Culture,
                                                        List<CommonCode> CodeCarryItemApplyStatus,
                                                        int ImportPurposeCodeID,
                                                        int CarryItemApplyStatus,
                                                        int IsElApproveItem=0 //전자결재 승인건인지 체크
           )
        {
            List<OptionModel> options = new List<OptionModel>();
            //승인상태(CarryItemApplyStatus): 승인대기(0) / 승인완료(1) / 반려(2) / 결재중(3) / 결재완료(4)
            if (IsElApproveItem ==0) //전자결재승인건이 아니면 가각 옵션표시
            {
                switch (CarryItemApplyStatus)
                {
                    case 0: //신청(0)
                        {
                            options.Add(new OptionModel { Selected = 1, Id = "0", Text = GetCodeName(GetCodeByValue(0, CodeCarryItemApplyStatus), Culture) }); //신청
                            options.Add(new OptionModel { Selected = 0, Id = "1", Text = GetCodeName(GetCodeByValue(1, CodeCarryItemApplyStatus), Culture) }); //결재완료
                            options.Add(new OptionModel { Selected = 0, Id = "2", Text = GetCodeName(GetCodeByValue(2, CodeCarryItemApplyStatus), Culture) }); //반려
                        }
                        break;
                    default:
                        {
                            options.Add(new OptionModel { Selected = 1, Id = CarryItemApplyStatus.ToString(), Text = GetCodeName(GetCodeByValue(CarryItemApplyStatus, CodeCarryItemApplyStatus), Culture) }); //반출대기
                        }
                        break;
                }
            }
            else
            {
                options.Add(new OptionModel { Selected = 1, Id = CarryItemApplyStatus.ToString(), Text = GetCodeName(GetCodeByValue(CarryItemApplyStatus, CodeCarryItemApplyStatus), Culture) }); //반출대기
            }
            return options;
        }
        #endregion
        //2023.09.11 dsyoo
        #region CarryItemOptions 휴대물품 반입 옵션
        public static List<OptionModel> CarryItemOptions(string Culture,
                                                    List<CommonCode> CodeCarryItemStatus,
                                                    int ImportPurposeCodeID,
                                                    int CarryItemApplyStatus,
                                                    int CarryItemStatus,
                                                    string mySabun,
                                                    string ContactSabun)
        {
            List<OptionModel> options = new List<OptionModel>();
            //if (ImportPurposeCodeID == 4)
            //{  // 선입고
            
            //    //승인 > 반입 완료(선입고 6): 확인대기(2) / 반출요(3) / 반출불필요(유상 4) / 반출불필요(무상 5) / 선입고(6)
            //    //승인 > 확인대기: 
            //    //승인 > 반출요: 반출대기(7) / 반출완료(8)
            //    //승인 > 반출불필요(유상 4): 반출불필요(유상 4) - 비활성화
            //    //승인 > 반출불필요(무상 5): 반출불필요(무상 5) - 비활성화
            //    //승인 > 선입고: 선입고 비활성화
            //    //보안실: 반입대기(0) / 반출 요(3) 에서만 수정 가능.
                
            //    if (CarryItemApplyStatus == 4)
            //    { // 결재완료
            //        switch (CarryItemStatus)
            //        {
            //            case 0: //반입대기(0)
            //                {
            //                    options.Add(new OptionModel { Selected = 1, Id = "0", Text = GetCodeName(GetCodeByValue(0, CodeCarryItemStatus), Culture) }); //반출대기
            //                    options.Add(new OptionModel { Selected = 0, Id = "1", Text = GetCodeName(GetCodeByValue(1, CodeCarryItemStatus), Culture) }); //반출완료
            //                }
            //                break;
            //            case 1: //반입완료(1)
            //            case 2: //확인대기(2)
            //                {
            //                    options.Add(new OptionModel { Selected = 1, Id = "2", Text = GetCodeName(GetCodeByValue(2, CodeCarryItemStatus), Culture) }); //확인대기
            //                    options.Add(new OptionModel { Selected = 0, Id = "3", Text = GetCodeName(GetCodeByValue(3, CodeCarryItemStatus), Culture) }); //반출요
            //                    options.Add(new OptionModel { Selected = 0, Id = "4", Text = GetCodeName(GetCodeByValue(4, CodeCarryItemStatus), Culture) }); //반출불요(무상)
            //                    options.Add(new OptionModel { Selected = 0, Id = "5", Text = GetCodeName(GetCodeByValue(5, CodeCarryItemStatus), Culture) }); //반출불요(유상)
            //                    options.Add(new OptionModel { Selected = 1, Id = "6", Text = GetCodeName(GetCodeByValue(6, CodeCarryItemStatus), Culture) }); //선입고
            //                }
            //                break;
            //            case 3: //반출요(3)
            //            case 7: //반출대기(7)
            //                {
            //                    options.Add(new OptionModel { Selected = 1, Id = "7", Text = GetCodeName(GetCodeByValue(7, CodeCarryItemStatus), Culture) }); //반출대기
            //                    options.Add(new OptionModel { Selected = 0, Id = "8", Text = GetCodeName(GetCodeByValue(8, CodeCarryItemStatus), Culture) }); //반출완료
            //                }
            //                break;
            //            default:
            //                {
            //                    options.Add(new OptionModel { Selected = 0, Id = CarryItemStatus.ToString(), Text = GetCodeName(GetCodeByValue(CarryItemStatus, CodeCarryItemStatus), Culture) }); //반출완료
            //                }
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        options.Add(new OptionModel { Selected = 1, Id = "", Text = "" }); //반입상태값 없음
            //    }
            //}
            //else
            { //선입고 외
                if (CarryItemApplyStatus == 1)
                { // 승인
                    
                    //승인 > 반입대기(0) : 반입대기(0) / 반입완료(1)
                    //승인 > 반입완료(1) : 확인대기(2) / 반출요(3) / 반출불필요(유상 4) / 반출불필요(무상 5)
                    //승인 > 반출요(3) : 반출대기(7) / 반출완료(8)
                    //승인 > 반출불필요(유상 4) : 반출불필요(유상 4) - 비활성화
                    //승인 > 반출불필요(무상 5) : 반출불필요(무상 5) - 비활성화
                    //승인 > 반출완료(8) : 반출완료 - 비활성화
                    switch(CarryItemStatus) 
                    {
                        case 0: //반입대기(0)
                            {
                                options.Add(new OptionModel { Selected = 1, Id = "0", Text = GetCodeName(GetCodeByValue(0, CodeCarryItemStatus), Culture) }); //반출대기
                                options.Add(new OptionModel { Selected = 0, Id = "1", Text = GetCodeName(GetCodeByValue(1, CodeCarryItemStatus), Culture) }); //반출완료
                            }
                            break;
                        case 1: //반입완료(1)
                        case 2: //확인대기(2)
                            {
                                options.Add(new OptionModel { Selected = 1, Id = "2", Text = GetCodeName(GetCodeByValue(2, CodeCarryItemStatus), Culture) }); //확인대기
                                options.Add(new OptionModel { Selected = 0, Id = "3", Text = GetCodeName(GetCodeByValue(3, CodeCarryItemStatus), Culture) }); //반출요
                                options.Add(new OptionModel { Selected = 0, Id = "4", Text = GetCodeName(GetCodeByValue(4, CodeCarryItemStatus), Culture) }); //반출불요(무상)
                                options.Add(new OptionModel { Selected = 0, Id = "5", Text = GetCodeName(GetCodeByValue(5, CodeCarryItemStatus), Culture) }); //반출불요(유상)
                                if(ImportPurposeCodeID == 4) //2023.09.20 dsyoo 선입고
                                {
                                    options.Add(new OptionModel { Selected = 0, Id = "6", Text = GetCodeName(GetCodeByValue(6, CodeCarryItemStatus), Culture) }); 
                                }
                            }
                            break;
                        case 3: //반출요(3)
                        case 7: //반출대기(7)
                            {
                                options.Add(new OptionModel { Selected = 1, Id = "7", Text = GetCodeName(GetCodeByValue(7, CodeCarryItemStatus), Culture) }); //반출대기
                                options.Add(new OptionModel { Selected = 0, Id = "8", Text = GetCodeName(GetCodeByValue(8, CodeCarryItemStatus), Culture) }); //반출완료
                            }
                            break;
                        default:
                            {
                                options.Add(new OptionModel { Selected = 0, Id = CarryItemStatus.ToString(), Text = GetCodeName(GetCodeByValue(CarryItemStatus, CodeCarryItemStatus), Culture) }); //반출완료
                            }
                            break;
                    }
                }
                else
                {
                    options.Add(new OptionModel { Selected = 1, Id = "", Text = "" }); //반입상태값 없음
                }

            }

            return options;
        }
        #endregion
        #region CarryItemText 휴대물품 반입 문자변환
        public static string CarryItemText(string Culture,
                                                        List<CommonCode> CodeCarryItemStatus,
                                                        int ImportPurposeCodeID,
                                                        int CarryItemApplyStatus,
                                                        int CarryItemStatus,
                                                        string mySabun,
                                                        string ContactSabun)
        {
            string strRet = "";           

            if (ImportPurposeCodeID == 4)
            {  // 선입고

                //승인 > 반입 완료(선입고 6): 확인대기(2) / 반출요(3) / 반출불필요(유상 4) / 반출불필요(무상 5) / 선입고(6)
                //승인 > 확인대기: 
                //승인 > 반출요: 반출대기(7) / 반출완료(8)
                //승인 > 반출불필요(유상 4): 반출불필요(유상 4) - 비활성화
                //승인 > 반출불필요(무상 5): 반출불필요(무상 5) - 비활성화
                //승인 > 선입고: 선입고 비활성화
                //보안실: 반입대기(0) / 반출 요(3) 에서만 수정 가능.

                if (CarryItemApplyStatus == 4)  //결재완료 - 선입고 결재완료
                { // 결재완료
                    switch (CarryItemStatus)
                    {
                        case 0: //반입대기(0)
                            {
                                strRet = GetCodeName(GetCodeByValue(0, CodeCarryItemStatus), Culture);
                            }
                            break;
                        case 1: //반입완료(1)
                        case 2: //확인대기(2)
                            {
                                strRet = GetCodeName(GetCodeByValue(2, CodeCarryItemStatus), Culture);
                            }
                            break;
                        case 3: //반출요(3)
                        case 7: //반출대기(7)
                            {
                                strRet = GetCodeName(GetCodeByValue(7, CodeCarryItemStatus), Culture);
                            }
                            break;
                        default:
                            {
                                strRet = GetCodeName(GetCodeByValue(CarryItemStatus, CodeCarryItemStatus), Culture);
                            }
                            break;
                    }
                }
            }
            else
            { //선입고 외
                if (CarryItemApplyStatus == 1)
                { // 승인

                    //승인 > 반입대기(0) : 반입대기(0) / 반입완료(1)
                    //승인 > 반입완료(1) : 확인대기(2) / 반출요(3) / 반출불필요(유상 4) / 반출불필요(무상 5)
                    //승인 > 반출요(3) : 반출대기(7) / 반출완료(8)
                    //승인 > 반출불필요(유상 4) : 반출불필요(유상 4) - 비활성화
                    //승인 > 반출불필요(무상 5) : 반출불필요(무상 5) - 비활성화
                    //승인 > 반출완료(8) : 반출완료 - 비활성화
                    switch (CarryItemStatus)
                    {
                        case 0: //반입대기(0)
                            {
                                strRet = GetCodeName(GetCodeByValue(0, CodeCarryItemStatus), Culture);
                            }
                            break;
                        case 1: //반입완료(1)
                        case 2: //확인대기(2)
                            {
                                strRet = GetCodeName(GetCodeByValue(2, CodeCarryItemStatus), Culture);
                            }
                            break;
                        case 3: //반출요(3)
                        case 7: //반출대기(7)
                            {
                                strRet = GetCodeName(GetCodeByValue(7, CodeCarryItemStatus), Culture);
                            }
                            break;
                        default:
                            {
                                strRet = GetCodeName(GetCodeByValue(CarryItemStatus, CodeCarryItemStatus), Culture);
                            }
                            break;
                    }
                }

            }

            return strRet;
        }
        #endregion
        #region VisitApplyOptions 2023.10.24 dsyoo 방문신청 상태 옵션
        public static List<OptionModel> VisitApplyOptions(string Culture,
                                                        List<CommonCode> CodeVisitApplyStatus,
                                                        int VisitApplyStatus
           )
        {
            List<OptionModel> options = new List<OptionModel>();
            //승인상태(VisitApplyStatus): 승인대기(0) / 승인완료(1) / 반려(2) / 결재중(3) / 결재완료(4)
            switch (VisitApplyStatus)
            {
                case 0: //신청(0)
                    {
                        options.Add(new OptionModel { Selected = 1, Id = "0", Text = GetCodeName(GetCodeByValue(0, CodeVisitApplyStatus), Culture) }); //신청
                        options.Add(new OptionModel { Selected = 0, Id = "1", Text = GetCodeName(GetCodeByValue(1, CodeVisitApplyStatus), Culture) }); //결재완료
                        options.Add(new OptionModel { Selected = 0, Id = "2", Text = GetCodeName(GetCodeByValue(2, CodeVisitApplyStatus), Culture) }); //반려
                    }
                    break;
                default:
                    {
                        options.Add(new OptionModel { Selected = 1, Id = VisitApplyStatus.ToString(), Text = GetCodeName(GetCodeByValue(VisitApplyStatus, CodeVisitApplyStatus), Culture) }); 
                    }
                    break;
            }
            return options;
        }
        #endregion

        #region VisitStatusOptions  방문상태 옵션
        public static List<OptionModel> VisitStatusOptions(string Culture,
                                                        List<CommonCode> CodeVisitStatus,
                                                        int VisitPurposeCodeID,
                                                        int VisitApplyStatus,
                                                        int VisitStatus,
                                                        string mySabun,
                                                        string ContactSabun)
        {
            List<OptionModel> options = new List<OptionModel>();
            if (VisitApplyStatus == 1)
            {
                //승인 > 미방문(0)
                // - 방문대기(0) / 방문(1)
                //승인 > 방문(1)
                //- 방문(1) / 방문완료(2)
                //승인 > 출입증미반납(3)
                // - 출입증미반납(3) / 방문완료(2) / 출입증분실(5)
                //기타: 선택없음

                //승인 > 미방문
                // - 방문대기 / 방문
                //승인 > 방문
                //- 방문 / 방문완료 / 출입증분실
                //승인 > 출입증미반납
                // - 출입증미반납 / 방문완료 / 출입증분실
                //기타: 선택없음
                switch (VisitStatus)
                {
                    case 0: //방문대기(0)
                        {
                            options.Add(new OptionModel { Selected = 1, Id = "0", Text = GetCodeName(GetCodeByValue(0, CodeVisitStatus), Culture) }); //방문대기
                            options.Add(new OptionModel { Selected = 0, Id = "1", Text = GetCodeName(GetCodeByValue(1, CodeVisitStatus), Culture) }); //방문
                        }
                        break;
                    case 1: //방문(1)
                        {
                            options.Add(new OptionModel { Selected = 1, Id = "1", Text = GetCodeName(GetCodeByValue(1, CodeVisitStatus), Culture) }); //방문
                            options.Add(new OptionModel { Selected = 0, Id = "2", Text = GetCodeName(GetCodeByValue(2, CodeVisitStatus), Culture) }); //방문완료
                        }
                        break;
                    case 3: //출입증미반납(3)
                        {
                            options.Add(new OptionModel { Selected = 1, Id = "3", Text = GetCodeName(GetCodeByValue(3, CodeVisitStatus), Culture) }); //확인대기
                            options.Add(new OptionModel { Selected = 0, Id = "2", Text = GetCodeName(GetCodeByValue(2, CodeVisitStatus), Culture) }); //반출요
                        }
                        break;
                    default:
                        {
                            options.Add(new OptionModel { Selected = 0, Id = VisitStatus.ToString(), Text = GetCodeName(GetCodeByValue(VisitStatus, CodeVisitStatus), Culture) }); //
                        }
                        break;
                }
            }
            else
            {
                options.Add(new OptionModel { Selected = 1, Id = "0", Text = GetCodeName(GetCodeByValue(0, CodeVisitStatus), Culture) }); //방문대기
            }
            return options;
        }
        #endregion
        #region IsVisitManager  방문시스템(내방객,휴대물품)의 관리자레벨(마스터(1),보안실(8),방문지와 근무지가 같은 일반관리자(2)인지 리턴
        public static bool IsSecurityManager(int levelID, string VisitLocation="", string UserLocation = "") 
        {
            bool bRet = false;
            if(levelID == 1)  //마스터(1)
            {
                bRet = true;
            }
            else if(levelID == 2 || levelID == 8) //일반관리자(2),보안실(8) 
            {
                if (VisitLocation == UserLocation) //방문(반입)지와 관리자의 사업장 코드가 일치해야한다.
                {
                    bRet = true;
                }
            }
            return bRet;
        }
        #endregion
        #region IsLocalManager  사업장의 관리자레벨(마스터(1),방문지와 근무지가 같은 일반관리자(2)인지 리턴
        public static bool IsLocalManager(int levelID, string VisitLocation = "", string UserLocation = "")
        {
            bool bRet = false;
            if (levelID == 1)  //마스터(1)
            {
                bRet = true;
            }
            else if (levelID == 2 ) //일반관리자(2) 
            {
                if (VisitLocation == UserLocation) //방문(반입)지와 관리자의 사업장 코드가 일치해야한다.
                {
                    bRet = true;
                }
            }
            return bRet;
        }
        #endregion

        #region IsPurchasingManager  2023.09.27 dsyoo 사업장의 구매담당자레벨(마스터(1),방문지와 근무지가 같은 구매담당자(10)인지 리턴 
        public static bool IsPurchasingManager(int levelID, string VisitLocation = "", string UserLocation = "")
        {
            bool bRet = false;
            if (levelID == 1)  //마스터(1)
            {
                bRet = true;
            }
            else if (levelID == 10) //구매담당자(10)
            {
                if (VisitLocation == UserLocation) //방문(반입)지와 관리자의 사업장 코드가 일치해야한다.
                {
                    bRet = true;
                }
            }
            return bRet;
        }
        #endregion

        private static string GetCodeName(CommonCode cd, string Lang)
        {
            string strRet = "";
            try
            {
                strRet=cd.CodeNameKor;
                if (Lang == "en")
                {
                    strRet = (string)cd.CodeNameEng;
                }

            }
            catch(Exception ex)
            {
                strRet = "";
            }
            return (string)strRet;
        }
        private static string GetCodeName(string Kor, string Eng, string Lang)
        {
            string strRet = Kor;
            if (Lang == "en")
            {
                strRet = (string)Eng;
            }
            return (string)strRet;
        }
        private static CommonCode GetCodeByValue(int nVal, List<CommonCode> listCommonCode)
        {
            CommonCode cc = null;
            foreach (var m2 in listCommonCode)
            {
                if (nVal == m2.SubCodeID)
                {
                    cc = m2;
                    break;
                }
            }
            return cc;
        }
        //2023.09.21 dsyoo Value, 언어선택을 입력받아 Text를 받는다
        public static string GetNameByValue(int nVal,  List<CommonCode> listCommonCode, string Lang)
        {
            string strRet = "";
            CommonCode cc = null;
            foreach (var m2 in listCommonCode)
            {
                if (nVal == m2.SubCodeID)
                {
                    cc = m2;
                    strRet = GetCodeName(cc, Lang);
                    break;
                }
            }
            return strRet;
        }


        #region IsNotebookSecurityObj 2023.10.18 dsyoo 노트북 방역대상인지 확인
        public bool IsNotebookSecurityObj(int PlaceCodeID, int CarryItemCodeID)
        {
            bool bRet = false;
            //PlaceCodeID : 클린룸(6)
            //CarryItemCodeID : 노트북및PC(0)
            if (PlaceCodeID == 6 && CarryItemCodeID == 0)
            {
                bRet = true;
            }
            return bRet;
        }
        #endregion
        #region DisableNotebookSecurityObj  휴대물품 승인,반입 상태에따라 QuarantineConfirmationGate, QuarantineConfirmationContact 을 Enable("")/Disable("disabled")시킨다.
        public static string DisableNotebookSecurityObj(int CarryItemApplyStatus, int CarryItemStatus
                                                    , string UserOrgCode, string ContactOrgCode
                                                    , string Obj
                                                    , int levelID, string VisitLocation = "", string UserLocation = "")
        {
            string Disabled = "disabled";
            //승인상태(CarryItemApplyStatus): 승인대기(0) / 승인완료(1) / 반려(2) / 결재중(3) / 결재완료(4)
            //반입상태(CarryItemStatus):반입대기(0), 반입완료(1), 확인대기(2)
            if (CarryItemApplyStatus == 1)
            {
                switch (CarryItemStatus)
                {
                    case 0:   //반입대기-경비근무자가 체크할수 있음
                        {
                            if(Obj == "QuarantineConfirmationGate")  //보안실확인
                            {
                                if (IsSecurityManager(levelID, VisitLocation, UserLocation)) 
                                {
                                    Disabled = "";
                                }
                            }
                        }
                        break;
                    case 1:   //반입완료-매니저 또는 담당자(부서)에서 체크할수 있음
                    case 2:   //확인대기-매니저 또는 담당자(부서)에서 체크할수 있음
                        {
                            if (Obj == "QuarantineConfirmationContact")  //담당자확인
                            {
                                if (IsLocalManager(levelID, VisitLocation, UserLocation) || UserOrgCode == ContactOrgCode)
                                {
                                    Disabled = "";
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return Disabled;
        }
        #endregion
        #region IsSecurityObjRequested  방역관련 체크가 필수인지반환
        public static bool IsSecurityObjRequested(int CarryItemApplyStatus, int CarryItemStatus
                                                    , string UserOrgCode, string ContactOrgCode
                                                    , string Obj
                                                    , int levelID, string VisitLocation = "", string UserLocation = "")
        {
            bool bRet = false;
            //승인상태(CarryItemApplyStatus): 승인대기(0) / 승인완료(1) / 반려(2) / 결재중(3) / 결재완료(4)
            //반입상태(CarryItemStatus):반입대기(0), 반입완료(1), 확인대기(2)
            if (CarryItemApplyStatus == 1)
            {
                switch (CarryItemStatus)
                {
                    case 0:   //반입대기-경비근무자가 체크할수 있음
                        {
                            if (Obj == "QuarantineConfirmationGate")  //보안실확인
                            {
                                if (IsSecurityManager(levelID, VisitLocation, UserLocation))
                                {
                                    bRet = true;
                                }
                            }
                        }
                        break;
                    case 1:   //반입완료-매니저 또는 담당자(부서)에서 체크할수 있음
                    case 2:   //확인대기-매니저 또는 담당자(부서)에서 체크할수 있음
                        {
                            if (Obj == "QuarantineConfirmationContact")  //담당자확인
                            {
                                if (IsLocalManager(levelID, VisitLocation, UserLocation) || UserOrgCode == ContactOrgCode)
                                {
                                    bRet = true;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return bRet;
        }
        #endregion
        #region IsContact  담당자인지 확인한다. 담당자의 범위(관리자, 자신, 팀원)
        public static bool IsContact(int CarryItemApplyStatus, int CarryItemStatus
                                                    , string UserOrgCode, string ContactOrgCode
                                                    , string Obj
                                                    , int levelID, string VisitLocation = "", string UserLocation = "")
        {
            bool bRet = false;
            if (IsLocalManager(levelID, VisitLocation, UserLocation) || UserOrgCode == ContactOrgCode)
            {
                bRet = true;
            }
            return bRet;
        }
        #endregion
    }
    //2023.09.11 dsyoo
    public class OptionModel
    {
        public int Selected { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public int Disabled { get; set; }
    }
}
