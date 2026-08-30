using GerenciadorTarefasApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public ActionResult AdicionarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("O usuário não pode ser nulo.");
            }

            _usuarioService.AdicionarUsuario(usuario);
            return CreatedAtAction(nameof(AdicionarUsuario), new { id = usuario.UsuarioId }, usuario);
        }

        [HttpPut("atualizar-usuario/{id}")]
        public ActionResult AtualizarUsuario(int id, Usuario usuario)
        {
            if (usuario == null || id != usuario.UsuarioId)
            {
                return BadRequest("Dados do usuário inválidos.");
            }
           
            _usuarioService.AtualizarUsuario(id, usuario);

            return NoContent();
        }

        [HttpDelete("excluir-usuario/{id}")]
        public ActionResult ExcluirUsuario(int id)
        {
            _usuarioService.DeletarUsuario(id);
            return NoContent();
        }

        [HttpGet]
        public ActionResult ObterTodosUsuarios() { 
        
           var usuarios = _usuarioService.ObterTodosUsuarios();

            if(usuarios.Count() == 0)
            {
                return NotFound("Nenhum usuário encontrado.");
            }

            return Ok(usuarios);
        }

        [HttpGet("obter-usuario/{id}")]
        public ActionResult ObterUsuarioPorId(int id)
        {
            var usuario = _usuarioService.ObterUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(usuario);
        }
    }
}
