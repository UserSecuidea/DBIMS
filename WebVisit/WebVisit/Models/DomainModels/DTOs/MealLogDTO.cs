using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVisit.Models
{
    public class MealLogDTO
    {
        public int Count0 { get; set; } = 0; // 이상 식수
        public int Count1 { get; set; } = 0; // 조식
        public int Count2 { get; set; } = 0; // 중식
        public int Count3 { get; set; } = 0; // 석식
        public int Count4 { get; set; } = 0; // 야식
        public int Count5 { get; set; } = 0; // 조식 수기
        public int Count6 { get; set; } = 0; // 중식 수기
        public int Count7 { get; set; } = 0; // 석식 수기
        public int Count8 { get; set; } = 0; // 야식 수기
        public int CountTotal { get; set; } = 0;
        public int PriceTotal { get; set;} = 0;
        public int Year { get; set; } = 0;
        public int Month { get; set; } = 0;
        public MealLog MealLog { get; set; } = new MealLog();
        
        public int MealLogID { get; set; } = 0;
        public int TabIdx { get; set; } = 0;
        public List<CommonCode> CodeLocation { get; set; } = new (); 
   }
}