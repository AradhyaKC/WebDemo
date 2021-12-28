using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebDemo.Validator
{
    public class LettersOnly: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string thisValue = (string)value;
            try
            {
                if (thisValue.All(char.IsLetter))
                {
                    return ValidationResult.Success;
                }
                else return new ValidationResult(ErrorMessage);
            }
            catch(Exception e)
            {
                return new ValidationResult(ErrorMessage); 
            }
            
           
        }
    }
}