using System.Collections.Generic;

namespace ContactApp.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string Username { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty; 

        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
