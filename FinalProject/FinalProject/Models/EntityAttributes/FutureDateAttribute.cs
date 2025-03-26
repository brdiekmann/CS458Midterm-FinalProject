using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.EntityAttributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public required string ErrorMessge { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime deadline)
            {
                if (deadline >= DateTime.Today.AddDays(7))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Deadline must be at least 7 days in the future.");
                }
            }
            else
            {
                return new ValidationResult("Invalid date.");
            }
        }
    }
}
