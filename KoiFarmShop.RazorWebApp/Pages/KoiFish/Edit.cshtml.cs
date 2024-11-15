using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;
using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;

namespace KoiFarmShop.RazorWebApp.Pages.KoiFish
{
    public class EditModel : PageModel
    {
        private readonly IPetServiceLogic _petServiceLogic;

        public EditModel(IPetServiceLogic petServiceLogic)
        {
            _petServiceLogic = petServiceLogic;
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _petServiceLogic.GetPetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound();
            }

            Pet = result.Object as Pet;
<<<<<<< HEAD

=======
            var owners = _petServiceLogic.GetAllOwnerAsync().Result.Object as List<User>;
            ViewData["OwnerId"] = new SelectList(owners, "Id", "FullName");
>>>>>>> Dev_Danh_skibidi
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var addPetRequest = new AddPetRequest
                {
                    Name = Pet.Name,
                    Age = Pet.Age,
                    Gender = Pet.Gender,
                    ImageUrl = Pet.ImageUrl,
                    Color = Pet.Color,
                    Length = Pet.Length,
                    Weight = Pet.Weight,
                    Quantity = Pet.Quantity,
                    LastHealthCheck = Pet.LastHealthCheck,
                    Note = Pet.Note,
<<<<<<< HEAD
                    HealthStatus = Pet.HealthStatus
=======
                    OwnerId = Pet.OwnerId,
>>>>>>> Dev_Danh_skibidi
                };
                var result = await _petServiceLogic.UpdatePetAsync(Pet.Id, addPetRequest);

                // Kiểm tra kết quả trả về từ dịch vụ
                if (result.IsSuccess)
                {
                    // Nếu thành công, chuyển hướng về trang index hoặc trang cần thiết
                    return RedirectToPage("./Index");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        switch (error.Code)
                        {
                            case "Pet.Empty":
                                if (error.Description.Contains("Name"))
                                    ModelState.AddModelError("Pet.Name", error.Description);
                                if (error.Description.Contains("Gender"))
                                    ModelState.AddModelError("Pet.Gender", error.Description);
                                if (error.Description.Contains("Color"))
                                    ModelState.AddModelError("Pet.Color", error.Description);
                                if (error.Description.Contains("ImageUrl"))
                                    ModelState.AddModelError("Pet.ImageUrl", error.Description);
                                if (error.Description.Contains("Note"))
                                    ModelState.AddModelError("Pet.Note", error.Description);
                                break;
                            case "Pet.InvalidValue":
                                if (error.Description.Contains("Age"))
                                    ModelState.AddModelError("Pet.Age", error.Description);
                                if (error.Description.Contains("Length"))
                                    ModelState.AddModelError("Pet.Length", error.Description);
                                if (error.Description.Contains("Weight"))
                                    ModelState.AddModelError("Pet.Weight", error.Description);
                                if (error.Description.Contains("Quantity"))
                                    ModelState.AddModelError("Pet.Quantity", error.Description);
                                if (error.Description.Contains("HealthStatus"))
                                    ModelState.AddModelError("Pet.HealthStatus", error.Description);
                                break;
                            default:
                                ModelState.AddModelError(string.Empty, error.Description);
                                break;
                        }
                    }
<<<<<<< HEAD
=======
                    var owners = _petServiceLogic.GetAllOwnerAsync().Result.Object as List<User>;
                    ViewData["OwnerId"] = new SelectList(owners, "Id", "FullName");
>>>>>>> Dev_Danh_skibidi
                    return Page();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                // Kiểm tra nếu đối tượng không tồn tại
                if (!PetExists(Pet.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        private bool PetExists(Guid id)
        {
            var petService = _petServiceLogic.GetPetByIdAsync(id);
            return petService != null;
        }
    }
}
