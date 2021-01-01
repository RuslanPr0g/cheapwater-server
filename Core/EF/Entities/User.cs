using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.DB.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [MaxLength(60)]
        public string Username { get; set; }
        [MaxLength(60)]
        [Required]
        public string Email { get; set; }
        [MaxLength(60)]
        [Required]
        public string Password { get; set; }
        [Required]
        public int Balance { get; set; } = 0;
    }
}
