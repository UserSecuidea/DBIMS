using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.ModelConfiguration.Configuration;

namespace WebVisit.Models
{
    internal class ConfigureMaterial : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> entity)
        {
            entity.Property(b => b.IsDelete).HasDefaultValue("N");
            // seed initial data
            entity.HasData(new { MaterialID = 1, DIV_CODE = "11", STOCK_CODE="B_GRINDING_CHG", STOCK_NM="Back Grinding Service Charge", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2005-06-07"});
            entity.HasData(new { MaterialID = 2, DIV_CODE = "11", STOCK_CODE="N00MSKIG", STOCK_NM="MASK", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2019-02-21"});
            entity.HasData(new { MaterialID = 3, DIV_CODE = "11", STOCK_CODE="N00MSKME", STOCK_NM="MASK", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2017-12-01"});
            entity.HasData(new { MaterialID = 4, DIV_CODE = "11", STOCK_CODE="N00PRBSJ", STOCK_NM="PROBE CARD", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2017-12-06"});
            entity.HasData(new { MaterialID = 5, DIV_CODE = "11", STOCK_CODE="N00SPCIG", STOCK_NM="Sample Wafer", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2020-02-25"});
            entity.HasData(new { MaterialID = 6, DIV_CODE = "11", STOCK_CODE="N00SPCME", STOCK_NM="Sample Wafer", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2018-03-14"});
            entity.HasData(new { MaterialID = 7, DIV_CODE = "11", STOCK_CODE="N00SPCMO", STOCK_NM="Sample Wafer", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2019-04-22"});
            entity.HasData(new { MaterialID = 8, DIV_CODE = "11", STOCK_CODE="N00SPCSJ", STOCK_NM="Sample Wafer", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2018-12-11"});
            entity.HasData(new { MaterialID = 9, DIV_CODE = "11", STOCK_CODE="N00TSCIG", STOCK_NM="SCRAP", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2019-03-08"});
            entity.HasData(new { MaterialID = 10, DIV_CODE = "11", STOCK_CODE="N00TSCMO", STOCK_NM="SCRAP", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2020-02-28"});
            entity.HasData(new { MaterialID = 11, DIV_CODE = "11", STOCK_CODE="N00TTSMO", STOCK_NM="TEST", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2017-09-27"});
            entity.HasData(new { MaterialID = 12, DIV_CODE = "11", STOCK_CODE="N06MSKMO", STOCK_NM="MASK", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2016-06-09"});
            entity.HasData(new { MaterialID = 13, DIV_CODE = "11", STOCK_CODE="N06TTSMO", STOCK_NM="TEST", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2016-06-09"});
            entity.HasData(new { MaterialID = 14, DIV_CODE = "11", STOCK_CODE="N09APASF", STOCK_NM="PRICE ADJUSTMENT", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2013-03-04"});
            entity.HasData(new { MaterialID = 15, DIV_CODE = "11", STOCK_CODE="N09BGCSF", STOCK_NM="BACK GRINDING", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2013-02-28"});
            entity.HasData(new { MaterialID = 16, DIV_CODE = "11", STOCK_CODE="N09DDSSF", STOCK_NM="DESIGN SERVICE", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2013-03-04"});
            entity.HasData(new { MaterialID = 17, DIV_CODE = "11", STOCK_CODE="N09ENGSF", STOCK_NM="ENGINEERRING", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2013-03-15"});
            entity.HasData(new { MaterialID = 18, DIV_CODE = "11", STOCK_CODE="N09HHCSF", STOCK_NM="HOLD", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2013-03-05"});
            entity.HasData(new { MaterialID = 19, DIV_CODE = "11", STOCK_CODE="N09HOTSF", STOCK_NM="HOT", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2013-03-04"});
            entity.HasData(new { MaterialID = 20, DIV_CODE = "11", STOCK_CODE="N09LLCSF", STOCK_NM="PROTOTYPE LOT", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2013-03-05"});
            entity.HasData(new { MaterialID = 21, DIV_CODE = "11", STOCK_CODE="N09LMFSF", STOCK_NM="MPW SHUTTLE", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2013-03-04"});
            entity.HasData(new { MaterialID = 22, DIV_CODE = "11", STOCK_CODE="N09MHFSF", STOCK_NM="HANDLING", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2013-03-05"});
            entity.HasData(new { MaterialID = 23, DIV_CODE = "11", STOCK_CODE="N09MSKCS", STOCK_NM="MASK", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2019-06-18"});
            entity.HasData(new { MaterialID = 24, DIV_CODE = "11", STOCK_CODE="N09MSKSF", STOCK_NM="MASK", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2013-02-28"});
            entity.HasData(new { MaterialID = 25, DIV_CODE = "11", STOCK_CODE="N09PCBCS", STOCK_NM="PROBE CARD", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2017-08-02"});
            entity.HasData(new { MaterialID = 26, DIV_CODE = "11", STOCK_CODE="N09PRBCS", STOCK_NM="PROBE CARD", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2017-08-02"});
            entity.HasData(new { MaterialID = 27, DIV_CODE = "11", STOCK_CODE="N09PSCSF", STOCK_NM="SPLIT", STOCK_RULE="", STOCK_UNIT="AU", W_DATE="2013-03-04"});
        }
    }
}