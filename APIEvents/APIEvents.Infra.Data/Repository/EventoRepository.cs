using APIEvents.Service.Entity;
using APIEvents.Service.Interface;
using Dapper;
using MySqlConnector;

namespace APIEvents.Infra.Data.Repository
{
    public class EventoRepository : IEventoRepository
    {
        private string _stringConnection;
        public EventoRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }

        public async Task<bool> CadastrarEvento(EventoEntity eventoEntity)
        {
            string query = "INSERT INTO CityEvent (title, description, dateHourEvent, local, address, price, status)" +
                           "VALUES (@title, @description, @dateHourEvent, @local, @address, @price, true)";

            DynamicParameters parametros = new(eventoEntity);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }

        public async Task<bool> EditarEvento(EventoEntity eventoEntity, long idEvent)
        {
            string query = "UPDATE CityEvent SET title = @title, description = @description, dateHourEvent = @dateHourEvent, local = @local, " +
                           "address = @address, price = @price where idEvent = @idEvent";

            DynamicParameters parametros = new(eventoEntity);
            parametros.Add("idEvent", idEvent);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }

        public async Task<bool> DeletarEvento(long idEvent)
        {
            string query = "DELETE FROM CityEvent WHERE idEvent = @idEvent";

            DynamicParameters parametros = new();
            parametros.Add("idEvent", idEvent);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }

        public async Task<bool> InativarEvento(long idEvent)
        {
            string query = "UPDATE CityEvent set status = false WHERE IdEvent = @idEvent";

            DynamicParameters parametros = new();
            parametros.Add("idEvent", idEvent);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }

        public async Task<List<EventoEntity>> ConsultarEventosPorTitulo(string tituloEvento)
        {
            string query = "SELECT * FROM CityEvent where title like @tituloEvento";
            tituloEvento = $"%{tituloEvento}%";
            DynamicParameters parametros = new();
            parametros.Add("tituloEvento", tituloEvento);
            using MySqlConnection conn = new(_stringConnection);
            try
            {
                return (await conn.QueryAsync<EventoEntity>(query, parametros)).ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<EventoEntity>> ConsultarEventosPorLocal(string local, DateTime data)
        {
            string query = "SELECT * FROM CityEvent WHERE local = @local AND dateHourEvent = @data;";
            DynamicParameters parametros = new();
            parametros.Add("local", local);
            parametros.Add("data", data);
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            try
            {
                return (await conn.QueryAsync<EventoEntity>(query, parametros)).ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<EventoEntity>> ConsultarEventosPorPreco(decimal precoMinimo, decimal precoMaximo, DateTime data)
        {
            string query = "SELECT * FROM CityEvent WHERE price >= @precoMinimo AND price <= @precoMaximo AND dateHourEvent = @data";
            DynamicParameters parametros = new();
            parametros.Add("precoMinimo", precoMinimo);
            parametros.Add("precoMaximo", precoMaximo);
            parametros.Add("data", data);
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            try
            {
                return (await conn.QueryAsync<EventoEntity>(query, parametros)).ToList();
            }
            catch
            {
                return null;
            }
        }

    }
}
