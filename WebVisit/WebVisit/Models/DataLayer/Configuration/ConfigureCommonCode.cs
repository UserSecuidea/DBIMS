using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.ModelConfiguration.Configuration;

namespace WebVisit.Models
{
    internal class ConfigureCommonCode : IEntityTypeConfiguration<CommonCode>
    {
        public void Configure(EntityTypeBuilder<CommonCode> entity)
        {
            // entity.Property(b => b.CommonCodeID).ValueGeneratedOnAdd();
            // entity.Property(b => b.CommonCodeID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnName("CommonCodeID");

            entity.Property(b => b.IsActive).HasDefaultValue("Y");
            entity.Property(b => b.IsSys).HasDefaultValue("N");
            entity.Property(b => b.IsDelete).HasDefaultValue("N");
            entity.Property(b => b.InsertDate).HasDefaultValueSql("getdate()");

            // seed initial data
            entity.HasData( new { CommonCodeID = 1, GroupNo = 1, CodeNameKor = "사업장", CodeNameEng = "Location", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 2, GroupNo = 1, SubCodeID = 0, SubCodeDesc="1000", CodeNameKor = "서울", CodeNameEng = "Seoul", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1", IsDelete = "Y"});
            entity.HasData( new { CommonCodeID = 3, GroupNo = 1, SubCodeID = 1, SubCodeDesc="2000", CodeNameKor = "DB HiTek 부천캠퍼스", CodeNameEng = "DB HiTek Bucheon Cam.", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"});
            entity.HasData( new { CommonCodeID = 4, GroupNo = 1, SubCodeID = 2, SubCodeDesc="3000", CodeNameKor = "DB HiTek 상우캠퍼스", CodeNameEng = "DB HiTek Sangwoo Cam.", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"});
            entity.HasData( new { CommonCodeID = 5, GroupNo = 1, SubCodeID = 3, SubCodeDesc="6000", CodeNameKor = "DB GlobalChip", CodeNameEng = "DB GlobalChip", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"});

            entity.HasData( new { CommonCodeID = 6, GroupNo = 6, CodeNameKor = "업체구분", CodeNameEng = "Company Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 7, GroupNo = 6, SubCodeID = 0, CodeNameKor = "본사", CodeNameEng = "Head Office", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 8, GroupNo = 6, SubCodeID = 1, CodeNameKor = "상주협력사", CodeNameEng = "Resident Partner", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 9, GroupNo = 6, SubCodeID = 2, CodeNameKor = "비상주협력사", CodeNameEng = "NonResident Partner", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 10, GroupNo = 10, CodeNameKor = "공종코드", CodeNameEng = "WorkType Code", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 11, GroupNo = 10, SubCodeID = 0, CodeNameKor = "전기", CodeNameEng = "Electricity", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 12, GroupNo = 10, SubCodeID = 1, CodeNameKor = "토건", CodeNameEng = "Construction", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 13, GroupNo = 10, SubCodeID = 2, CodeNameKor = "기계", CodeNameEng = "Machine", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 14, GroupNo = 10, SubCodeID = 3, CodeNameKor = "정비", CodeNameEng = "Maintenance", OrderNo = 4, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 15, GroupNo = 10, SubCodeID = 4, CodeNameKor = "기타", CodeNameEng = "Etc", OrderNo = 5, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 16, GroupNo = 16, CodeNameKor = "체류자격코드", CodeNameEng = "ImmStatus Code", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 17, GroupNo = 16, SubCodeID = 0, CodeNameKor = "F-2(거주)", CodeNameEng = "F-2", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 18, GroupNo = 16, SubCodeID = 1, CodeNameKor = "F-4(재외동포)", CodeNameEng = "F-4", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 19, GroupNo = 16, SubCodeID = 2, CodeNameKor = "F-5(영주)", CodeNameEng = "F-5", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 20, GroupNo = 16, SubCodeID = 3, CodeNameKor = "F-6(결혼이민)", CodeNameEng = "F-6", OrderNo = 4, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 21, GroupNo = 16, SubCodeID = 4, CodeNameKor = "H-2(방문취업)", CodeNameEng = "H-2", OrderNo = 5, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 22, GroupNo = 16, SubCodeID = 5, CodeNameKor = "E-9(비전문취업)", CodeNameEng = "E-9", OrderNo = 6, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 23, GroupNo = 16, SubCodeID = 6, CodeNameKor = "E-9(비전문취업)", CodeNameEng = "E-9", OrderNo = 7, IsActive = "N", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 24, GroupNo = 16, SubCodeID = 7, CodeNameKor = "C-4(단기취업)", CodeNameEng = "C-4", OrderNo = 8, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 25, GroupNo = 16, SubCodeID = 8, CodeNameKor = "D-7(주재)", CodeNameEng = "D-7", OrderNo = 9, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 26, GroupNo = 26, CodeNameKor = "차량구분코드", CodeNameEng = "Car Type", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 27, GroupNo = 26, SubCodeID = 0, CodeNameKor = "개인용", CodeNameEng = "Passenger Car", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 28, GroupNo = 26, SubCodeID = 1, CodeNameKor = "업무용", CodeNameEng = "Business Car", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 29, GroupNo = 26, SubCodeID = 2, CodeNameKor = "화물(1톤이하)", CodeNameEng = "", OrderNo = 3, IsActive = "N", IsSys = "N", InsertSabun="1", IsDelete = "Y"} );
            entity.HasData( new { CommonCodeID = 30, GroupNo = 26, SubCodeID = 3, CodeNameKor = "화물(1톤초과)", CodeNameEng = "", OrderNo = 4, IsActive = "N", IsSys = "N", InsertSabun="1", IsDelete = "Y"} );
            entity.HasData( new { CommonCodeID = 31, GroupNo = 26, SubCodeID = 4, CodeNameKor = "중장비(특수용도)", CodeNameEng = "", OrderNo = 5, IsActive = "N", IsSys = "N", InsertSabun="1", IsDelete = "Y"} );
            entity.HasData( new { CommonCodeID = 32, GroupNo = 26, SubCodeID = 5, CodeNameKor = "기타", CodeNameEng = "", OrderNo = 6, IsActive = "N", IsSys = "N", InsertSabun="1", IsDelete = "Y"} );

            // 방문목적코드 수정 2023.07.20 
            entity.HasData( new { CommonCodeID = 33, GroupNo = 33, CodeNameKor = "방문목적코드", CodeNameEng = "Visit Purpose", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 34, GroupNo = 33, SubCodeID = 0, CodeNameKor = "업무협의", CodeNameEng = "Meeting", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 35, GroupNo = 33, SubCodeID = 1, CodeNameKor = "공사/수리/Setup", CodeNameEng = "Construction,/Repair/Setup", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 36, GroupNo = 33, SubCodeID = 2, CodeNameKor = "납품", CodeNameEng = "Delivery", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 37, GroupNo = 33, SubCodeID = 3, CodeNameKor = "교육", CodeNameEng = "Education", OrderNo = 4, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 38, GroupNo = 33, SubCodeID = 4, CodeNameKor = "공무 및 기타", CodeNameEng = "Public Affairs Others", OrderNo = 5, IsActive = "N", IsSys = "N", InsertSabun="1"} );

            // 반입목적코드 수정 2023.07.20
            entity.HasData( new { CommonCodeID = 39, GroupNo = 39, CodeNameKor = "반입목적코드", CodeNameEng = "Import Purpose", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 40, GroupNo = 39, SubCodeID = 0, CodeNameKor = "Setup", CodeNameEng = "Setup", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 41, GroupNo = 39, SubCodeID = 1, CodeNameKor = "공사/수리", CodeNameEng = "Construction/Repair", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 42, GroupNo = 39, SubCodeID = 2, CodeNameKor = "장비교체", CodeNameEng = "Equipment Change", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 43, GroupNo = 39, SubCodeID = 3, CodeNameKor = "납품", CodeNameEng = "", OrderNo = 4, IsActive = "N", IsSys = "N", InsertSabun="1", IsDelete = "Y"} );
            // 반입목적코드에 선입고 항목추가 2023.07.06 PreOrder
            entity.HasData( new { CommonCodeID = 44, GroupNo = 39, SubCodeID = 4, CodeNameKor = "선입고", CodeNameEng = "PreOrder", OrderNo = 5, IsActive = "Y", IsSys = "N", InsertSabun="1"} );


            entity.HasData( new { CommonCodeID = 45, GroupNo = 45, CodeNameKor = "반출입목적", CodeNameEng = "Export Import Purpose Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 46, GroupNo = 45, SubCodeID = 0, SubCodeDesc="2001", CodeNameKor = "수리", CodeNameEng = "Repair", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 47, GroupNo = 45, SubCodeID = 1, SubCodeDesc="2002", CodeNameKor = "리클레임", CodeNameEng = "Reclaim", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 48, GroupNo = 45, SubCodeID = 2, SubCodeDesc="2003", CodeNameKor = "판매", CodeNameEng = "Sale", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 49, GroupNo = 45, SubCodeID = 3, SubCodeDesc="2004", CodeNameKor = "세정(재생)", CodeNameEng = "Cleaning", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 50, GroupNo = 45, SubCodeID = 4, SubCodeDesc="2005", CodeNameKor = "교환", CodeNameEng = "Exchange", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 51, GroupNo = 45, SubCodeID = 5, SubCodeDesc="2006", CodeNameKor = "이체(이동)", CodeNameEng = "Transfer", OrderNo = 6, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 52, GroupNo = 45, SubCodeID = 6, SubCodeDesc="2007", CodeNameKor = "기타", CodeNameEng = "", OrderNo = 7, IsActive = "N", IsSys = "Y", InsertSabun="1", IsDelete = "Y"} );
            entity.HasData( new { CommonCodeID = 53, GroupNo = 45, SubCodeID = 7, SubCodeDesc="2008", CodeNameKor = "분석", CodeNameEng = "Analysis", OrderNo = 8, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 54, GroupNo = 45, SubCodeID = 8, SubCodeDesc="2009", CodeNameKor = "Demo", CodeNameEng = "Demo", OrderNo = 9, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 55, GroupNo = 45, SubCodeID = 9, SubCodeDesc="2010", CodeNameKor = "우수협력업체", CodeNameEng = "", OrderNo = 10, IsActive = "N", IsSys = "Y", InsertSabun="1", IsDelete = "Y"} );
            entity.HasData( new { CommonCodeID = 56, GroupNo = 45, SubCodeID = 10, SubCodeDesc="2011", CodeNameKor = "Warranty", CodeNameEng = "Warranty", OrderNo = 11, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 57, GroupNo = 45, SubCodeID = 11, SubCodeDesc="2012", CodeNameKor = "반환", CodeNameEng = "Return", OrderNo = 12, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 58, GroupNo = 45, SubCodeID = 12, SubCodeDesc="", CodeNameKor = "태블릿", CodeNameEng = "Tablets", OrderNo = 13, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 60, GroupNo = 60, CodeNameKor = "반출입구분", CodeNameEng = "Export Import Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 61, GroupNo = 60, SubCodeID = 0, CodeNameKor = "자산", CodeNameEng = "Property", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 62, GroupNo = 60, SubCodeID = 1, CodeNameKor = "수리", CodeNameEng = "Repair", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 63, GroupNo = 60, SubCodeID = 2, CodeNameKor = "노트북", CodeNameEng = "Notebook", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 64, GroupNo = 64, CodeNameKor = "반출구분", CodeNameEng = "Export Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 65, GroupNo = 64, SubCodeID = 0, SubCodeDesc="O", CodeNameKor = "외부업체", CodeNameEng = "External Company", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 66, GroupNo = 64, SubCodeID = 1, SubCodeDesc="I", CodeNameKor = "공장간이동", CodeNameEng = "Inter Factory Movement", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 67, GroupNo = 64, SubCodeID = 2, SubCodeDesc="OI", CodeNameKor = "외부업체경유공장간이동", CodeNameEng = "External Company Passage", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 68, GroupNo = 64, SubCodeID = 3, SubCodeDesc="N", CodeNameKor = "개인노트북", CodeNameEng = "Personal Notebook", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} ); 
            entity.HasData( new { CommonCodeID = 69, GroupNo = 64, SubCodeID = 4, SubCodeDesc="", CodeNameKor = "대여노트북", CodeNameEng = "Rental Notebook", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 70, GroupNo = 70, CodeNameKor = "반입구분", CodeNameEng = "Import Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 71, GroupNo = 70, SubCodeID = 0, SubCodeDesc="12001", CodeNameKor = "반입요", CodeNameEng = "Bring", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 72, GroupNo = 70, SubCodeID = 1, SubCodeDesc="12002", CodeNameKor = "반입불요", CodeNameEng = "Do Not Bring", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 73, GroupNo = 70, SubCodeID = 2, SubCodeDesc="12003", CodeNameKor = "매각", OrderNo = 3, IsActive = "N", IsSys = "Y", InsertSabun="1", IsDelete = "Y"} );
            entity.HasData( new { CommonCodeID = 74, GroupNo = 70, SubCodeID = 3, SubCodeDesc="12004", CodeNameKor = "반입불요(무상)", CodeNameEng = "", OrderNo = 4, IsActive = "N", IsSys = "Y", InsertSabun="1", IsDelete = "Y"} );
            entity.HasData( new { CommonCodeID = 75, GroupNo = 70, SubCodeID = 4, SubCodeDesc="12006", CodeNameKor = "대체품반입요", CodeNameEng = "Replacement Item Return", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 76, GroupNo = 70, SubCodeID = 5, SubCodeDesc="12007", CodeNameKor = "대체품선입고", CodeNameEng = "Replacement Item Prereceiving", OrderNo = 6, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 77, GroupNo = 77, CodeNameKor = "반입자구분", CodeNameEng = "Import Way Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 78, GroupNo = 77, SubCodeID = 0, CodeNameKor = "대행등록", CodeNameEng = "Agency Registration", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 79, GroupNo = 77, SubCodeID = 1, CodeNameKor = "본인", CodeNameEng = "Self", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 80, GroupNo = 80, CodeNameKor = "반출물전달방법", CodeNameEng = "Delivery Method Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 81, GroupNo = 80, SubCodeID = 0, SubCodeDesc="8001", CodeNameKor = "방문수령", CodeNameEng = "Receipt Visit", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 82, GroupNo = 80, SubCodeID = 1, SubCodeDesc="8002", CodeNameKor = "우편/택배", CodeNameEng = "Mail/Delivery", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 83, GroupNo = 80, SubCodeID = 2, SubCodeDesc="8003", CodeNameKor = "대리인수령", CodeNameEng = "Receipt Agent", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 84, GroupNo = 80, SubCodeID = 3, SubCodeDesc="8004", CodeNameKor = "핸드캐리", CodeNameEng = "Hand Carry", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 85, GroupNo = 80, SubCodeID = 4, SubCodeDesc="8005", CodeNameKor = "물류차량", CodeNameEng = "Logistics Vehicle", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );


            entity.HasData( new { CommonCodeID = 86, GroupNo = 86, CodeNameKor = "장소코드", CodeNameEng = "Place", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 87, GroupNo = 86, SubCodeID = 0, CodeNameKor = "부천캠퍼스 본관", CodeNameEng = "Bucheon Cam. Fab.", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 88, GroupNo = 86, SubCodeID = 1, CodeNameKor = "부천캠퍼스 쇼콜리동", CodeNameEng = "Bucheon Campus Shocoli", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 89, GroupNo = 86, SubCodeID = 2, CodeNameKor = "부천캠퍼스 어린이집", CodeNameEng = "Bucheon Cam. Kindergarden", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 90, GroupNo = 86, SubCodeID = 3, CodeNameKor = "상우캠퍼스 본관", CodeNameEng = "Sangwoo Cam. Fab.", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 91, GroupNo = 86, SubCodeID = 4, CodeNameKor = "상우캠퍼스 어드민동", CodeNameEng = "Sangwoo Admin", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 92, GroupNo = 86, SubCodeID = 5, CodeNameKor = "DBGlobalChip", CodeNameEng = "DBGlobalChip", OrderNo = 6, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 93, GroupNo = 86, SubCodeID = 6, CodeNameKor = "클린룸", CodeNameEng = "Clean Room", OrderNo = 7, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 94, GroupNo = 86, SubCodeID = 7, CodeNameKor = "↓↓사고대비물질 취급시설↓↓", CodeNameEng = "↓↓Accident Preparedness Material Handling Facility↓↓", OrderNo = 8, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 95, GroupNo = 86, SubCodeID = 8, CodeNameKor = "공통-제한구역-황색-가스케미칼보관창고", CodeNameEng = "Chemical Storage Warehouse", OrderNo = 9, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 96, GroupNo = 86, SubCodeID = 9, CodeNameKor = "공통-제한구역-황색-화학분석실", CodeNameEng = "Chemical Analysis Room", OrderNo = 10, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 97, GroupNo = 86, SubCodeID = 10, CodeNameKor = "공통-제한구역-황색-불량분석실", CodeNameEng = "Defect Analysis Office", OrderNo = 11, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 98, GroupNo = 86, SubCodeID = 11, CodeNameKor = "공통-제한구역-황색-CCSS", CodeNameEng = "CCSS", OrderNo = 12, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 99, GroupNo = 86, SubCodeID = 12, CodeNameKor = "부천-제한구역-황색-MBR", CodeNameEng = "MBR", OrderNo = 13, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 100, GroupNo = 86, SubCodeID = 13, CodeNameKor = "상우-제한구역-황색-WWT", CodeNameEng = "WWT", OrderNo = 14, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 101, GroupNo = 86, SubCodeID = 14, CodeNameKor = "부천-제한구역-황색-황산탱크(옥상)", CodeNameEng = "Sulfuric Acid Tank", OrderNo = 15, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            
            // 2023.07.20 휴대물품구분코드 수정 
            entity.HasData( new { CommonCodeID = 102, GroupNo = 102, CodeNameKor = "휴대물품구분코드", CodeNameEng = "Carry Item", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 103, GroupNo = 102, SubCodeID = 0, CodeNameKor = "노트북 및 PC ", CodeNameEng = "Laptops and PC", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 104, GroupNo = 102, SubCodeID = 1, CodeNameKor = "작업공도구", CodeNameEng = "Work Tools", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 105, GroupNo = 102, SubCodeID = 2, CodeNameKor = "카메라", CodeNameEng = "Camera", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1", IsDelete = "Y"} );
            entity.HasData( new { CommonCodeID = 106, GroupNo =	102, SubCodeID = 3, CodeNameKor = "기타", CodeNameEng = "Etc", OrderNo = 4, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 107, GroupNo =	107, CodeNameKor = "출입증발급구분", CodeNameEng = "Card Issue Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 108, GroupNo =	107, SubCodeID = 0, CodeNameKor = "신규", CodeNameEng = "New", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 109, GroupNo =	107, SubCodeID = 1, CodeNameKor = "재발급", CodeNameEng = "Reissuance", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 110, GroupNo =	110, CodeNameKor = "방문신청구분", CodeNameEng = "Visit Apply Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 111, GroupNo =	110, SubCodeID = 0, CodeNameKor = "방문신청", CodeNameEng = "Application Visit", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 112, GroupNo =	110, SubCodeID = 1, CodeNameKor = "공사출입인원신청", CodeNameEng = "Application Work Visit", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 113, GroupNo =	110, SubCodeID = 2, CodeNameKor = "안전교육", CodeNameEng = "Safety Education", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 114, GroupNo =	110, SubCodeID = 3, CodeNameKor = "방문객수기등록", CodeNameEng = "Visitor Manual ", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 115, GroupNo =	110, SubCodeID = 4, CodeNameKor = "택배", CodeNameEng = "Delivery", OrderNo = 5, IsActive = "N", IsSys = "Y", InsertSabun="1", IsDelete = "Y"} );

            entity.HasData( new { CommonCodeID = 116, GroupNo =	116, CodeNameKor = "VIP구분코드", CodeNameEng = "Vip Type", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 117, GroupNo =	116, SubCodeID = 0, CodeNameKor = "중요바이어", CodeNameEng = "Important Buyer", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 118, GroupNo =	118, CodeNameKor = "SMS발송구분", CodeNameEng = "SMS Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 119, GroupNo =	118, SubCodeID = 0, CodeNameKor = "방문객 개인정보동의 및 교육 안내", CodeNameEng = "", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 120, GroupNo =	120, CodeNameKor = "업체상태", CodeNameEng = "Company Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 121, GroupNo =	120, SubCodeID = 0, CodeNameKor = "승인대기", CodeNameEng = "Waiting Approval", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 122, GroupNo =	120, SubCodeID = 1, CodeNameKor = "승인완료", CodeNameEng = "Approved", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 123, GroupNo =	120, SubCodeID = 2, CodeNameKor = "반려", CodeNameEng = "Companion", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 124, GroupNo =	124, CodeNameKor = "출입증발급상태", CodeNameEng = "Card Issue Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 125, GroupNo =	124, SubCodeID = 0, CodeNameKor = "발급", CodeNameEng = "Issuance", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 126, GroupNo =	124, SubCodeID = 1, CodeNameKor = "미발급", CodeNameEng = "Not issued", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 127, GroupNo =	127, CodeNameKor = "출입증신청상태", CodeNameEng = "CardApplyStatus", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 128, GroupNo =	127, SubCodeID = 0, CodeNameKor = "승인대기", CodeNameEng = "Waiting Approval", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 129, GroupNo =	127, SubCodeID = 1, CodeNameKor = "승인완료", CodeNameEng = "Approved", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 130, GroupNo =	127, SubCodeID = 2, CodeNameKor = "반려", CodeNameEng = "Companion", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 131, GroupNo =	131, CodeNameKor = "임시증발급상태", CodeNameEng = "Temp Card Issue Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 132, GroupNo =	131, SubCodeID = 0, CodeNameKor = "발급", CodeNameEng = "Issuance", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 133, GroupNo =	131, SubCodeID = 1, CodeNameKor = "회수", CodeNameEng = "Collection", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 134, GroupNo =	131, SubCodeID = 2, CodeNameKor = "반려", CodeNameEng = "Companion", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1", IsDelete="Y"} );
            entity.HasData( new { CommonCodeID = 135, GroupNo =	131, SubCodeID = 3, CodeNameKor = "승인대기", CodeNameEng = "Waiting Approval", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1", IsDelete="Y"} );

            entity.HasData( new { CommonCodeID = 136, GroupNo =	136, CodeNameKor = "안전수칙위반상태", CodeNameEng = "Safety Violation Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 137, GroupNo =	136, SubCodeID = 0, CodeNameKor = "신규발행", CodeNameEng = "New Issue", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 138, GroupNo =	136, SubCodeID = 1, CodeNameKor = "방지대책등록", CodeNameEng = "Prevention Measures Registration", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 139, GroupNo =	136, SubCodeID = 2, CodeNameKor = "방지대책승인", CodeNameEng = "Prevention Measures Approved", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 140, GroupNo =	136, SubCodeID = 3, CodeNameKor = "방지대책재등록요청", CodeNameEng = "Request Reregistration Preventive Measures", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 141, GroupNo =	141, CodeNameKor = "블랙리스트상태", CodeNameEng = "BlackList Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 142, GroupNo =	141, SubCodeID = 0, CodeNameKor = "등록요청", CodeNameEng = "Registration Request", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 143, GroupNo =	141, SubCodeID = 1, CodeNameKor = "등록", CodeNameEng = "Registration", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 144, GroupNo =	144, CodeNameKor = "방문신청상태", CodeNameEng = "Visit Apply Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 145, GroupNo =	144, SubCodeID = 0, CodeNameKor = "승인대기", CodeNameEng = "Waiting Approval", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 146, GroupNo =	144, SubCodeID = 1, CodeNameKor = "승인완료", CodeNameEng = "Approved", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 147, GroupNo =	144, SubCodeID = 2, CodeNameKor = "반려", CodeNameEng = "Companion", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            // 2023.08.27 삭제된 미반납을 미승인으로 추가 
            entity.HasData( new { CommonCodeID = 148, GroupNo =	144, SubCodeID = 3, CodeNameKor = "미승인", CodeNameEng = "Unapproved", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 149, GroupNo =	144, SubCodeID = 4, CodeNameKor = "방문종료", CodeNameEng = "End Visit", OrderNo = 5, IsActive = "N", IsSys = "Y", InsertSabun="1", IsDelete="Y"} );

            entity.HasData( new { CommonCodeID = 150, GroupNo =	150, CodeNameKor = "방문상태", CodeNameEng = "Visit Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 151, GroupNo =	150, SubCodeID = 0, CodeNameKor = "방문대기", CodeNameEng = "Waiting Visit", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 152, GroupNo =	150, SubCodeID = 1, CodeNameKor = "방문", CodeNameEng = "Visit", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 153, GroupNo =	150, SubCodeID = 2, CodeNameKor = "방문완료", CodeNameEng = "Visit Completed", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 154, GroupNo =	150, SubCodeID = 3, CodeNameKor = "미반납", CodeNameEng = "Not Returned", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 155, GroupNo =	150, SubCodeID = 4, CodeNameKor = "방문종료", CodeNameEng = "End Visit", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 156, GroupNo =	156, CodeNameKor = "교육신청상태", CodeNameEng = "Education Apply Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 157, GroupNo =	156, SubCodeID = 0, CodeNameKor = "승인대기", CodeNameEng = "Waiting Approval", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 158, GroupNo =	156, SubCodeID = 1, CodeNameKor = "승인완료", CodeNameEng = "Approved", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 159, GroupNo =	159, CodeNameKor = "교육이수상태", CodeNameEng = "Education Complete Status", OrderNo = 0	, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 160, GroupNo =	159, SubCodeID = 0, CodeNameKor = "미이수", CodeNameEng = "Not Complete", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 161, GroupNo =	159, SubCodeID = 1, CodeNameKor = "이수", CodeNameEng = "Complete", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            // 2023.08.27 반출입상태 변경 및 추가 
            // 2023.09.13 반출입상태에서 반출입신청상태 코드 분리 (SubCodeID 0~4 사용안함.) 
            entity.HasData( new { CommonCodeID = 162, GroupNo =	162, CodeNameKor = "반출입상태", CodeNameEng = "Export Import Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 163, GroupNo =	162, SubCodeID = 0, CodeNameKor = "신청", CodeNameEng = "Application", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1", IsDelete="Y"} );
            entity.HasData( new { CommonCodeID = 164, GroupNo =	162, SubCodeID = 1, CodeNameKor = "반출상신", CodeNameEng = "Export Approval Request", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1", IsDelete="Y"} );
            entity.HasData( new { CommonCodeID = 165, GroupNo =	162, SubCodeID = 2, CodeNameKor = "결재완료", CodeNameEng = "Approval Complete", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1", IsDelete="Y"} );
            entity.HasData( new { CommonCodeID = 166, GroupNo =	162, SubCodeID = 3, CodeNameKor = "반려", CodeNameEng = "Companion", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1", IsDelete="Y"} );
            entity.HasData( new { CommonCodeID = 167, GroupNo =	162, SubCodeID = 4, CodeNameKor = "반출대기", CodeNameEng = "Waiting Export", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            // 2023.08.27 휴대물품반입반출상태 변경 및 추가  
            entity.HasData( new { CommonCodeID = 168, GroupNo =	168, CodeNameKor = "휴대물품반입반출상태", CodeNameEng = "Carry Item Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 169, GroupNo =	168, SubCodeID = 0, SubCodeDesc="13001", CodeNameKor = "반입대기", CodeNameEng = "Waiting Bring", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 170, GroupNo =	168, SubCodeID = 1, SubCodeDesc="", CodeNameKor = "반입완료", CodeNameEng = "Carry In Completed", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 171, GroupNo =	168, SubCodeID = 2, SubCodeDesc="13003", CodeNameKor = "확인대기", CodeNameEng = "Waiting Confirmation", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 172, GroupNo =	168, SubCodeID = 3, SubCodeDesc="", CodeNameKor = "반출요", CodeNameEng = "TakeOut", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 173, GroupNo =	168, SubCodeID = 4, SubCodeDesc="", CodeNameKor = "반출불필요(유상)", CodeNameEng = "Do Not TakeOut", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 174, GroupNo =	168, SubCodeID = 5, SubCodeDesc="", CodeNameKor = "반출불필요(무상)", CodeNameEng = "Do Not TakeOut(No)", OrderNo = 6, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 175, GroupNo =	168, SubCodeID = 6, SubCodeDesc="", CodeNameKor = "선입고", CodeNameEng = "PreOrder", OrderNo = 7, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 176, GroupNo =	168, SubCodeID = 7, SubCodeDesc="13002", CodeNameKor = "반출대기", CodeNameEng = "Waiting Take Out", OrderNo = 8, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            // 2023.09.19 휴대물품신청상태 수정
            entity.HasData( new { CommonCodeID = 177, GroupNo =	177, CodeNameKor = "휴대물품신청상태", CodeNameEng = "Carry Item Apply Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 178, GroupNo =	177, SubCodeID = 0, CodeNameKor = "신청", CodeNameEng = "Application", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 179, GroupNo =	177, SubCodeID = 1, CodeNameKor = "결재완료 ", CodeNameEng = "Payment Completed", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 180, GroupNo =	177, SubCodeID = 2, CodeNameKor = "반려", CodeNameEng = "Companion", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 181, GroupNo =	177, SubCodeID = 3, CodeNameKor = "결재중", CodeNameEng = "Under Approval", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 182, GroupNo =	177, SubCodeID = 4, CodeNameKor = "승인완료", CodeNameEng = "Approved", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 183, GroupNo =	183, CodeNameKor = "식수구분", CodeNameEng = "Meal Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 184, GroupNo =	183, SubCodeID = 0, CodeNameKor = "이상식수", CodeNameEng = "Unusual Meal", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 185, GroupNo =	183, SubCodeID = 1, CodeNameKor = "조식", CodeNameEng = "Breakfast", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 186, GroupNo =	183, SubCodeID = 2, CodeNameKor = "중식", CodeNameEng = "Lunch", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 187, GroupNo =	183, SubCodeID = 3, CodeNameKor = "석식", CodeNameEng = "Dinner", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 188, GroupNo =	183, SubCodeID = 4, CodeNameKor = "야식", CodeNameEng = "LatenightMeal", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 189, GroupNo =	189, CodeNameKor = "계절구분", CodeNameEng = "Term Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 190, GroupNo =	189, SubCodeID = 0, CodeNameKor = "하계", CodeNameEng = "Summer Season Term", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 191, GroupNo =	189, SubCodeID = 1, CodeNameKor = "동계", CodeNameEng = "Winter Season Term", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 192, GroupNo =	192, CodeNameKor = "수기등록구분", CodeNameEng = "Meal Menual Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 193, GroupNo =	192, SubCodeID = 0, CodeNameKor = "식수리더", CodeNameEng = "Reader", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 194, GroupNo =	192, SubCodeID = 1, CodeNameKor = "수기등록", CodeNameEng = "Menual", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 195, GroupNo =	195, CodeNameKor = "외국인구분", CodeNameEng = "Foreigner Type", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 196, GroupNo =	195, SubCodeID = 0, CodeNameKor = "내국인", CodeNameEng = "Domestic", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 197, GroupNo =	195, SubCodeID = 1, CodeNameKor = "외국인", CodeNameEng = "Foreigner", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 198, GroupNo =	198, CodeNameKor = "성별구분", CodeNameEng = "Gender Type", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 199, GroupNo =	198, SubCodeID = 0, CodeNameKor = "남", CodeNameEng = "Man", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 200, GroupNo =	198, SubCodeID = 1, CodeNameKor = "여", CodeNameEng = "Woman", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 201, GroupNo =	201, CodeNameKor = "방문자구분", CodeNameEng = "Visitor Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 202, GroupNo =	201, SubCodeID = 0, CodeNameKor = "임직원", CodeNameEng = "Employees", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 203, GroupNo =	201, SubCodeID = 1, CodeNameKor = "방문자", CodeNameEng = "Visitor", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 204, GroupNo =	204, CodeNameKor = "재직상태", CodeNameEng = "Person Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 205, GroupNo =	204, SubCodeID = 0, CodeNameKor = "재직", CodeNameEng = "Hold Office", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 206, GroupNo =	204, SubCodeID = 1, CodeNameKor = "휴직", CodeNameEng = "Take time Off", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 207, GroupNo =	204, SubCodeID = 2, CodeNameKor = "퇴직", CodeNameEng = "Rretirement", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 208, GroupNo =	208, CodeNameKor = "결재유형", CodeNameEng = "Approval Type", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 209, GroupNo =	208, SubCodeID = 0, CodeNameKor = "사원추가", CodeNameEng = "Add Person", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 210, GroupNo =	208, SubCodeID = 1, CodeNameKor = "카드추가", CodeNameEng = "Add Card", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 211, GroupNo =	208, SubCodeID = 2, CodeNameKor = "반출입신청", CodeNameEng = "Apply Export Import ", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 212, GroupNo =	208, SubCodeID = 3, CodeNameKor = "공사신청", CodeNameEng = "Apply Work", OrderNo = 4, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 213, GroupNo =	208, SubCodeID = 4, CodeNameKor = "안전교육신청", CodeNameEng = "Apply Safety Education", OrderNo = 5, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 214, GroupNo =	214, CodeNameKor = "결재자유형", CodeNameEng = "Approval Sys Type", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 215, GroupNo =	214, SubCodeID = 0, CodeNameKor = "통문관리시스템", CodeNameEng = "Management System", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 216, GroupNo =	214, SubCodeID = 1, CodeNameKor = "ERP", CodeNameEng = "ERP", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 217, GroupNo =	214, SubCodeID = 2, CodeNameKor = "사용자설정", CodeNameEng = "User Settings", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 218, GroupNo =	218, CodeNameKor = "회원구분", CodeNameEng = "Person Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 219, GroupNo =	218, SubCodeID = 0, CodeNameKor = "임직원", CodeNameEng = "Executives", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 220, GroupNo =	218, SubCodeID = 1, CodeNameKor = "상주직원", CodeNameEng = "Resident Employee", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 221, GroupNo =	218, SubCodeID = 2, CodeNameKor = "비상주관리자", CodeNameEng = "NonResident Manager", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 222, GroupNo =	218, SubCodeID = 3, CodeNameKor = "비상주직원", CodeNameEng = "NonResident Employee", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 223, GroupNo =	218, SubCodeID = 4, CodeNameKor = "방문객", CodeNameEng = "Visitor", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            
            // 반입목적코드에 기타 항목추가 2023.08.07 
            entity.HasData( new { CommonCodeID = 224, GroupNo =	39, SubCodeID = 5, CodeNameKor = "기타", CodeNameEng = "Etc", OrderNo = 6, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 225, GroupNo =	225, CodeNameKor = "공사신청상태", CodeNameEng = "WorkApply Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 226, GroupNo =	225, SubCodeID = 0, CodeNameKor = "승인대기", CodeNameEng = "Waiting Approval", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 227, GroupNo =	225, SubCodeID = 1, CodeNameKor = "승인완료", CodeNameEng = "Approved", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 228, GroupNo =	225, SubCodeID = 2, CodeNameKor = "반려", CodeNameEng = "Companion", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 229, GroupNo =	229, CodeNameKor = "공사출입신청상태", CodeNameEng = "WorkVisitApply Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 230, GroupNo =	229, SubCodeID = 0, CodeNameKor = "승인대기", CodeNameEng = "Waiting Approval", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 231, GroupNo =	229, SubCodeID = 1, CodeNameKor = "승인완료", CodeNameEng = "Approved", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 232, GroupNo =	229, SubCodeID = 2, CodeNameKor = "반려", CodeNameEng = "Companion", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 233, GroupNo =	233, CodeNameKor = "방문수기등록방문목적", CodeNameEng = "Visit Manual Purpose", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 234, GroupNo =	233, SubCodeID = 0, CodeNameKor = "택배", CodeNameEng = "Delivery", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 235, GroupNo =	235, CodeNameKor = "등록사유구분", CodeNameEng = "BlackList Type", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 236, GroupNo =	235, SubCodeID = 0, CodeNameKor = "퇴직임직원", CodeNameEng = "Retired Employee", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 237, GroupNo =	235, SubCodeID = 1, CodeNameKor = "안전수칙위반", CodeNameEng = "Violation Safety Rules", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 238, GroupNo =	235, SubCodeID = 2, CodeNameKor = "보안수칙위반", CodeNameEng = "Violation Security Rules", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 239, GroupNo =	235, SubCodeID = 3, CodeNameKor = "기타", CodeNameEng = "Etc", OrderNo = 4, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            // 블랙리스트상태 해제 항목추가 2023.07.13 
            entity.HasData( new { CommonCodeID = 240, GroupNo =	141, SubCodeID = 2, CodeNameKor = "해제", CodeNameEng = "Release", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            
            entity.HasData( new { CommonCodeID = 241, GroupNo =	241, CodeNameKor = "휴대물품반입구분", CodeNameEng = "Carry Item Import Type", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 242, GroupNo =	241, SubCodeID = 0, CodeNameKor = "반입대기", CodeNameEng = "Waiting Bring", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 243, GroupNo =	241, SubCodeID = 1, CodeNameKor = "반입처리", CodeNameEng = "Carryin Processing", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 244, GroupNo =	241, SubCodeID = 2, CodeNameKor = "반출요", CodeNameEng = "Take Out", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 245, GroupNo =	241, SubCodeID = 3, CodeNameKor = "반출불가", CodeNameEng = "Not Allowed Take Out", OrderNo = 4, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 246, GroupNo =	241, SubCodeID = 4, CodeNameKor = "반출불필요(유상)", CodeNameEng = "No Need Take Out", OrderNo = 5, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 247, GroupNo =	241, SubCodeID = 5, CodeNameKor = "반출불필요(무상)", CodeNameEng = "No Need Take Out(free)", OrderNo = 6, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 248, GroupNo =	241, SubCodeID = 6, CodeNameKor = "입고확인 ", CodeNameEng = "Receipt Confirmation", OrderNo = 7, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            entity.HasData( new { CommonCodeID = 249, GroupNo =	249, CodeNameKor = "출입증구분", CodeNameEng = "Card Type", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 250, GroupNo =	249, SubCodeID = 0, CodeNameKor = "일반", CodeNameEng = "General Pass", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 251, GroupNo =	249, SubCodeID = 1, CodeNameKor = "방문증", CodeNameEng = "Visitor Pass", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            
            // 출입증발급상태 분실, 휴면 항목추가 2023.07.16 
            entity.HasData( new { CommonCodeID = 252, GroupNo =	124, SubCodeID = 2, CodeNameKor = "분실", CodeNameEng = "Lost", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 253, GroupNo =	124, SubCodeID = 3, CodeNameKor = "휴면", CodeNameEng = "Dormancy", OrderNo = 4, IsActive = "Y", IsSys = "N", InsertSabun="1", IsDelete="Y"} );
            
            // 단위 코드화 추가 2023.08.04
            entity.HasData( new { CommonCodeID = 254, GroupNo =	254, CodeNameKor = "단위", CodeNameEng = "Unit", OrderNo = 0, IsActive = "Y", IsSys= "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 255, GroupNo =	254, SubCodeID = 0, CodeNameKor = "개", CodeNameEng = "Count", OrderNo = 1, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 256, GroupNo =	254, SubCodeID = 1, CodeNameKor = "대", CodeNameEng = "Mark", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 257, GroupNo =	254, SubCodeID = 2, CodeNameKor = "EA", CodeNameEng = "EA", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 258, GroupNo =	254, SubCodeID = 3, CodeNameKor = "SIK", CodeNameEng = "SIK", OrderNo = 4, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            // 반출입상태: 반입확인, 반출확인 항목 추가 => 2023.08.27 반출입상태 수정 및 추가             
            entity.HasData( new { CommonCodeID = 259, GroupNo =	162, SubCodeID = 5, CodeNameKor = "반출확인", CodeNameEng = "Export Confirmation", OrderNo = 6, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 260, GroupNo =	162, SubCodeID = 6, CodeNameKor = "정문반송", CodeNameEng = "Return Front Door", OrderNo = 7, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            // 2023.08.11 장소코드 추가 
            entity.HasData( new { CommonCodeID = 261, GroupNo = 86, SubCodeID = 15, CodeNameKor = "상우-제한구역-황색-유기회수실", CodeNameEng = "Organic Recovery Room", OrderNo = 16, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            
            // DB하이텍DEV 수정 시 주석 처리 
            // 2023.08.17 VIP 추가 
            entity.HasData( new { CommonCodeID = 262, GroupNo =	116, SubCodeID = 1, CodeNameKor = "주요고객사", CodeNameEng = "Important Buyer", OrderNo = 2, IsActive = "Y", IsSys = "N", InsertSabun="1", IsDelete="Y"} );
            entity.HasData( new { CommonCodeID = 263, GroupNo =	116, SubCodeID = 2, CodeNameKor = "임원", CodeNameEng = "Executive", OrderNo = 3, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 264, GroupNo =	116, SubCodeID = 3, CodeNameKor = "관공서", CodeNameEng = "Government office", OrderNo = 4, IsActive = "Y", IsSys = "N", InsertSabun="1", IsDelete="Y"} );
            entity.HasData( new { CommonCodeID = 265, GroupNo =	116, SubCodeID = 4, CodeNameKor = "주요고객사", CodeNameEng = "Important Buyer", OrderNo = 5, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 266, GroupNo =	116, SubCodeID = 5, CodeNameKor = "관공서", CodeNameEng = "Government office", OrderNo = 6, IsActive = "Y", IsSys = "N", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 267, GroupNo =	116, SubCodeID = 6, CodeNameKor = "기타", CodeNameEng = "etc", OrderNo = 7, IsActive = "Y", IsSys = "N", InsertSabun="1"} );

            // 2023.08.26 방문상태 추가 
            entity.HasData( new { CommonCodeID = 268, GroupNo =	150, SubCodeID = 5, CodeNameKor = "출입증분실", CodeNameEng = "Lost AccessCard", OrderNo = 6, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            
            // 2023.08.27 휴대물품반입반출상태 수정 및 추가 
            entity.HasData( new { CommonCodeID = 269, GroupNo =	168, SubCodeID = 8, SubCodeDesc="13004", CodeNameKor = "반출완료", CodeNameEng = "Transfer Completed", OrderNo = 9, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            // 2023.08.27 반출입상태 수정 및 추가 
            entity.HasData( new { CommonCodeID = 270, GroupNo =	162, SubCodeID = 7, CodeNameKor = "반출완료", CodeNameEng = "Export Completed", OrderNo = 8, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 271, GroupNo =	162, SubCodeID = 8, CodeNameKor = "반입상신", CodeNameEng = "Import Approval Request", OrderNo = 9, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            // 2023.08.30 반출입상태 추가 
            entity.HasData( new { CommonCodeID = 272, GroupNo =	162, SubCodeID = 9, CodeNameKor = "반입대기", CodeNameEng = "Waiting Import", OrderNo = 10, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 273, GroupNo =	162, SubCodeID = 10, CodeNameKor = "반입확인", CodeNameEng = "Import Confirmation", OrderNo = 11, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 274, GroupNo =	162, SubCodeID = 11, CodeNameKor = "반입완료", CodeNameEng = "Import Completed", OrderNo = 12, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            
            // 2023.09.08 휴대물품신청상태 미승인 추가 
            // 2023.09.19 휴대물품신청상태 수정
            entity.HasData( new { CommonCodeID = 275, GroupNo =	177, SubCodeID = 5, CodeNameKor = "미승인", CodeNameEng = "Unapproved", OrderNo = 6, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            // 2023.09.08 휴대물품반입반출상태 미반입 추가 
            entity.HasData( new { CommonCodeID = 276, GroupNo =	168, SubCodeID = 9, CodeNameKor = "미반입", CodeNameEng = "Not Import", OrderNo = 10, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );

            // 2023.09.13 반출입신청상태 추가
            // 2023.09.15 반출입신청상태 수정 
            entity.HasData( new { CommonCodeID = 277, GroupNo =	277, CodeNameKor = "반출입신청상태", CodeNameEng = "Export Import Apply Status", OrderNo = 0, IsActive = "Y", IsSys= "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 278, GroupNo =	277, SubCodeID = 0, CodeNameKor = "신청", CodeNameEng = "Application", OrderNo = 1, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 279, GroupNo =	277, SubCodeID = 1, CodeNameKor = "결재완료 ", CodeNameEng = "Payment Completed", OrderNo = 2, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 280, GroupNo =	277, SubCodeID = 2, CodeNameKor = "반려", CodeNameEng = "Companion", OrderNo = 3, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 281, GroupNo =	277, SubCodeID = 3, CodeNameKor = "결재중", CodeNameEng = "Under Approval", OrderNo = 4, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 282, GroupNo =	277, SubCodeID = 4, CodeNameKor = "승인완료", CodeNameEng = "Approved", OrderNo = 5, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 283, GroupNo =	277, SubCodeID = 5, CodeNameKor = "미승인", CodeNameEng = "Unapproved", OrderNo = 6, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            
            // 2023.09.15 반출입신청상태 추가 
            entity.HasData( new { CommonCodeID = 284, GroupNo =	277, SubCodeID = 64, CodeNameKor = "상신", CodeNameEng = "Approval Request", OrderNo = 7, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 285, GroupNo =	277, SubCodeID = 256, CodeNameKor = "임시저장", CodeNameEng = "Temporary Save", OrderNo = 8, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 286, GroupNo =	277, SubCodeID = 512, CodeNameKor = "승인보류", CodeNameEng = "Approval Pending", OrderNo = 9, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 287, GroupNo =	277, SubCodeID = 1024, CodeNameKor = "삭제", CodeNameEng = "Delete", OrderNo = 10, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
           
            // 2023.09.19 휴대물품신청상태 추가 
            entity.HasData( new { CommonCodeID = 288, GroupNo =	177, SubCodeID = 64, CodeNameKor = "상신", CodeNameEng = "Approval Request", OrderNo = 7, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 289, GroupNo =	177, SubCodeID = 256, CodeNameKor = "임시저장", CodeNameEng = "Temporary Save", OrderNo = 8, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 290, GroupNo =	177, SubCodeID = 512, CodeNameKor = "승인보류", CodeNameEng = "Approval Pending", OrderNo = 9, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            entity.HasData( new { CommonCodeID = 291, GroupNo =	177, SubCodeID = 1024, CodeNameKor = "삭제", CodeNameEng = "Delete", OrderNo = 10, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
           
            // 2023.09.19 반출입상태 추가 
            entity.HasData( new { CommonCodeID = 292, GroupNo =	162, SubCodeID = 12, CodeNameKor = "확인취소", CodeNameEng = "Verification Cancel", OrderNo = 13, IsActive = "Y", IsSys = "Y", InsertSabun="1"} );
            
        }
    }
}