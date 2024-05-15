using Services.Models;
using Utils.Enums;

namespace Services.Interfaces
{
    public interface IOrdenesService
    {
        Task<List<OrdenesDTO>> GetAll();
        Task<OrdenesDTO?> GetByOrdenId(int ordenId);
        Task<bool> Update(int ordenId, EstadosEnum estadoEnum);
        Task<OrdenesDTO?> Post(OrdenesRequestDTO ordenReqDTO);
        Task<bool> Delete(int ordenId);
    }
}
