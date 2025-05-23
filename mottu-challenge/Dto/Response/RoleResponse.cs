namespace mottu_challenge.Dto.Response
{
    public class RoleResponse
    {
        public int IdRole { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string RoleDescription { get; set; } = string.Empty;
        public string FlagAtivo { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
