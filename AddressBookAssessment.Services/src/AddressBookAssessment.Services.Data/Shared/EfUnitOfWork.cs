using AddressBookAssessment.Core.Shared;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace AddressBookAssessment.Services.Data.Shared;

public class EfUnitOfWork<TContext>(TContext context, IMediator mediator) : IUnitOfWork
	where TContext : DbContext
{
	public async Task SaveChanges(CancellationToken cancellationToken)
	{
		var strategy = context.Database.CreateExecutionStrategy();

		await strategy.ExecuteAsync(async () =>
		{
			using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

			var count = await context.SaveChangesAsync(cancellationToken);

			var events = context.ChangeTracker.Entries<IEventSource>()
				.Select(e => e.Entity)
				.SelectMany(e => e.PublishEvents())
				.ToArray();

			if (events.Any())
			{
				foreach (var @event in events)
				{
					await mediator.Publish(@event, cancellationToken);
				}

				count += await context.SaveChangesAsync(cancellationToken);
			}

			transactionScope.Complete();

			return count;
		});
	}
}