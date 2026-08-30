using GerenciadorTarefasApi.Context;
using GerenciadorTarefasCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefasApi.Services
{
    public class TarefaService : ITarefaService
    {

        private readonly GerenciadorContext _context;

        public TarefaService(GerenciadorContext context)
        {
            _context = context;
        }

        public void AdicionarTarefa(Tarefa tarefa)
        {
            try
            {
                if (tarefa == null)
                {
                    throw new ArgumentNullException("A tarefa não pode ser nula.");
                }
  

                _context.Add(tarefa);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw;
            }
        }

        public void AtualizarTarefa(int id, Tarefa tarefa)
        {

            try
            {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID da tarefa deve ser um valor positivo.");
                }

                var tarefaExistente = _context.Tarefas?.FirstOrDefault(t => t.TarefaId == id);

                tarefaExistente.Titulo = tarefa.Titulo;
                tarefaExistente.Descricao = tarefa.Descricao;
                tarefaExistente.Status = tarefa.Status;
                tarefaExistente.UsuarioId = tarefa.UsuarioId;

                _context.Update(tarefaExistente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw;
            }


        }

        public void DeletarTarefa(int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
                }

                var excluirTarefa = _context.Tarefas?.FirstOrDefault(t => t.TarefaId == id);

                _context.Remove(excluirTarefa);
                _context.SaveChanges();

            }catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw;
            }
           
             
        }

        public void FinalizarTarefa(int id, string confirmacao)
        {
            try
            {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
                }

                var finalizarTarefa = _context.Tarefas?.FirstOrDefault(t => t.TarefaId == id);

                if (finalizarTarefa == null)
                {
                    throw new KeyNotFoundException("Tarefa não encontrada para finalização!!");
                }

                if (confirmacao.ToUpper() == "SIM")
                {
                    finalizarTarefa.FinalizarTarefa(confirmacao);


                    _context.Update(confirmacao);
                    _context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Não foi possível finalizar tarefa!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Tarefa> ObterTodasTarefas()
        {
            
             var tarefas = _context.Tarefas?.AsNoTracking().Take(10).ToList();
             return tarefas;
       
        }

        public Tarefa ObterTarefaPorId(int id)
        {
            if (id < 0)
            {
              throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
            }

            var tarefaExistente = _context.Tarefas?.FirstOrDefault(t => t.TarefaId == id);

            return tarefaExistente;

            
        }

        public IEnumerable<Tarefa> ObterTarefaPorStatusPendente()
        {
          
              var statusTarefas = _context.Tarefas?.Include(t => t.Status == StatusTarefa.Pendente).AsNoTracking().Take(5).ToList(); ;

              return statusTarefas;
           
        }


      




    }
}
