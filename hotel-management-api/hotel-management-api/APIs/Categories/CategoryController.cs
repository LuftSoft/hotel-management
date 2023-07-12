using hotel_management_api.APIs.Categories.DTOs;
using hotel_management_api.Business.Interactor.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotel_management_api.APIs.Categories
{
    [Route("api/v{version:apiVersion}/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGetAllCategoryInteractor _getAllCategoryInteractor;
        private readonly ICreateCategoryInteractor _createCategoryInteractor;
        private readonly IUpdateCategoryInteractor _updateCategoryInteractor;
        private readonly IDeleteCategoryInteractor _deleteCategoryInteractor;
        public CategoryController
            (
            IGetAllCategoryInteractor getAllCategoryInteractor,
            ICreateCategoryInteractor createCategoryInteractor,
            IUpdateCategoryInteractor updateCategoryInteractor,
            IDeleteCategoryInteractor deleteCategoryInteractor
            )
        {
            _getAllCategoryInteractor = getAllCategoryInteractor;
            _createCategoryInteractor = createCategoryInteractor;
            _updateCategoryInteractor = updateCategoryInteractor;
            _deleteCategoryInteractor = deleteCategoryInteractor;
        }
        [HttpGet] 
        public async Task<IActionResult> Get()
        {
            var result = await _getAllCategoryInteractor.GetAllAsync();
            return Ok(result);
        }
        [HttpPost]
        [Authorize("owner")]
        public async Task<IActionResult> Post(string cateName)
        {
            var result = await _createCategoryInteractor.CreateAsync(cateName);
            return Ok(result);
        }
        [HttpPut]
        [Authorize("owner")]
        public async Task<IActionResult> Put(CategoryDto dto)
        {
            var result = await _updateCategoryInteractor.UpdateAsync(new IUpdateCategoryInteractor.Request() { category = dto});
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize("owner")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _deleteCategoryInteractor.DeleteAsync(id);
            return Ok(result);
        }
    }
}
