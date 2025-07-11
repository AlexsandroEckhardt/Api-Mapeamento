using NDD.Space.Base.Domain;

namespace NDD.Api.Mapeamento.Domain.Features.Mapeamento.Repositorio
{
    public interface IRepositorioMapeamento
    {
        Result<Exception, Unit> SalvarDocumento(object documento);
    }
}