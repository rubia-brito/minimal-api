using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Db
{
    public class DbContexto : DbContext
    {
        private IConfigurationRoot configuration;

        public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }

        public DbContexto(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
             if (!optionsBuilder.IsConfigured)
       {
        var stringConexao = configuration.GetConnectionString("mysql");

        optionsBuilder.UseMySql(
            stringConexao,
            ServerVersion.AutoDetect(stringConexao)
        );
       }
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Email)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(a => a.Senha)
                      .HasMaxLength(50);

                entity.Property(a => a.Perfil)
                      .HasMaxLength(10);

                entity.HasData(
                    new Administrador
                    {
                        Id= 1,
                        Email = "Administrador@teste.com",
                        Senha = "123456",
                        Perfil = "Adm"
                    }
                );
            });
        }
    }
}

