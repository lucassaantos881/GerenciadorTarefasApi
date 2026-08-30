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
        public ActionResult Post([FromBody] Projeto projeto)
        {

            if (projeto == null)
            {
                return BadRequest("Não foi possível adicionar o projeto");

            }

            _projetoService.AdicionarProjeto(projeto);
            return CreatedAtAction(nameof(Post), new { id = projeto.ProjetoId }, projeto);


        }

        [HttpPut("atualizar-projeto/{id}")]
        public ActionResult Put(int id, Projeto projeto)
        {
           
            if (projeto == null || projeto.ProjetoId != id)
            {
                    return BadRequest("O projeto não pode ser nulo e o ID deve corresponder.");
            }

           _projetoService.AtualizarProjeto(id, projeto);
           return NoContent();

         
        }

        [HttpDelete("excluir-projeto/{id}")]
        public ActionResult Delete(int id)
        {
            
             if (id < 0)
             {
                return BadRequest();
             }

             _projetoService.DeletarProjeto(id);
             return NoContent();
 
        }

        [HttpGet]
        public ActionResult<IEnumerable<Projeto>> GetProjetos()
        {
            var projetos = _projetoService.ObterTodosProjetos().ToList();

            if (projetos.Count == 0)
            {
                return NotFound("Projetos não encontrados");

            }

            return Ok(projetos);
        }

        [HttpGet("obter-projeto/{id}")]
        public ActionResult<Projeto> GetProjetoPorId(int id)
        {
            var projeto = _projetoService.ObterProjetoPorId(id);

            if (projeto == null)
            {
                return NotFound();
            }

            return Ok(projeto);


        }
    }
}
