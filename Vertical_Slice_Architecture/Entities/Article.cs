namespace Vertical_Slice_Architecture.Entities;

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public DateTime CreateOnUtc { get; set; }
    public DateTime? PublishedOnUtc { get; set; }
}
