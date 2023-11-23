namespace Application.Customer.Queries;

public class CustomerGetByIdCommand : Domain.Dtos.Customer.CustomerGetById, IRequest<Result<List<Entities.Customer>>> { }

public class CustomerGetByIdValidator { }