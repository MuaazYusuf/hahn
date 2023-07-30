using Domain.Base;
using Domain.Entities;

namespace Domain.Interfaces.Properties
{
    public interface IPropertyRepository : IBaseAsyncRepository<Property, int>
    {
    }
}