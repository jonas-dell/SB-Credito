using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SB.Credito.Application.Commands;
using System.Net;

namespace SB.Credito.API.Configuration
{
    public static class ExceptionConfig
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var response = new CommandResponse("Erro ao processar a requisição");
                        await context.Response.WriteAsync(response.ToString());
                    }
                });
            });
        }
    }
}
