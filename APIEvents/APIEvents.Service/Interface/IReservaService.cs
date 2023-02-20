
using APIEvents.Service.Dto;

namespace APIEvents.Service.Interface
{
    public interface IReservaService
    {
        Task<bool> CadastrarReserva(ReservaDto reserva);
        Task<List<ReservaDto>> ConsultarReserva(string nome, string tituloEvento);
        Task<bool> DeletarReserva(long idReservation);
        Task<bool> EditarQuantidade(long idReservation, long quantity);
        Task<bool> EscolherDeletarOuInativarEvento(long idEvent);
    }
}
