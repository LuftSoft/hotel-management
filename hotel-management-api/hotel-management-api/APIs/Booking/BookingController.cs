using hotel_management_api.APIs.Booking.DTOs;
using hotel_management_api.Business.Interactor.Booking;
using hotel_management_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotel_management_api.APIs.Booking
{
    [Route("api/v{version:apiVersion}/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IGetAllBookingByUserInteractor getAllBookingByUser;
        private readonly ICreateBookingRoomInteractor createBookingRoomInteractor;
        private readonly IUpdateBookingRoomInteractor updateBookingRoomInteractor;
        private readonly ICancelBookingRoomInteractor cancelBookingRoomInteractor;
        public BookingController
            (
            IJwtUtil jwtUtil,
            IGetAllBookingByUserInteractor getAllBookingByUser,
            ICreateBookingRoomInteractor createBookingRoomInteractor,
            IUpdateBookingRoomInteractor updateBookingRoomInteractor,
            ICancelBookingRoomInteractor cancelBookingRoomInteractor
            )
        {
            this.jwtUtil = jwtUtil;
            this.getAllBookingByUser = getAllBookingByUser;
            this.createBookingRoomInteractor = createBookingRoomInteractor;
            this.updateBookingRoomInteractor = updateBookingRoomInteractor;
            this.cancelBookingRoomInteractor = cancelBookingRoomInteractor; 
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest("Not authorized");
            }
            var result = await getAllBookingByUser.GetAsync(new IGetAllBookingByUserInteractor.Request()
            {
                token = token
            });
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost]
        [Authorize("user")]
        public async Task<IActionResult> Post(BookingDto bookingDto) 
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest("Not authorized");
            }
            var result = await createBookingRoomInteractor.Create(new ICreateBookingRoomInteractor.Request() 
            {
                Booking = bookingDto,
                token = token
            });
            if(result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut] public void Put() { }
        [HttpDelete] public void Delete() { }
    }
}
