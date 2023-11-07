using System.Collections.Generic;

namespace MiniBlog.Model
{
    public class User
    {
        public User()
        {
        }

        public User(string name, string email = "anonymous@unknow.com")
        {
            this.Name = name;
            this.Email = email;
        }

        public static string CollectionName { get; set; } = "User";

        public string Name { get; set; }

        public string Email { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            User other = (User)obj;

            return Name == other.Name && Email == other.Email;
        }
    }
}
