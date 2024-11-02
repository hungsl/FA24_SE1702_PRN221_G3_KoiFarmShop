using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using Microsoft.Extensions.Logging;
using System.Windows;
using System.Windows.Controls;

namespace KoiFarmShop.WPFApp
{
    public partial class WindowAppointment : Window
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPetServiceLogic _petServiceLogic;  // Logic to fetch pets
        private readonly IPetServiceService _petServiceService;  // Service to fetch services
        private readonly ILogger<WindowAppointment> _logger;

        public WindowAppointment(IAppointmentService appointmentService, IPetServiceLogic petServiceLogic, IPetServiceService petServiceService, ILogger<WindowAppointment> logger)
        {
            InitializeComponent();
            _appointmentService = appointmentService;
            _petServiceLogic = petServiceLogic;
            _petServiceService = petServiceService;
            _logger = logger;
            LoadAppointments();
            LoadPetServices();  // Load services into ComboBox
        }

        private async void LoadPetServices()
        {
            var result = await _petServiceService.GetAllPetServicesAsync();

            if (result.IsSuccess)
            {
                ServiceComboBox.ItemsSource = result.Object as List<PetService>;  // Bind services to ComboBox
            }
            else
            {
                MessageBox.Show("Failed to load services.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void CustomerIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Guid.TryParse(CustomerIdTextBox.Text, out var customerId))
            {
                await LoadPetsForCustomer(customerId);
            }
            else
            {
                PetComboBox.ItemsSource = null; // Clear pets if invalid Customer ID
            }
        }

        private async Task LoadPetsForCustomer(Guid ownerId)
        {
            try
            {
                var result = await _petServiceLogic.GetPetsByOwnerIdAsync(ownerId);

                if (result.IsSuccess)
                {
                    PetComboBox.ItemsSource = result.Object as List<Pet>;  // Bind pets to ComboBox
                }
                else
                {
                    PetComboBox.ItemsSource = null;
                    MessageBox.Show("No pets found for this customer.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading pets for customer ID: {OwnerId}", ownerId);
                MessageBox.Show("An error occurred while loading pets.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadAppointments()
        {
            var result = await _appointmentService.GetAllAppointmentsAsync();

            if (result.IsSuccess)
            {
                AppointmentsDataGrid.ItemsSource = result.Object as List<Appointment>;
            }
            else
            {
                MessageBox.Show("Failed to load appointments.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PetComboBox.SelectedValue is Guid petId && ServiceComboBox.SelectedValue is Guid serviceId)
                {
                    var request = new MakeAppointmentForServiceRequest
                    {
                        CustomerId = Guid.Parse(CustomerIdTextBox.Text),
                        PetId = petId,
                        PetServiceId = serviceId,
                        AppointmentDate = AppointmentDatePicker.SelectedDate ?? DateTime.Now
                    };

                    var result = await _appointmentService.MakeAppointmentForServiceAsync(request);

                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Appointment created successfully.");
                        LoadAppointments();
                    }
                    else
                    {
                        var errorMessage = result.Errors.Count > 0
                            ? string.Join("\n", result.Errors.Select(e => e.Description))
                            : "Failed to create appointment.";
                        MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a pet and a service.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating appointment");
                MessageBox.Show("An unexpected error occurred while creating the appointment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsDataGrid.SelectedItem is Appointment selectedAppointment)
            {
                var result = await _appointmentService.DeleteAppointmentAsync(selectedAppointment.Id);

                if (result.IsSuccess)
                {
                    MessageBox.Show("Appointment deleted successfully.");
                    LoadAppointments();
                }
                else
                {
                    var errorMessage = result.Errors.Count > 0
                        ? string.Join("\n", result.Errors.Select(e => e.Description))
                        : "Failed to delete appointment.";
                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadAppointments();
        }
    }
}
