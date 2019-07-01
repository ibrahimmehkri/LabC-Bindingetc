using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xamarin.Forms;
using XYZApp.Data;
using XYZApp.Models;

namespace XYZApp
{
    public class ViewModel
    {
        public List<MyMenuItem> MenuItems { get; set; }

        public User User { get; set; }

        public ViewModel()
        {
            User = MySingleton.Instance.User;
            MenuItems = MySingleton.Instance.MenuItems;
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
