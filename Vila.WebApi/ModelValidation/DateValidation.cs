using System.ComponentModel.DataAnnotations;
using Vila.WebApi.Dtos;

namespace Vila.WebApi.ModelValidation
{
    public class DateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var vila = (VilaDto)validationContext.ObjectInstance;
            try
            {
                var date = vila.BuildDate;
                if(date > DateTime.Now)
                {
                    return new ValidationResult("تاریخ ساخت باید قبل از زمان حال باشد.");
                }
                return ValidationResult.Success;
            }catch
            {
                return new ValidationResult("تاریخ ساخت باید در فرمت 2024/04/08 باشد.");
            }
        }
    }
}
