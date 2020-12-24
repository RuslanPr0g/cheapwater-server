using System.ComponentModel.DataAnnotations;

namespace WEBApi.DTOs
{
    public class UserModelBase
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}