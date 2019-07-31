using BLL.NewsBll;
using DAL;
using DAL.Models;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace NewsProject.Controllers
{
    public class NewsController : BaseController
    {
        private News_Bll bll = new News_Bll();
        // GET: News
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.CreatedSuccessfully = TempData["CreatedSuccessfully"];


            return View();
        }
        public PartialViewResult Edit(int code)
        {
            var result = bll.GetNews(code.ToString());

            return PartialView(result);
        }
        [HttpPost]
        public ActionResult Edit(News news, HttpPostedFileBase ImageUploaded)
        {
            string message = string.Empty;
            var result = bll.GetNews(news.Code.ToString());
            result.Title = news.Title;
            if (ImageUploaded != null)
            {
                news.Image = "/Images/" + ImageUploaded.FileName;
                ImageUploaded.SaveAs(Path.Combine(Server.MapPath("/Images/"), ImageUploaded.FileName));
                result.Image = news.Image;
            }
            else
            {
                result.Image = result.Image;

            }
            result.Details = news.Details;
            result.IsDeleted = news.IsDeleted;
            bll.UpdateNews(result, out message);
            TempData["Message"] = message;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult Delete(int code)
        {
            string message = string.Empty;
            if (bll.DeleteNews(code, out message))
            {
                return Json(new { Status = 1, Message = message }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { Status = -1, Message = message }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ImageRequired = TempData["ImageRequired"];
            ViewBag.ErrorInSave = TempData["ErrorInSave"];
            ViewBag.DetailsRequired = TempData["DetailsRequired"];


            return View();
        }
        [HttpPost]
        public ActionResult Create(News news, HttpPostedFileBase ImageUploaded)
        {
            string message = string.Empty;
            if (ImageUploaded != null)
            {
                news.Image = "/Images/" + ImageUploaded.FileName;
                ImageUploaded.SaveAs(Path.Combine(Server.MapPath("/Images/"), ImageUploaded.FileName));
            }
            else
            {
                TempData["ImageRequired"] = "Image is Required";
                return RedirectToAction("Create");
            }
            if (news.Details == null)
            {
                TempData["DetailsRequired"] = "Details is Required";
                return RedirectToAction("Create");
            }
            if (bll.AddNews(news, out message))
            {
                TempData["CreatedSuccessfully"] = message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorInSave"] = message;
                return RedirectToAction("Create");
            }
        }
        public ActionResult ChangeLanguage(string lang)
        {
            new LanguageManage().SetLanguage(lang);
            return RedirectToAction("Index", "News");
        }
    }
}