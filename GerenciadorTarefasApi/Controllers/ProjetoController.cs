using GerenciadorTarefasApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GerenciadorTarefasCore.DTO_s;
using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {

        private readonly IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProjetoDto projetoDto)
        {

            if (projetoDto == null)
            {
                return BadRequest("Não foi possível adicionar o projeto");

            }

            var projetoCriado = await _projetoService.AdicionarProjetoAsync(projetoDto);
            return CreatedAtAction(nameof(Post), new { id = projetoCriado.ProjetoId }, projetoCriado);


        }

      
        [HttpPut("atualizar-projeto/{id}")]
        public async Task<ActionResult> Put(int id, ProjetoDto projetoDto)
        {
            
            if (projetoDto == null)
            {
                    return BadRequest("O projeto não pode ser nulo");
            }

           await _projetoService.AtualizarProjetoAsync(id, projetoDto);
           return NoContent();

         
        }

        [HttpPut("finalizar-projeto/{id},{confirmacao}/{dataConclusao}")]
        public async Task<ActionResult> FinalizarProjetoAsync(int id, string confirmacao, DateTime dataConclusao)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            await _projetoService.FinalizarProjeto(id, confirmacao, dataConclusao);
            return NoContent();
        }

        
        [HttpDelete("excluir-projeto/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            
             if (id < 0)
             {
                return BadRequest();
             }

             await _projetoService.DeletarProjetoAsync(id);
             return NoContent();
 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projeto>>> GetProjetos()
        {
            var projetos = await _projetoService.ObterTodosProjetosAsync();

            if(projetos == null)
            {
                return NotFound("Projetos não encontrados");
            }

            return Ok(projetos);
        }

        
        [HttpGet("obter-projeto/{id}")]
        public async Task <ActionResult<Projeto>> GetProjetoPorId(int id)
        {
            var projeto = await _projetoService.ObterProjetoPorIdAsync(id);

            if (projeto == null)
            {
                return NotFound();
            }

            return Ok(projeto);


        }
    }
}
