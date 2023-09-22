using Application.Abstractions.Messaging;

namespace Application.Domains;

public class CreateArticleRequest : ICommandBase
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
}