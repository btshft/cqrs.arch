namespace Cqrs.Contracts.Application
{
    /// <summary>
    /// Фильтр заявок.
    /// </summary>
    public class ApplicationFilterDto
    {
        public int? Status { get; set; }
        public int? Skip { get; set; }
        public int Take { get; set; }
    }
}