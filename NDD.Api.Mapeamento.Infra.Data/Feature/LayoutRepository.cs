using NDD.Api.Mapeamento.Domain.Features.Layout.Entidade;
using NDD.Api.Mapeamento.Domain.Features.Layout.Repositorio;
using NDD.Api.Mapeamento.Infra.Data.Contexts;
using NDD.Space.Base.Database.Repositorio;
using NDD.Space.Base.Domain;

namespace NDD.Api.Mapeamento.Infra.Data.Feature
{
    public class LayoutRepository : RepositorioBase<MapeamentoVo, TemplateDbContext>, IRepositorioLayout
    {
        public LayoutRepository(TemplateDbContext context) : base(context)
        {
        }

        public Result<Exception, Unit> SalvarDocumento(object documento)
        {
            throw new NotImplementedException();
        }
    }
}