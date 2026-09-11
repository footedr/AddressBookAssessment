using System.Net;

namespace AddressBookAssessment.Core.Shared;

public record ProblemDetails
{
	public required HttpStatusCode Status { get; init; }
	public required Uri Type { get; init; }
	public required string Title { get; init; }
	public Uri? Instance { get; init; }
	public string? Detail { get; init; }
	public Dictionary<string, object?> Extensions { get; init; } = new();
}

public class ProblemDetailsException : Exception
{
	public ProblemDetailsException(ProblemDetails problemDetails) : base(problemDetails.Detail)
	{
		ProblemDetails = problemDetails;
	}

	public ProblemDetailsException(ProblemDetails problemDetails, string message) : base(message)
	{
		ProblemDetails = problemDetails;
	}

	public ProblemDetails ProblemDetails { get; }
}