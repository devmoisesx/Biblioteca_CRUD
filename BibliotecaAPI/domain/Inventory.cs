public class Inventory
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Catalog_Id { get; set; }
    public int Condition { get; set; }
    public bool Is_Avaible { get; set; }

    public Inventory(DateTime createdAt, DateTime updatedAt, string catalog_id, int condition, bool is_Avaible)
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Catalog_Id = catalog_id;
        Condition = condition;
        Is_Avaible = is_Avaible;
    }
}