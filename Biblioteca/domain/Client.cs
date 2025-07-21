public class Client
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Client(string name, string email, string phone, DateTime createdAt, DateTime updatedAt)
    {
        Id =
        Name = name;
        Email = email;
        Phone = phone;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}