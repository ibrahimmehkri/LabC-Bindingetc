using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace XYZApp.Validations
{
    public class ValidatableObject<T> : INotifyPropertyChanged
    {

        public ValidatableObject()
        {
            _isValid = false;
            _validations = new List<IValidationRule<T>>();
            _errors = new List<string>(); 
        }

        private List<IValidationRule<T>> _validations;
        public List<IValidationRule<T>> Validations
        {
            get
            {
                return _validations; 
            }
            set
            {
                _validations = value;
            }
        }

        private List<string> _errors;
        public List<string> Errors
        {
            get
            {
                return _errors; 
            }
            set
            {
                _errors = value;
            }
        }

        private T _value;
        public T Value
        {
            get
            {
                return _value; 
            }
            set
            {
                _value = value;
                Validate();
                OnPropertyChanged("Value");
            }
        }

        private bool _isValid;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsValid
        {
            get
            {
                return _isValid; 
            }
            set
            {
                _isValid = value;
                OnPropertyChanged("IsValid");
            }
        }

        public bool Validate()
        {
            Errors.Clear();

            var errorQuery = from validation in Validations
                             where !validation.Check(Value)
                             select validation.ValidationMessage;

            Errors = errorQuery.ToList();
            IsValid = !(Errors.Count > 0);
            return this.IsValid; 
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
