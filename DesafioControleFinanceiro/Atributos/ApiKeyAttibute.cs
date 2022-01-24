using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace DesafioControleFinanceiro.API.Atributos
{
    public class ApiKeyAttibute : Attribute, IAsyncActionFilter
    {
        private const string apiKey = "api_key";
        private const string apiKeyValor = "aXRhw7o=";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(apiKey, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey não foi encontrada"
                };
                return;
            }


            if (!apiKeyValor.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "ApiKey incorreta. Acesso não autorizado."
                };
                return;
            }

            await next();
        }
    }
}
