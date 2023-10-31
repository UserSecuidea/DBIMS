using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebVisit.Models
{
    public class Notice
    {
        /*
        req: NoticeID, Title, Contents, StartDate, EndDate, IsPublish
        no req: 

        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int NoticeID { get; set; }
        
        [MaxLength(300)]
        public string Title { get; set; } = string.Empty;
        
        [Column(TypeName = "nvarchar(max)")]        
        public string Contents { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd}")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;

        [MaxLength(1)]
        public string IsPublish { get; set; } = string.Empty;
        
        [MaxLength(1000)]
        public string? FileData1 { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(max)")]
        public byte[]? FileDataHash1 {get; set;}

        [NotMapped]
        public List<IFormFile>? FormFile { get; set; }
        // public IFormFile[]? FormFile { get; set; }
        

        [MaxLength(1000)]
        public string? FileData2 { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(max)")]
        public byte[]? FileDataHash2 {get; set;}

        //등록자: 사번, 이름, 부서ID, 부서명
        [Required]
        [MaxLength(50)]
        public string InsertSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? InsertName { get; set; } = string.Empty;

        // public int? InsertOrgID {get; set;}
        [MaxLength(50)]
        public string? InsertOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? InsertOrgNameKor { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? InsertOrgNameEng { get; set; } = string.Empty;

        //갱신일, 생성일, 삭제여부 
        public DateTime? UpdateDate { get; set; } 

        public DateTime InsertDate { get; set; } 
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = string.Empty;


        public Notice(){
            //
        }

        public T? Deserialize<T>(string obj)
        {
            try {
                return JsonSerializer.Deserialize<T>(obj);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
            return default;
        }
    }
}
