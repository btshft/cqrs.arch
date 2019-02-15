using Cqrs.Infrastructure.Messages;

namespace Cqrs.Infrastructure.Filtering
{
    public abstract class FilterQuery<TProjection> : Query<FilterResult<TProjection>>
    {
        public abstract int? Skip { get; }
        public abstract int Take { get; }
    }
}