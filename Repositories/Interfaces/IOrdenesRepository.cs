using Domains.Entities;

namespace Repositories.Interfaces
{
    public interface IOrdenesRepository
    {
        Task<List<Ordenes>> GetAll();
        Task<Ordenes?> GetByOrdenId(int ordenId);
        Task<Ordenes> Update(int ordenId, int estado);
        Task<Ordenes> Post(Ordenes orden);
        Task<Ordenes> Delete(Ordenes orden);
    }
}
