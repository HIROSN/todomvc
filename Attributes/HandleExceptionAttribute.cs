using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace TodoMvc.Attributes
{
    public class HandleExceptionAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new StatusCodeResult((int)HttpStatusCode.NotFound);
        }
    }
}
