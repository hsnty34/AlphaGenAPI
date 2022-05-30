using System.ComponentModel.DataAnnotations;

namespace AlphaGenAPI.Models
{
    public class Hareket
    {
        [Key]
        public int HareketId { get; set; }
        public string Ad { get; set; }
        public string Target { get; set; }
         public ICollection<AlanHareket> AlanHarekets { get; set; }

    }
}
