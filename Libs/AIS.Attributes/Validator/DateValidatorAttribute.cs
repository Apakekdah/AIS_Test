using Hero;
using System;
using System.ComponentModel.DataAnnotations;

namespace AIS.Attributes.Validator
{
    public class DateValidatorAttribute : ValidationAttribute
    {
        private const string DefaultMessage = "The {0} for date {1:yyyy-MM-dd} is invalid, minimum date is {2:yyyy-MM-dd} and date format must be 'yyyy-MM-dd HH:mm:ss'";

        private readonly string errMsg;
        private readonly DateTime minDate;

        public DateValidatorAttribute() : this(0, null) { }

        public DateValidatorAttribute(int beforeToday) : this(beforeToday, null) { }

        public DateValidatorAttribute(int beforeToday, string errorMessage) : base(errorMessage)
        {
            minDate = DateTime.Now.AddDays(beforeToday).Date;
            errMsg = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult vr = ValidationResult.Success;
            if (value == null)
            {
                vr = new ValidationResult($"The {validationContext.MemberName} is Required", new[] { validationContext.MemberName });
            }
            else
            {
                DateTime dt = ((DateTime)value).Date;
                if (dt < minDate)
                {
                    if (errMsg.IsNullOrEmptyOrWhitespace())
                    {
                        vr = new ValidationResult(string.Format(DefaultMessage, validationContext.MemberName, dt, minDate), new[] { validationContext.MemberName });
                    }
                    else
                    {
                        vr = new ValidationResult(errMsg, new[] { validationContext.MemberName });
                    }
                }
            }
            return vr;
        }
    }
}