using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces.Properties;

namespace Infrastructure.Data.Repositories.Properties
{
    public class PropertyRepository : AsyncBaseRepository<Property, int>, IPropertyRepository
    {
        public PropertyRepository(EFContext context) : base(context) { }
    }
}