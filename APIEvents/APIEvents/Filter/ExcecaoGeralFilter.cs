using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace APIEvents.Filter
{
    public class ExcecaoGeralFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problema = new ProblemDetails
            {
                Title = "Erro inesperado",
                Detail = "Ocorreu um erro inesperado na solicitação",
                Type = context.Exception.GetType().Name
            };

            switch (context.Exception)
            {
                case Exception:
                    problema.Detail = "Ocorreu um erro no servidor";
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
            context.Result = new ObjectResult(problema);
        }
    }
}
