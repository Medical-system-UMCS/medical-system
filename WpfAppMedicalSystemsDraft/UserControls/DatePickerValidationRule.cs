using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Controls;

namespace WpfAppMedicalSystemsDraft.UserControls
{
    public class DatePickerValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var userInput = value as string;


            if (string.IsNullOrWhiteSpace(userInput))
                return new ValidationResult(false, "Podaj datę");

            // Email regex pattern for basic validation (you can adjust it as needed)
            var newRegex = new Regex("^[a-zA-Z]{2,40}$");

            if (!newRegex.IsMatch(userInput))
                return new ValidationResult(false, "Podaj poprawną datę");

            return ValidationResult.ValidResult;
        }
    }
}