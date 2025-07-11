using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NDD.Api.Mapeamento.Base.Constantes;
using NDD.Api.Mapeamento.Domain.Features.Layout.Entidade;

namespace NDD.Api.Mapeamento.Infra.Data.Feature
{
    public class LayoutConfiguration : IEntityTypeConfiguration<MapeamentoVo>
    {
        public void Configure(EntityTypeBuilder<MapeamentoVo> builder)
        {
            builder.ToTable("Feature", TemplateConst.NomeSchema);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("FeatureId").IsRequired();
            builder.Property(c => c.Cnpj);
        }
    }
}