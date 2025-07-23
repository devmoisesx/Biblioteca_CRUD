using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Client
    {
        [Key]
        public string Id { get; set; }
        public TimeSpan CreatedAt { get; set; }
        public TimeSpan UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Client()
        {
            Id = Ulid.NewUlid().ToString();
            CreatedAt = DateTime.Now.TimeOfDay;
            UpdatedAt = DateTime.Now.TimeOfDay;
        }

        public Client(string id, TimeSpan createdAt, TimeSpan updatedAt, string name, string email, string phone)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}