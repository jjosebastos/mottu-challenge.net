using System.ComponentModel.DataAnnotations;

namespace mottu_challenge.Model
{
    public class RoleDto
    {


        [Key]
        public int IdRole { get; set; }

        [Required(ErrorMessage = "Nome da Role ser preenchida")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Descrição da Role precisa ser preenchida")]
        public String RoleDescription { get; set; }



    }
}
