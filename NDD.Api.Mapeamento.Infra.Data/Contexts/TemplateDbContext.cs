using Microsoft.EntityFrameworkCore;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Entidade;
using NDD.Api.Mapeamento.Infra.Data.Feature;
using NDD.Space.Base.Database.Repositorio;

namespace NDD.Api.Mapeamento.Infra.Data.Contexts
{
    public class TemplateDbContext : DbContextBase<TemplateDbContext>
    {
        public TemplateDbContext(DbContextOptions<TemplateDbContext> options) : base(options)
        {
        }

        public DbSet<NomeFeatureVo> Empresa { get; set; }

        /// <summary>
        /// Método que é executado quando o modelo de banco de dados está sendo criado pelo EF.
        /// Útil para realizar configurações
        /// </summary>
        /// <param name="modelBuilder">É o construtor de modelos do EF</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());

            // Chama o OnModelCreating do EF para dar continuidade na criação do modelo
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false);
        }
    }
}