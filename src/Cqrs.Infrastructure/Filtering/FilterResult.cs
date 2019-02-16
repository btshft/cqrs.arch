using System.Collections.Generic;

namespace Cqrs.Infrastructure.Filtering
{
    public class FilterResult<T>
    {
        public FilterResult(IReadOnlyCollection<T> items, int total)
        {
            Items = items;
            Total = total;
        }

        public IReadOnlyCollection<T> Items { get; }
        public int Total { get; }
    }
}