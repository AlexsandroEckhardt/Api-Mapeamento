using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NDD.Api.Mapeamento.Base.Constantes;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Entidade;

namespace NDD.Api.Mapeamento.Infra.Data.Feature
{
    public class FeatureConfiguration : IEntityTypeConfiguration<NomeFeatureVo>
    {
        public void Configure(EntityTypeBuilder<NomeFeatureVo> builder)
        {
            builder.ToTable("Feature", TemplateConst.NomeSchema);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("FeatureId").IsRequired();
            builder.Property(c => c.Cnpj);
        }
    }
}