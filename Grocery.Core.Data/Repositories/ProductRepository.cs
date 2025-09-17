using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;

namespace Grocery.Core.Data.Repositories {
    public class ProductRepository : IProductRepository {
        private readonly List<Product> products;
        public ProductRepository() {
            products = [
                new Product(1, "Melk", 300),
                new Product(2, "Kaas", 3),
                new Product(3, "Brood", 400),
                new Product(4, "Cornflakes", 0)];
        }

        public List<Product> GetAll() => products;
        public Product? Get(int id) => products.FirstOrDefault(p => p.Id == id) ?? throw new NullReferenceException();

        public Product Add(Product item) {
            if (item == null)
                throw new NullReferenceException();
            int newId = products.Max(p => p.Id) + 1;
            item.Id = newId;
            products.Add(item);
            return Get(item.Id) ?? throw new NullReferenceException();
        }

        public Product? Delete(Product item) {
            Product? toDelProduct = products.Find(p => p.Id == item.Id)
                                   ?? throw new NullReferenceException();
            products.Remove(toDelProduct);
            return toDelProduct;
        }

        public Product? Update(Product item) {
            Product? product = products.FirstOrDefault(p => p.Id == item.Id) ?? throw new NullReferenceException();
            product.Id = item.Id;
            return product;
        }
    }
}
