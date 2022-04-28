using Microservices.Api.Logging.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Microservices.Api.Logging.Repository
{
    public class LogRepository : ActionFilterAttribute, ILogRepository
    {
        private readonly ILogger _logger;
        private string _routeRequestInfo;

        public LogRepository(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("LoggingActionFilter");            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("ClassFilter OnActionExecuting");

            if(context.ActionArguments.Count !=0)
                _logger.LogInformation("Parameter: " + JsonConvert.SerializeObject(context.ActionArguments.Values));

            //Record route info
            _routeRequestInfo = string.Format("Controller: {0}; Action: {1}", context.RouteData.Values["controller"], context.RouteData.Values["action"]);
            _logger.LogInformation(_routeRequestInfo);

            if (context.Result != null)
            {
                var resultMsg = context.Result;
                if (resultMsg is JsonResult json)
                {
                    _logger.LogInformation("JSON Serializer Setting: " + json.SerializerSettings);
                    _logger.LogInformation("JSON Content Type: " + json.ContentType);
                    _logger.LogInformation("JSON Status Code: " + json.StatusCode);
                    _logger.LogInformation("JSON Value: " + JsonConvert.SerializeObject(json.Value));
                }
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("ClassFilter OnActionExecuted");

            // Record http request info
            _logger.LogInformation("Http request info:");
            _logger.LogInformation($"Http URL: {context.HttpContext.Request.Path}");
            _logger.LogInformation($"Http Header: {JsonConvert.SerializeObject(context.HttpContext.Request.Headers, Formatting.Indented)}");
            _logger.LogInformation($"Http Host: {context.HttpContext.Request.Host}");
            _logger.LogInformation($"Http ContentType: {context.HttpContext.Request.ContentType}");
            _logger.LogInformation($"Http Method: {context.HttpContext.Request.Method}");
            _logger.LogInformation($"Http Query: {JsonConvert.SerializeObject(context.HttpContext.Request.Query, Formatting.Indented)}");
            _logger.LogInformation($"Http Body: {new System.IO.StreamReader(context.HttpContext.Request.Body).ReadToEndAsync().Result}");

            _logger.LogInformation("Http response info:");
            // Record http response info
            _logger.LogInformation($"Http status code: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation($"Http Header: {JsonConvert.SerializeObject(context.HttpContext.Response.Headers, Formatting.Indented)}");            
            _logger.LogInformation($"Http ContentType: {context.HttpContext.Response.ContentType}");
            _logger.LogInformation($"Http Method: {context.HttpContext.Request.Method}");
            _logger.LogInformation($"Http Query: {JsonConvert.SerializeObject(context.HttpContext.Request.Query, Formatting.Indented)}");
            _logger.LogInformation($"Http Body: {new System.IO.StreamReader(context.HttpContext.Request.Body).ReadToEndAsync().Result}");

            _logger.LogInformation("Result: ");
            // Record response result info
            _logger.LogInformation(JsonConvert.SerializeObject(JsonConvert.SerializeObject(context.Result, Formatting.Indented)));

            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("ClassFilter OnResultExecuting");

            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("ClassFilter OnResultExecuted");
            base.OnResultExecuted(context);
        }
    }
}
