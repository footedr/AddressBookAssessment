using AddressBookAssessment.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace AddressBookAssessment.Services.Data.Shared;

public class EfWorkspace<TContext>(TContext context) : IWorkspace
	where TContext : DbContext
{
	public void Add<T>(T entity) where T : class
	{
		context.Add(entity);
	}

	public void Remove<T>(T entity) where T : class
	{
		context.Remove(entity);
	}

	public async Task<IReadOnlyList<TEntity>> Load<TEntity>(ISpecification<TEntity> spec, CancellationToken cancellationToken)
		where TEntity : class
	{
		var entities = context.Set<TEntity>()
			.AsTracking()
			.AsSplitQuery();

		var results = await spec
			.Apply(entities)
			.ToListAsync(cancellationToken);

		return results;
	}
}