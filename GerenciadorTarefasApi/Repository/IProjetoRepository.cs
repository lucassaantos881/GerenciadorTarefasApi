using GerenciadorTarefasCore.Models;
namespace GerenciadorTarefasApi.Repository
{
    public interface IProjetoRepository
    {
        Task<Projeto> ConcluirProjetoAsync(int id, string confirmacao, DateTime dataConclusao);
    }
}
