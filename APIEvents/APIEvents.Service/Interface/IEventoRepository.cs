
using APIEvents.Service.Entity;

namespace APIEvents.Service.Interface
{
    public interface IEventoRepository
    {
        Task<bool> CadastrarEvento(EventoEntity eventoEntity);
        Task<bool> EditarEvento(EventoEntity eventoEntity, long idEvent);
        Task<bool> DeletarEvento(long idEvent);
        Task<bool> InativarEvento(long idEvent);
        Task<List<EventoEntity>> ConsultarEventosPorTitulo(string tituloEvento);
        Task<List<EventoEntity>> ConsultarEventosPorLocal(string local, DateTime data);
        Task<List<EventoEntity>> ConsultarEventosPorPreco(decimal precoMinimo, decimal precoMaximo, DateTime data);
    }
}
