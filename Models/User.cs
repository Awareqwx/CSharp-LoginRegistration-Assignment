using System.ComponentModel.DataAnnotations;

namespace LoginRegistration.Models
{
    public class User
    {
        [Required]
        [MinLength(2)]
        public string firstName { get; set; }
        [Required]
        [MinLength(2)]
        public string lastName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}