
using APIEvents.Service.Dto;
using APIEvents.Service.Entity;
using APIEvents.Service.Interface;
using AutoMapper;

namespace APIEvents.Service.Service
{
    public class EventoService : IEventoService
    {
        private IEventoRepository _repository;
        private IMapper _mapper;
        public EventoService(IEventoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CadastrarEvento(EventoDto evento)
        {
            EventoEntity entidade = _mapper.Map<EventoEntity>(evento);
            return await _repository.CadastrarEvento(entidade);
        }

        public async Task<List<EventoDto>> ConsultarEventosPorLocal(string local, DateTime data)
        {
            List<EventoEntity> entidade = await _repository.ConsultarEventosPorLocal(local, data);
            if (entidade == null)
            {
                return null;
            }

            List<EventoDto> eventoDto = _mapper.Map<List<EventoDto>>(entidade);
            return eventoDto;
        }

        public async Task<List<EventoDto>> ConsultarEventosPorPreco(decimal precoMinimo, decimal precoMaximo, DateTime data)
        {
            List<EventoEntity> entidade = await _repository.ConsultarEventosPorPreco(precoMinimo, precoMaximo, data);
            if (entidade == null)
            {
                return null;
            }
            List<EventoDto> eventoDto = _mapper.Map<List<EventoDto>>(entidade);
            return eventoDto;
        }

        public async Task<List<EventoDto>> ConsultarEventosPorTitulo(string tituloEvento)
        {

            List<EventoEntity> entidade = await _repository.ConsultarEventosPorTitulo(tituloEvento);
            if (entidade == null)
            {
                return null;
            }
            List<EventoDto> eventoDto = _mapper.Map<List<EventoDto>>(entidade);
            return eventoDto;
        }

        public async Task<bool> EditarEvento(EventoDto evento, long idEvent)
        {
            EventoEntity entidade = _mapper.Map<EventoEntity>(evento);
            return await _repository.EditarEvento(entidade, idEvent);
        }

    }
}
