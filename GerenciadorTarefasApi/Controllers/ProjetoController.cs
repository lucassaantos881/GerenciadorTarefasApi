using GerenciadorTarefasApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> Post([FromBody] Projeto projeto)
        {

            if (projeto == null)
            {
                return BadRequest("Não foi possível adicionar o projeto");

            }

            await _projetoService.AdicionarProjetoAsync(projeto);
            return CreatedAtAction(nameof(Post), new { id = projeto.ProjetoId }, projeto);


        }

        [HttpPut("atualizar-projeto/{id}")]
        public async Task<ActionResult> Put(int id, Projeto projeto)
        {
           
            if (projeto == null || projeto.ProjetoId != id)
            {
                    return BadRequest("O projeto não pode ser nulo e o ID deve corresponder.");
            }

           await _projetoService.AtualizarProjetoAsync(id, projeto);
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
