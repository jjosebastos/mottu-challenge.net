using mottu_challenge.Dto.Shared;

namespace mottu_challenge.Dto.Response
{
    public class UserResponse
    {
        public int IdUser { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
