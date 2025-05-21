using System.ComponentModel.DataAnnotations;

namespace mottu_challenge.Model
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido")]
        public string Email { get; set; }
    }
}
