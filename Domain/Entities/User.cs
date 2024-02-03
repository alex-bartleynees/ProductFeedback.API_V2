using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public User(string name, string username, string image)
        {
            Name = name;
            Username = username;
            Image = image;
        }
    }
}