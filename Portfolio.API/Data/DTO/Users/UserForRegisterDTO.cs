using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Data.DTO.Users
{
    public class UserForRegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 4 , ErrorMessage ="Must meet password criteria")]
        public string Password { get; set; }
    }
}