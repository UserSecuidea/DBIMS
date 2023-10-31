using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class WorkApplyAttachFile
    {
        /*         
        req: WorkApplyAttachFileID, WorkApplyID, AttachFile
        no req: 

        [common]
        req: InsertDate, IsDelete
        no req: UpdateDate, 
        */
        [Key]
        public int WorkApplyAttachFileID { get; set; }
        public int WorkApplyID { get; set; }
        
        [MaxLength(300)]
        public string? Title { get; set; } = string.Empty;


        [MaxLength(1000)]	
        public string? AttachFileData { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(max)")]
        public byte[]? AttachFileDataHash {get; set;}

        [NotMapped]
        public List<IFormFile>? FormFile { get; set; }


        //갱신일, 생성일, 삭제여부 
        public DateTime? UpdateDate { get; set; } 

        public DateTime InsertDate { get; set; } 
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = "N";

        public WorkApplyAttachFile(){
            //
        }
        

    }
}
