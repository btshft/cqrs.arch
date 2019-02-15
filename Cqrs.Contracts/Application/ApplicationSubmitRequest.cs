namespace Cqrs.Contracts.Application
{
    /// <summary>
    /// Запрос на утверждение заявки.
    /// </summary>
    public class ApplicationSubmitRequest
    {
        public int ApplicationId { get; set; }
    }
}