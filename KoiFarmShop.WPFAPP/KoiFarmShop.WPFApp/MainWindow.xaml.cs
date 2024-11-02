using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
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

        // Constructor with dependency injection for IAppointmentService
        public MainWindow(IAppointmentService appointmentService)
        {
            InitializeComponent();
            _appointmentService = appointmentService;
            LoadData(); // Initialize data loading here
        }

        private async void LoadData()
        {
            var result = await _appointmentService.GetAllAppointmentsAsync();
            if (result.IsSuccess)
            {
                grdServices.ItemsSource = result.Object as List<Appointment>;
            }
            else
            {
                MessageBox.Show("Failed to load appointments.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (!ValidateInputs())
            //    {
            //        MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return;
            //    }

            //    var appointmentRequest = new MakeAppointmentForServiceRequest
            //    {
            //        CustomerId = Guid.Parse(txtCustomerId.Text),
            //        PetId = Guid.Parse(txtPetId.Text),
            //        PetServiceId = Guid.Parse(cboServiceCategory.SelectedValue.ToString()),
            //        AppointmentDate = dpAppointmentDate.SelectedDate.Value
            //    };

            //    var result = string.IsNullOrEmpty(txtAppointmentId.Text)
            //        ? await _appointmentService.MakeAppointmentForServiceAsync(appointmentRequest)
            //        : await _appointmentService.UpdateAppointmentAsync(Guid.Parse(txtAppointmentId.Text), appointmentRequest);

            //    MessageBox.Show(result.IsSuccess ? "Appointment saved successfully." :
            //        $"Failed to save appointment:\n{string.Join(Environment.NewLine, result.Errors.Select(e => e.Description))}",
            //        result.IsSuccess ? "Success" : "Error",
            //        MessageBoxButton.OK,
            //        result.IsSuccess ? MessageBoxImage.Information : MessageBoxImage.Warning);

            //    LoadData();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Failed to save appointment. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private bool ValidateInputs()
        {
            return !string.IsNullOrEmpty(txtCustomerId.Text) &&
                   !string.IsNullOrEmpty(txtPetId.Text) &&
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
                        LoadData(); // Refresh data
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
                    txtPetId.Text = appointmentDetails.PetId.ToString();
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
            txtPetId.Text = string.Empty;
            cboServiceCategory.SelectedValue = null;
            dpAppointmentDate.SelectedDate = null;
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            this.ReSet();
        }
    }

}