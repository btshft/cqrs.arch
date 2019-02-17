namespace Cqrs.Contracts.Application
{
    /// <summary>
    /// Запрос на отзыв заявки.
    /// </summary>
    public class ApplicationWithdrawRequest
    {
        public int ApplicationId { get; set; }
    }
}