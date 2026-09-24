using GerenciadorTarefasApi.Context;
using GerenciadorTarefasCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefasApi.Repository
{
    public class ProjetoRepository : GerenciadorRepository<Projeto>, IProjetoRepository
    {
        private readonly GerenciadorContext _context;

        public ProjetoRepository(GerenciadorContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Projeto> ConcluirProjetoAsync(int id, string confirmacao, DateTime dataConclusao)
        {
            
            var projetoExistente = _context.Projetos.FirstOrDefault(p => p.ProjetoId == id);

            if(projetoExistente == null)
            {
                throw new KeyNotFoundException($"Projeto com ID {id} não encontrado.");
            }

            projetoExistente.FinalizarProjeto(confirmacao, dataConclusao);
            _context.Update(projetoExistente);
            await _context.SaveChangesAsync();

            return projetoExistente;
        }
    }
}
