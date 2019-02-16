using System;
using System.Linq;

namespace Cqrs.Infrastructure.Mapper
{
    public interface ITypeMapper
    {
        object MapCreate(object source, Type sourceType, Type destinationType);
        void MapUpdate(object source, object destination, Type sourceType, Type destinationType);
        IQueryable<TProjection> Project<TProjection>(IQueryable queryable);
    }
}