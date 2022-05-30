using System.ComponentModel.DataAnnotations;

namespace AlphaGenAPI.Models
{
    public class Alan
    {
        [Key]
        public int AlanId { get; set; }
        public string Ad { get; set; }
        public string Hedef { get; set; }
       
        public ICollection<UserAlan> UserAlans { get; set; }
        public ICollection<AlanHareket> AlanHarekets { get; set; }
       
    }
}
