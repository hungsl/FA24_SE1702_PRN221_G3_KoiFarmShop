﻿using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Drawing;

namespace KoiFarmShop.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPetServiceService _petServiceService;
        private readonly IPetServiceCategoryService _petCategoryService;

        // Constructor có tham số cho DI
        public MainWindow(IPetServiceService petServiceService, IPetServiceCategoryService petCategoryService)
        {
            InitializeComponent();
            _petServiceService = petServiceService;
            _petCategoryService = petCategoryService;
            LoadData(); // Khởi tạo dữ liệu tại đây
        }
        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                {
                    MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var petService = new AddPetServiceRequest
                {
                    Name = txtServiceName.Text,
                    PetServiceCategoryId = (Guid)cboServiceCategory.SelectedValue,
                    BasePrice = decimal.Parse(txtBasePrice.Text),
                    Duration = txtDuration.Text,
                    AvailableFrom = dpAvailableFrom.SelectedDate.Value,
                    AvailableTo = dpAvailableTo.SelectedDate.Value,
                    TravelCost = decimal.Parse(txtTravelCost.Text),
                    ImageUrl = txtImageUrl.Text
                };
                Guid? petServiceId = string.IsNullOrEmpty(txtPetServiceId.Text) ? (Guid?)null : Guid.Parse(txtPetServiceId.Text);
                var result = petServiceId.HasValue
                ? await _petServiceService.UpdatePetServiceAsync(petServiceId.Value, petService)
                : await _petServiceService.CreatePetServiceAsync(petService);

                MessageBox.Show(result.IsSuccess ? "Save data success" :
                      $"Failed to save data. Errors:\n{string.Join(Environment.NewLine, result.Errors.Select(e => e.Description))}",
                      result.IsSuccess ? "Success" : "Error",
                      MessageBoxButton.OK,
                      result.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Warning);

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save service information. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool ValidateInputs()
        {
            return !string.IsNullOrEmpty(txtServiceName.Text) &&
                   cboServiceCategory.SelectedValue != null &&
                   !string.IsNullOrEmpty(txtBasePrice.Text) &&
                   !string.IsNullOrEmpty(txtDuration.Text) &&
                   dpAvailableFrom.SelectedDate.HasValue &&
                   dpAvailableTo.SelectedDate.HasValue &&
                   !string.IsNullOrEmpty(txtTravelCost.Text) &&
                   !string.IsNullOrEmpty(txtImageUrl.Text);
        }
        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Chọn Ảnh",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                txtImageUrl.Text = openFileDialog.FileName; // Gán đường dẫn ảnh vào TextBox
            }
        }
        private async void LoadData()
        {
            // Lấy danh sách các dịch vụ thú cưng
            var servicesResult = (await _petServiceService.GetAllPetServicesAsync()).Object as List<PetService>;
            var serviceCategories = (await _petCategoryService.GetAllPetServiceCategoriesAsync()).Object as List<PetServiceCategory>;

            // Đổ dữ liệu vào DataGrid
            grdServices.ItemsSource = servicesResult;

            // Đổ dữ liệu vào ComboBox cho việc chọn loại dịch vụ
            cboServiceCategory.ItemsSource = serviceCategories;
            cboServiceCategory.DisplayMemberPath = "Name";  
            cboServiceCategory.SelectedValuePath = "Id";   

            // Đổ dữ liệu vào ComboBox tìm kiếm loại dịch vụ
            cboSearchServiceCategory.ItemsSource = serviceCategories;
            cboSearchServiceCategory.DisplayMemberPath = "Name"; 
            cboSearchServiceCategory.SelectedValuePath = "Id";    
        }
        private async void grdServices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var petService = row.Item as PetService;
                    if (petService != null)
                    {
                        var serviceResult = await _petServiceService.GetPetServiceByIdAsync(petService.Id); 

                        if (serviceResult.IsSuccess  && serviceResult.Object != null)
                        {
                            petService = serviceResult.Object as PetService; // Đảm bảo lấy được dịch vụ đã được cập nhật

                            txtPetServiceId.Text = petService.Id.ToString();
                            txtServiceName.Text = petService.Name; // Đảm bảo TextBox tên dịch vụ
                            cboServiceCategory.SelectedValue = petService.PetServiceCategoryId; // Lấy loại dịch vụ
                            txtBasePrice.Text = petService.BasePrice.ToString(); // Giá cơ bản
                            txtDuration.Text = petService.Duration?.ToString(); // Thời gian
                            dpAvailableFrom.SelectedDate = petService.AvailableFrom; // Ngày khả dụng từ
                            dpAvailableTo.SelectedDate = petService.AvailableTo; // Ngày khả dụng đến
                            txtTravelCost.Text = petService.TravelCost.ToString(); // Chi phí di chuyển
                            txtImageUrl.Text = petService.ImageUrl; // Đường dẫn hình ảnh
                        }
                    }
                }
            }
        }
        public void ReSet()
        {
            txtServiceName.Text = string.Empty; 
            cboServiceCategory.SelectedValue = null;
            txtBasePrice.Text = string.Empty; 
            txtDuration.Text = string.Empty; 
            dpAvailableFrom.SelectedDate = null; 
            dpAvailableTo.SelectedDate = null;
            txtTravelCost.Text = string.Empty; 
            txtImageUrl.Text = string.Empty;
            txtPetServiceId.Text= string.Empty;
        }
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            this.ReSet();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (grdServices.SelectedItem is PetService selectedService)
            {
                var dialogResult = MessageBox.Show("Do you want to delete this item", "Delete", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (dialogResult == MessageBoxResult.OK)
                {
                    var result = await _petServiceService.DeletePetServiceAsync(selectedService.Id); 

                    
                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Delete data success");
                    }
                    else
                    {
                        MessageBox.Show("Delete data fail");
                    }
                    this.LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a service to delete");
            }
        }
        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var serviceName = txtSearchServiceName.Text.Trim();

                var selectedCategoryId = (Guid?)cboSearchServiceCategory.SelectedValue;

                var serviceResult = (await _petServiceService.GetAllPetServicesAsync()).Object as List<PetService>;

                // Lọc theo tên và danh mục dịch vụ
                var serviceFilter = serviceResult?.Where(service =>
                    (string.IsNullOrEmpty(serviceName) || service.Name.Contains(serviceName, StringComparison.OrdinalIgnoreCase)) &&
                    (!selectedCategoryId.HasValue || service.PetServiceCategoryId == selectedCategoryId))
                    .ToList();


                grdServices.ItemsSource = serviceFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching for services: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}