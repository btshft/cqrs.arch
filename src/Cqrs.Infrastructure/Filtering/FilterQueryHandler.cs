using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Handlers;
using Cqrs.Infrastructure.Mapper;

namespace Cqrs.Infrastructure.Filtering
{
    public abstract class FilterQueryHandler<TQuery, TEntity, TProjection> : IQueryHandler<TQuery, FilterResult<TProjection>>
        where TQuery : FilterQuery<TProjection> 
        where TEntity : class
    {
        protected IRepository<TEntity> Repository { get; }
        protected ITypeMapper TypeMapper { get; }
        
        protected FilterQueryHandler(IRepository<TEntity> repository, ITypeMapper typeMapper)
        {
            Repository = repository;
            TypeMapper = typeMapper;
        }
        
        /// <inheritdoc />
        public Task<FilterResult<TProjection>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var predicateBuilder = new PredicateBuilder<TEntity>();
            BuildPredicate(request, predicateBuilder);

            var filter = new EntityFilter<TQuery, TEntity, TProjection>(request, predicateBuilder.Expression);
            return Handle(filter, cancellationToken);
        }

        protected virtual async Task<FilterResult<TProjection>> Handle(
            EntityFilter<TQuery, TEntity, TProjection> filter, CancellationToken cancellationToken)
        {
            return await Repository.ApplyFilterAsync(filter, TypeMapper, cancellation: cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        protected abstract void BuildPredicate(TQuery query, IPredicateBuilder<TEntity> builder);
    }
}