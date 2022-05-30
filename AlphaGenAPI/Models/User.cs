using System.ComponentModel.DataAnnotations;

namespace AlphaGenAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserInApp UserInApps { get; set; }
        public UserParams UserParams { get; set; }
        public ICollection<UserAlan> UserAlans { get; set; }
    }
}
