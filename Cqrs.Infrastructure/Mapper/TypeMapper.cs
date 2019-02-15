using System;
using AutoMapper;

namespace Cqrs.Infrastructure.Mapper
{
    public class TypeMapper : ITypeMapper
    {
        private readonly IMapper _mapper;

        public TypeMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <inheritdoc />
        public object MapCreate(object source, Type sourceType, Type destinationType)
        {
            return _mapper.Map(source, sourceType, destinationType);
        }

        /// <inheritdoc />
        public void MapUpdate(object source, object destination, Type sourceType, Type destinationType)
        {
            _mapper.Map(source, destination, sourceType, destinationType);
        }
    }
}