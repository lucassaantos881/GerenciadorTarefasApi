using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public interface IProjetoService
    {

        void AdicionarProjeto(Projeto projeto);
        void AtualizarProjeto(int id, Projeto projeto);
        IEnumerable<Projeto> ObterTodosProjetos();
        Projeto ObterProjetoPorId(int id);
        void DeletarProjeto(int id);
        
       


    }
}
