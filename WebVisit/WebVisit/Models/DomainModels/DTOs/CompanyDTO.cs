using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVisit.Models
{
    public class CompanyDTO
    {
        public int CompanyID { get; set; }
        public CompanyDTO() { }

        /// <summary>
        /// overloaded constructor
        /// </summary>
        /// <param name="company"></param>
        public CompanyDTO(Company company){
            CompanyID = company.CompanyID;
        }
    }
}