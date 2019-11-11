using Fatec.Alunos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fatec.Alunos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"conexão aqui");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .IsRequired(true);

                entity.Property(d => d.Documento)
                    .HasMaxLength(255)
                    .IsRequired(true);

                entity.Property(d => d.Ip)
                    .HasMaxLength(255)
                    .IsRequired(true);

                entity.Property(d => d.NomeComputador)
                    .HasMaxLength(255)
                    .IsRequired(true);

            });

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Fatec.Alunos.Models.Aluno> Aluno { get; set; }
    }
}
