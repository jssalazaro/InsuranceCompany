using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.WebApi.DTOs.UserDTO
{
    public class UserLoginRequestDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
