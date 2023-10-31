using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVisit.Models
{
    public class VisitSession
    {
        private ISession Session { get; set; }
        private IRequestCookieCollection RequestCookies { get; set; }
        private IResponseCookies ResponseCookies { get; set; }

        public string? Lang { get; set; } = "ko";
        public int? CurrentMenuGroupNo { get; set; }
        public int? CurrentMenuID { get; set; }

        private const string PersonKey = "PersonKey";
        private const string LangKey = "LangKey";
        private const string MenuGroupKey = "MenuGroupKey";
        private const string MenuIDKey = "MenuIDKey";
        public VisitSession(HttpContext ctx) {
            Session = ctx.Session;
            RequestCookies = ctx.Request.Cookies;
            ResponseCookies = ctx.Response.Cookies;
        }
        
        public void SetPerson(Person? person) {
            if (person == null) {
                Session.Remove(PersonKey);
                // ResponseCookies.Delete(PersonKey); // Cookie
            } else {
                PersonDTO personDTO = new(person);
                Session.SetObject<PersonDTO>(PersonKey, personDTO);
                Console.WriteLine("[SetPerson]"+personDTO.Sabun);
                // ResponseCookies.SetObject<PersonDTO>(PersonKey, personDTO); // Cookie
            }
        }

        public void SetLang(string lang) {
            if(lang == null) {
                Session.Remove(LangKey);
            } else {
                Session.SetString(LangKey, lang);
            }
        }

        public string GetLang() {
            return Session.GetString(LangKey)??"ko";
        }

        public void SetMenuGroup(int value) {
            if(value == -1) {
                Session.Remove(MenuGroupKey);
            } else {
                Session.SetInt32(MenuGroupKey, value);
            }
        }

        public int GetMenuGroup() {
            return Session.GetInt32(MenuGroupKey)??0;
        }

        public void SetMenuID(int value) {
            if(value == -1) {
                Session.Remove(MenuIDKey);
            } else {
                Session.SetInt32(MenuIDKey, value);
            }
        }

        public int GetMenuID() {
            return Session.GetInt32(MenuIDKey)??0;
        }

        public PersonDTO? GetPersonDTO(){
            PersonDTO personDTO = Session.GetObject<PersonDTO>(PersonKey)??new PersonDTO();
            // PersonDTO personDTO = RequestCookies.GetObject<PersonDTO>(PersonKey)??new PersonDTO(); // Cookie
            return personDTO;        
        }
    }
}