using Cqrs.Infrastructure.Messages;

namespace Cqrs.Infrastructure.Filtering
{
    public abstract class FilterQuery<TProjection> : Query<FilterResult<TProjection>>
    {
        public abstract int? Skip { get; set; }
        public abstract int Take { get; set; }
    }
}