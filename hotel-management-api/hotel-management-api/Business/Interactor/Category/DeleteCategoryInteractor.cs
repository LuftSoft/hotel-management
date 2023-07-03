using hotel_management_api.Business.Services;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Business.Interactor.Category
{
    public interface IDeleteCategoryInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IDeleteCategoryInteractor.Response> DeleteAsync(int categoryId);
    }
    public class DeleteCategoryInteractor : IDeleteCategoryInteractor
    {
        private readonly ICategoryService _categoryService;
        public DeleteCategoryInteractor(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IDeleteCategoryInteractor.Response> DeleteAsync(int categoryId)
        {
            return await _categoryService.deleteAsync(categoryId);
        }
    }
}
