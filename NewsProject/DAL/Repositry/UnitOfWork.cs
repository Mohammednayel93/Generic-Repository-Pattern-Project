using DAL.Interfaces;
using DAL.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace DAL.Repositry
{
    public class UnitOfWork : IUnitOfWork
    {
        internal NewsDBEntities context;
        public UnitOfWork()
        {
            this.context = new NewsDBEntities();
        }
        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {

            try
            {
                return context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                return -1;
            }
        }


    }
}

