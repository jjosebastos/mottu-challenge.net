using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mottu_challenge.Model
{
    public class Role
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRole { get; set; }

        [Required(ErrorMessage = "Nome da Role ser preenchida")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Descrição da Role precisa ser preenchida")]
        public String RoleDescription { get; set; }

        public DateTime CreatedAt { get; set; }

        public String FlagAtivo { get; set; }
        public ICollection<User> Users { get; set; }


    }
}
