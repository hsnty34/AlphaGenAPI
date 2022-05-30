using System.ComponentModel.DataAnnotations;

namespace AlphaGenAPI.Models
{
    public class UserParams
    {
        [Key]
        public int Id { get; set; }
        public int Old { get; set; }
        public double Weight { get; set; }
        public double Lenght { get; set; }
        public double Biceps { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
