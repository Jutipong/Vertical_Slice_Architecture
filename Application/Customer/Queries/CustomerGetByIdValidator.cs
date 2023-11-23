namespace Application.Customer.Queries;

public class CustomerGetByIdCommand : Domain.Dtos.Customer.GetById, IRequest<Result<List<Entities.Customer>>> { }

public class CustomerGetByIdValidator { }