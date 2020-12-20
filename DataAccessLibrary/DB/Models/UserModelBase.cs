using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.DB.Models
{
    public class UserModelBase
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}