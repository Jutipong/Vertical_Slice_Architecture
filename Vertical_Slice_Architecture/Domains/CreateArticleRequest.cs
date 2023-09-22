using Vertical_Slice_Architecture.Abstractions.Messaging;

namespace Vertical_Slice_Architecture.Domains;

public class CreateArticleRequest : ICommandBase
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
}