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

        public async Task<ActivosDTO?> GetByTicker(string ticker)
        {
            var activo = await _iActivosRepository.GetByTicker(ticker);
            return _mapper.Map<ActivosDTO>(activo);
        }
    }
}
