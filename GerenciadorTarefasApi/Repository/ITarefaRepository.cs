using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Repository
{
    public interface ITarefaRepository
    {
        //Repositorio específico para a entidade Tarefa.
        Task<Tarefa> IniciarTarefaAsync(int id, int idUsuario);
        Task<Tarefa> CancelarTarefaAsync(int id, string confirmacao);
        Task<Tarefa> FinalizarTarefaAsync(int id, string confirmacao);
        Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync();
        
        
    }
}
