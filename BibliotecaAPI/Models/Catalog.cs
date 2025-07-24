public class Catalog
{
    public string Id { get; set; }
    public TimeSpan CreatedAt { get; set; }
    public TimeSpan UpdatedAt { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public int Rev { get; set; }
    public string Publisher_Id { get; set; }
    public int Pages { get; set; }
    public string Synopsis { get; set; }
    public string Language_Id { get; set; }
    public int Is_Foreign { get; set; }

    public Catalog()
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
    }

    public Catalog(string title, string author, int year, int rev, string publisher_id, int pages, string synopsis, string language_id, int is_foreign)
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
        Title = title;
        Author = author;
        Year = year;
        Rev = rev;
        Publisher_Id = publisher_id;
        Pages = pages;
        Synopsis = synopsis;
        Language_Id = language_id;
        Is_Foreign = is_foreign;
    }

     public Catalog(string id, TimeSpan createdAt, TimeSpan updatedAt, string title, string author, int year, int rev, string publisher_id, int pages, string synopsis, string language_id, int is_foreign)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Title = title;
        Author = author;
        Year = year;
        Rev = rev;
        Publisher_Id = publisher_id;
        Pages = pages;
        Synopsis = synopsis;
        Language_Id = language_id;
        Is_Foreign = is_foreign;
    }
}