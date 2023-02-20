
using APIEvents.Service.Entity;

namespace APIEvents.Service.Interface
{
    public interface IReservaRepository
    {
        Task<bool> CadastrarReserva(ReservaEntity reservaEntity);
        Task<bool> EditarQuantidade(long quantity, long idReservation);
        Task<bool> DeletarReserva(long idReservation);
        Task<List<ReservaEntity>> ConsultarReserva(string nome, string tituloEvento);
        Task<bool> VerificarExistenciaDeReservas(long idEvent);
    }
}
