using FluentValidation;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.Interface;
using KoiFarmShop.Infrastructure.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;


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
            // Nếu không có bác sĩ nào được chọn, tự động lấy bác sĩ dựa trên lịch trống
            if (request.VeterinarianIds == null || !request.VeterinarianIds.Any())
            {
                var availableVeterinarian = await _unitOfWork.AppointmentRepository.GetAvailableVeterinarianAsync(appointment.AppointmentDate);
                if (availableVeterinarian != null)
                {
                    // Chỉ gán một bác sĩ thú y cho cuộc hẹn
                    appointment.AppointmentVeterinarians = new List<AppointmentVeterinarian>
                    {
                        new AppointmentVeterinarian { VeterinarianId = availableVeterinarian.Id }
                    };
                }
            }
            else
            {
                // Nếu có bác sĩ được chọn, thêm vào cuộc hẹn
                appointment.AppointmentVeterinarians = request.VeterinarianIds.Select(v => new AppointmentVeterinarian
                {
                    VeterinarianId = v
                }).ToList();
            }
            // Lưu cuộc hẹn
            await _unitOfWork.AppointmentRepository.CreateAppointmentAsync(appointment);

            // Cập nhật trạng thái IsAvailable của lịch bác sĩ qua repository
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
