using GerenciadorTarefasApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GerenciadorTarefasCore.Models;

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
        public async Task <ActionResult> AdicionarTarefa([FromBody] Tarefa tarefa)
        {
            if (tarefa == null)
            {
                return BadRequest("A tarefa não pode ser nula.");
            }

            await _tarefaService.AdicionarTarefaAsync(tarefa);
            return CreatedAtAction(nameof(AdicionarTarefa), new { id = tarefa.TarefaId }, tarefa);
        }

        [HttpPut ("atualizar-tarefa/{id}")]
        public async Task <ActionResult> AtualizarTarefa(int id, Tarefa tarefa)
        {
            if (tarefa == null || tarefa.TarefaId != id)
            {
                return BadRequest("A tarefa não pode ser nula e o ID deve corresponder.");
            }

            await _tarefaService.AtualizarTarefaAsync(id, tarefa);
            return NoContent();
        }

        [HttpPut ("finalizar-tarefa/{id}/{confirmacao}")]
        public async Task <ActionResult> FinalizarTarefa(int id, string confirmacao)
        {

            await _tarefaService.FinalizarTarefaAsync(id, confirmacao);

            return NoContent();

        }

        [HttpDelete("excluir-tarefa/{id}")]
        public async Task <ActionResult> ExcluirTarefa(int id)
        {
           
            await _tarefaService.DeletarTarefaAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task <ActionResult> ObterTodasTarefas()
        {
            var tarefas = await _tarefaService.ObterTodasTarefasAsync();

            if (tarefas.Count() == 0)
            {
                return NotFound("Nenhuma tarefa encontrada.");
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
