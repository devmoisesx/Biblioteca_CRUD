public class Publisher
{
    public string Id { get; set; }
    public TimeSpan CreatedAt { get; set; }
    public TimeSpan UpdatedAt { get; set; }
    public string Name { get; set; }

    public Publisher()
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
    }

    // Construtor para quando instanciar a classe
    public Publisher(DateTime createdAt, DateTime updatedAt, string name)
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
        Name = name;
    }

    // Construtor usado para quando puxar dados do Db
    public Publisher(string id, TimeSpan createdAt, TimeSpan updatedAt, string name)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Name = name;
    }
}