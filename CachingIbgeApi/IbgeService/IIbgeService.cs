using CachingIbgeApi.Models;
using Refit;

namespace CachingIbgeApi.IbgeService;

public interface IIbgeService
{
    [Get("/api/v1/localidades/estados/{uf}/municipios")]
    Task<IEnumerable<Municipio>> ListaMunicipios(string uf);
}