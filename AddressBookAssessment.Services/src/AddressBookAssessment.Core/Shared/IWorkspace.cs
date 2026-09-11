namespace AddressBookAssessment.Core.Shared;

public interface IWorkspace
{
	void Add<TEntity>(TEntity entity) where TEntity : class;

	void Remove<TEntity>(TEntity entity) where TEntity : class;

	Task<IReadOnlyList<TEntity>> Load<TEntity>(ISpecification<TEntity> spec, CancellationToken cancellationToken)
		where TEntity : class;
}