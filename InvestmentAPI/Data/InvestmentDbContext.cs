using Microsoft.EntityFrameworkCore;
using InvestmentAPI.Models;

namespace InvestmentAPI.Data
{
    public class InvestmentDbContext : DbContext
    {
        public InvestmentDbContext(DbContextOptions<InvestmentDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Investment> Investments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relacionamento User -> Investments
            modelBuilder.Entity<Investment>()
                .HasOne(i => i.User)
                .WithMany(u => u.Investments)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar índices
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Seed de dados iniciais
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "João Silva",
                    Email = "joao@email.com",
                    Phone = "(11) 99999-1234",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 2,
                    Name = "Maria Santos",
                    Email = "maria@email.com",
                    Phone = "(11) 99999-5678",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 3,
                    Name = "Pedro Oliveira",
                    Email = "pedro@email.com",
                    Phone = "(11) 99999-9012",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Investments
            modelBuilder.Entity<Investment>().HasData(
                new Investment
                {
                    Id = 1,
                    Name = "Tesouro Selic",
                    Type = "Tesouro Direto",
                    Amount = 5000.00m,
                    ExpectedReturn = 12.5m,
                    InvestmentDate = DateTime.UtcNow.AddDays(-30),
                    Description = "Investimento em Tesouro Selic para reserva de emergência",
                    UserId = 1
                },
                new Investment
                {
                    Id = 2,
                    Name = "PETR4",
                    Type = "Ação",
                    Amount = 2500.00m,
                    ExpectedReturn = 15.0m,
                    InvestmentDate = DateTime.UtcNow.AddDays(-15),
                    Description = "Ações da Petrobras",
                    UserId = 1
                },
                new Investment
                {
                    Id = 3,
                    Name = "CDB Banco Inter",
                    Type = "CDB",
                    Amount = 10000.00m,
                    ExpectedReturn = 13.2m,
                    InvestmentDate = DateTime.UtcNow.AddDays(-45),
                    Description = "CDB com liquidez diária",
                    UserId = 2
                },
                new Investment
                {
                    Id = 4,
                    Name = "VALE3",
                    Type = "Ação",
                    Amount = 3000.00m,
                    ExpectedReturn = 18.0m,
                    InvestmentDate = DateTime.UtcNow.AddDays(-20),
                    Description = "Ações da Vale",
                    UserId = 2
                },
                new Investment
                {
                    Id = 5,
                    Name = "LCI Santander",
                    Type = "LCI",
                    Amount = 7500.00m,
                    ExpectedReturn = 11.8m,
                    InvestmentDate = DateTime.UtcNow.AddDays(-60),
                    Description = "Letra de Crédito Imobiliário isenta de IR",
                    UserId = 3
                }
            );
        }
    }
}
