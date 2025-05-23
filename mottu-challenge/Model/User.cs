using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace mottu_challenge.Model
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; } 

        [Required(ErrorMessage = "Username precisa ser preenchido")]
        [MaxLength(20, ErrorMessage = "Precisa ter no máximo 20 caracteres")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Email precisa ser preenchido")]
        [EmailAddress]
        public String Email { get; set; }

        [Required(ErrorMessage = "Senha precisa ser preenchida")]
        [MinLength(6, ErrorMessage = "Senha precisa ter no mínimo 6 caracteres")]
        public String Password { get; set; }


        public String FlagAtivo { get; set; }

        public DateTime CreatedAt { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
