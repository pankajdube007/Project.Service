using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    internal class DateRangeAttribute : ValidationAttribute
    {
        private DateTime _enddate = DateTime.Now.AddDays(1);
        private DateTime _startDate = new DateTime(2015, 04, 01);

        //public DateRangeAttribute(DateTime startdate, DateTime enddate)
        //{
        //    startdate = _startDate;
        //    enddate = _enddate;
        //}
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is DateTime))
            {
                throw new ValidationException("DateRange is valid for DateTime property only.");
            }
            else
            {
                var date = (DateTime)value;
                if ((_startDate <= date) && (date <= _enddate))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.MemberName));
                }
            }
        }
    }
}