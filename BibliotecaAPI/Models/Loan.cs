public class Loan
{
    public string Id { get; set; }
    public TimeSpan CreatedAt { get; set; }
    public TimeSpan UpdatedAt { get; set; }
    public string Client_Id { get; set; }
    public string Inventory_Id { get; set; }
    public int Days_To_Expire { get; set; }
    public DateTime Returned_At { get; set; }

    public Loan()
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
    }

    // Construtor para quando instanciar a classe
    public Loan(string client_id, string inventory_id, int days_to_expire, DateTime returned_at)
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
        Client_Id = client_id;
        Inventory_Id = inventory_id;
        Days_To_Expire = days_to_expire;
        Returned_At = returned_at;
    }

    // Construtor usado para quando puxar dados do Db
    public Loan(string id, TimeSpan createdAt, TimeSpan updatedAt, string client_id, string inventory_id, int days_to_expire, DateTime returned_at)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Client_Id = client_id;
        Inventory_Id = inventory_id;
        Days_To_Expire = days_to_expire;
        Returned_At = returned_at;
    }
}