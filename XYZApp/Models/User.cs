using System;
namespace XYZApp.Models
{
    public class User
    {
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Phonenumber { get; set; }
        public string Followers { get; set; }
        public string Following { get; set; }
        public string Posts { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", Username, EmailAddress, Phonenumber);
        }
    }
}
