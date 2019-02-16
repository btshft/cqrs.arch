namespace Cqrs.Domain.Models
{
    public class Application
    {
        public int Id { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}