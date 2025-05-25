using mottu_challenge.Model;
using System.ComponentModel.DataAnnotations;

namespace mottu_challenge.Dto.Request
{
    public class UserRequest
    {

        public String Username { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public int RoleId { get; set; }

    }
}
