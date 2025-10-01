using System.ComponentModel.DataAnnotations;

namespace mottu_challenge.Dto.Request
{
    public class MotorcycleRequest
    {
        [Required(ErrorMessage = "O ano é obrigatório")]
        public int Year { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório")]
        public string Model { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória")]
        public string Plate { get; set; }
    }
}