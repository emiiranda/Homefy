namespace Homefy.Application.DTOs.Person;

public sealed class GetAllPersonsResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Age { get; init; }
}