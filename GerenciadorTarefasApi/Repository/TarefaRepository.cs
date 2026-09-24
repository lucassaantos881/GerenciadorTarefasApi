using GerenciadorTarefasApi.Context;
using GerenciadorTarefasCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefasApi.Repository
{

    public class TarefaRepository : GerenciadorRepository<Tarefa>, ITarefaRepository
    {

        private readonly GerenciadorContext _context;

        public TarefaRepository(GerenciadorContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Tarefa> IniciarTarefaAsync(int idTarefa, int idUsuario)
        {
            var tarefaExistente = _context.Tarefas.FirstOrDefault(t => t.TarefaId == idTarefa);

            if (tarefaExistente == null)
            {
                throw new KeyNotFoundException($"Tarefa com ID: {idTarefa} não encontrada para iniciar!!");


            }
            else
            {
                var usuarioExistente = _context.Tarefas.FirstOrDefault(u => u.UsuarioId == idUsuario);

                if (usuarioExistente == null)
                {
                    throw new KeyNotFoundException($"Usuário não vinculado a tarefa para iniciar!!");
                }

                tarefaExistente.IniciarTarefa(usuarioExistente.UsuarioId);
                _context.Update(tarefaExistente);
                await _context.SaveChangesAsync();

                return tarefaExistente;
            }
        }


        public async Task<Tarefa> CancelarTarefaAsync(int id,string confirmacao)
        {
            var cancelarTarefa = _context.Tarefas.FirstOrDefault(t => t.TarefaId == id);

            if(cancelarTarefa == null)
            {
                throw new KeyNotFoundException($"Tarefa com ID: {id} não encontrada para cancelar!!");
            }

            if(confirmacao == "SIM")
            {
                cancelarTarefa.CancelarTarefa(confirmacao);
                _context.Update(cancelarTarefa);
                await _context.SaveChangesAsync();

                
            }

            return cancelarTarefa;
        }

        public async Task<Tarefa> FinalizarTarefaAsync(int id, string confirmacao)
        {
            var finalizarTarefa = _context.Tarefas?.FirstOrDefault(t => t.TarefaId == id);

            if (finalizarTarefa == null)
            {
                throw new KeyNotFoundException($"Tarefa com ID: {id} não encontrada para finalizar!.");
            }

            if(confirmacao == "SIM")
            {
                finalizarTarefa.FinalizarTarefa(confirmacao);
                _context.Update(finalizarTarefa);
                await _context.SaveChangesAsync();
            }
           
            return finalizarTarefa;
        }

        public async Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync()
        {
            return await _context.Tarefas.Where(t => t.Status == StatusTarefa.Pendente).ToListAsync();
        }
    }
}
