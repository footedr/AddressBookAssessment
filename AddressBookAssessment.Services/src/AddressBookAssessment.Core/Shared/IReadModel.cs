namespace AddressBookAssessment.Core.Shared;

public interface IReadModel
{
	Task<IReadOnlyList<TResult>> Search<TEntity, TResult>(ISpecification<TEntity, TResult> spec, CancellationToken cancellationToken)
		where TEntity : class;

	Task<IReadOnlyList<TResult>> Search<T1, T2, TResult>(ISpecification<T1, T2, TResult> spec, CancellationToken cancellationToken)
		where T1 : class
		where T2 : class;

	Task<int> Count<TEntity, TResult>(ISpecification<TEntity, TResult> spec, CancellationToken cancellationToken)
		where TEntity : class;

	Task<int> Count<T1, T2, TResult>(ISpecification<T1, T2, TResult> spec, CancellationToken cancellationToken)
		where T1 : class
		where T2 : class;
}