using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;
using XYZApp.Data;
using XYZApp.Models;
using System.Linq;
using System.Text.RegularExpressions;
using XYZApp.Validations;

namespace XYZApp
{
    public class ViewModel : INotifyPropertyChanged
    {
        public List<MyMenuItem> MenuItems { get; set; }

        public User User { get; set; }

        private bool _isValid;
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

        private ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        private ValidatableObject<string> _name;
        public ValidatableObject<string> Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private ValidatableObject<string> _username;
        public ValidatableObject<string> Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        private ValidatableObject<string> _phonenumber;

        public event PropertyChangedEventHandler PropertyChanged;

        public ValidatableObject<string> Phonenumber
        {
            get
            {
                return _phonenumber; 
            }
            set
            {
                _phonenumber = value;
            }
        }

        public ICommand Command { get; set; }

        public ViewModel()
        {
            User = MySingleton.Instance.User;
            MenuItems = MySingleton.Instance.MenuItems;

            _email = new ValidatableObject<string>();
            _username = new ValidatableObject<string>();
            _phonenumber = new ValidatableObject<string>();
            _name = new ValidatableObject<string>();

            AddValidations();

            Command = new Command(async () => {
                bool isValid = Validate();
                if (isValid)
                {
                    UpdateUserData();
                    await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
                } else
                {
                    Debug.WriteLine("Error");
                }
                
            }); 
        }

        private void UpdateUserData()
        {
            User.EmailAddress = Email.Value;
            User.FullName = Name.Value;
            User.Phonenumber = Phonenumber.Value;
            User.Username = Username.Value;
        }

        private void AddValidations()
        {
            Email.Validations.Add(new IsNotNullOrWhitespaceRule { ValidationMessage = "Email address is required." });
            Email.Validations.Add(new EmailValidationRule { ValidationMessage = "Email address format is incorrect." });

            Name.Validations.Add(new IsNotNullOrWhitespaceRule { ValidationMessage = "Name is required." });

            Username.Validations.Add(new IsNotNullOrWhitespaceRule { ValidationMessage = "Username is required." });
            Username.Validations.Add(new NoWhiteSpaceRule { ValidationMessage = "Username cannot have whitespaces." });

            Phonenumber.Validations.Add(new IsNotNullOrWhitespaceRule { ValidationMessage = "Phone number is required." });
            Phonenumber.Validations.Add(new OnlyNumberInputRule { ValidationMessage = "Phone number can only contain numbers." });
        }

        private bool Validate()
        {
            bool isValidName = _name.Validate();
            bool isValidEmail = _email.Validate();
            bool isValidPhone = _phonenumber.Validate();
            bool isValidUsername = _username.Validate();

            return isValidName && isValidEmail && isValidPhone && isValidUsername; 
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

    public class StringToImageSourceConverter : IValueConverter
    {
        public string ImageFolderPath { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = ImageFolderPath + "." + (string)value;
            return ImageSource.FromResource(path);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    

}
