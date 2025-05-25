namespace mottu_challenge.Dto.Response
{
    public class UserResponse
    {
        public int IdUser { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; } 
    }
}
