using System.Net;
using System.Text.Json;

namespace GerenciadorTarefasApi.Middleware
{
    public class ExceptionMiddleware
    {

        //Cria duas variáveis privadas para armazenar o próximo middleware e outra para os logs de erro
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        //Crio uma injeção de dependência para o próximo middleware e para o logger
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        //Método assincrono para invocar o próximo middleware e capturar possíveis exceções
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //garante que o próximo middleware seja chamado e que a requisição continue seu fluxo normal
                await _next(context);
            }catch(ArgumentException ex)
            {
                //captura a exceção e escreve no log de erro
                _logger.LogWarning($"Erro de validação: {ex.Message}");
                await EscreverResposta(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro inesperado: {ex.Message}");
                await EscreverResposta(context, HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado. Tente novamente mais tarde!");
            }
        }


        //Método privado para escrever a resposta de erro.
        private static async Task EscreverResposta(HttpContext context, HttpStatusCode status, string mensagem)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var resposta = new { error = mensagem };
            await context.Response.WriteAsync(JsonSerializer.Serialize(resposta));
        }
    }
}
