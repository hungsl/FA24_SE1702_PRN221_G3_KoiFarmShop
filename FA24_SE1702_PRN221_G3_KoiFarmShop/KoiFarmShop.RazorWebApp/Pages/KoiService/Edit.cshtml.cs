using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;

namespace KoiFarmShop.RazorWebApp.Pages.KoiService
{
    public class EditModel : PageModel
    {
        private readonly IPetServiceService _petServiceService;
        private readonly IPetServiceCategoryService _petCategoryService;
        public EditModel(IPetServiceService petServiceService, IPetServiceCategoryService petCategoryService)
        {
            _petServiceService = petServiceService;
            _petCategoryService = petCategoryService;
        }

        [BindProperty]
        public PetService PetService { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var result = await _petServiceService.GetPetServiceByIdAsync(id);
          
            if (result.IsSuccess)
            {
                // Chuyển Object thành danh sách PetService
                PetService = result.Object as PetService ?? new PetService();
            }
            else
            {
                PetService = new PetService(); // Xử lý khi thất bại
            }
            var PetType = await _petCategoryService.GetAllPetServiceCategoriesAsync();
               var PetCategory = PetType.Object as List<PetServiceCategory>;
            ViewData["PetServiceCategoryId"] = new SelectList(PetCategory, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var addPetServiceRequest = new AddPetServiceRequest
                {
                    Name = PetService.Name,
                    PetServiceCategoryId = PetService.PetServiceCategoryId,
                    BasePrice = PetService.BasePrice,
                    Duration = PetService.Duration,
                    ImageUrl = PetService.ImageUrl,
                    AvailableFrom = PetService.AvailableFrom,
                    AvailableTo = PetService.AvailableTo,
                    TravelCost = PetService.TravelCost,
                    MaxNumberOfPets = PetService.MaxNumberOfPets,
                    Description = PetService.Description
                };
                var result = await _petServiceService.UpdatePetServiceAsync(PetService.Id, addPetServiceRequest);

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
                            case "PetService.Empty":
                                ModelState.AddModelError("PetService.Name", error.Description);
                                break;
                            case "PetService.InvalidValue":
                                ModelState.AddModelError("PetService.BasePrice", error.Description);
                                break;
                            case "PetService.InvalidDate":
                                ModelState.AddModelError("PetService.AvailableFrom", error.Description);
                                break;
                            default:
                                ModelState.AddModelError(string.Empty, error.Description);
                                break;
                        }
                    }
                    // Cần gọi lại ViewBag để có dữ liệu cho dropdown
                    var petCat = await _petCategoryService.GetAllPetServiceCategoriesAsync();
                    ViewData["PetServiceCategoryId"] = new SelectList(petCat.Object as IList<PetServiceCategory>, "Id", "Name");
                    // Hiển thị lại trang với các lỗi trong ModelState
                    return Page();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                // Kiểm tra nếu đối tượng không tồn tại
                if (!PetServiceExists(PetService.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool PetServiceExists(Guid id)
        {
            var petService =  _petServiceService.GetPetServiceByIdAsync(id);
            return petService != null;

        }
    }
}
