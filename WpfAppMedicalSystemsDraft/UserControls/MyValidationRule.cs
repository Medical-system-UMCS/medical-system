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
    public class MyValidationRule : ValidationRule
    {

        public string Msg1 { get; set; }
        public string Msg2 { get; set; }

        public string MyRegex { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var userInput = value as string;


            if (string.IsNullOrWhiteSpace(userInput))
                return new ValidationResult(false, Msg1);

            // Email regex pattern for basic validation (you can adjust it as needed)
            var newRegex = new Regex(MyRegex);

            if (!newRegex.IsMatch(userInput))
                return new ValidationResult(false, Msg2);

            return ValidationResult.ValidResult;
        }
    }
}