namespace Sales.ViewModels
{
    using Sales.Services;
    using System.Collections.ObjectModel;
    using Sales.Common.Models;
    using System;
    using Xamarin.Forms;
    using System.Collections.Generic;

    public class ProductsViewModel : BaseViewModel
    {
        public ApiService apiservice;

        private ObservableCollection<Product> products; 
        public ObservableCollection<Product> Products 
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }

        }
        public ProductsViewModel()
        {
            this.apiservice = new ApiService();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiservice.GetList<Product>("http://server-etiqueta:8080", "/Api", "/Products");
            if (!response.IsSuccess) 
            {
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Accept");
                return;
            }
            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
        }
    }
}
