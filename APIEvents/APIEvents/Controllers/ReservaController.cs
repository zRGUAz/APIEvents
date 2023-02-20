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

    public class ReservaController : ControllerBase
    {
        private IReservaService _reservaService;
        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReservaDto>> CadastrarReserva([FromBody] ReservaDto reservaDto)
        {
            if (!await _reservaService.CadastrarReserva(reservaDto))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(ConsultarReserva), reservaDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> EditarQuantidade(int NovaQuantidade, long idReservation)
        {
            if (!await _reservaService.EditarQuantidade(NovaQuantidade, idReservation))
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
        public async Task<IActionResult> DeletarReserva(long idReservation)
        {
            if (!await _reservaService.DeletarReserva(idReservation))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<ReservaDto>> ConsultarReserva(string NomeDaPessoa, string NomeDoEvento)
        {
            return Ok(await _reservaService.ConsultarReserva(NomeDaPessoa, NomeDoEvento));
        }

    }
}
