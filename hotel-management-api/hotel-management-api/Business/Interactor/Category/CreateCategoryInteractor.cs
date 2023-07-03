using hotel_management_api.APIs.Categories.DTOs;
using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Business.Interactor.Category
{
    public interface ICreateCategoryInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public CategoryDto? category { set; get; }
        }
        Task<ICreateCategoryInteractor.Response> CreateAsync(string cateName);
    }
    public class CreateCategoryInteractor : ICreateCategoryInteractor
    {
        private readonly ICategoryService _categoryService;
        public CreateCategoryInteractor(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<ICreateCategoryInteractor.Response> CreateAsync(string cateName)
        {
            return await _categoryService.createAsync(cateName);
        }
    }
}
