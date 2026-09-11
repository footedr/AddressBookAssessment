namespace AddressBookAssessment.Core.Shared;

public interface IEntity<TId>
{
    TId Id { get; }
}
