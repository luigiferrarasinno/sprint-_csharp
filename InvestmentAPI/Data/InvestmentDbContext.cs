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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle("Data Source=oracle.fiap.com.br:1521/ORCL;User Id=RM98047;Password=201104;Connection Timeout=60;");
            }
            
            // Habilitar logging detalhado para debug
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Não usar schema - as tabelas estão no usuário atual
            // Configurar tabelas diretamente com os nomes das tabelas do Oracle
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS"); // Oracle guarda em maiúsculo
                entity.Property(e => e.Id).HasColumnName("ID").HasColumnType("NUMBER(10)")
                      .ValueGeneratedNever(); // Não gerar automaticamente - vamos fazer manualmente
                entity.Property(e => e.Name).HasColumnName("NAME").HasColumnType("NVARCHAR2(100)");
                entity.Property(e => e.Email).HasColumnName("EMAIL").HasColumnType("NVARCHAR2(100)");
                entity.Property(e => e.Phone).HasColumnName("PHONE").HasColumnType("NVARCHAR2(20)");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATEDAT").HasColumnType("TIMESTAMP").HasDefaultValueSql("SYSDATE");
                
                // Configurar constraint de email único
                entity.HasIndex(u => u.Email)
                      .IsUnique()
                      .HasDatabaseName("UQ_USERS_EMAIL");
            });

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.ToTable("INVESTMENTS"); // Oracle guarda em maiúsculo
                entity.Property(e => e.Id).HasColumnName("ID").HasColumnType("NUMBER(10)")
                      .ValueGeneratedNever(); // Não gerar automaticamente - vamos fazer manualmente
                entity.Property(e => e.Name).HasColumnName("NAME").HasColumnType("NVARCHAR2(100)");
                entity.Property(e => e.Type).HasColumnName("TYPE").HasColumnType("NVARCHAR2(50)");
                entity.Property(e => e.Amount).HasColumnName("AMOUNT").HasColumnType("NUMBER(18,2)");
                entity.Property(e => e.ExpectedReturn).HasColumnName("EXPECTEDRETURN").HasColumnType("NUMBER(5,2)");
                entity.Property(e => e.InvestmentDate).HasColumnName("INVESTMENTDATE").HasColumnType("TIMESTAMP").HasDefaultValueSql("SYSDATE");
                entity.Property(e => e.Description).HasColumnName("DESCRIPTION").HasColumnType("NVARCHAR2(500)");
                entity.Property(e => e.UserId).HasColumnName("USERID").HasColumnType("NUMBER(10)");
                
                // Configurar relacionamento explicitamente
                entity.HasOne(i => i.User)
                      .WithMany(u => u.Investments)
                      .HasForeignKey(i => i.UserId)
                      .HasConstraintName("FK_INVESTMENTS_USERS")
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
