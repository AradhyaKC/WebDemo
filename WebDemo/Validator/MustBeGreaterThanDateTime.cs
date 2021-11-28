using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebDemo.Validator
{
    public class MustBeGreaterThanDateTime:ValidationAttribute
    {
        public string otherPropertyName { get; set; }
        public int days { get; set; }
        protected override ValidationResult IsValid(object value ,ValidationContext validationContext)
        {
            DateTime thisDate = (DateTime)value;
            var otherProperty =validationContext.ObjectType.GetProperty(otherPropertyName);
            DateTime otherDate= (DateTime)otherProperty.GetValue(validationContext.ObjectInstance, null);
            if (thisDate.Subtract(otherDate) >= new TimeSpan(days, 0, 0, 0))
            {
                return ValidationResult.Success;
            }
            else return new ValidationResult(ErrorMessage);
        }
    }
}