
using APIEvents.Service.Dto;
using APIEvents.Service.Entity;
using APIEvents.Service.Interface;
using AutoMapper;

namespace APIEvents.Service.Service
{
    public class ReservaService : IReservaService
    {
        private IReservaRepository _repository;
        private IEventoRepository _eventoRepository;
        private IMapper _mapper;
        public ReservaService(IReservaRepository repository, IEventoRepository eventoRepository, IMapper mapper)
        {
            _repository = repository;
            _eventoRepository = eventoRepository;
            _mapper = mapper;
        }

        public async Task<bool> CadastrarReserva(ReservaDto reserva)
        {
            ReservaEntity entidade = _mapper.Map<ReservaEntity>(reserva);
            return await _repository.CadastrarReserva(entidade);
        }

        public async Task<List<ReservaDto>> ConsultarReserva(string nome, string tituloEvento)
        {
            List<ReservaEntity> entidade = await _repository.ConsultarReserva(nome, tituloEvento);
            if (entidade == null)
            {
                return null;
            }
            List<ReservaDto> reservaDto = _mapper.Map<List<ReservaDto>>(entidade);
            return reservaDto;
        }

        public async Task<bool> DeletarReserva(long idReservation)
        {
            return await _repository.DeletarReserva(idReservation);
        }

        public async Task<bool> EditarQuantidade(long quantity, long idReservation)
        {
            return await _repository.EditarQuantidade(idReservation, quantity);
        }

        public async Task<bool> EscolherDeletarOuInativarEvento(long idEvent)
        {

            try
            {
                bool naoExisteReserva = await _repository.VerificarExistenciaDeReservas(idEvent);

                if (naoExisteReserva)
                {
                    return await _eventoRepository.DeletarEvento(idEvent);
                }
                return await _eventoRepository.InativarEvento(idEvent);
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
