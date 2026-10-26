using GerenciadorTarefasApi.Pagination;
using GerenciadorTarefasApi.Services;
using GerenciadorTarefasCore.DTO_s;
using GerenciadorTarefasCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorTarefasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {

        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpPost]
        public async Task <ActionResult> AdicionarTarefa([FromBody] TarefaDto tarefaDto)
        {
            if (tarefaDto == null)
            {
                return BadRequest("A tarefa não pode ser nula.");
            }

            var tarefaCriada = await _tarefaService.AdicionarTarefaAsync(tarefaDto);
            return CreatedAtAction(nameof(AdicionarTarefa), new { id = tarefaCriada.TarefaId }, tarefaCriada);
        }

        [HttpPut ("atualizar-tarefa/{id}")]
        public async Task <ActionResult> AtualizarTarefa(int id, TarefaDto tarefaDto)
        {
            if (tarefaDto == null)
            {
                return BadRequest("A tarefa não pode ser nula");
            }

            await _tarefaService.AtualizarTarefaAsync(id, tarefaDto);
            return NoContent();
        }

        [HttpPut("iniciar-tarefa/{id}/{idUsuario}")]
        public async Task<ActionResult> IniciarTarefa(int id, int idUsuario)
        {
      
            await _tarefaService.IniciarTarefaAsync(id, idUsuario);
            return NoContent();
        }

        [HttpPut("cancelar-tarefa/{id}/{confirmacao}")]
        public async Task<ActionResult> CancelarTarefa(int id, string confirmacao)
        {
     
            await _tarefaService.CancelarTarefaAsync(id, confirmacao);
            return NoContent();
        }

        [HttpPut ("finalizar-tarefa/{id}/{confirmacao}")]
        public async Task <ActionResult> FinalizarTarefa(int id, string confirmacao)
        {
            if(confirmacao == null)
            {
                return BadRequest();
            }

            await _tarefaService.FinalizarTarefaAsync(id, confirmacao);

            return NoContent();

        }

        [HttpDelete("excluir-tarefa/{id}")]
        public async Task <ActionResult> ExcluirTarefa(int id)
        {
           
            await _tarefaService.DeletarTarefaAsync(id);
            return NoContent();
        }

        [HttpGet("paginacao")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefasPaginado([FromQuery] GerenciadorParameter parameter)
        {
            var tarefas = await _tarefaService.ObterTarefasPaginadoAsync(parameter);

            if (tarefas == null)
            {
                return NotFound("Tarefas não encontradas");
            }

            return Ok(tarefas);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTodosProjetos()
        {
            var tarefas = await _tarefaService.ObterTodasTarefasAsync();
            if (tarefas == null)
            {
                return NotFound("Tarefas não encontradas");
            }
            return Ok(tarefas);
        }


        [HttpGet("obter-pendentes")]
        public async Task <ActionResult<IEnumerable<Tarefa>>> ObterTarefasPendentes()
        {
            var tarefasPendentes = await _tarefaService.ObterTarefaPorStatusPendenteAsync();
            if (tarefasPendentes.Count() == 0)
            {
                return NotFound("Nenhuma tarefa pendente encontrada.");
            }
            return Ok(tarefasPendentes);
        }

        [HttpGet("obter-tarefa/{id}")]
        public async Task <ActionResult<Tarefa>> ObterTarefaPorId(int id)
        {
            var tarefa = await _tarefaService.ObterTarefaPorIdAsync(id);

            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            return Ok(tarefa);
        }
    }
}
