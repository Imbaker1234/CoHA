namespace CoHAMVC
{
    using System;
    using System.Linq;
    using CoHAExceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Handles incoming and outgoing traffic to the Api. Providing
    /// logging and exception handling.
    /// </summary>
    public class IoFilter : IActionFilter, IExceptionFilter
    {
        /// <summary>
        /// <para>
        /// After the action has executed we want to determine if an
        /// unhandled exception has been thrown and respond appropriately
        /// if so.
        /// </para>
        /// <para>
        /// Afterwards we want to log the overall result of the call.
        /// </para>
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null && !context.ExceptionHandled)
            {
                context.Result = HandleException(context);
                context.ExceptionHandled = true;
                return;
            }

            // Log.Logger.Information(
            //     $"Request ID: '{context.HttpContext.TraceIdentifier.Split(':').First()}' : Successfully exited {context.ActionDescriptor.DisplayName}");
            //
            // Log.Logger.Debug($"Request ID: '{context.HttpContext.TraceIdentifier}' : Response -- {context.Result}");
        }

        /// <summary>
        /// In the event that we have an unhandled exception we should
        /// replace the current return with the appropriate HttpStatus
        /// Code.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private IActionResult HandleException(ActionExecutedContext context)
        {
            var ex = context.Exception;

            // Log.Logger.Information(
            //     $"Request ID: '{GetRequestId(context)}': {ex.GetType()} - {ex.Message} :: {ex.StackTrace}");

            if (ex.GetType() == typeof(NotFoundException))
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            if (ex.GetType() == typeof(ConflictException))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            else
            {
                //TODO Properly implement SeriLog stack trace logging.
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// In the event that Debug level logging is enabled, log the
        /// response body on exit from the Api.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Log.Logger.Information(
            //     $"Request ID: '{GetRequestId(context)}' : Entering {context.ActionDescriptor.DisplayName}");
            //
            // Log.Logger.Debug($"Request ID: '{GetRequestId(context)}': Response Body : {context.Result}");
        }

        /// <summary>
        /// Snuffs the Unhandled Exception status. If this is not done we can only
        /// return HTTP Status Code 500 and the Stack Trace which isn't necessarily
        /// consumer facing information.
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            //WHY THIS METHOD IS EMPTY:
            //We are using more in depth Exception Handling than a typical ExceptionFilter
            //would allow. This method is required by the IException Interface and the 
            //IException interface snuffs exceptions which occur within (They are all still
            //handled via the OnActionExecuted filter method) so that we can transform detailed
            //stack traces into more generic HTTP Status code responses.
            //
            //The typical exception would:
            //
            //1. Catch an exception, throwing up the appropriate custom exception (i.e a ConflictException)
            //2. That exception is snuffed by this method (So that we can return something other than Http 500).
            //3. The response is transformed via the HandleException method in the OnActionExecuted filter method.
            //4. The now consumer-facing-friendly response is returned to the caller.
        }

        /// <summary>
        /// <para>
        /// Provided here to allow the request ID to match every place it is used.
        /// </para>
        /// <para>
        /// Turning 0HLU0BP3G8B9M:00000001 into 0HLU0BP3G8B9M
        /// </para>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetRequestId(FilterContext context)
        {
            try
            {
                return context.HttpContext.TraceIdentifier.Split(':').First();
            }
            catch (NullReferenceException e)
            {
                //For testing or in some bizarre scenario where we don't otherwise have a 
                //valid HTTPContext with an incoming web request.
                return "HTTPContextNull";  
            }
        }
    }
}