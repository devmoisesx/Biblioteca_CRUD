using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Client
    {   
        [Key]
        public string Id { get; set; } = Ulid.NewUlid().ToString();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}