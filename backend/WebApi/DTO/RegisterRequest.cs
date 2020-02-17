using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class RegisterRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public int? CountryId { get; set; }

        [Required]
        public int? ProvinceId { get; set; }
    }
}