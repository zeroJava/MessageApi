namespace MessageApi.Application;

internal abstract class MapperBase<TEntity, TDto> where TEntity : class where TDto : class
{
   public abstract TDto? Map(TEntity? entity);
}