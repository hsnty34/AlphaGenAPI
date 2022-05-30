namespace AlphaGenAPI.Models
{
    public class UserAlan
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int AlanId { get; set; }
        public Alan Alan { get; set; }
    }
}
