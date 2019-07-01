using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace XYZApp
{
    public class ViewModel
    {
        public List<MenuItem> MenuItems { get; set; }

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

            MenuItems = new List<MenuItem>();
            MenuItems.Add(new MenuItem {
                Text = "Bookmarks",
                ImageFileName = "bookmarks.jpg"
            });
            MenuItems.Add(new MenuItem {
                Text = "Categories",
                ImageFileName = "categories.jpg"
            });
            MenuItems.Add(new MenuItem
            {
                Text = "Drafts",
                ImageFileName = "drafts.jpg"
            });
            MenuItems.Add(new MenuItem
            {
                Text = "Edit Profile",
                ImageFileName = "editProfile.jpg"
            });
            MenuItems.Add(new MenuItem
            {
                Text = "Messages",
                ImageFileName = "message.jpg"
            });
            MenuItems.Add(new MenuItem
            {
                Text = "Offers",
                ImageFileName = "offers.jpg"
            });

        }
    }

    public class User
    {
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public string Followers { get; set; }
        public string Following { get; set; }
        public string Posts { get; set; }
    }

    public class MenuItem
    {
        public string Text { get; set; }
        public string ImageFileName { get; set; }
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
