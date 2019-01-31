using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PowerBIWebApp.Filters
{
    public class UnhandledExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {            
            context.Result = new BadRequestObjectResult(context.Exception.Message);
        }
    }
}
