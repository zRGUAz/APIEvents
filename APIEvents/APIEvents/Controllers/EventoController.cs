using APIEvents.Filter;
using APIEvents.Service.Dto;
using APIEvents.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [TypeFilter(typeof(ExcecaoGeralFilter))]

    public class EventoController : ControllerBase
    {
        private IEventoService _eventoService;
        private IReservaService _reservaService;
        public EventoController(IEventoService eventoService, IReservaService reservaService)
        {
            _eventoService = eventoService;
            _reservaService = reservaService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<EventoDto>> CadastrarEvento([FromBody] EventoDto eventoDto)
        {
            if (!await _eventoService.CadastrarEvento(eventoDto))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(ConsultarEventosPorTitulo), eventoDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> EditarEvento([FromBody] EventoDto eventoDto, long idReservation)
        {
            if (!await _eventoService.EditarEvento(eventoDto, idReservation))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeletarOuInativarEvento(long idEvent)
        {
            if (!await _reservaService.EscolherDeletarOuInativarEvento(idEvent))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpGet("Por Titulo")]
        public async Task<ActionResult<EventoDto>> ConsultarEventosPorTitulo(string tituloEvento)
        {
            return Ok(await _eventoService.ConsultarEventosPorTitulo(tituloEvento));
        }

        [HttpGet("Por Local e Data")]
        public async Task<ActionResult<EventoDto>> ConsultarEventosPorLocal(string local, DateTime data)
        {
            return Ok(await _eventoService.ConsultarEventosPorLocal(local, data));
        }

        [HttpGet("Por Range de Preço e Data")]
        public async Task<ActionResult<EventoDto>> ConsultarEventosPorPreco(decimal precoMinimo, decimal precoMaximo, DateTime data)
        {
            return Ok(await _eventoService.ConsultarEventosPorPreco(precoMinimo, precoMaximo, data));
        }

    }
}
