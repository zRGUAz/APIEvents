
using APIEvents.Service.Dto;

namespace APIEvents.Service.Interface
{
    public interface IEventoService
    {
        Task<bool> CadastrarEvento(EventoDto evento);
        Task<List<EventoDto>> ConsultarEventosPorLocal(string local, DateTime data);
        Task<List<EventoDto>> ConsultarEventosPorPreco(decimal precoMinimo, decimal precoMaximo, DateTime data);
        Task<List<EventoDto>> ConsultarEventosPorTitulo(string tituloEvento);
        Task<bool> EditarEvento(EventoDto evento, long idEvent);
    }
}
