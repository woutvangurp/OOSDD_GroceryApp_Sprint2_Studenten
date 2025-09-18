using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;

namespace Grocery.Core.Data.Repositories {
    public class GroceryListRepository : IGroceryListRepository {
        private readonly List<GroceryList> groceryLists;

        public GroceryListRepository() {
            groceryLists = [
                new GroceryList(1, "Boodschappen familieweekend", DateOnly.Parse("2024-12-14"), "#FF6A00", 1),
                new GroceryList(2, "Kerstboodschappen", DateOnly.Parse("2024-12-07"), "#626262", 1),
                new GroceryList(3, "Weekend boodschappen", DateOnly.Parse("2024-11-30"), "#003300", 1)];
        }

        public List<GroceryList> GetAll() {
            return groceryLists;
        }
        public GroceryList Add(GroceryList item) {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            int newId = groceryLists.Count > 0 ? groceryLists.Max(g => g.Id) + 1 : 1;
            item.Id = newId;
            groceryLists.Add(item);
            return item;
        }

        public GroceryList? Delete(GroceryList item) {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            GroceryList? groceryList = groceryLists.FirstOrDefault(g => g.Id == item.Id) ?? throw new ArgumentNullException(nameof(item));

            groceryLists.Remove(groceryList);
            return groceryList;
        }

        public GroceryList? Get(int id) {
            if (int.IsNegative(id))
                throw new ArgumentOutOfRangeException(nameof(id));

            GroceryList? groceryList = groceryLists.FirstOrDefault(g => g.Id == id);
            return groceryList;
        }

        public GroceryList? Update(GroceryList item) {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            GroceryList? groceryList = groceryLists.FirstOrDefault(g => g.Id == item.Id);
            groceryList = item;
            return groceryList;
        }
    }
}
