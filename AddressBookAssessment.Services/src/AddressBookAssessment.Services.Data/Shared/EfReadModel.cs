using AddressBookAssessment.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace AddressBookAssessment.Services.Data.Shared;

public class EfReadModel<TContext>(AddressBookAssessmentContext dbContext) : IReadModel
	where TContext : DbContext
{
	public async Task<IReadOnlyList<TResult>> Search<TEntity, TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken)
		where TEntity : class
	{
		var queryable = dbContext.Set<TEntity>()
			.AsNoTracking()
			.AsSplitQuery();

		return await specification.Apply(queryable).ToArrayAsync(cancellationToken);
	}

	public async Task<IReadOnlyList<TResult>> Search<T1, T2, TResult>(ISpecification<T1, T2, TResult> specification, CancellationToken cancellationToken)
		where T1 : class
		where T2 : class
	{
		var q1 = dbContext.Set<T1>()
			.AsNoTracking();

		var q2 = dbContext.Set<T2>()
			.AsNoTracking();

		return await specification.Apply(q1, q2)
			.ToArrayAsync(cancellationToken);
	}

	public async Task<int> Count<TEntity, TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken) where TEntity : class
	{
		var queryable = dbContext.Set<TEntity>()
			.AsNoTracking()
			.AsSplitQuery();

		return await specification.Apply(queryable).CountAsync(cancellationToken);
	}

	public async Task<int> Count<T1, T2, TResult>(ISpecification<T1, T2, TResult> specification, CancellationToken cancellationToken)
		where T1 : class
		where T2 : class
	{
		var q1 = dbContext.Set<T1>()
			.AsNoTracking();

		var q2 = dbContext.Set<T2>()
			.AsNoTracking();

		return await specification.Apply(q1, q2)
			.CountAsync(cancellationToken);
	}
}