using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.DB.Entities
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        [MaxLength(60)]
        public string Username { get; set; }
        [MaxLength(60)]
        [Required]
        public string Email { get; set; }
        [MaxLength(60)]
        [Required]
        public string Password { get; set; }
        public string Nickname { get; set; }
    }
}
