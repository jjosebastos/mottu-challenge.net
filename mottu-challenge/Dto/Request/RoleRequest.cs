using System.ComponentModel.DataAnnotations;

namespace mottu_challenge.Dto.Request
{
    public class RoleRequest
    {
        [Required(ErrorMessage = "Campo RoleName obrigatório")]
        [MaxLength(20, ErrorMessage = "Campo RoleName deve ter no máximo 20 caracteres")]
        public String RoleName { get; set; }
        public String RoleDescription { get; set; }
    }
}
