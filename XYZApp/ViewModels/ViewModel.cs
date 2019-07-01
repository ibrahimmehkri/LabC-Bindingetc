using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xamarin.Forms;
using XYZApp.Models;

namespace XYZApp
{
    public class ViewModel
    {
        public List<MyMenuItem> MenuItems { get; set; }

        public User User { get; set; }

        public ViewModel()
        {
            User = new User {
                ImageUrl = "https://www.w3schools.com/w3images/avatar2.png",
                FullName = "Elon Musk",
                Followers = "2.4K",
                Following = "150",
                Posts = "175"
            };

            MenuItems = new List<MyMenuItem>();
            MenuItems.Add(new MyMenuItem {
                Text = "Bookmarks",
                ImageFileName = "bookmarks.jpg"
            });
            MenuItems.Add(new MyMenuItem {
                Text = "Categories",
                ImageFileName = "categories.jpg"
            });
            MenuItems.Add(new MyMenuItem
            {
                Text = "Drafts",
                ImageFileName = "drafts.jpg"
            });
            MenuItems.Add(new MyMenuItem
            {
                Text = "Edit Profile",
                ImageFileName = "editProfile.jpg"
            });
            MenuItems.Add(new MyMenuItem
            {
                Text = "Messages",
                ImageFileName = "message.jpg"
            });
            MenuItems.Add(new MyMenuItem
            {
                Text = "Offers",
                ImageFileName = "offers.jpg"
            });

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
