using FluentValidation;
using System.Data;

namespace ApiDemo.Model.EmployeeValidator
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }

        
        public EmployeeValidator()
        {
            RuleFor(emp => emp.EmpName)
                .NotEmpty()
                .WithMessage("Employee Name is required")
                .Must(IsValidName).WithMessage("{PropertyName} should be all letters");
            RuleFor(emp => emp.EmpCode).NotEmpty().WithMessage("Employee Code is required");
            RuleFor(emp => emp.Email).NotEmpty().EmailAddress().WithMessage("Enter valid email address");
            RuleFor(emp => emp.Contact).NotEmpty().Length(10).WithMessage("Contact number should be of 10 digits");
            RuleFor(emp => emp.Salary)
                .NotEmpty().WithMessage("Salary is required")
                .InclusiveBetween(1000, 2500).WithMessage("Field value is out of range");
            RuleFor(emp => emp.EventDate)
                .GreaterThan(DateTime.Now).WithMessage("Event Date must be in the future");
            RuleFor(emp => emp.EventDate )
                .LessThan(DateTime.Now.AddDays(30)).WithMessage("Event Date must be within next 30 days");
            RuleFor(emp => emp.EventDate )
                .Must(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                .WithMessage("Event on weekends are not allowed");
            RuleFor(emp => emp.EventDate )
                .InclusiveBetween(DateTime.Now,DateTime.Now.AddMonths(1))
                .WithMessage("Event Date must be within next month");

        }
    }
}
