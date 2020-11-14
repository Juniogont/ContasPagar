using Microsoft.EntityFrameworkCore;
using TesteContasPagar.Models;

namespace TesteContasPagar.Dal
{
    public class ContasPagarContext : DbContext
    {        
        public ContasPagarContext(DbContextOptions<ContasPagarContext> opcoes) 
            : base(opcoes)
        {
        }
        public ContasPagarContext()
      : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(@"Server = DESKTOP-G9N4BMQ\SQLEXPRESS; Database = ContasPagarDb; Trusted_Connection = True; ");
        public DbSet<ContaPagar> ContaPagar { get; set; }
        public DbSet<RegraAtraso> RegraAtraso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContaPagar>().Property(p => p.ValorOriginal).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ContaPagar>().Property(p => p.ValorCorrigido).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<RegraAtraso>().Property(p => p.Multa).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<RegraAtraso>().Property(p => p.JurosDia).HasColumnType("decimal(18,2)");
        }
    }
}
