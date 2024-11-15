﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Application.Implement.Service;
using Microsoft.AspNetCore.Mvc;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Application.Interface.IService;

namespace KoiFarmShop.RazorWebApp.Pages.KoiFish
{
    public class IndexModel : PageModel
    {
        private readonly IPetServiceLogic _petServiceLogic;

        public IndexModel(IPetServiceLogic petServiceLogic)
        {
            _petServiceLogic = petServiceLogic;
        }

<<<<<<< HEAD
        public ResultSearch<Pet> Pet { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string searchName { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string searchColor { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string searchNote { get; set; } = string.Empty;
        public async Task OnGetAsync()
        {
            var result = await _petServiceLogic.GetSearchPetAsync(searchName, searchColor, searchNote);
            if (result.IsSuccess)
            {
                Pet = result.Object as ResultSearch<Pet>;
            }
            else
            {
                Pet = new ResultSearch<Pet>
                {
=======
        public PagedResultSearch<Pet> Pet { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SearchColor { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SearchNote { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
   
        public async Task OnGetAsync()
        {
            var result = await _petServiceLogic.GetSearchPetAsync(SearchName, SearchColor, SearchNote, PageIndex, PageSize);
            if (result.IsSuccess)
            {
                // Chuyển Object thành danh sách PetService
                Pet = result.Object as PagedResultSearch<Pet> ?? new PagedResultSearch<Pet>
                {
                    TotalItems = 0,
                    Items = new List<Pet>()
                };
            }
            else
            {
                Pet = new PagedResultSearch<Pet>
                {
                    TotalItems = 0,
>>>>>>> Dev_Danh_skibidi
                    Items = new List<Pet>()
                };
            }
        }
    }
}