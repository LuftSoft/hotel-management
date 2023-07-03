using hotel_management_api.APIs.Categories.DTOs;
using hotel_management_api.Business.Interactor.Category;
using hotel_management_api.Database.Model;
using hotel_management_api.Database.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace hotel_management_api.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHotelCategoryRepository _categoryRepository;
        public CategoryService(
            IHotelCategoryRepository categoryRepository
            ) 
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<ICreateCategoryInteractor.Response> createAsync(string categoryName)
        {
            try
            {
                var isExists = await _categoryRepository.GetByCategoryName(categoryName);
                if (isExists != null)
                {
                    return new ICreateCategoryInteractor.Response()
                    {
                        Success = false,
                        Message = $"Category name is aready exist",
                    };
                }
                HotelCategory category = await _categoryRepository.CreateAsync(categoryName);
                return new ICreateCategoryInteractor.Response()
                {
                    Success = true,
                    Message = "Create category success!",
                    category = new CategoryDto()
                    {
                        Id = category.Id,
                        Name = categoryName
                    }
                };
            }
            catch(Exception ex)
            {
                return new ICreateCategoryInteractor.Response()
                {
                    Success = false,
                    Message = $"An error occur when create category: {ex.Message}",
                };
            }
        }
        public async Task<IUpdateCategoryInteractor.Response> updateAsync(CategoryDto categoryDto)
        {
            try
            {
                var isExists = await _categoryRepository.GetByCategoryName(categoryDto.Name);
                if (isExists != null)
                {
                    return new IUpdateCategoryInteractor.Response()
                    {
                        Success = false,
                        Message = $"Category name is aready exist",
                    };
                }
                HotelCategory category = await _categoryRepository.UpdateAsync(categoryDto);
                return new IUpdateCategoryInteractor.Response()
                {
                    Success = true,
                    Message = "Update category success!",
                    category = new CategoryDto()
                    {
                        Id = categoryDto.Id,
                        Name = categoryDto.Name
                    }
                };
            }
            catch (Exception ex)
            {
                return new IUpdateCategoryInteractor.Response()
                {
                    Success = false,
                    Message = $"An error occur when update category: {ex.Message}",
                };
            }
        }
        public async Task<IDeleteCategoryInteractor.Response> deleteAsync(int categoryId)
        {
            try
            {
                var isExists = await _categoryRepository.GetByCategoryId(categoryId);
                if (isExists == null)
                {
                    return new IDeleteCategoryInteractor.Response()
                    {
                        Success = false,
                        Message = $"Category name is't exist",
                    };
                }
                bool isSuccess = await _categoryRepository.DeleteAsync(categoryId);
                if (isSuccess)
                {
                    return new IDeleteCategoryInteractor.Response()
                    {
                        Success = true,
                        Message = "Delete category success!",
                    };
                }
                return new IDeleteCategoryInteractor.Response()
                {
                    Success = false,
                    Message = "Delete category failed!",
                };
            }
            catch (Exception ex)
            {
                return new IDeleteCategoryInteractor.Response()
                {
                    Success = false,
                    Message = $"An error occur when delete category: {ex.Message}",
                };
            }
        }
        public async Task<IGetAllCategoryInteractor.Response> GetAllAsync()
        {
            try
            {
                var listCate = (await _categoryRepository.GetAll()).ToList();
                var listCateDto = new List<CategoryDto>();
                listCate.ForEach(cate => {
                    listCateDto.Add(new CategoryDto()
                    {
                        Id = cate.Id,
                        Name = cate.Name
                    });
                });
                return new IGetAllCategoryInteractor.Response()
                {
                    category = listCateDto,
                    Message = "success",
                    Success = true
                };
            }
            catch(Exception ex)
            {
                return new IGetAllCategoryInteractor.Response()
                {
                    Message = "failed when get all category",
                    Success = false
                };
            }
        }
    }
}
