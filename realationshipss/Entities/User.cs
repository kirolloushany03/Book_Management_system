using System.ComponentModel.DataAnnotations;

namespace realationshipss.Entities
{
    public class User: BaseEntity
    {
        [Key]
        public int Id { get; set; } 
        public string Email { get; set; } = null!;
        public bool EmailVerified { get; set; } = false;
        public string Role { get; set; } = "User";
        public string PasswordHash { get; set; } = null!;
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
    }
}
