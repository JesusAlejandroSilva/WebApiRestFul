using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface IServicesAPI
    {
        Task<List<Aspirante>> Listar();

        Task<Aspirante> Buscar(int idAspirante);

        Task<bool> Guardar(Aspirante aspirante);

        Task<bool> Editar(Aspirante aspirante);

        Task<bool> Eliminar(int idAspirante);
    }
}
