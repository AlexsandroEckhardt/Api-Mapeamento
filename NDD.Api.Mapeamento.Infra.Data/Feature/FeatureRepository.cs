using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Entidade;
using NDD.Api.Mapeamento.Domain.Features.NomeFeature.Repositorio;
using NDD.Api.Mapeamento.Infra.Data.Contexts;
using NDD.Space.Base.Database.Repositorio;
using NDD.Space.Base.Domain;

namespace NDD.Api.Mapeamento.Infra.Data.Feature
{
    public class FeatureRepository : RepositorioBase<NomeFeatureVo, TemplateDbContext>, IRepositorioNomeFeature
    {
        public FeatureRepository(TemplateDbContext context) : base(context)
        {
        }

        public Result<Exception, Unit> SalvarDocumento(object documento)
        {
            throw new NotImplementedException();
        }
    }
}