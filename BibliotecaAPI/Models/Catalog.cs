public class Catalog
{
    public string Id { get; set; }
    public TimeSpan CreatedAt { get; set; }
    public TimeSpan UpdatedAt { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public int Rev { get; set; }
    public int Publisher_Id { get; set; }
    public int Pages { get; set; }
    public string Synopsis { get; set; }
    public int Language_Id { get; set; }
    public int Is_Foreign { get; set; }

    public Catalog()
    {
        Id = Ulid.NewUlid().ToString();
        CreatedAt = DateTime.Now.TimeOfDay;
        UpdatedAt = DateTime.Now.TimeOfDay;
    }

    // Construtor para quando instanciar a classe
    public Catalog(string title, string author, int year, int rev, int publisher_id, int pages, string synopsis, int language_id, int is_foreign)
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

    // Construtor usado para quando puxar dados do Db
    public Catalog(string id, TimeSpan createdAt, TimeSpan updatedAt, string title, string author, int year, int rev, int publisher_id, int pages, string synopsis, int language_id, int is_foreign)
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
