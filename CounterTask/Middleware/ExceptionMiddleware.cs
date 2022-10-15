using System.Net;
using System.Text.Json;

namespace CounterTask.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Something Went wrong while processing {context.Request.Path}");
				await HandleExceptionAsync(context, ex);
			}
		}
		private Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			context.Response.ContentType = "application/json";
			HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
			var errorDetails = new ErrorDeatils
			{
				ErrorType = "Failure",
				ErrorMessage = ex.Message,
			};
			switch (ex)
			{
				case EntryPointNotFoundException:
					statusCode = HttpStatusCode.NotFound;
					errorDetails.ErrorType = "Not Found";
					break;
				case BadHttpRequestException badRequestException:
					statusCode = HttpStatusCode.BadRequest;
					errorDetails.ErrorType = "Bad Request";
					break;
				default:
					break;
			}

			string response = JsonSerializer.Serialize(errorDetails);
			context.Response.StatusCode = (int)statusCode;
			return context.Response.WriteAsync(response);
		}

		private class ErrorDeatils
		{
			public string ErrorType { get; set; }
			public string ErrorMessage { get; set; }
		}
	}
}
