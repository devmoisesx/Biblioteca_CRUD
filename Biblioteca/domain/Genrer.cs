public class Genrer
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Value { get; set; }

    public Genrer(string id, DateTime createdAt, DateTime updatedAt, string value)
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Value = value;
    }
}