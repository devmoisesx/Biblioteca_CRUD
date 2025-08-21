public class Inventory
{
    public string Id { get; set; }
    public TimeSpan CreatedAt { get; set; }
    public TimeSpan UpdatedAt { get; set; }
    public string Book_Id { get; set; }
    public int Condition { get; set; }
    public int Is_Avaible { get; set; }

    public Inventory()
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
    }

    // Construtor para quando instanciar a classe
    public Inventory(string book_id, int condition, int is_Avaible)
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
        Book_Id = book_id;
        Condition = condition;
        Is_Avaible = is_Avaible;
    }

    // Construtor usado para quando puxar dados do Db
    public Inventory(string id, TimeSpan createdAt, TimeSpan updatedAt, string book_id, int condition, int is_Avaible)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Book_Id = book_id;
        Condition = condition;
        Is_Avaible = is_Avaible;
    }
}