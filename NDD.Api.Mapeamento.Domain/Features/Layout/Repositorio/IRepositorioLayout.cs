using NDD.Space.Base.Domain;

namespace NDD.Api.Mapeamento.Domain.Features.Layout.Repositorio
{
    public interface IRepositorioLayout
    {
        Result<Exception, Unit> SalvarDocumento(object documento);
    }
}