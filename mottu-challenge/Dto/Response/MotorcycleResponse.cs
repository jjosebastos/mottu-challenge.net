using mottu_challenge.Dto.Shared;

namespace mottu_challenge.Dto.Response
{
    public class MotorcycleResponse
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}