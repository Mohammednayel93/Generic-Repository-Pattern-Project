using DAL;
using System;
using System.Web;
using System.Web.Mvc;

namespace NewsProject.Controllers
{
    public class BaseController : Controller
    {

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            string lang = null;
            HttpCookie langCookie = Request.Cookies["language"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = LanguageManage.GetDefaultLanguage();
                }
            }
            new LanguageManage().SetLanguage(lang);

            return base.BeginExecuteCore(callback, state);
        }

    }
}