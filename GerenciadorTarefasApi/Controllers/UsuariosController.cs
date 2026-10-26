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
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }   

        [HttpPost]
        public async Task <ActionResult> AdicionarUsuario([FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest("O usuário não pode ser nulo.");
            }

            var usuarioCriado = await _usuarioService.AdicionarUsuarioAsync(usuarioDto);

            return CreatedAtAction(nameof(AdicionarUsuario), new { id = usuarioCriado.UsuarioId }, usuarioCriado);
        }

        [HttpPut("atualizar-usuario/{id}")]
        public async Task <ActionResult> AtualizarUsuario(int id, UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest("Dados do usuário inválidos.");
            }
           
            await _usuarioService.AtualizarUsuarioAsync(id, usuarioDto);

            return NoContent();
        }

        [HttpDelete("excluir-usuario/{id}")]
        public async Task <ActionResult> ExcluirUsuario(int id)
        {
            await _usuarioService.DeletarUsuarioAsync(id);
            return NoContent();
        }

        [HttpGet("paginacao")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios([FromQuery] GerenciadorParameter parameter)
        {
            var usuarios = await _usuarioService.ObterUsuariosPaginadoAsync(parameter);

            if (usuarios == null)
            {
                return NotFound("Usuários não encontrados");
            }

            return Ok(usuarios);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetTodosUsuarios()
        {
            var usuarios = await _usuarioService.ObterTodosUsuariosAsync();
            if (usuarios == null)
            {
                return NotFound("Usuários não encontrados");
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
