using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public interface ITarefaService
    {

        void AdicionarTarefa(Tarefa tarefa);
        void AtualizarTarefa(int id, Tarefa tarefa);
        void FinalizarTarefa(int id, string confirmacao);
        void DeletarTarefa(int id);
        IEnumerable<Tarefa> ObterTodasTarefas();
        Tarefa ObterTarefaPorId(int id);
        IEnumerable<Tarefa> ObterTarefaPorStatusPendente();







    }
}
