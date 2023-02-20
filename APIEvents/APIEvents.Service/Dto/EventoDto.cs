
using System.ComponentModel.DataAnnotations;

namespace APIEvents.Service.Dto
{
    public class EventoDto
    {
        public decimal IdEvent { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Titulo é obrigatório!")]
        [MaxLength(50, ErrorMessage = "Maximo de 50 caracteres!")]
        public string Title { get; set; }

        [MaxLength(250, ErrorMessage = "Maximo de 250 caracteres!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Data é obrigatorio!")]
        [DataType(DataType.DateTime, ErrorMessage = "Data é obrigatorio!")]
        public DateTime DateHourEvent { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Local é obrigatório!")]
        [MaxLength(50, ErrorMessage = "Maximo de 50 caracteres!")]
        public string Local { get; set; }

        [MaxLength(75, ErrorMessage = "Maximo de 75 caracteres!")]
        public string Address { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Status é obrigatorio!")]
        public bool Status { get; set; }
    }
}
