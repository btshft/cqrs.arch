namespace Cqrs.Infrastructure.Mapper
{
    public static class TypeMapperExtensions
    {
        public static TOut Map<TOut>(this ITypeMapper mapper, object source)
        {
            return (TOut) mapper.MapCreate(source, source.GetType(), typeof(TOut));
        }

        public static TOut Map<TIn, TOut>(this ITypeMapper mapper, TIn source)
        {
            return (TOut) mapper.MapCreate(source, typeof(TIn), typeof(TOut));
        }

        public static void Map<TIn, TOut>(this ITypeMapper mapper, TIn source, TOut destination)
        {
            mapper.MapUpdate(source, destination, typeof(TIn), typeof(TOut));
        }
    }
}