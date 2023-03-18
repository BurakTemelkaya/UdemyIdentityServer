using System.ComponentModel.DataAnnotations;

namespace UdemyIdentityServerIdentityAPI.AuthServer.Models
{
    public class UserSaveViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Required]
        public string City { get; set; }
    }
}
