using KoiFarmShop.Application.Interface.IService;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace KoiFarmShop.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<MainWindow> _logger;

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}