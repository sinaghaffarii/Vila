using System.ComponentModel.DataAnnotations;

namespace Vila.WebApi.CustomerModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "وارد کردن شماره موبایل اجباری است")]
        [MaxLength(11, ErrorMessage = "شماره موبایل باید 11 رقمی باشد.")]
        [MinLength(11, ErrorMessage = "شماره موبایل باید 11 رقمی باشد.")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "کلمه عبور اجباری است.")]
        [MaxLength(90)]
        public string Pass { get; set; }    
    }
}
