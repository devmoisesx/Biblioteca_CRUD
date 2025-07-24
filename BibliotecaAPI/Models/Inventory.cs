public class Inventory
{
    public string Id { get; set; }
    public TimeSpan CreatedAt { get; set; }
    public TimeSpan UpdatedAt { get; set; }
    public string Catalog_Id { get; set; }
    public int Condition { get; set; }
    public int Is_Avaible { get; set; }

    public Inventory()
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
    }

    public Inventory(string catalog_id, int condition, int is_Avaible)
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
        Catalog_Id = catalog_id;
        Condition = condition;
        Is_Avaible = is_Avaible;
    }

    public Inventory(string id, TimeSpan createdAt, TimeSpan updatedAt, string catalog_id, int condition, int is_Avaible)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Catalog_Id = catalog_id;
        Condition = condition;
        Is_Avaible = is_Avaible;
    }
}