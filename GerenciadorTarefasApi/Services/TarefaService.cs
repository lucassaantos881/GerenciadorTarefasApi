
using GerenciadorTarefasApi.Repository;
using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public class TarefaService : ITarefaService
    {

        private readonly IGerenciadorRepository<Tarefa> _tarefaRepository;
        private readonly ITarefaRepository _tarefaRepositoryEspecifico;

        private readonly IProjetoService _projetoServiceValidacao;

        public TarefaService(IGerenciadorRepository<Tarefa> tarefaRepository, ITarefaRepository tarefaRepositoryEspecifico, IProjetoService projetoServiceValidacao)
        {
            _tarefaRepository = tarefaRepository;
            _tarefaRepositoryEspecifico = tarefaRepositoryEspecifico;
            _projetoServiceValidacao = projetoServiceValidacao;
        }

        public async Task AdicionarTarefaAsync(Tarefa tarefa)
        {
            
                if (tarefa == null)
                {
                    throw new ArgumentNullException("A tarefa não pode ser nula.");
                }
                
                var projetoExistente = await _projetoServiceValidacao.ObterProjetoPorIdAsync(tarefa.ProjetoId);

                if(tarefa.DataPrazo < projetoExistente.DataCriacao)
                {
                    throw new ArgumentException("A data do prazo da tarefa não pode ser anterior à data de criação do projeto.");
                }

                await _tarefaRepository.AdicionarAsync(tarefa);

            
        }

        public async Task AtualizarTarefaAsync(int id, Tarefa tarefa)
        {
                if (id <= 0)
                {
                    throw new ArgumentOutOfRangeException("O ID da tarefa deve ser um valor positivo.");
                }

                var tarefaExistente = await _tarefaRepository.ObterPorIdAsync(id);

                if(tarefaExistente == null)
                {
                    throw new KeyNotFoundException("Tarefa não encontrada.");
                }

                tarefaExistente.Titulo = tarefa.Titulo;
                tarefaExistente.Descricao = tarefa.Descricao;   
                tarefaExistente.Status = tarefa.Status;
                tarefaExistente.DataPrazo = tarefa.DataPrazo;
                tarefaExistente.UsuarioId = tarefa.UsuarioId;
                tarefaExistente.ProjetoId = tarefa.ProjetoId;

                await _tarefaRepository.AtualizarAsync(id, tarefaExistente);

        }

        public async Task DeletarTarefaAsync(int id)
        {
                if (id <= 0)
                {
                    throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
                }

                await _tarefaRepository.DeletarAsync(id);

        }

        public async Task FinalizarTarefaAsync(int id, string confirmacao)
        {
           
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
                }

                if (confirmacao.ToUpper() == "SIM")
                {
                    confirmacao = confirmacao.ToUpper();
                    await _tarefaRepositoryEspecifico.FinalizarTarefaAsync(id, confirmacao);
                }
                else
                {
                    throw new ArgumentException("Não foi possível finalizar tarefa!");
                }
         
        }

        public async Task<IEnumerable<Tarefa>> ObterTodasTarefasAsync()
        {
            
            return await _tarefaRepository.ObterTodosAsync();

        }

        public async Task<Tarefa> ObterTarefaPorIdAsync(int id)
        {
            if (id < 0)
            {
              throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
            }

           return await _tarefaRepository.ObterPorIdAsync(id);

        }

        public async Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync()
        {
          
            return await _tarefaRepositoryEspecifico.ObterTarefaPorStatusPendenteAsync();

        }

    }
}
