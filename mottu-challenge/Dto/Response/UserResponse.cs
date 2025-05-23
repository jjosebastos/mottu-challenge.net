namespace mottu_challenge.Dto.Response
{
    public class UserResponse
    {
        public int IdUser { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FlagAtivo { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int RoleId { get; set; } 
    }
}
