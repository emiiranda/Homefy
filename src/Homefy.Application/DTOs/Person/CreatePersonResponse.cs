
namespace Homefy.Application.DTOs.Person
{
    public sealed class CreatePersonResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Age { get; init; }
    }
}