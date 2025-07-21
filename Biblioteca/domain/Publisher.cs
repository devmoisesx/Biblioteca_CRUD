public class Publisher
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }

    public Publisher(string id, DateTime createdAt, DateTime updatedAt, string name)
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Name = name;
    }
}