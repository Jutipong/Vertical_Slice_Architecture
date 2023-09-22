namespace Vertical_Slice_Architecture.Abstractions.Messaging;

public interface ICommand : IRequest, ICommandBase
{

}

public interface ICommand<TResponse> : IRequest<TResponse>, ICommandBase
{

}

public interface ICommandBase
{
}
