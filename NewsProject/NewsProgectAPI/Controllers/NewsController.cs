using BLL.NewsBll;
using DAL.Models;
using System.Linq;
using System.Web.Http;

namespace NewsProgectAPI.Controllers
{
    public class NewsController : ApiController
    {
        private News_Bll bll = new News_Bll();

        // GET: api/News


        public IQueryable<News> GetNews()
        {
            return bll.GetAllNews();
        }



        // // POST: api/News
        // [ResponseType(typeof(News))]
        // public string Post(News news)
        // {
        //     //try
        //     //{
        //     //    if (HttpContext.Current.Request.Files.AllKeys.Any())
        //     //    {
        //     //        var files = HttpContext.Current.Request.Files;
        //     //        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Folder1/Folder2/"), "My up Image.jpg");
        //     //        files[Para1].SaveAs(fileSavePath);
        //     //        return "Image Uploaded";
        //     //    }
        //     //    else
        //     //    {
        //     //        return "Image Not Found";
        //     //    }
        //     //}
        //     //catch (Exception ex)
        //     //{
        //     //    return "Error";
        //     //}

        //     var file = HttpContext.Current.Request.Files.Count > 0 ?
        //HttpContext.Current.Request.Files[0] : null;

        //     if (file != null && file.ContentLength > 0)
        //     {
        //         var fileName = Path.GetFileName(file.FileName);

        //         var path = Path.Combine(
        //             HttpContext.Current.Server.MapPath("/Content/Images/"),
        //             fileName
        //         );

        //         file.SaveAs(path);
        //     }

        //     return file != null ? "/uploads/" + file.FileName : null;
        //     //string message = string.Empty;
        //     //if (!ModelState.IsValid)
        //     //{
        //     //    return BadRequest(ModelState);
        //     //}

        //     //bll.AddNews(news, out message);


        //     //return CreatedAtRoute("DefaultApi", new { id = news.Code }, news);
        // }



    }
}