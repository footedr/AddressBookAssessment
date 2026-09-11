namespace AddressBookAssessment.Core.Shared;

public interface IUnitOfWork
{
	Task SaveChanges(CancellationToken cancellationToken);
}
