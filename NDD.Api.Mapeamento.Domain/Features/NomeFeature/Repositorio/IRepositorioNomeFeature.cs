using NDD.Space.Base.Domain;

namespace NDD.Api.Mapeamento.Domain.Features.NomeFeature.Repositorio
{
    public interface IRepositorioNomeFeature
    {
        Result<Exception, Unit> SalvarDocumento(object documento);
    }
}