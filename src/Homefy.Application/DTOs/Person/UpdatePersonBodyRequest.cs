namespace Homefy.Application.DTOs.Person
{
    public sealed class UpdatePersonBodyRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}