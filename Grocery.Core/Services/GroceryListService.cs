using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services {
    public class GroceryListService : IGroceryListService {
        private readonly IGroceryListRepository _groceryRepository;
        public GroceryListService(IGroceryListRepository groceryRepository) {
            _groceryRepository = groceryRepository;
        }

        public List<GroceryList> GetAll() => _groceryRepository.GetAll();
        public GroceryList Add(GroceryList item) => _groceryRepository.Add(item);
        public GroceryList? Delete(GroceryList item) => _groceryRepository.Delete(item);
        public GroceryList? Get(int id) => _groceryRepository.Get(id);
        public GroceryList? Update(GroceryList item) => _groceryRepository.Update(item);

    }
}
