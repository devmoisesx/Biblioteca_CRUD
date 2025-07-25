public class Genrer
{
    public string Id { get; set; }
    public TimeSpan CreatedAt { get; set; }
    public TimeSpan UpdatedAt { get; set; }
    public string Value { get; set; }

    public Genrer()
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
    }

    // Construtor para quando instanciar a classe
    public Genrer(string value)
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
        Value = value;
    }

    // Construtor usado para quando puxar dados do Db
    public Genrer(string id, TimeSpan createdAt, TimeSpan updatedAt, string value)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Value = value;
    }
}