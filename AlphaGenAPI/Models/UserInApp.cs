using AlphaGenAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace AlphaGenAPI.Models
{
    public class UserInApp
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int Yas { get; set; }
        public string Salon { get; set; }
        public string Adres { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

}
}
