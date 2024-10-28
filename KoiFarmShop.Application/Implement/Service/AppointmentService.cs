using FluentValidation;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using KoiFarmShop.Infrastructure.Interface;


namespace KoiFarmShop.Application.Implement.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<MakeAppointmentForServiceRequest> _serviceValidator;
        private readonly IValidator<MakeAppointmentForComboRequest> _comboValidator;

        public AppointmentService(IUnitOfWork unitOfWork,
                             IValidator<MakeAppointmentForServiceRequest> serviceValidator,
                             IValidator<MakeAppointmentForComboRequest> comboValidator)
        {
            _unitOfWork = unitOfWork;
            _serviceValidator = serviceValidator;
            _comboValidator = comboValidator;
        }


        public async Task<Result> DeleteAppointmentAsync(Guid appointmentId)
        {
            var deleted = await _unitOfWork.AppointmentRepository.DeleteAppointmentAsync(appointmentId);

            if (!deleted)
            {
                return Result.Failure(Error.NotFound("Appointment", "The appointment was not found or is already deleted"));
            }

            return Result.Success();
        }
        public async Task<Result> GetAllAppointmentsAsync()
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsAsync();

            if (appointments == null || !appointments.Any())
            {
                return Result.Failure(Error.NotFound("Appointments", "No appointments found"));
            }

            return Result.SuccessWithObject(appointments);
        }


        public async Task<Result> GetByIdAsync(Guid appointmentId)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return Result.Failure(Error.NotFound("Appointment", "Appointment not found."));
            }

            return Result.SuccessWithObject(appointment);
        }




        public async Task<Result> MakeAppointmentForServiceAsync(MakeAppointmentForServiceRequest request)
        {
            // Validate the input
            var validationResult = await _serviceValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();
                return Result.Failures(errors);
            }

            var pet = await _unitOfWork.PetRepository.GetByIdAsync(request.PetId);
            if (pet == null)
            {
                return Result.Failure(PetErrorMessage.FieldIsEmpty("pet"));
            }

            var appointment = new Appointment
            {
                CustomerId = request.CustomerId,
                PetId = request.PetId,
                PetServiceId = request.PetServiceId,
                Status = "Pending",
                AppointmentDate = request.AppointmentDate,
            };

            // Assigning Veterinarian(s)
            if (request.VeterinarianIds == null || !request.VeterinarianIds.Any())
            {
                var availableVeterinarian = await _unitOfWork.AppointmentRepository.GetAvailableVeterinarianAsync(appointment.AppointmentDate);
                if (availableVeterinarian != null)
                {
                    appointment.AppointmentVeterinarians = new List<AppointmentVeterinarian>
            {
                new AppointmentVeterinarian { VeterinarianId = availableVeterinarian.Id }
            };
                }
            }
            else
            {
                appointment.AppointmentVeterinarians = request.VeterinarianIds.Select(v => new AppointmentVeterinarian
                {
                    VeterinarianId = v
                }).ToList();
            }

            // Save the Appointment
            await _unitOfWork.AppointmentRepository.CreateAppointmentAsync(appointment);

            // Update Veterinarian Schedule Availability
            foreach (var veterinarian in appointment.AppointmentVeterinarians)
            {
                await _unitOfWork.AppointmentRepository.UpdateScheduleAvailabilityAsync(
                    veterinarian.VeterinarianId,
                    appointment.AppointmentDate
                );
            }

            var response = new CreateResponse { Id = appointment.Id };
            return Result.SuccessWithObject(response);
        }


        public async Task<Result> MakeAppointmentForComboAsync(MakeAppointmentForComboRequest request)
        {
            // Validate the input
            var validationResult = await _comboValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();
                return Result.Failures(errors);
            }

            //var pet = await _unitOfWork.PetRepository.GetByIdAsync(request.PetId);
            //if (pet == null)
            //{
            //    return Result.Failure("Pet not found.");
            //}

            var appointment = new Appointment
            {
                CustomerId = request.CustomerId,
                PetId = request.PetId,
                ComboServiceId = request.ComboServiceId,
                AppointmentDate = DateTime.UtcNow, // Hoặc thời gian mà người dùng chọn
                Status = "Pending"
            };

            await _unitOfWork.AppointmentRepository.CreateAppointmentAsync(appointment);
            var response = new CreateResponse { Id = appointment.Id };
            return Result.SuccessWithObject(response);
        }


    }
}
