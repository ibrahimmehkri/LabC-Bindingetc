using System;
using System.Text.RegularExpressions;

namespace XYZApp.Validations
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }
        bool Check(T value);
    }

    class EmailValidationRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            if(value == null)
            {
                return false; 
            }

            string pattern = "([\\w\\.\\-]+)@([\\w\\-]+)((\\.\\w{2,3})+)";
            Match match = Regex.Match(value, pattern);
            return match.Success;
        }
    }

    class IsNotNullOrWhitespaceRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            if(value == null)
            {
                return false; 
            }

            return !string.IsNullOrWhiteSpace(value);
        }
    }

    class OnlyNumberInputRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            if(value == null || value.Length < 10)
            {
                return false;
            }

            double result;
            bool isValid = Double.TryParse(value, out result);
            return isValid; 
        }
    }

    class NoWhiteSpaceRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            if(value == null)
            {
                return false;
            }
            return !value.Contains(" ");
        }
    }
}
