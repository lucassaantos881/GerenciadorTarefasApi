using Microsoft.EntityFrameworkCore;
using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Context
{
    public class GerenciadorContext : DbContext
    {

        public GerenciadorContext(DbContextOptions<GerenciadorContext> options) : base(options)
        {
        }

        public DbSet<Projeto>? Projetos { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Tarefa>? Tarefas { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>()
              .Property(s => s.Status)
              .HasConversion<string>();

           modelBuilder.Entity<Tarefa>()
              .Property(t => t.DataPrazo)
              .HasColumnType("timestamp without time zone");

           modelBuilder.Entity<Projeto>()
               .Property(p => p.DataCriacao)
               .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Projeto>()
                .Property(p => p.DataConclusao)
                .HasColumnType("timestamp without time zone");
        }

    }
}
