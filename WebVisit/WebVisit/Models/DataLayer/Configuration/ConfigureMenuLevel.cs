using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.ModelConfiguration.Configuration;

namespace WebVisit.Models
{
    internal class ConfigureMenuLevel : IEntityTypeConfiguration<MenuLevel>
    {
        public void Configure(EntityTypeBuilder<MenuLevel> entity)
        {
            entity.Property(b => b.IsAccess).HasDefaultValue("Y");
            entity.Property(b => b.IsDisplay).HasDefaultValue("N");
            entity.Property(b => b.IsDelete).HasDefaultValue("N");
            entity.Property(b => b.InsertDate).HasDefaultValueSql("getdate()");

            // seed initial data
            entity.HasData( new { MenuLevelID = 1, MenuID = 2, LevelID = 1, MenuNameKor = "임직원 관리", MenuNameEng = "Person Info", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 2, MenuID = 3, LevelID = 1, MenuNameKor = "출입증 관리", MenuNameEng = "Card", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 3, MenuID = 4, LevelID = 1, MenuNameKor = "차량 관리", MenuNameEng = "Card Car ", InsertSabun="1"});	
            
            entity.HasData( new { MenuLevelID = 4, MenuID = 6, LevelID = 1, MenuNameKor = "업체정보관리", MenuNameEng = "Company", InsertSabun="1"});	
            
            entity.HasData( new { MenuLevelID = 5, MenuID = 8, LevelID = 1, MenuNameKor = "방문객 관리", MenuNameEng = "Visit ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 6, MenuID = 9, LevelID = 1, MenuNameKor = "휴대물품 관리", MenuNameEng = "CarryItem ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 7, MenuID = 10, LevelID = 1, MenuNameKor = "방문객수기입력", MenuNameEng = "Visit Manual", InsertSabun="1"});	
            
            entity.HasData( new { MenuLevelID = 8, MenuID = 12, LevelID = 1, MenuNameKor = "임시증 관리", MenuNameEng = "Card Temp ", InsertSabun="1"});	
            
            entity.HasData( new { MenuLevelID = 9, MenuID = 14, LevelID = 1, MenuNameKor = "반출입관리", MenuNameEng = "ExportImport ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 10, MenuID = 15, LevelID = 1, MenuNameKor = "노트북자가결재", MenuNameEng = "ExportImport Notebook ", InsertSabun="1"});	
            
            entity.HasData( new { MenuLevelID = 11, MenuID = 17, LevelID = 1, MenuNameKor = "공사등록현황", MenuNameEng = "Work", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 12, MenuID = 18, LevelID = 1, MenuNameKor = "공사신청현황", MenuNameEng = "Work Visit ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 13, MenuID = 19, LevelID = 1, MenuNameKor = "안전교육현황", MenuNameEng = "Safety Edu", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 14, MenuID = 20, LevelID = 1, MenuNameKor = "I/R발행", MenuNameEng = "I/R ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 15, MenuID = 21, LevelID = 1, MenuNameKor = "통계관리", MenuNameEng = "Statistics ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 16, MenuID = 22, LevelID = 1, MenuNameKor = "안전위규관리", MenuNameEng = "Safety Violation ", InsertSabun="1"});	
            
            entity.HasData( new { MenuLevelID = 17, MenuID = 24, LevelID = 1, MenuNameKor = "식수현황조회", MenuNameEng = "Meal", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 18, MenuID = 25, LevelID = 1, MenuNameKor = "이상식수조회", MenuNameEng = "Meal Invalid ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 19, MenuID = 26, LevelID = 1, MenuNameKor = "식수권한관리", MenuNameEng = "Meal Permission ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 20, MenuID = 27, LevelID = 1, MenuNameKor = "식수정보관리", MenuNameEng = "Meal Info ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 21, MenuID = 28, LevelID = 1, MenuNameKor = "식수수기등록", MenuNameEng = "Meal Manual", InsertSabun="1"});	
            
            entity.HasData( new { MenuLevelID = 22, MenuID = 30, LevelID = 1, MenuNameKor = "내정보관리", MenuNameEng = "MyInfo ", InsertSabun="1"});	

            entity.HasData( new { MenuLevelID = 23, MenuID = 32, LevelID = 1, MenuNameKor = "공지사항", MenuNameEng = "Notice ", InsertSabun="1"});	
            
            entity.HasData( new { MenuLevelID = 24, MenuID = 34, LevelID = 1, MenuNameKor = "메뉴관리", MenuNameEng = "Menu", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 25, MenuID = 35, LevelID = 1, MenuNameKor = "레벨관리", MenuNameEng = "Level ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 26, MenuID = 36, LevelID = 1, MenuNameKor = "결재경로관리", MenuNameEng = "Approval", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 27, MenuID = 37, LevelID = 1, MenuNameKor = "공통코드관리", MenuNameEng = "CommonCode ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 28, MenuID = 38, LevelID = 1, MenuNameKor = "장비관리", MenuNameEng = " ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 29, MenuID = 39, LevelID = 1, MenuNameKor = "공휴일등록관리", MenuNameEng = "Holiday ", InsertSabun="1"});	
            
            entity.HasData( new { MenuLevelID = 30, MenuID = 41, LevelID = 1, MenuNameKor = "인원출입조회", MenuNameEng = "Access Record Person ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 31, MenuID = 42, LevelID = 1, MenuNameKor = "차량출입조회", MenuNameEng = "Access Record Car ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 32, MenuID = 43, LevelID = 1, MenuNameKor = "인원출입현황", MenuNameEng = "Access Record", InsertSabun="1"});	
            
            entity.HasData( new { MenuLevelID = 33, MenuID = 45, LevelID = 1, MenuNameKor = "인원출입증신청현황", MenuNameEng = "Card Apply Person ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 34, MenuID = 46, LevelID = 1, MenuNameKor = "차량출입증승인현황", MenuNameEng = "Card Apply Car ", InsertSabun="1"});	
            entity.HasData( new { MenuLevelID = 35, MenuID = 47, LevelID = 1, MenuNameKor = "블랙리스트관리", MenuNameEng = "BlackList ", InsertSabun="1"});

        }
    }
}