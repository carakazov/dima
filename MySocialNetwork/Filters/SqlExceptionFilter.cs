using System.Data.SqlClient;
using System.Web.Mvc;

namespace MySocialNetwork.Filters
{
    public class SqlExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled && context.Exception is SqlException)
            {
                context.Result = new RedirectResult("/Shared/Error.cshtml");
                context.ExceptionHandled = true;
            }
        }
    }
}