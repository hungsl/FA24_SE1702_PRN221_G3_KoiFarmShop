namespace KoiFarmShop.Infrastructure.DTOs.Appointment.UpdateAppointment
{
    public class UpdateAppointmentRequest
    {
        public Guid CustomerId { get; set; }
        public Guid PetId { get; set; }
        public Guid? PetServiceId { get; set; }
        public Guid? ComboServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }

}
