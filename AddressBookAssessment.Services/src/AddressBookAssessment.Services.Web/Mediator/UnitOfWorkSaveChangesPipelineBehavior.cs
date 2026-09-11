using AddressBookAssessment.Core.Shared;
using Mediator;

namespace AddressBookAssessment.Services.Web.Mediator;

public class UnitOfWorkSaveChangesPipelineBehavior<TMessage, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TMessage, TResponse>
	where TMessage : IMessage
{
	public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
	{
		var response = await next(message, cancellationToken);

		await unitOfWork.SaveChanges(cancellationToken);

		return response;
	}
}