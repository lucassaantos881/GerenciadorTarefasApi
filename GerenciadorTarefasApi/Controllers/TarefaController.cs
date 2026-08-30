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
        public ActionResult AdicionarTarefa([FromBody] Tarefa tarefa)
        {
            if (tarefa == null)
            {
                return BadRequest("A tarefa não pode ser nula.");
            }

            _tarefaService.AdicionarTarefa(tarefa);
            return CreatedAtAction(nameof(AdicionarTarefa), new { id = tarefa.TarefaId }, tarefa);
        }

        [HttpPut ("atualizar-tarefa/{id}")]
        public ActionResult AtualizarTarefa(int id, Tarefa tarefa)
        {
            if (tarefa == null || tarefa.TarefaId != id)
            {
                return BadRequest("A tarefa não pode ser nula e o ID deve corresponder.");
            }

            var tarefaExistente = _tarefaService.ObterTarefaPorId(id);

            if (tarefaExistente == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            _tarefaService.AtualizarTarefa(id, tarefa);
            return NoContent();
        }

        [HttpPut ("finalizar-tarefa/{id}/{confirmacao}")]
        public ActionResult FinalizarTarefa(int id, string confirmacao)
        {
          
            var tarefaExistente = _tarefaService.ObterTarefaPorId(id);

            if (tarefaExistente == null)
            {
                return NotFound("Não foi possível finalizar a tarefa.");
            }

            return NoContent();

        }

        [HttpDelete("excluir-tarefa/{id}")]
        public ActionResult ExcluirTarefa(int id)
        {
           
            _tarefaService.DeletarTarefa(id);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tarefa>> ObterTodasTarefas()
        {
            var tarefas = _tarefaService.ObterTodasTarefas();

            if (tarefas.Count() == 0)
            {
                return NotFound("Nenhuma tarefa encontrada.");
            }

            return Ok(tarefas);
        }

        [HttpGet("obter-pendentes")]
        public ActionResult<IEnumerable<Tarefa>> ObterTarefasPendentes()
        {
            var tarefasPendentes = _tarefaService.ObterTarefaPorStatusPendente();
            if (tarefasPendentes.Count() == 0)
            {
                return NotFound("Nenhuma tarefa pendente encontrada.");
            }
            return Ok(tarefasPendentes);
        }

        [HttpGet("obter-tarefa/{id}")]
        public ActionResult<Tarefa> ObterTarefaPorId(int id)
        {
            var tarefa = _tarefaService.ObterTarefaPorId(id);

            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            return Ok(tarefa);
        }
    }
}
