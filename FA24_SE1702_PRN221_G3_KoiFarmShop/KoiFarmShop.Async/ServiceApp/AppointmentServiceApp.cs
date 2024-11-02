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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Starting Async Demo with AppointmentService...");
            Console.ResetColor();

            // Retrieve all appointments
            var result = await _appointmentService.GetAllAppointmentsAsync();
            var appointments = result.Object as List<Appointment>;

            if (appointments == null || !appointments.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No appointments found!");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- Running Synchronously ---");
            Console.ResetColor();
            await RunSynchronousDemo(appointments);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- Running Asynchronously ---");
            Console.ResetColor();
            await RunAsynchronousDemo(appointments);
        }

        private async Task RunSynchronousDemo(IList<Appointment> appointments)
        {
            var stopwatch = Stopwatch.StartNew();

            foreach (var appointment in appointments)
            {
                await ProcessAppointment(appointment);
            }

            stopwatch.Stop();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Synchronous processing completed in: {stopwatch.Elapsed.TotalSeconds:F2} seconds\n");
            Console.ResetColor();
        }

        private async Task RunAsynchronousDemo(IList<Appointment> appointments)
        {
            var stopwatch = Stopwatch.StartNew();

            // Process all appointments asynchronously
            var tasks = appointments.Select(appointment => ProcessAppointment(appointment));
            await Task.WhenAll(tasks);

            stopwatch.Stop();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Asynchronous processing completed in: {stopwatch.Elapsed.TotalSeconds:F2} seconds\n");
            Console.ResetColor();
        }

        private async Task ProcessAppointment(Appointment appointment)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Processing appointment for: {appointment.Id} on {appointment.AppointmentDate}");
            Console.ResetColor();

            // Simulate processing time
            await Task.Delay(1000);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Completed appointment for: {appointment.Id} on {appointment.AppointmentDate}");
            Console.ResetColor();
        }
    }


}
