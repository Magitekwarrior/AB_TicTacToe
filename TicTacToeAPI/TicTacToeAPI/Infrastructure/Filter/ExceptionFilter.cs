using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace TicTacToeAPI.Infrastructure.Filter
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class ExceptionFilter : ExceptionFilterAttribute
  {
    public ExceptionFilter()
    { }

    public override void OnException(ExceptionContext context)
    {
      Log.Error(context.Exception, "Exception Filter");

      context.HttpContext.Response.ContentType = "application/json";
      context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
      context.Result = new JsonResult(new
      {
        error = new[] { context.Exception.Message },
        stackTrace = context.Exception.StackTrace
      });
    }
  }
}
