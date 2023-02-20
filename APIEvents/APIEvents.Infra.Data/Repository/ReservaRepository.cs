using APIEvents.Service.Entity;
using APIEvents.Service.Interface;
using Dapper;
using MySqlConnector;

namespace APIEvents.Infra.Data.Repository
{
    public class ReservaRepository : IReservaRepository
    {
        private string _stringConnection;
        public ReservaRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }

        public async Task<bool> CadastrarReserva(ReservaEntity reservaEntity)
        {
            string query = "INSERT INTO EventReservation (idEvent, personName, quantity) " +
                           "VALUES (@idEvent, @personName, @quantity)";

            DynamicParameters parametros = new(reservaEntity);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }

        public async Task<bool> EditarQuantidade(long quantity, long idReservation)
        {
            string query = "UPDATE EventReservation SET quantity = @quantity WHERE idReservation = @idReservation";

            DynamicParameters parametros = new();
            parametros.Add("quantity", quantity);
            parametros.Add("idReservation", idReservation);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }

        public async Task<bool> DeletarReserva(long idReservation)
        {
            string query = "DELETE FROM EventReservation where idReservation = @idReservation";

            DynamicParameters parametros = new();
            parametros.Add("idReservation", idReservation);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }

        public async Task<List<ReservaEntity>> ConsultarReserva(string nome, string tituloEvento)
        {
            string query = "SELECT * FROM CityEvent INNER JOIN EventReservation ON CityEvent.IdEvent = EventReservation.IdEvent WHERE PersonName = @nome AND Title LIKE @tituloEvento;";
            tituloEvento = $"%{tituloEvento}%";
            DynamicParameters parameters = new();
            parameters.Add("nome", nome);
            parameters.Add("tituloEvento", tituloEvento);
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            try
            {
                return (await conn.QueryAsync<ReservaEntity>(query, parameters)).ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> VerificarExistenciaDeReservas(long idEvent)
        {
            string query = "SELECT * FROM EventReservation WHERE idEvent = @idEvent";

            DynamicParameters parametros = new();
            parametros.Add("idEvent", idEvent);

            using MySqlConnection conn = new(_stringConnection);

            var result = await conn.QueryFirstOrDefaultAsync(query, parametros);

            if (result == null)
            {
                return true;
            }
            return false;
        }

    }
}
