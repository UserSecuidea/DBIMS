using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVisit.Models
{
    class FileDTO
    {
        public string ContentType { get; set; } = string.Empty;

        public string ContentDisposition { get; set; } = string.Empty;

        public long Length { get; set; }

        public string FileName { get; set; } = string.Empty;

    }
}