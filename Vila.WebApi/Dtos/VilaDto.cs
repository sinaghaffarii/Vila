using System.ComponentModel.DataAnnotations;
using Vila.WebApi.ModelValidation;

namespace Vila.WebApi.Dtos
{
    public class VilaDto
    {
       
        public int VilaId { get; set; }
        [Required(ErrorMessage = "نام ویلا اجباری است.")]
        [MaxLength(255, ErrorMessage = "نام ویلا نباید بیش از 255 حرف باشد.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "نام استان اجباری است.")]
        [MaxLength(255, ErrorMessage = "نام استان نباید بیش از 255 حرف باشد.")]
        public string State { get; set; }
        [Required(ErrorMessage = "نام شهر اجباری است.")]
        [MaxLength(255, ErrorMessage = "نام شهر نباید بیش از 255 حرف باشد.")]
        public string Shahr { get; set; }
        [Required(ErrorMessage = "آدرس ویلا اجباری است.")]
        [MaxLength(500, ErrorMessage = "آدرس ویلا نباید بیش از 500 حرف باشد.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "وارد کردن شماره تماس الزامیست")]
        [MaxLength(11, ErrorMessage = "شماره تماس وارد شده نباید بیش از 11 کاراکتر باشد.")]
        [MinLength(11, ErrorMessage = "شماره تماس وارد شده نباید کمتر از 11 کاراکتر باشد.")]
        public string Mobile { get; set; }
        [Required]
        [DateValidation]
        public DateTime BuildDate { get; set; }
    }
}
 