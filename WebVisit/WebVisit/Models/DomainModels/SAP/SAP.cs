using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WebVisit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Dynamic;
using WebVisit.Utilities;
using System.Text.Json;
using SapNwRfc;
using System.Text;

namespace WebVisit.Controllers
{
        public class RFCZMMVisitorPRCheckParameters
    {
        [SapName("I_BANFN")]
        public string BANFN { get; set; } = string.Empty; // PR 번호

        // [SapName("Test")]
        // public string Test { get; set; } = string.Empty; // 자재코드
    }
    public class RFCZMMVisitorPRCheckResult
    {
        [SapName("E_FRGKZ")]
        public string E_FRGKZ { get; set; } // R : 승인 L : 삭제
        
        [SapName("IZSMM0097")]
        public IZSMM0097[] IZSMM0097 { get; set; }
        // [SapName("E_SUBRC")]
        // public string E_SUBRC { get; set; }

        // [SapName("E_MESSAGE")]
        // public string E_MESSAGE { get; set; }
    }

    public class IZSMM0097{
        [SapName("BANFN")]
        public string BANFN { get; set; } = string.Empty; // PR 번호 1000169868

        [SapName("BNFPO")]
        public int BNFPO { get; set; } = 0; // PR Item 00010

        [SapName("MATNR")]
        public string MATNR { get; set; } = string.Empty; // 자재코드 MEM0317

        [SapName("MAKTX")]
        public string MAKTX { get; set; } = string.Empty; // 자재 description MST CHAMBER O-RING SET

        [SapName("MENGE")]
        public string MENGE { get; set; } = string.Empty; // 수량 12.000

        [SapName("MEINS")]
        public string MEINS { get; set; } = string.Empty; // 단위 EA

        [SapName("WADYN")]
        public string WADYN { get; set; } = string.Empty; // 품질 보장 유무(보증기간 유무) Y N

        [SapName("EXTSRVNO")]
        public string EXTSRVNO { get; set; } = string.Empty; // serial no

        [SapName("CHARG")]
        public string CHARG { get; set; } = string.Empty; // bach no

        [SapName("MBLNR")]
        public string MBLNR { get; set; } = string.Empty; // 출고 문서번호

        [SapName("MJAHR")]
        public int MJAHR { get; set; } = -1; // 출고년도

        [SapName("GIDAT")]
        public DateTime GIDAT { get; set; } // 출고 일자
    }

    public class RFCZMMVisitorVendorCheckParameters
    {
        [SapName("I_WERKS")]
        public string I_WERKS { get; set; } = string.Empty;

        [SapName("I_MATNR")]
        public string I_MATNR { get; set; } = string.Empty;
    }

    public class RFCZMMVisitorVendorCheckResult
    {
        [SapName("E_LIFNR")]
        public string E_LIFNR { get; set; }

        [SapName("E_SUBRC")]
        public string E_SUBRC { get; set; }
        
        [SapName("E_MESSAGE")]
        public string E_MESSAGE { get; set; }

        
    }
}