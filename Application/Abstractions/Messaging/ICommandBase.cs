namespace Application.Abstractions.Messaging;

public interface ICommand : IRequest, IValidator
{

}

public interface ICommand<TResponse> : IRequest<TResponse>, IValidator
{

}

public interface ICommandBase
{

}
