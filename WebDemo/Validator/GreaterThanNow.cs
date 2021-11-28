using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebDemo.Validator
{
    public class GreaterThanNow: ValidationAttribute
    {
        public int Years { get; set; }
        private TimeSpan timespan { get; set; }
        public int days { get; set; }
        protected override ValidationResult IsValid(object dateTime, ValidationContext validationContext)
        {
            timespan = new TimeSpan(Years * 365, 0, 0,0);
            if (DateTime.Now.Subtract((DateTime)dateTime) < -timespan)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    };
};
