public class Loan
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Client_Id { get; set; }
    public string Inventory_Id { get; set; }
    public int Days_To_Expire { get; set; }
    public DateTime Returned_At { get; set; }

    public Loan(DateTime createdAt, DateTime updatedAt, string client_id, string inventory_id, int days_to_expire, DateTime returned_at)
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Client_Id = client_id;
        Inventory_Id = inventory_id;
        Days_To_Expire = days_to_expire;
        Returned_At = returned_at;
    }
}