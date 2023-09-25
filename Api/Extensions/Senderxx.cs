namespace Api.Extensions;

public abstract class Senderxx : ISender
{
    private readonly SqlContext _db;
    public Senderxx()
    {
    }

    public abstract IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default);
    public abstract IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default);
    public abstract Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    public abstract Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest;
    public abstract Task<object?> Send(object request, CancellationToken cancellationToken = default);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _db.SaveChangesAsync(cancellationToken);
    }
}
