using System;
using System.Threading.Tasks;

namespace Product.Infrastructure
{
    public interface IProductRepository
    {
        Task CreateAsync(Models.Product product);

        Task UpdateAsync(Models.Product product);

        Task DeleteAsync(Models.Product product);

        Task<Models.Product> GetById(Guid id);
    }
}
