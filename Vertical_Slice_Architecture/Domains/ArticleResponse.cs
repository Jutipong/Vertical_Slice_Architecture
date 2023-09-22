namespace Vertical_Slice_Architecture.Domains;

public class ArticleResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public DateTime CreateOnUtc { get; set; }
    public DateTime? PublishedOnUtc { get; set; }
}