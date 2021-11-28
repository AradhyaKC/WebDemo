using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebDemo.Validator
{
    public class LesserThanNow:ValidationAttribute
    {
        public int Years { get; set; }
        public int days { get; set; }
        private TimeSpan timespan { get; set; }
        protected override ValidationResult IsValid(object dateTime, ValidationContext validationContext)
        {
            timespan = new TimeSpan(Years * 365 + days, 0, 0,0);
            if (DateTime.Now.Subtract((DateTime)dateTime) > timespan)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        //protected override ValidationResult isValid(object value, ValidationContext context)
        //{
        //    if (DateTime.Now.Subtract(dateTime) > timespan)
        //    {
        //        return ValidationResult.Success;
        //    }else
        //    {
        //        return new ValidationResult(errorMessage);
        //    }
        //}
        //public static ValidationResult (DateTime date)
        //{

        //}
    }
}