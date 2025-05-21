using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace mottu_challenge.Model
{
    public class UserDto
    {

        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Usuário precisa ser preenchido")]
        [MaxLength(20, ErrorMessage = "Precisa ter no máximo 20 caracteres")]
        public String Username { get; set; }
        [Required(ErrorMessage = "")]

    }
}
