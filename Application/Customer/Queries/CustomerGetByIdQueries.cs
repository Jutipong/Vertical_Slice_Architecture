namespace Application.Customer.Queries;

public class CustomerGetByIdQueries : Domain.Dtos.Customer.CustomerGetById, IRequest<Result<List<Entities.Customer>>> { }

public class CustomerGetByIdValidator { }