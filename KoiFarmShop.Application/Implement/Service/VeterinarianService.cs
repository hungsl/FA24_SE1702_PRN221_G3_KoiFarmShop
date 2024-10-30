using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using Microsoft.Extensions.Logging;

namespace KoiFarmShop.Application.Implement.Service;

public class VeterinarianService : IVeterinarianService
{
    private readonly IVeterinarianRepository _veterinarianRepository;
    private readonly ILogger<VeterinarianService> _logger;

    public VeterinarianService(IVeterinarianRepository veterinarianRepository, ILogger<VeterinarianService> logger)
    {
        _veterinarianRepository = veterinarianRepository;
        _logger = logger;
    }

    public async Task<Result> GetAllVeterinariansAsync()
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve all veterinarians.");

            // Retrieve all veterinarians
            var veterinarians = await _veterinarianRepository.GetAllVeterinariansAsync();

            // Check if the list is empty or null and return appropriate error if so
            if (veterinarians == null || veterinarians.Count == 0)
            {
                var notFoundError = Error.NotFound("Veterinarians", "No veterinarians found in the system.");
                _logger.LogWarning("Veterinarian retrieval failed: no veterinarians found in the system.");
                return Result.Failure(notFoundError);
            }

            _logger.LogInformation("Successfully retrieved all veterinarians. Count: {Count}", veterinarians.Count);

            // Return success with the list of veterinarians
            return Result.SuccessWithObject(veterinarians);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving veterinarians.");
            var error = Error.Failure("Veterinarian Retrieval Error", "An unexpected error occurred while retrieving veterinarians.");
            return Result.Failure(error);
        }
    }

}