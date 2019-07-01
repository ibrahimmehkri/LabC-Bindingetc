using System;
using System.Collections.Generic;
using XYZApp.Models;

namespace XYZApp.Data
{
    public class MySingleton
    {
        private static readonly MySingleton instance = new MySingleton();
        public List<MyMenuItem> MenuItems { get; }
        public User User { get; }

        private MySingleton()
        {
            User = new User
            {
                ImageUrl = "https://www.w3schools.com/w3images/avatar2.png",
                FullName = "Elon Musk",
                Followers = "2.4K",
                Following = "150",
                Posts = "175"
            };

            MenuItems = new List<MyMenuItem>();
            MenuItems.Add(new MyMenuItem
            {
                Text = "Bookmarks",
                ImageFileName = "bookmarks.jpg"
            });
            MenuItems.Add(new MyMenuItem
            {
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

        public static MySingleton Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
