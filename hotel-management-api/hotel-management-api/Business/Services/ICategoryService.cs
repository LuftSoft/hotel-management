using hotel_management_api.APIs.Categories.DTOs;
using hotel_management_api.Business.Interactor.Category;

namespace hotel_management_api.Business.Services
{
    public interface ICategoryService
    {
        Task<ICreateCategoryInteractor.Response> createAsync(string categoryName);
        Task<IUpdateCategoryInteractor.Response> updateAsync(CategoryDto categoryDto);
        Task<IDeleteCategoryInteractor.Response> deleteAsync(int categoryId);
        Task<IGetAllCategoryInteractor.Response> GetAllAsync();
    }
}
