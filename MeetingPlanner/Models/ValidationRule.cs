using System;

namespace MeetingPlanner.Models
{
    public class ValidationRule<T>
    {
        public ValidationRule(Func<T, bool> validatePredicate, string errorText)
        {
            Validate = validatePredicate;
            ErrorText = errorText;
        }

        public string ErrorText { get; set; }
        public Func<T, bool> Validate;
    }
}