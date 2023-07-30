using Domain.Entities;
using Domain.Interfaces.Properties;

namespace Application.Services.Properties
{
    public class PropertyService : BaseService<Property, int>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        : base(propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public new async Task<Property> AddAsync(Property property)
        {
            return await base.AddAsync(property);
        }
    }
}