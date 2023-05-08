using FluentValidation.Results;

namespace Application.Base
{
    public interface IBaseValidator<TEntity>
    {
        Task<ValidationResult> ValidateAsync(TEntity entity);
    }
}