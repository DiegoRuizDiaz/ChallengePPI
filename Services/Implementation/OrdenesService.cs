using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System.Drawing;
using Utils.Enums;

namespace Services.Implementation
{
    public class OrdenesService : IOrdenesService
    {
        private readonly IOrdenesRepository _iOrdenesRepository;
        private readonly IActivosService _iActivosService;
        private readonly IMapper _mapper;

        private const decimal COMISION_BONO = 0.002m;
        private const decimal COMISION_ACCION = 0.006m;
        private const decimal IMPUESTOS = 0.21m;

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
            if(orden == null)
            {
                return false;
            }

            await _iOrdenesRepository.Update(ordenId, (int)estadoEnum);
            return true;
        }

        public async Task<OrdenesDTO?> Post(OrdenesRequestDTO ordenReqDTO)
        {
            //Valido si existe un activo con el IdCuenta enviado.
            var activoOrdenDTO = await this._iActivosService.GetById((int)ordenReqDTO.IdCuenta);

            if (activoOrdenDTO == null)
            {
                return null;
            }
            //Calcular Monto Total
            var montoTotal = await SeteoMontoTotal(ordenReqDTO, activoOrdenDTO);
              
            //Construir OrdenDTO y mappear a Entidad.
            var ordenDTO = ConstruirOrdenDTO(ordenReqDTO, activoOrdenDTO, montoTotal);
            var orden = _mapper.Map<Ordenes>(ordenDTO);

            orden = await _iOrdenesRepository.Post(orden);
            return _mapper.Map<OrdenesDTO>(orden);            
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
        public async Task<decimal> SeteoMontoTotal(OrdenesRequestDTO ordenReqDTO, ActivosDTO activoOrdenDTO)
        {
            decimal montoTotal = 0;
            try
            {
                //Logica para realizar los calculos del monto total dependiendo el tipo de activo.
                switch (activoOrdenDTO.TipoActivo)
                {
                    case TiposActivosEnum.Accion:
                        montoTotal = await this.CalcularMontoTotalAccion((int)ordenReqDTO.Cantidad, activoOrdenDTO.PrecioUnitario, COMISION_ACCION, IMPUESTOS);
                        break;

                    case TiposActivosEnum.Bono:
                        montoTotal = await this.CalcularMontoTotalBono((int)ordenReqDTO.Cantidad, ordenReqDTO.Precio, COMISION_BONO, IMPUESTOS);
                        break;

                    case TiposActivosEnum.FCI:
                        montoTotal = this.CalcularMontoTotalFCI((int)ordenReqDTO.Cantidad, ordenReqDTO.Precio);
                        break;
                }

                if (montoTotal == 0)
                {
                    throw new ArgumentNullException("MontoTotal", "No se pudo calcular el Monto Total.");
                }

                return montoTotal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<decimal> CalcularMontoTotalAccion(int cantidad, decimal precioUnitario, decimal comisionAccion, decimal impuesto)
        {
            try
            {
                decimal montoTotal = precioUnitario * cantidad;
                decimal comisionEImpuestos = await this.CalcularComisionEImpuestos(montoTotal, comisionAccion, impuesto);
                montoTotal = montoTotal + comisionEImpuestos;

                return montoTotal;
            }
            catch
            {
                return 0;
            }
        }
        public async Task<decimal> CalcularMontoTotalBono(int cantidad, decimal precio, decimal comisionBono, decimal impuesto)
        {
            try
            {
                decimal montoTotal = cantidad * precio;
                decimal comisionEImpuestos = await this.CalcularComisionEImpuestos(montoTotal, comisionBono, impuesto);

                montoTotal = montoTotal + comisionEImpuestos;

                return montoTotal;
            }
            catch
            {
                return 0;
            }
        }
        public decimal CalcularMontoTotalFCI(int cantidad, decimal precio)
        {
            try
            {
                decimal montoTotal = cantidad * precio;
                return montoTotal;
            }
            catch
            {
                return 0;
            }
        }
        public async Task<decimal> CalcularComisionEImpuestos(decimal montoTotal, decimal comision, decimal impuesto)
        {
            try
            {
                //calculo comisiones sobre el monto total
                decimal comisiones = montoTotal * comision;

                //calculo impuestos sobre las comisiones
                decimal impuestos = comisiones * impuesto;

                //devuelvo la suma de ambos para luego cacular monto final.
                decimal sumaFinal = comisiones + impuestos;
                return sumaFinal;
            }
            catch
            {
                return 0;
            }
        }
        public OrdenesDTO ConstruirOrdenDTO(OrdenesRequestDTO ordenReqDTO, ActivosDTO activoOrdenDTO, decimal montoTotal)
        {
            //Maximo 32 caracteres.
            if (activoOrdenDTO.Nombre.Length > 32)
            {
                activoOrdenDTO.Nombre = activoOrdenDTO.Nombre.Substring(0, 32);
            }
            
            //Instancio objeto OrdenDTO y asigno sus valores.
            return new OrdenesDTO
            {
                IdCuenta = ordenReqDTO.IdCuenta,
                Cantidad = ordenReqDTO.Cantidad,
                Precio = Math.Round(ordenReqDTO.Precio, 4),
                Operacion = ordenReqDTO.Operacion,
                Estado = EstadosEnum.EnProceso,
                NombreActivo = activoOrdenDTO.Nombre,
                MontoTotal = Math.Round(montoTotal, 4)  //solo 4 decimales.
            };
        }        

    }
}
