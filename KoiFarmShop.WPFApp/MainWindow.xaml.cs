using KoiFarmShop.Application.Implement.Service;
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
using System.Net;

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
                    ImageUrl = txtImageUrl.Text,
                    Description = txtDescription.Text, // Thêm Description
                    MaxNumberOfPets = int.Parse(txtMaxNumberOfPets.Text) // Thêm MaxNumberOfPets
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
            try
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
            
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(txtImageUrl.Text, UriKind.Absolute);
                bitmap.EndInit();
                imgDisplay.Source = bitmap; // Hiển thị ảnh trong Image control
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}");
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
                            petService = serviceResult.Object as PetService;
                            txtPetServiceId.Text = petService.Id.ToString();
                            txtServiceName.Text = petService.Name;
                            cboServiceCategory.SelectedValue = petService.PetServiceCategoryId;
                            txtBasePrice.Text = petService.BasePrice.ToString();
                            txtDuration.Text = petService.Duration?.ToString();
                            dpAvailableFrom.SelectedDate = petService.AvailableFrom;
                            dpAvailableTo.SelectedDate = petService.AvailableTo;
                            txtTravelCost.Text = petService.TravelCost.ToString();
                            txtImageUrl.Text = petService.ImageUrl;
                            txtDescription.Text = petService.Description;
                            txtMaxNumberOfPets.Text = petService.MaxNumberOfPets.ToString();

                            try
                            {
                                BitmapImage bitmap = new BitmapImage();
                                bitmap.BeginInit();
                                bitmap.UriSource = new Uri(txtImageUrl.Text, UriKind.Absolute);
                                bitmap.EndInit();
                                imgDisplay.Source = bitmap; // Hiển thị ảnh trong Image control
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error loading image: {ex.Message}");
                            }
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
            txtPetServiceId.Text = string.Empty;
            imgDisplay.Source = null;
            txtDescription.Text = string.Empty;
            txtMaxNumberOfPets.Text = string.Empty;
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
                // Lấy tên dịch vụ
                var serviceName = txtSearchServiceName.Text.Trim();

                // Lấy danh mục dịch vụ được chọn
                var selectedCategoryId = (Guid?)cboSearchServiceCategory.SelectedValue;

                // Lấy giá dịch vụ
                decimal? basePrice = null;
                if (decimal.TryParse(txtSearchBasePrice.Text.Trim(), out decimal parsedPrice))
                {
                    basePrice = parsedPrice;
                }

                // Lấy danh sách dịch vụ từ API hoặc dữ liệu
                var serviceResult = (await _petServiceService.GetAllPetServicesAsync()).Object as List<PetService>;

                // Lọc theo tên, danh mục và giá dịch vụ
                var serviceFilter = serviceResult?.Where(service =>
                    (string.IsNullOrEmpty(serviceName) || service.Name.Contains(serviceName, StringComparison.OrdinalIgnoreCase)) &&
                    (!selectedCategoryId.HasValue || service.PetServiceCategoryId == selectedCategoryId) &&
                    (!basePrice.HasValue || service.BasePrice == basePrice))
                    .ToList();

                // Cập nhật danh sách dịch vụ vào GridView
                grdServices.ItemsSource = serviceFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching for services: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnLookup_Click(object sender, RoutedEventArgs e)
        {
            string domainName = txtDomainName.Text;//Lấy tên miền mà người dùng đã nhập vào ô
            txtResult.Clear(); // Xóa kết quả trước đó từ ô txtResult

            try
            {
                // Truy vấn DNS để lấy địa chỉ IP
                var hostEntry = Dns.GetHostEntry(domainName);
                txtResult.AppendText($"IP addresses for {domainName}:\n");
                foreach (var ip in hostEntry.AddressList)//Lặp qua danh sách các địa chỉ IP được trả về từ truy vấn DNS.
                {
                    txtResult.AppendText(ip.ToString() + "\n");// Thêm mỗi địa chỉ IP vào ô kết quả
                }
            }
            catch (Exception ex)
            {
                txtResult.AppendText($"Error: {ex.Message}");
            }
        }
    }

}