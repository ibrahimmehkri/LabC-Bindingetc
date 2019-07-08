using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace XYZApp
{
    public class RoughVM : INotifyPropertyChanged
    {
        public RoughVM()
        {
           
        }

        public event PropertyChangedEventHandler PropertyChanged;


    }

    class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);
            entry.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry);
            entry.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            double result;
            bool isValid = Double.TryParse(args.NewTextValue, out result);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }

    class IsFocusedBehavior : Behavior<Entry>
    {
        private Entry entry;

        protected override void OnAttachedTo(Entry bindable)
        {
            entry = ((Entry)bindable);
            entry.PropertyChanged += OnPropertyChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            entry.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if(args.PropertyName == "IsFocused")
            {
                entry.Scale = entry.IsFocused ? 1.5 : 1; 
            }
        }
    }

    class HasEmailFormatEntryBehavior : Behavior<Entry>
    {
        private Entry entry;

        //Creating IsValid as a read only bindable property.
        private static BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(HasEmailFormatEntryBehavior), false);
        public static BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
        public bool IsValid
        {
            get
            {
                return (bool)GetValue(HasEmailFormatEntryBehavior.IsValidProperty);
            }
            set
            {
                SetValue(IsValidPropertyKey, value);
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            entry = bindable;
            entry.TextChanged += OnEntryTextChanged; 
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            entry.TextChanged -= OnEntryTextChanged;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = IsInValidFormat(e.NewTextValue);
            entry.TextColor = IsValid ? Color.Default : Color.Red;
        }

        private bool IsInValidFormat(string text)
        {
            string pattern = "([\\w\\.\\-]+)@([\\w\\.\\-]+)((\\.(\\w){2,3})+)";
            Match match = Regex.Match(text, pattern);
            return match.Success; 
        }
    }

    class BoolToStringConverter : IValueConverter
    {
        public string TrueObject { get; set; }
        public string FalseObject { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("Value received in converter is: {0}", (bool)value); 
            return ((bool)value) ? TrueObject : FalseObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    class AltEntry : Entry
    {
        public AltEntry()
        {
            TextChanged += OnEntryTextChanged;
        }

        private static BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(HasEmailFormatEntryBehavior), false);
        public static BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
        public bool IsValid
        {
            get
            {
                return (bool)GetValue(HasEmailFormatEntryBehavior.IsValidProperty);
            }
            set
            {
                SetValue(IsValidPropertyKey, value);
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = IsInValidFormat(e.NewTextValue);
            TextColor = IsValid ? Color.Default : Color.Red;
        }

        private bool IsInValidFormat(string text)
        {
            string pattern = "([\\w\\.\\-]+)@([\\w\\.\\-]+)((\\.(\\w){2,3})+)";
            Match match = Regex.Match(text, pattern);
            return match.Success;
        }
    }

    class ToggleBehavior : Behavior<View>
    {
        TapGestureRecognizer tapGestureRecognizer;

        public static BindableProperty IsTappedProperty = BindableProperty.Create("IsTapped", typeof(bool), typeof(ToggleBehavior), false);
        public bool IsTapped
        {
            get { return (bool)GetValue(IsTappedProperty);  }
            set { SetValue(IsTappedProperty, value);  }
        }

        protected override void OnAttachedTo(View view)
        {
            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTap;
            view.GestureRecognizers.Add(tapGestureRecognizer);
        }

        
        protected override void OnDetachingFrom(View view)
        {
            view.GestureRecognizers.Remove(tapGestureRecognizer);
            tapGestureRecognizer.Tapped -= OnTap;
        }

        private void OnTap(object sender, EventArgs e)
        {
            IsTapped = !IsTapped; 
        }

    }
}
