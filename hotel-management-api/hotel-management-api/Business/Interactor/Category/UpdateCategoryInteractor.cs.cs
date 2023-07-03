using hotel_management_api.APIs.Categories.DTOs;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Business.Interactor.Category
{
    public interface IUpdateCategoryInteractor
    {
        public class Request
        {
            public CategoryDto category { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public CategoryDto? category { set; get; }
        }
        Task<IUpdateCategoryInteractor.Response> UpdateAsync(IUpdateCategoryInteractor.Request request);
    }
    public class UpdateCategoryInteractor : IUpdateCategoryInteractor
    {
        private readonly ICategoryService _categoryService;
        public UpdateCategoryInteractor(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IUpdateCategoryInteractor.Response> UpdateAsync(IUpdateCategoryInteractor.Request request)
        {
            return await _categoryService.updateAsync(request.category);
        }
    }
}
