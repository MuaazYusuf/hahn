namespace Application.Base
{
    public interface IBaseValidator<TEntity>
    {
        Task<bool> ValidateAsync(TEntity entity);
    }
}