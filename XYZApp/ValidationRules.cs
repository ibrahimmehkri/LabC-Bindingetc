using System;
using System.Text.RegularExpressions; 

namespace XYZApp
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }
        bool Check(T value);
    }

    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public IsNotNullOrEmptyRule()
        {
        }

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if(value == null)
            {
                return false;
            }

            var str = value as string;
            return !string.IsNullOrWhiteSpace(str);
            
        }
    }

    public class EmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if(value == null)
            {
                return false;
            }

            var str = value as string;
            var pattern = "([\\w\\.\\-]+)@([\\w\\.\\-]+)((\\.\\w{2,3})+)";
            Match match = Regex.Match(str, pattern);
            return match.Success; 
        }
    }
}
