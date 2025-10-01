using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mottu_challenge.Model
{
    public class Motorcycle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        [Required]
        [MaxLength(10)]
        public string Plate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string FlagAtivo { get; set; }
    }
}