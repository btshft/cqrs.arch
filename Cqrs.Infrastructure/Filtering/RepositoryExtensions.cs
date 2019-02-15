using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Mapper;

namespace Cqrs.Infrastructure.Filtering
{
    public static class RepositoryExtensions
    {
        public static async Task<FilterResult<TOut>> ApplyFilterAsync<TIn, TOut>(
            this IRepository<TIn> repository, EntityFilter<TIn, TOut> filter, ITypeMapper mapper, CancellationToken cancellation = default)
            where TIn : class
        {
            var query = filter.Query;
            var filtered = repository.Query(filter.Predicate);

            if (query.Skip.HasValue)
                filtered = filtered.Skip(query.Skip.Value);

            filtered = filtered.Take(query.Take);

            var totalCount = await repository.CountAsync(filter.Predicate, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);

            var pagedItems = mapper.Project<TOut>(filtered).ToList();
            
            return new FilterResult<TOut>(pagedItems, totalCount);
        }
    }
}