using AutoMapper;
using Domain.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementation
{
    public class ActivosService : IActivosService
    {
        private readonly IActivosRepository _iActivosRepository;
        private readonly IMapper _mapper;

        public ActivosService(IActivosRepository iActivosRepository, IMapper mapper)
        {
            _iActivosRepository = iActivosRepository;
            _mapper = mapper;
        }

        public async Task<ActivosDTO?> GetById(int id)
        {
            var activo = await _iActivosRepository.GetById(id);

            if (activo == null)
            {
                return null;
            }
            return _mapper.Map<ActivosDTO>(activo);
        }
    }
}
