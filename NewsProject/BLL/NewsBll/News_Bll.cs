using DAL.Models;
using DAL.Repositry;
using LangPack;
using LangPack.Helpers;
using System;
using System.Linq;

namespace BLL.NewsBll
{
    public class News_Bll
    {
        string langName = CultureHelper.GetCurrentNeutralCulture();
        UnitOfWork uow;
        Repository<News> repository;
        public News_Bll()
        {
            uow = new UnitOfWork();
            repository = new Repository<News>(uow);
        }
        public bool AddNews(News news, out string message)
        {
            try
            {

                repository.Insert(news);

                message = Common.SavedSuccessfully;

                return true;
            }
            catch (Exception ex)
            {
                message = LangPack.Common.ErrorInSave;
                return false;
            }
        }
        public bool UpdateNews(News news, out string message)
        {
            try
            {


                repository.Update(news, news.Code);

                message = Common.UpdatedSuccessfully;

                return true;
            }
            catch (Exception ex)
            {
                message = Common.ErrorInUpdate;

                return false;
            }
        }
        public bool DeleteNews(double code, out string message)
        {
            try
            {
                repository.Delete(code);

                message = Common.DeletedSuccessfully;

                return true;
            }
            catch (Exception ex)
            {
                message = Common.ErrorInDelete;

                return false;
            }
        }

        public IQueryable<News> GetAllNews()
        {
            return repository.GetAll();
        }

        public News GetNews(String code)
        {
            long LongCode;
            if (long.TryParse(code, out LongCode))
                return repository.GetById(LongCode);

            return null;
        }
    }
}
