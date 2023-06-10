using hotel_management_api.Business.Interactor.Location;
using Microsoft.AspNetCore.Mvc;

namespace hotel_management_api.APIs.Location
{
    [Route("/api/v{version:apiVersion}/location")]
    [ApiVersion("1.0")]
    public class LocationController: ControllerBase
    {
        private readonly IGetHomeletInteractor getHomeletInteractor;
        private readonly IGetProvineInteractor getProvineInteractor;
        private readonly IGetDistrictInteractor getDistrictInteractor;
        public LocationController
            (
            IGetHomeletInteractor getHomeletInteractor,
            IGetProvineInteractor getProvineInteractor,
            IGetDistrictInteractor getDistrictInteractor
            )
        {
            this.getHomeletInteractor = getHomeletInteractor;
            this.getProvineInteractor = getProvineInteractor;
            this.getDistrictInteractor = getDistrictInteractor;
        }
        [HttpGet("provine")]
        public async Task<IActionResult> GetProvine()
        {
            var result = await getProvineInteractor.GetAsync();
            if(result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("district/{provineId}")]
        public async Task<IActionResult> GetDistrict(string provineId)
        {
            var result = await getDistrictInteractor.GetAsync(provineId);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("homelet/{districtId}")]
        public async Task<IActionResult> GetHomelet(string districtId)
        {
            var result = await getHomeletInteractor.GetAsync(districtId);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
