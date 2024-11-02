using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using System.Windows;
using System.Windows.Input;

namespace KoiFarmShop.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPetServiceLogic _petServiceLogic;
        private readonly IPetServiceService _petServiceService;

        // Constructor with dependency injection for IAppointmentService
        public MainWindow(IAppointmentService appointmentService, IPetServiceLogic petServiceLogic, IPetServiceService petServiceService)
        {
            InitializeComponent();
            _appointmentService = appointmentService;
            _petServiceLogic = petServiceLogic;
            _petServiceService = petServiceService;
            LoadAppointmentAndPetServicesData();
        }

        private async Task LoadAppointmentAndPetServicesData()
        {
            // Load Appointments
            var appointmentResult = await _appointmentService.GetAllAppointmentsAsync();
            if (appointmentResult.IsSuccess)
            {
                grdServices.ItemsSource = appointmentResult.Object as List<Appointment>;
            }
            else
            {
                MessageBox.Show("Failed to load appointments.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Load Pet Services
            var petServiceResult = await _petServiceService.GetAllPetServicesAsync();
            if (petServiceResult.IsSuccess)
            {
                var petServices = petServiceResult.Object as List<PetService>; // Assuming PetService is the type of items in the list
                cboServiceCategory.ItemsSource = petServices;
                cboServiceCategory.DisplayMemberPath = "Name"; // Assuming PetService has a Name property
                cboServiceCategory.SelectedValuePath = "Id";   // Assuming PetService has an Id property
            }
            else
            {
                MessageBox.Show("Failed to load pet services.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        private async void txtCustomerId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Guid.TryParse(txtCustomerId.Text, out var customerId))
            {
                var result = await _petServiceLogic.GetPetsByOwnerIdAsync(customerId);
                if (result.IsSuccess)
                {
                    var pets = result.Object as List<Pet>;
                    cboPetId.ItemsSource = pets;
                    cboPetId.DisplayMemberPath = "Name";  // Assuming Pet has a Name property
                    cboPetId.SelectedValuePath = "Id";   // Assuming Pet has an Id property
                }
                else
                {
                    MessageBox.Show("No pets found for this customer.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    cboPetId.ItemsSource = null;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Customer ID.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                cboPetId.ItemsSource = null;
            }
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                {
                    MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var appointmentRequest = new MakeAppointmentForServiceRequest
                {
                    CustomerId = Guid.Parse(txtCustomerId.Text),
                    PetId = Guid.Parse(cboPetId.SelectedValue.ToString()),
                    PetServiceId = Guid.Parse(cboServiceCategory.SelectedValue.ToString()),
                    AppointmentDate = dpAppointmentDate.SelectedDate.Value
                };

                var result = await _appointmentService.MakeAppointmentForServiceAsync(appointmentRequest);

                MessageBox.Show(result.IsSuccess ? "Appointment added successfully." :
                    $"Failed to add appointment:\n{string.Join(Environment.NewLine, result.Errors.Select(e => e.Description))}",
                    result.IsSuccess ? "Success" : "Error",
                    MessageBoxButton.OK,
                    result.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Warning);

                LoadAppointmentAndPetServicesData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add appointment. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAppointmentId.Text))
                {
                    MessageBox.Show("Please select an appointment to update.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!ValidateInputs())
                {
                    MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var appointmentRequest = new MakeAppointmentForServiceRequest
                {
                    CustomerId = Guid.Parse(txtCustomerId.Text),
                    PetId = Guid.Parse(cboPetId.SelectedValue.ToString()),
                    PetServiceId = Guid.Parse(cboServiceCategory.SelectedValue.ToString()),
                    AppointmentDate = dpAppointmentDate.SelectedDate.Value
                };

                var result = await _appointmentService.UpdateAppointmentAsync(Guid.Parse(txtAppointmentId.Text), appointmentRequest);

                MessageBox.Show(result.IsSuccess ? "Appointment updated successfully." :
                    $"Failed to update appointment:\n{string.Join(Environment.NewLine, result.Errors.Select(e => e.Description))}",
                    result.IsSuccess ? "Success" : "Error",
                    MessageBoxButton.OK,
                    result.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Warning);

                LoadAppointmentAndPetServicesData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update appointment. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            return !string.IsNullOrEmpty(txtCustomerId.Text) &&
                   cboPetId.SelectedValue != null &&
                   cboServiceCategory.SelectedValue != null &&
                   dpAppointmentDate.SelectedDate.HasValue;
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (grdServices.SelectedItem is Appointment selectedAppointment)
            {
                var dialogResult = MessageBox.Show("Do you want to delete this appointment?", "Delete", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (dialogResult == MessageBoxResult.OK)
                {
                    var result = await _appointmentService.DeleteAppointmentAsync(selectedAppointment.Id);

                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Appointment deleted successfully.");
                        LoadAppointmentAndPetServicesData(); // Refresh data
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete appointment.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to delete.");
            }
        }

        private async void grdServices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdServices.SelectedItem is Appointment appointment)
            {
                var result = await _appointmentService.GetByIdAsync(appointment.Id);

                if (result.IsSuccess)
                {
                    var appointmentDetails = result.Object as Appointment;
                    txtAppointmentId.Text = appointmentDetails.Id.ToString();
                    txtCustomerId.Text = appointmentDetails.CustomerId.ToString();
                    cboPetId.SelectedValue = appointmentDetails.PetId;
                    cboServiceCategory.SelectedValue = appointmentDetails.PetServiceId;
                    dpAppointmentDate.SelectedDate = appointmentDetails.AppointmentDate;
                }
                else
                {
                    MessageBox.Show("Failed to load appointment details.");
                }
            }
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userId = Guid.Parse(txtSearchUserId.Text);

                var result = await _appointmentService.GetAppointmentsByUserIdAsync(userId);

                if (result.IsSuccess)
                {
                    grdServices.ItemsSource = result.Object as List<Appointment>;
                }
                else
                {
                    MessageBox.Show("No appointments found for this user.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching for appointments: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ReSet()
        {
            txtAppointmentId.Text = string.Empty;
            txtCustomerId.Text = string.Empty;
            cboPetId.SelectedValue = null;
            cboServiceCategory.SelectedValue = null;
            dpAppointmentDate.SelectedDate = null;
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            this.ReSet();
        }
    }


}