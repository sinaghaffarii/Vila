using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vila.WebApi.Dtos
{
    public class LoginResultDto
    {
        public int CustomerId { get; set; }
        public string Mobile { get; set; }
        /// <summary>
        /// توکی احراز هویت
        /// </summary>
        public string JwtSecret { get; set; }
        public string Role { get; set; }
    }
}
