namespace Cqrs.Infrastructure.Data
{
    public interface IDataSessionFactory
    {
        IDataSession CreateDataSession();
    }
}