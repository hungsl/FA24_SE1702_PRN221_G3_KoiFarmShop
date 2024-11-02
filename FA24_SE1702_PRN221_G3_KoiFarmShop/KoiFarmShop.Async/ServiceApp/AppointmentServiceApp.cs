using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using System.Diagnostics;

namespace KoiFarmShop.Async.ServiceApp
{
    public class AppointmentServiceApp
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentServiceApp(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Start Async Demo with AppointmentService...");

            // Retrieve all appointments
            var result = await _appointmentService.GetAllAppointmentsAsync();
            var appointments = result.Object as List<Appointment>;

            if (appointments == null || !appointments.Any())
            {
                Console.WriteLine("No appointments found!");
                return;
            }

            Console.WriteLine("\nRun Synchronously:");
            await RunSynchronousDemo(appointments);

            Console.WriteLine("\nRun Asynchronously:");
            await RunAsynchronousDemo(appointments);
        }

        private async Task RunSynchronousDemo(IList<Appointment> appointments)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (var appointment in appointments)
            {
                await ProcessAppointment(appointment);
            }
            stopwatch.Stop();
            Console.WriteLine($"Synchronous processing completed in: {stopwatch.Elapsed.TotalSeconds:F2} seconds\n");
        }

        private async Task RunAsynchronousDemo(IList<Appointment> appointments)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var tasks = appointments.Select(appointment => ProcessAppointment(appointment));
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"Asynchronous processing completed in: {stopwatch.Elapsed.TotalSeconds:F2} seconds\n");
        }

        private async Task ProcessAppointment(Appointment appointment)
        {
            Console.WriteLine($"Processing appointment for: {appointment.Id} on {appointment.AppointmentDate}");
            await Task.Delay(1000); // Simulate processing time
            Console.WriteLine($"Completed appointment for: {appointment.Id} on {appointment.AppointmentDate}");
        }
    }

}
