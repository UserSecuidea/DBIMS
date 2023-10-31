using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text;
using System.Security.Cryptography;
using WebVisit.Utilities;

namespace WebVisit.Models
{
    public static class StringExtensions
    {
        /// <summary>
        /// dash로 구분된 문자열 생성
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Slug(this string str)
        {
            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if (!char.IsPunctuation(c) || c == '-') { // IsPunctuation(): dash(-) 를 제외한 문장부호 제거
                    sb.Append(c);
                }
            }
            return sb.ToString().Replace(' ', '-').ToLower();
        }

        /// <summary>
        /// 대소문자 구분을 없이 두 문자열을 비교
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tocompare"></param>
        /// <returns></returns>
        public static bool EqualsNoCase(this string str, string tocompare) =>
            str?.ToLower() == tocompare?.ToLower();


        /// <summary>
        /// string 을 int 로 변환
        /// </summary>
        /// <param name="str"></param>
        /// <returns>int로 변환될 수 없다면 0을 반환</returns>
        public static int ToInt(this string str)
        {
            int.TryParse(str, out int value);
            return value;
        }

        /// <summary>
        /// 첫 글자를 대문자, 나머지 문자들은 소문자로 변환
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Capitalize(this string str) =>
            str?.Substring(0, 1)?.ToUpper() + str?.Substring(1)?.ToLower();

        public static string SHA256Encrypt(this string data){
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));
            // Utils.WriteLog("hash base64:" + Convert.ToBase64String(hash));
            return Convert.ToBase64String(hash);
            // StringBuilder stringBuilder = new StringBuilder();
            // foreach(byte b in hash){
            //     stringBuilder.AppendFormat("{0:x2}", b);
            // }
            // return stringBuilder.ToString();
        }

        public static string HideEven(this string str){
            StringBuilder sb = new();
            for(var i =0; i<str.Length;i++){
                if(i%2==0){
                    sb.Append(str?.Substring(i,1));
                } else {
                    sb.Append("*");
                }
            }
            return sb.ToString();
        }
    }
}