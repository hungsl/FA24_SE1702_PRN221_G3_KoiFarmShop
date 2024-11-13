using System.ComponentModel.DataAnnotations;

public class AppointmentDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    public Guid PetId { get; set; }

    [Required]
    public Guid PetServiceId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime AppointmentDate { get; set; }

    [Required]
    [EnumDataType(typeof(AppointmentStatus))]
    public AppointmentStatus Status { get; set; }
}

public enum AppointmentStatus
{
    Scheduled,
    Completed,
    Cancelled
}
