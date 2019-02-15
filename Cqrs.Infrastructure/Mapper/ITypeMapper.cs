using System;
using AutoMapper;

namespace Cqrs.Infrastructure.Mapper
{
    public interface ITypeMapper
    {
        object MapCreate(object source, Type sourceType, Type destinationType);
        void MapUpdate(object source, object destination, Type sourceType, Type destinationType);
    }
}