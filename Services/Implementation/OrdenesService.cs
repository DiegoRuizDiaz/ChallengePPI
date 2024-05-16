using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Domains.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementation
{
    public class OrdenesService : IOrdenesService
    {
        private readonly IOrdenesRepository _iOrdenesRepository;
        private readonly IActivosService _iActivosService;
        private readonly IMapper _mapper;

        public OrdenesService(IOrdenesRepository iOrdenesRepository, IActivosService iActivosService, IMapper mapper)
        {
            this._iOrdenesRepository = iOrdenesRepository;
            this._iActivosService = iActivosService;

            this._mapper = mapper;
        }

        public async Task<List<OrdenesDTO>> GetAll()
        {
            var ordenes = await _iOrdenesRepository.GetAll();
            return _mapper.Map<List<OrdenesDTO>>(ordenes);
        }

        public async Task<OrdenesDTO?> GetByOrdenId(int id)
        {
            var orden = await _iOrdenesRepository.GetByOrdenId(id);
            return _mapper.Map<OrdenesDTO>(orden);
        }

        public async Task<bool> Update(int ordenId, EstadosEnum estadoEnum)
        {
            //Valido si la orden existe.
            var orden = await _iOrdenesRepository.GetByOrdenId(ordenId);
            if (orden == null)
            {
                return false;
            }

            //Valido que la orden este En Proceso.
            if (orden.Estado != (int)EstadosEnum.EnProceso)
            {
                throw new InvalidOperationException("La orden ya fue Ejecutada o Cancelada.");
            }

            await _iOrdenesRepository.Update(ordenId, (int)estadoEnum);
            return true;
        }

        public async Task<OrdenesDTO?> Post(OrdenesRequestDTO ordenReqDTO)
        {
            //Valido si existe un activo con el Ticker enviado.
            var activoDTO = await this._iActivosService.GetByTicker(ordenReqDTO.Ticker);
            if (activoDTO == null)
            {
                return null;
            }
            //Calculo Monto Total
            var montoTotal = await SeteoMontoTotal(ordenReqDTO, activoDTO);

            //Preparo OrdenDTO y mappeo a la Entidad.
            var ordenDTO = ConstruirOrdenDTO(ordenReqDTO, activoDTO, Math.Round(montoTotal,4));
            var ordenToPost = _mapper.Map<Ordenes>(ordenDTO);

            //Post
            ordenToPost = await _iOrdenesRepository.Post(ordenToPost);

            //Retorno la orden creada.
            return _mapper.Map<OrdenesDTO>(ordenToPost);
        }

        public async Task<bool> Delete(int ordenId)
        {

            var orden = await _iOrdenesRepository.GetByOrdenId(ordenId);
            if (orden == null)
            {
                return false;
            }
            await _iOrdenesRepository.Delete(orden);
            return true;
        }

        //Metodos
        public async Task<decimal> SeteoMontoTotal(OrdenesRequestDTO ordenReqDTO, ActivosDTO activoDTO)
        {
            IOrdenes orden = null;
            switch (activoDTO.TipoActivo)
            {
                case TiposActivosEnum.Accion:
                    if (ordenReqDTO.Precio != null)
                    {
                        throw new InvalidOperationException("No se puede enviar un precio para el tipo de activo que intenta grabar.");
                    }

                    orden = new Accion { Cantidad = ordenReqDTO.Cantidad, PrecioUnitario = activoDTO.PrecioUnitario };
                break;

                case TiposActivosEnum.FCI:
                    orden = new FCI { Cantidad = ordenReqDTO.Cantidad, PrecioUnitario = activoDTO.PrecioUnitario };
                break;

                case TiposActivosEnum.Bono:
                    if (ordenReqDTO.Precio == null || ordenReqDTO.Precio <= 0)
                    {
                        throw new InvalidOperationException("El precio para este tipo de activo es requerido y no puede ser menor o igual a 0.");
                    }

                    orden = new Bono { Cantidad = ordenReqDTO.Cantidad, Precio = (decimal)ordenReqDTO.Precio };
                break;

                default:
                break;
            }

            return orden.CalcularMontoTotal();
        }
        
        public OrdenesDTO ConstruirOrdenDTO(OrdenesRequestDTO ordenReqDTO, ActivosDTO activoDTO, decimal montoTotal)
        {
            if (activoDTO.TipoActivo != TiposActivosEnum.Bono)
            {
                ordenReqDTO.Precio = activoDTO.PrecioUnitario;
            }

            return new OrdenesDTO
            {
                IdCuenta = ordenReqDTO.IdCuenta,
                IdActivo = activoDTO.Id,
                NombreActivo = ordenReqDTO.NombreActivo,
                Cantidad = ordenReqDTO.Cantidad,
                Precio = ordenReqDTO.Precio,
                Operacion = ordenReqDTO.Operacion,
                Estado = EstadosEnum.EnProceso,
                MontoTotal = montoTotal,
            };
        }
    }
}
