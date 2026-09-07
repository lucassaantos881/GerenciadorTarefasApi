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
        public async Task <ActionResult> AdicionarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("O usuário não pode ser nulo.");
            }

            await _usuarioService.AdicionarUsuarioAsync(usuario);
            return CreatedAtAction(nameof(AdicionarUsuario), new { id = usuario.UsuarioId }, usuario);
        }

        [HttpPut("atualizar-usuario/{id}")]
        public async Task <ActionResult> AtualizarUsuario(int id, Usuario usuario)
        {
            if (usuario == null || id != usuario.UsuarioId)
            {
                return BadRequest("Dados do usuário inválidos.");
            }
           
            await _usuarioService.AtualizarUsuarioAsync(id, usuario);

            return NoContent();
        }

        [HttpDelete("excluir-usuario/{id}")]
        public async Task <ActionResult> ExcluirUsuario(int id)
        {
            await _usuarioService.DeletarUsuarioAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<Usuario>>> ObterTodosUsuarios() { 
        
           var usuarios = await _usuarioService.ObterTodosUsuariosAsync();

            if(usuarios.Count() == 0)
            {
                return NotFound("Nenhum usuário encontrado.");
            }

            return Ok(usuarios);
        }

        [HttpGet("obter-usuario/{id}")]
        public async Task <ActionResult<Usuario>> ObterUsuarioPorId(int id)
        {
            var usuario = await _usuarioService.ObterUsuarioPorIdAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(usuario);
        }
    }
}
