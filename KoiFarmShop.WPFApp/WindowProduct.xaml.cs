using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using KVSC.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KVSC.Domain.Entities;
using KVSC.Infrastructure.DTOs.Product.AddProduct;
using KVSC.Application.Implement.Service;
using KVSC.Infrastructure.DTOs.Product.UpdateProduct;

namespace KoiFarmShop.WPFApp
{
    /// <summary>
    /// Interaction logic for WindowProduct.xaml
    /// </summary>
    public partial class WindowProduct : Window
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public WindowProduct(IProductService productService, IProductCategoryService productCategoryService)
        {
            InitializeComponent();
            _productService = productService; // Dependency Injection for product service
            _productCategoryService = productCategoryService;
            LoadProducts();
            
        }
        

        // Method to load products
        private async void LoadProducts(string searchTerm = null)
        {
            var results = await _productCategoryService.GetAllProductCategoriesAsync();
            var categories = results.Object as List<ProductCategory>;



            cboProductCategory.ItemsSource = categories;
            cboProductCategory.DisplayMemberPath = "Name";  // Displaying the category name in ComboBox
            cboProductCategory.SelectedValuePath = "Id";
            var result = await _productService.GetAllProductsAsync();
            if (result.IsSuccess)
            {
                var products = result.Object as List<Product>;
                if (products != null)
                {
                    grdProducts.ItemsSource = products;
                }
                else
                {
                    MessageBox.Show("Unexpected data format.");
                }
            }
            else
            {
                MessageBox.Show("Error loading products.");
            }
        }


        // Search button click event handler
        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            // Get the search term
            var searchTerm = txtSearchTerm.Text.Trim();

            // Get the selected search field from ComboBox
            string searchField = (string)((ComboBoxItem)cboSearchField.SelectedItem).Content;

            // Get the price range filters
            decimal? minPrice = null;
            decimal? maxPrice = null;
            decimal? minDiscountPrice = null;
            decimal? maxDiscountPrice = null;

            if (decimal.TryParse(txtMinPrice.Text, out var minP))
            {
                minPrice = minP;
            }

            if (decimal.TryParse(txtMaxPrice.Text, out var maxP))
            {
                maxPrice = maxP;
            }

            if (decimal.TryParse(txtMinDiscountPrice.Text, out var minDP))
            {
                minDiscountPrice = minDP;
            }

            if (decimal.TryParse(txtMaxDiscountPrice.Text, out var maxDP))
            {
                maxDiscountPrice = maxDP;
            }

            // Call the service with the search parameters
            var result = await _productService.GetAllProductsAsync(searchTerm, searchField, 1, 10, minPrice, maxPrice, minDiscountPrice, maxDiscountPrice);

            if (result.IsSuccess)
            {
                var products = (PagedResultSearch<Product>)result.Object;
                grdProducts.ItemsSource = products.Items; // Bind the result to the DataGrid
            }
            else
            {
                // Handle error with available Error or Errors property
                if (result.Error != null)
                {
                    MessageBox.Show("Error: " + result.Error.ToString());
                }
                else if (result.Errors != null && result.Errors.Count > 0)
                {
                    var errorMessages = string.Join(Environment.NewLine, result.Errors.Select(e => e.ToString()));
                    MessageBox.Show("Errors: " + errorMessages);
                }
                else
                {
                    MessageBox.Show("An unknown error occurred.");
                }
            }
        }


        // Save button click event handler for adding/updating product
        // Save button click event handler for adding/updating product
        private async void BtnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Guid? productId = string.IsNullOrEmpty(txtProductId.Text) ? (Guid?)null : Guid.Parse(txtProductId.Text);

                // If productId is present, it's an update; otherwise, it's a create operation
                if (productId.HasValue)
                {
                    // Update scenario - Use UpdateProductRequest
                    var updateProduct = new UpdateProductRequest
                    {
                        Id = productId.Value, // Ensure the product ID is passed for updating
                        Name = txtProductName.Text,
                        Description = "Product Description", // Replace with actual description from the UI
                        Price = decimal.Parse(txtPrice.Text),
                        DiscountPrice = decimal.Parse(txtDiscountPrice.Text),
                        StockQuantity = int.Parse(txtStockQuantity.Text),
                        ReleaseDate = dpReleaseDate.SelectedDate.Value,
                        SKU = txtSKU.Text,
                        Manufacturer = txtManufacturer.Text,
                        ProductDimensions = txtProductDimensions.Text,
                        Weight = decimal.Parse(txtWeight.Text),
                    };

                    var result = await _productService.UpdateProductAsync(productId.Value, updateProduct);
                    MessageBox.Show(result.IsSuccess ? "Product updated successfully!" :
                        $"Failed to update product. Errors:\n{string.Join(Environment.NewLine, result.Errors.Select(e => e.ToString()))}",
                        result.IsSuccess ? "Success" : "Error",
                        MessageBoxButton.OK,
                        result.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Warning);
                }
                else
                {
                    // Create scenario - Use AddProductRequest
                    var newProduct = new AddProductRequest
                    {
                        Name = txtProductName.Text,
                        Description = "Product Description", // Replace with actual description from the UI
                        Price = decimal.Parse(txtPrice.Text),
                        DiscountPrice = decimal.Parse(txtDiscountPrice.Text),
                        StockQuantity = int.Parse(txtStockQuantity.Text),
                        ReleaseDate = dpReleaseDate.SelectedDate.Value,
                        SKU = txtSKU.Text,
                        Manufacturer = txtManufacturer.Text,
                        ProductDimensions = txtProductDimensions.Text,
                        Weight = decimal.Parse(txtWeight.Text),
                        ProductCategoryId = ((ProductCategory)cboProductCategory.SelectedItem).Id, // Assuming ComboBox is populated with categories
                        ImageFile = null // Handle image file selection and uploading
                    };

                    var result = await _productService.CreateProductAsync(newProduct);
                    MessageBox.Show(result.IsSuccess ? "Product created successfully!" :
                        $"Failed to create product. Errors:\n{string.Join(Environment.NewLine, result.Errors.Select(e => e.ToString()))}",
                        result.IsSuccess ? "Success" : "Error",
                        MessageBoxButton.OK,
                        result.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Warning);
                }

                LoadProducts(); // Reload the product list
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save product information. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        // Reset button event handler to clear fields
        private void BtnResetProduct_Click(object sender, RoutedEventArgs e)
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtDiscountPrice.Clear();
            txtStockQuantity.Clear();
            dpReleaseDate.SelectedDate = null;
            txtSKU.Clear();
            txtManufacturer.Clear();
            txtProductDimensions.Clear();
            txtWeight.Clear();
        }

        // Delete button event handler
        private async void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (Product)grdProducts.SelectedItem; // Assuming the product is selected in the DataGrid
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            var result = await _productService.DeleteProductAsync(selectedProduct.Id);
            if (result.IsSuccess)
            {
                MessageBox.Show("Product deleted successfully!");
                LoadProducts(); // Reload the product list
            }
            else
            {
                // Handle error with available Error or Errors property
                if (result.Error != null)
                {
                    MessageBox.Show("Error: " + result.Error.ToString());
                }
                else if (result.Errors != null && result.Errors.Count > 0)
                {
                    var errorMessages = string.Join(Environment.NewLine, result.Errors.Select(e => e.ToString()));
                    MessageBox.Show("Errors: " + errorMessages);
                }
                else
                {
                    MessageBox.Show("An unknown error occurred.");
                }
            }
        }
        private void grdProducts_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (grdProducts.SelectedItem != null)
            {
                var selectedProduct = (Product)grdProducts.SelectedItem;

                // Fill the input fields with the selected product's data
                txtProductId.Text = selectedProduct.Id.ToString();
                txtProductName.Text = selectedProduct.Name;
                txtPrice.Text = selectedProduct.Price.ToString();
                txtDiscountPrice.Text = selectedProduct.DiscountPrice.ToString();
                txtStockQuantity.Text = selectedProduct.StockQuantity.ToString();
                dpReleaseDate.SelectedDate = selectedProduct.ReleaseDate;
                txtManufacturer.Text = selectedProduct.Manufacturer;
                txtSKU.Text = selectedProduct.SKU;
                txtProductDimensions.Text = selectedProduct.ProductDimensions;
                txtWeight.Text = selectedProduct.Weight.ToString();

                // Optionally, you can load product image as well if it's relevant
                // imgProductDisplay.Source = new BitmapImage(new Uri(selectedProduct.ImageUrl));
            }
        }
    }
}
