
using System.ComponentModel.DataAnnotations;

namespace APIEvents.Service.Dto
{
    public class ReservaDto
    {
        public decimal IdReservation { get; set; }

        [Required(ErrorMessage = "O IdEvent é obrigatório!")]
        [Range(0, double.MaxValue, ErrorMessage = "O IdEvent deve ser maior que zero!")]
        public decimal IdEvent { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome é obrigatório!")]
        [MaxLength(50, ErrorMessage = "Maximo de 50 caracteres!")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatoria!")]
        [Range(0, long.MaxValue, ErrorMessage = "A quantidade ser maior ou igual a zero!")]
        public long Quantity { get; set; }
    }
}
