namespace ContactApp.Models
{
    public class Contact
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public string Surname { get; set; } = string.Empty;
        public string? ImagePath { get; set; } 
        public string Phone { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 

        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
