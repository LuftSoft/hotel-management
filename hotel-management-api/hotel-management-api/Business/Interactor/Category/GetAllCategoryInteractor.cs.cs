using hotel_management_api.APIs.Categories.DTOs;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Model;
using hotel_management_api.Database.Repository;

namespace hotel_management_api.Business.Interactor.Category
{
    public interface IGetAllCategoryInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public List<CategoryDto>? category { set; get; }
        }
        Task<IGetAllCategoryInteractor.Response> GetAllAsync();
    }
    public class GetAllCategoryInteractor : IGetAllCategoryInteractor
    {
        private readonly ICategoryService _categoryService;
        public GetAllCategoryInteractor(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IGetAllCategoryInteractor.Response> GetAllAsync()
        {
            return await _categoryService.GetAllAsync();
        }
    }
}
