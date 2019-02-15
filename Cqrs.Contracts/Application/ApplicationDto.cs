namespace Cqrs.Contracts.Application
{
    /// <summary>
    /// Модель заявки.
    /// </summary>
    public class ApplicationDto
    {        
        public int Id { get; set; }
        public int Status { get; set; }
    }
}