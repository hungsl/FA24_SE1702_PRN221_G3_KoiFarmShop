using FluentValidation;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using KoiFarmShop.Infrastructure.Interface;
using Microsoft.Extensions.Logging;


namespace KoiFarmShop.Service.Implement.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<MakeAppointmentForServiceRequest> _serviceValidator;
        private readonly IValidator<MakeAppointmentForComboRequest> _comboValidator;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(
            IUnitOfWork unitOfWork,
            IValidator<MakeAppointmentForServiceRequest> serviceValidator,
            IValidator<MakeAppointmentForComboRequest> comboValidator,
            ILogger<AppointmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _serviceValidator = serviceValidator;
            _comboValidator = comboValidator;
            _logger = logger;
        }


        public async Task<Result> UpdateAppointmentAsync(Guid appointmentId, MakeAppointmentForServiceRequest request)
        {
            try
            {
                _logger.LogInformation("Attempting to update appointment with ID: {AppointmentId}", appointmentId);

                // Validate the request
                var validationResult = await _serviceValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => (Error)e.CustomState).ToList();
                    _logger.LogWarning("Validation failed for appointment update. Errors: {Errors}", errors);
                    return Result.Failures(errors);
                }

                var existingAppointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(appointmentId);
                if (existingAppointment == null)
                {
                    _logger.LogWarning("Appointment with ID {AppointmentId} not found.", appointmentId);
                    return Result.Failure(Error.NotFound("Appointment", "Appointment not found."));
                }

                // Update the appointment using the values from MakeAppointmentForServiceRequest
                existingAppointment.CustomerId = request.CustomerId;
                existingAppointment.PetId = request.PetId;
                existingAppointment.PetServiceId = request.PetServiceId;
                existingAppointment.AppointmentDate = request.AppointmentDate;

                var updatedResult = await _unitOfWork.AppointmentRepository.UpdateAppointmentAsync(appointmentId, existingAppointment);

                _logger.LogInformation("Successfully updated appointment with ID: {AppointmentId}", appointmentId);
                return Result.SuccessWithObject(updatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating appointment with ID: {AppointmentId}", appointmentId);
                return Result.Failure(Error.Failure("UpdateError", "An unexpected error occurred while updating the appointment."));
            }
        }





        //DELETE
        public async Task<Result> DeleteAppointmentAsync(Guid appointmentId)
        {
            try
            {
                _logger.LogInformation("Attempting to delete appointment with ID: {AppointmentId}", appointmentId);

                var deleted = await _unitOfWork.AppointmentRepository.DeleteAppointmentAsync(appointmentId);

                if (!deleted)
                {
                    _logger.LogWarning("Appointment with ID {AppointmentId} was not found or already deleted.", appointmentId);
                    return Result.Failure(Error.NotFound("Appointment", "The appointment was not found or is already deleted"));
                }

                _logger.LogInformation("Successfully deleted appointment with ID: {AppointmentId}", appointmentId);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting appointment with ID: {AppointmentId}", appointmentId);
                return Result.Failure(Error.Failure("DeleteError", "An unexpected error occurred while deleting the appointment."));
            }
        }


        //GET
        public async Task<Result> GetAllAppointmentsAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all appointments.");

                var appointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsAsync();

                if (appointments == null || !appointments.Any())
                {
                    _logger.LogWarning("No appointments found.");
                    return Result.Failure(Error.NotFound("Appointments", "No appointments found"));
                }

                _logger.LogInformation("Successfully retrieved {Count} appointments.", appointments.Count());
                return Result.SuccessWithObject(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all appointments.");
                return Result.Failure(Error.Failure("RetrievalError", "An unexpected error occurred while retrieving appointments."));
            }
        }
        public async Task<Result> GetByIdAsync(Guid appointmentId)
        {
            try
            {
                _logger.LogInformation("Retrieving appointment with ID: {AppointmentId}", appointmentId);

                var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(appointmentId);
                if (appointment == null)
                {
                    _logger.LogWarning("Appointment with ID {AppointmentId} not found.", appointmentId);
                    return Result.Failure(Error.NotFound("Appointment", "Appointment not found."));
                }

                _logger.LogInformation("Successfully retrieved appointment with ID: {AppointmentId}", appointmentId);
                return Result.SuccessWithObject(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointment with ID: {AppointmentId}", appointmentId);
                return Result.Failure(Error.Failure("RetrievalError", "An unexpected error occurred while retrieving the appointment."));
            }
        }
        public async Task<Result> GetAppointmentsByUserIdAsync(Guid userId)
        {
            try
            {
                _logger.LogInformation("Retrieving appointments for user ID: {UserId}", userId);

                var appointments = await _unitOfWork.AppointmentRepository.GetAppointmentsByUserIdAsync(userId);

                if (appointments == null || !appointments.Any())
                {
                    _logger.LogWarning("No appointments found for user ID: {UserId}", userId);
                    return Result.Failure(Error.NotFound("Appointments", "No appointments found for this user."));
                }

                _logger.LogInformation("Successfully retrieved {Count} appointments for user ID: {UserId}", appointments.Count(), userId);
                return Result.SuccessWithObject(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointments for user ID: {UserId}", userId);
                return Result.Failure(Error.Failure("RetrievalError", "An unexpected error occurred while retrieving appointments."));
            }
        }

        //CREATE
        public async Task<Result> MakeAppointmentForServiceAsync(MakeAppointmentForServiceRequest request)
        {
            try
            {
                _logger.LogInformation("Attempting to create appointment for service. Customer ID: {CustomerId}, Pet ID: {PetId}", request.CustomerId, request.PetId);

                // Validate the input
                var validationResult = await _serviceValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => (Error)e.CustomState).ToList();
                    _logger.LogWarning("Validation failed for appointment request. Errors: {Errors}", errors);
                    return Result.Failures(errors);
                }

                var pet = await _unitOfWork.PetRepository.GetByIdAsync(request.PetId);
                if (pet == null)
                {
                    _logger.LogWarning("Pet with ID {PetId} not found.", request.PetId);
                    return Result.Failure(PetErrorMessage.FieldIsEmpty("pet"));
                }

                var appointment = new Appointment
                {
                    CustomerId = request.CustomerId,
                    PetId = request.PetId,
                    PetServiceId = request.PetServiceId,
                    Status = "Pending",
                    AppointmentDate = request.AppointmentDate,
                    AppointmentVeterinarians = new List<AppointmentVeterinarian>() // Initialize as empty list
                };


                // Assigning Veterinarian(s)
                if (request.VeterinarianIds == null || !request.VeterinarianIds.Any())
                {
                    var availableVeterinarian = await _unitOfWork.AppointmentRepository.GetAvailableVeterinarianAsync(appointment.AppointmentDate);
                    if (availableVeterinarian != null)
                    {
                        appointment.AppointmentVeterinarians.Add(new AppointmentVeterinarian { VeterinarianId = availableVeterinarian.Id });
                        _logger.LogInformation("Assigned available veterinarian ID {VeterinarianId} to the appointment.", availableVeterinarian.Id);
                    }
                    else
                    {
                        _logger.LogWarning("No available veterinarian found for the appointment date.");
                    }
                }
                else
                {
                    appointment.AppointmentVeterinarians = request.VeterinarianIds.Select(v => new AppointmentVeterinarian
                    {
                        VeterinarianId = v
                    }).ToList();
                    _logger.LogInformation("Assigned {Count} veterinarian(s) to the appointment.", appointment.AppointmentVeterinarians.Count);
                }

                // Save the Appointment
                await _unitOfWork.AppointmentRepository.CreateAppointmentAsync(appointment);
                _logger.LogInformation("Successfully created appointment with ID: {AppointmentId}", appointment.Id);

                // Update Veterinarian Schedule Availability
                foreach (var veterinarian in appointment.AppointmentVeterinarians)
                {
                    if (veterinarian.VeterinarianId != Guid.Empty)
                    {
                        await _unitOfWork.AppointmentRepository.UpdateScheduleAvailabilityAsync(veterinarian.VeterinarianId, appointment.AppointmentDate);
                        _logger.LogInformation("Updated schedule availability for veterinarian ID {VeterinarianId}.", veterinarian.VeterinarianId);
                    }
                }


                var response = new CreateResponse { Id = appointment.Id };
                return Result.SuccessWithObject(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating appointment for service. Customer ID: {CustomerId}, Pet ID: {PetId}", request.CustomerId, request.PetId);
                return Result.Failure(Error.Failure("CreationError", "An unexpected error occurred while creating the appointment."));
            }
        }

        public async Task<Result> MakeAppointmentForComboAsync(MakeAppointmentForComboRequest request)
        {
            try
            {
                _logger.LogInformation("Attempting to create combo appointment. Customer ID: {CustomerId}, Pet ID: {PetId}", request.CustomerId, request.PetId);

                // Validate the input
                var validationResult = await _comboValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => (Error)e.CustomState).ToList();
                    _logger.LogWarning("Validation failed for combo appointment request. Errors: {Errors}", errors);
                    return Result.Failures(errors);
                }

                var appointment = new Appointment
                {
                    CustomerId = request.CustomerId,
                    PetId = request.PetId,
                    ComboServiceId = request.ComboServiceId,
                    AppointmentDate = DateTime.UtcNow, // User-selected date if applicable
                    Status = "Pending"
                };

                await _unitOfWork.AppointmentRepository.CreateAppointmentAsync(appointment);
                _logger.LogInformation("Successfully created combo appointment with ID: {AppointmentId}", appointment.Id);

                var response = new CreateResponse { Id = appointment.Id };
                return Result.SuccessWithObject(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating combo appointment. Customer ID: {CustomerId}, Pet ID: {PetId}", request.CustomerId, request.PetId);
                return Result.Failure(Error.Failure("CreationError", "An unexpected error occurred while creating the combo appointment."));
            }
        }
    }
}
