using ACME.DTO;
using ACME.Models;

namespace ACME.Repository
{
    public interface IProductRepository
    {
        int Add(ProductRequest request);
        bool Delete(int id);
        List<Product> Find(string description);
        List<Product> GetAll();
        Product? Get(int id);
        bool Update(int id, ProductRequest request);
    }
}