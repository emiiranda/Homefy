namespace Homefy.Application.DTOs.Person;

public sealed class UpdatePersonResponse
{
    public string Name { get; init; } = string.Empty;
    public int Age { get; init; }
}