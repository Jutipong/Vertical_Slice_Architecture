namespace Application.Common.Behaviours;

public interface ICommand : IRequest, IValidator
{

}

public interface ICommand<TResponse> : IRequest<TResponse>, IValidator
{

}

public interface ICommandBase
{

}