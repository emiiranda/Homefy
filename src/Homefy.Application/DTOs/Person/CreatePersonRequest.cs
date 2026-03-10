
namespace Homefy.Application.DTOs.Person
{
    public sealed class CreatePersonRequest
    {
        public string Name { get; init; } = string.Empty;
        public int Age { get; init; }
    }
}