namespace BibliotecaAPI.Models
{
    public class Client
    {
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

        public Client(string name, string email, string phone)
        {
            Id = Ulid.NewUlid().ToString();
            CreatedAt = DateTime.Now.TimeOfDay;
            UpdatedAt = DateTime.Now.TimeOfDay;
            Name = name;
            Email = email;
            Phone = string.IsNullOrWhiteSpace(phone) ? null : phone;
        }

        public Client(string id, TimeSpan createdAt, TimeSpan updatedAt, string name, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("ID do cliente não pode ser nulo ou vazio.", nameof(id));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Nome do cliente não pode ser nulo ou vazio.", nameof(name));
            }

            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Name = name;
            Email = email;
            Phone = string.IsNullOrWhiteSpace(phone) ? null : phone;
        }

        
    }
}