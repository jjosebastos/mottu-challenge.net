using System.ComponentModel.DataAnnotations;

namespace mottu_challenge.Model
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "O token é obrigatório")]
        public string Token { get; set; }

        [Required(ErrorMessage = "A nova senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string NewPassword { get; set; }
    }
}
