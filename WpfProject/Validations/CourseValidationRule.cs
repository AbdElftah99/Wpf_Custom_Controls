using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using WpfProject.Model;

namespace WpfProject.Validations
{
    public class CourseValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            Course course = (value as BindingGroup).Items[0] as Course;
            if (course.StartDate > course.EndDate)
            {
                return new ValidationResult(false,
                    "Start Date must be earlier than End Date.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
