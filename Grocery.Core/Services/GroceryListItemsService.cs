﻿using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services {
    public class GroceryListItemsService : IGroceryListItemsService {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;

        public GroceryListItemsService(IGroceryListItemsRepository groceriesRepository, IProductRepository productRepository) {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
        }

        public List<GroceryListItem> GetAll() {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId) {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll().Where(g => g.GroceryListId == groceryListId).ToList();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem Add(GroceryListItem item) => _groceriesRepository.Add(item);
        public GroceryListItem? Delete(GroceryListItem item) => _groceriesRepository.Delete(item);
        public GroceryListItem? Get(int id) => _groceriesRepository.Get(id);
        public GroceryListItem? Update(GroceryListItem item) => _groceriesRepository.Update(item);

        private void FillService(List<GroceryListItem> groceryListItems) {
            foreach (GroceryListItem g in groceryListItems)
                g.Product = _productRepository.Get(g.ProductId) ?? new(0, "", 0);
        }
    }
}
