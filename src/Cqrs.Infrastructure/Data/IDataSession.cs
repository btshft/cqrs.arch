using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.Infrastructure.Data
{
    public interface IDataSession : IDisposable
    {
        Task CommitAsync(CancellationToken cancellation);
        Task RollbackAsync(CancellationToken cancellation);
    }
}