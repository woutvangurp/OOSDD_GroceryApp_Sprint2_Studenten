﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels {
    [QueryProperty(nameof(GroceryList), nameof(GroceryList))]
    public partial class GroceryListItemsViewModel : BaseViewModel {
        private readonly IGroceryListItemsService _groceryListItemsService;
        private readonly IProductService _productService;
        public ObservableCollection<GroceryListItem> MyGroceryListItems { get; set; } = [];
        public ObservableCollection<Product> AvailableProducts { get; set; } = [];

        [ObservableProperty]
        GroceryList groceryList = new(0, "None", DateOnly.MinValue, "", 0);

        public GroceryListItemsViewModel(IGroceryListItemsService groceryListItemsService, IProductService productService) {
            _groceryListItemsService = groceryListItemsService;
            _productService = productService;
            Load(groceryList.Id);
        }

        private void Load(int id) {
            MyGroceryListItems.Clear();
            foreach (var item in _groceryListItemsService.GetAllOnGroceryListId(id))
                MyGroceryListItems.Add(item);
            GetAvailableProducts();
        }

        private void GetAvailableProducts() {
            AvailableProducts.Clear();
            List<Product> allProducts = _productService.GetAll();
            foreach (Product p in allProducts) {
                if (p.Stock <= 0 || AvailableProducts.Contains(p))
                    continue;
                AvailableProducts.Add(p);
            }
        }

        partial void OnGroceryListChanged(GroceryList value) {
            Load(value.Id);
        }

        [RelayCommand]
        public async Task ChangeColor() {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), GroceryList } };
            await Shell.Current.GoToAsync($"{nameof(ChangeColorView)}?Name={GroceryList.Name}", true, paramater);
        }

        [RelayCommand]
        public void AddProduct(Product product) {
            if (!AvailableProducts.Contains(product) || product.Id <= 0) return;
            GroceryListItem editedItem = MyGroceryListItems.FirstOrDefault(x => x.ProductId == product.Id) ?? throw new InvalidOperationException();
            editedItem.Amount += 1;
            _groceryListItemsService.Update(editedItem);

            Product? editProduct = _productService.Get(product.Id);
            if (editProduct == null)
                throw new NullReferenceException();
            editProduct.Stock -= 1;
            _productService.Update(editProduct);

            Load(groceryList.Id);
        }
    }
}
