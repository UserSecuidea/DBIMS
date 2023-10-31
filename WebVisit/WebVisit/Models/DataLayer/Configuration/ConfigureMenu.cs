using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.ModelConfiguration.Configuration;

namespace WebVisit.Models
{
    internal class ConfigureMenu : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> entity)
        {
            entity.Property(b => b.IsDisplay).HasDefaultValue("Y");
            entity.Property(b => b.IsDelete).HasDefaultValue("N");
            entity.Property(b => b.InsertDate).HasDefaultValueSql("getdate()");

            // seed initial data
            entity.HasData( new { MenuID = 1, GroupNo = 1, Depth = 0, OrderNo = 1, MenuNameKor = "임직원 정보관리", MenuNameEng = "Person Info", URL="", IconImg="/images/ico/ico-gnb-list01.svg", InsertSabun="1"});	
            entity.HasData( new { MenuID = 2, GroupNo = 1, Depth = 1, OrderNo = 2, MenuNameKor = "임직원 관리", MenuNameEng = "Person Info", URL="/person/list", InsertSabun="1"});
            entity.HasData( new { MenuID = 3, GroupNo =	1, Depth = 1, OrderNo = 5, MenuNameKor = "출입증 관리", MenuNameEng = "Card", URL="/card/list", InsertSabun="1"});
            entity.HasData( new { MenuID = 4, GroupNo =	1, Depth = 1, OrderNo = 7, MenuNameKor = "차량 관리", MenuNameEng = "Card Car", URL="/card/carlist", InsertSabun="1"});
            
            entity.HasData( new { MenuID = 5, GroupNo =	5, Depth = 0, OrderNo = 1, MenuNameKor = "업체정보관리", MenuNameEng = "Company", URL="", IconImg="/images/ico/ico-gnb-list02.svg", InsertSabun="1"});
            entity.HasData( new { MenuID = 6, GroupNo = 5, Depth = 1, OrderNo = 2, MenuNameKor = "업체정보관리", MenuNameEng = "Company", URL="/company/list", InsertSabun="1"});
            
            entity.HasData( new { MenuID = 7, GroupNo = 7, Depth = 0, OrderNo = 1, MenuNameKor = "방문객관리", MenuNameEng = "Visit", URL="", IconImg="/images/ico/ico-gnb-list03.svg", InsertSabun="1"});
            entity.HasData( new { MenuID = 8, GroupNo = 7, Depth = 1, OrderNo = 2, MenuNameKor = "방문객 관리", MenuNameEng = "Visit", URL="/visit/list", InsertSabun="1"});
            entity.HasData( new { MenuID = 9, GroupNo = 7, Depth = 1, OrderNo = 5, MenuNameKor = "휴대물품 관리", MenuNameEng = "CarryItem", URL="/carryitem/list", InsertSabun="1"});
            entity.HasData( new { MenuID = 10, GroupNo = 7, Depth = 1, OrderNo = 8, MenuNameKor = "방문객수기입력", MenuNameEng = "Visit Manual", URL="/visit/manuallist", InsertSabun="1"});
            
            entity.HasData( new { MenuID = 11, GroupNo = 11, Depth = 0, OrderNo = 1, MenuNameKor = "임시증 관리", MenuNameEng = "Card Temp", URL="", IconImg="/images/ico/ico-gnb-list04.svg", InsertSabun="1"});
            entity.HasData( new { MenuID = 12, GroupNo = 11, Depth = 1, OrderNo = 2, MenuNameKor = "임시증 관리", MenuNameEng = "Card Temp", URL="/card/templist", InsertSabun="1"});
            
            entity.HasData( new { MenuID = 13, GroupNo = 13, Depth = 0, OrderNo = 1, MenuNameKor = "반출입 관리", MenuNameEng = "ExportImport", URL="", IconImg="/images/ico/ico-gnb-list05.svg", InsertSabun="1"});
            entity.HasData( new { MenuID = 14, GroupNo = 13, Depth = 1, OrderNo = 2, MenuNameKor = "반출입관리", MenuNameEng = "ExportImport", URL="/exportimport/list", InsertSabun="1"});
            entity.HasData( new { MenuID = 15, GroupNo = 13, Depth = 1, OrderNo = 5, MenuNameKor = "노트북자가결재", MenuNameEng = "ExportImport Notebook", URL="/exportimport/selfapproval", InsertSabun="1"});
            
            entity.HasData( new { MenuID = 16, GroupNo = 16, Depth = 0, OrderNo = 1, MenuNameKor = "공사/안전 관리", MenuNameEng = "Work / Safety", URL="", IconImg="/images/ico/ico-gnb-list06.svg", InsertSabun="1"});
            entity.HasData( new { MenuID = 17, GroupNo = 16, Depth = 1, OrderNo = 2, MenuNameKor = "공사등록현황", MenuNameEng = "Work", URL="/work/list", InsertSabun="1"});
            entity.HasData( new { MenuID = 18, GroupNo = 16, Depth = 1, OrderNo = 5, MenuNameKor = "공사신청현황", MenuNameEng = "Work Visit", URL="/work/visitlist", InsertSabun="1"});
            entity.HasData( new { MenuID = 19, GroupNo = 16, Depth = 1, OrderNo = 8, MenuNameKor = "안전교육현황", MenuNameEng = "Safety Edu", URL="/work/safetyedulist", InsertSabun="1"});
            entity.HasData( new { MenuID = 20, GroupNo = 16, Depth = 1, OrderNo = 11, MenuNameKor = "I/R발행", MenuNameEng = "I/R", URL="/work/safetyviolationlist", InsertSabun="1"});
            entity.HasData( new { MenuID = 21, GroupNo = 16, Depth = 1, OrderNo = 14, MenuNameKor = "통계관리", MenuNameEng = "Statistics", URL="/work/statisticslist", InsertSabun="1"});
            entity.HasData( new { MenuID = 22, GroupNo = 16, Depth = 1, OrderNo = 15, MenuNameKor = "안전위규관리", MenuNameEng = "Safety Violation", URL="/work/safetyviolationreasonlist", InsertSabun="1"});
            
            entity.HasData( new { MenuID = 23, GroupNo = 23, Depth = 0, OrderNo = 1, MenuNameKor = "식수집계관리", MenuNameEng = "Meal", URL="", IconImg="/images/ico/ico-gnb-list07.svg", InsertSabun="1"});
            entity.HasData( new { MenuID = 24, GroupNo = 23, Depth = 1, OrderNo = 2, MenuNameKor = "식수현황조회", MenuNameEng = "Meal", URL="/meal/list", InsertSabun="1"});
            entity.HasData( new { MenuID = 25, GroupNo = 23, Depth = 1, OrderNo = 4, MenuNameKor = "이상식수조회", MenuNameEng = "Meal Invalid", URL="/meal/invalidlist", InsertSabun="1"});
            entity.HasData( new { MenuID = 26, GroupNo = 23, Depth = 1, OrderNo = 5, MenuNameKor = "식수권한관리", MenuNameEng = "Meal Permission", URL="/meal/permissionlist", InsertSabun="1"});
            entity.HasData( new { MenuID = 27, GroupNo = 23, Depth = 1, OrderNo = 6, MenuNameKor = "식수정보관리", MenuNameEng = "Meal Info", URL="/meal/infolist", InsertSabun="1"});
            entity.HasData( new { MenuID = 28, GroupNo = 23, Depth = 1, OrderNo = 7, MenuNameKor = "식수수기등록", MenuNameEng = "Meal Manual", URL="/meal/manuallist", InsertSabun="1"});
            
            entity.HasData( new { MenuID = 29, GroupNo = 29, Depth = 0, OrderNo = 1, MenuNameKor = "내정보관리", MenuNameEng = "MyInfo", URL="", IconImg="/images/ico/ico-gnb-list08.svg", InsertSabun="1"});
            entity.HasData( new { MenuID = 30, GroupNo = 29, Depth = 1, OrderNo = 2, MenuNameKor = "내정보관리", MenuNameEng = "MyInfo", URL="/person/myinfo", InsertSabun="1"});
            
            entity.HasData( new { MenuID = 31, GroupNo = 31, Depth = 0, OrderNo = 1, MenuNameKor = "공지사항관리", MenuNameEng = "Notice", URL="", IconImg="/images/ico/ico-gnb-list09.svg", InsertSabun="1"});
            entity.HasData( new { MenuID = 32, GroupNo = 31, Depth = 1, OrderNo = 2, MenuNameKor = "공지사항관리", MenuNameEng = "Notice", URL="/notice/list", InsertSabun="1"});
            
            entity.HasData( new { MenuID = 33, GroupNo = 33, Depth = 0, OrderNo = 1, MenuNameKor = "시스템관리", MenuNameEng = "System Management", URL="", IconImg="/images/ico/ico-gnb-list10.svg", InsertSabun="1"});
            entity.HasData( new { MenuID = 34, GroupNo = 33, Depth = 1, OrderNo = 2, MenuNameKor = "메뉴관리", MenuNameEng = "Menu", URL="/sys/menulist", InsertSabun="1"});
            entity.HasData( new { MenuID = 35, GroupNo = 33, Depth = 1, OrderNo = 3, MenuNameKor = "레벨관리", MenuNameEng = "Level", URL="/sys/levellist", InsertSabun="1"});
            entity.HasData( new { MenuID = 36, GroupNo = 33, Depth = 1, OrderNo = 4, MenuNameKor = "결재경로관리", MenuNameEng = "Approval", URL="/sys/approval", InsertSabun="1"});
            entity.HasData( new { MenuID = 37, GroupNo = 33, Depth = 1, OrderNo = 5, MenuNameKor = "공통코드관리", MenuNameEng = "CommonCode", URL="/sys/commoncode", InsertSabun="1"});
            entity.HasData( new { MenuID = 38, GroupNo = 33, Depth = 1, OrderNo = 6, MenuNameKor = "장비관리", MenuNameEng = "Equipment", URL="", InsertSabun="1"});
            entity.HasData( new { MenuID = 39, GroupNo = 33, Depth = 1, OrderNo = 7, MenuNameKor = "공휴일등록관리", MenuNameEng = "Holiday", URL="/sys/holidaylist", InsertSabun="1"});
            
            entity.HasData( new { MenuID = 40, GroupNo = 40, Depth = 0, OrderNo = 1, MenuNameKor = "출입기록관리", MenuNameEng = "Access Record", URL="", IconImg="/images/ico/ico-gnb-list11.svg", InsertSabun="1"});
            entity.HasData( new { MenuID = 41, GroupNo = 40, Depth = 1, OrderNo = 2, MenuNameKor = "인원출입조회", MenuNameEng = "Access Record Person", URL="/accessrecord/personlist", InsertSabun="1"});
            entity.HasData( new { MenuID = 42, GroupNo = 40, Depth = 1, OrderNo = 3, MenuNameKor = "차량출입조회", MenuNameEng = "Access Record Car", URL="/accessrecord/carlist", InsertSabun="1"});
            entity.HasData( new { MenuID = 43, GroupNo = 40, Depth = 1, OrderNo = 4, MenuNameKor = "인원출입현황", MenuNameEng = "Access Record", URL="/accessrecord/statisticslist", InsertSabun="1"});
            
            entity.HasData( new { MenuID = 44, GroupNo = 44, Depth = 0, OrderNo = 1, MenuNameKor = "인원출입관리", MenuNameEng = "Card Apply", URL="", IconImg="/images/ico/ico-gnb-list12.svg", InsertSabun="1"});
            entity.HasData( new { MenuID = 45, GroupNo = 44, Depth = 1, OrderNo = 2, MenuNameKor = "인원출입증현황 ", MenuNameEng = "Card Apply Person", URL="/card/applylist", InsertSabun="1"});
            entity.HasData( new { MenuID = 46, GroupNo = 44, Depth = 1, OrderNo = 5, MenuNameKor = "차량출입증현황", MenuNameEng = "Card Apply Car", URL="/card/carapplylist", InsertSabun="1"});
            entity.HasData( new { MenuID = 47, GroupNo = 44, Depth = 1, OrderNo = 8, MenuNameKor = "블랙리스트관리", MenuNameEng = "BlackList", URL="/person/blacklist", InsertSabun="1"});
        }
    }
}