﻿    using hotel_management_api.APIs.Booking.DTOs;
using hotel_management_api.Business.Interactor.Booking;
using hotel_management_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static hotel_management_api.Extension.Middlewares.IsUserBlockMiddleware;

namespace hotel_management_api.APIs.Booking
{
    [Route("api/v{version:apiVersion}/booking")]
    [ApiController]
    [ApiVersion("1.0")]
    [TypeFilter(typeof(hotelfilter))]
    public class BookingController : ControllerBase
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IGetAllBookingInteractor getAllBookingInteractor;
        private readonly IGetAllBookingByUserInteractor getAllBookingByUser;
        private readonly ICreateBookingRoomInteractor createBookingRoomInteractor;
        private readonly IUpdateBookingRoomInteractor updateBookingRoomInteractor;
        private readonly ICancelBookingRoomInteractor cancelBookingRoomInteractor;
        private readonly IGetAllBookingByHotelInteractor getAllBookingByHotelInteractor;
        public BookingController
            (
            IJwtUtil jwtUtil,
            IGetAllBookingInteractor getAllBookingInteractor,
            IGetAllBookingByUserInteractor getAllBookingByUser,
            ICreateBookingRoomInteractor createBookingRoomInteractor,
            IUpdateBookingRoomInteractor updateBookingRoomInteractor,
            ICancelBookingRoomInteractor cancelBookingRoomInteractor,
            IGetAllBookingByHotelInteractor getAllBookingByHotelInteractor
            )
        {
            this.jwtUtil = jwtUtil;
            this.getAllBookingByUser = getAllBookingByUser;
            this.getAllBookingInteractor = getAllBookingInteractor;
            this.createBookingRoomInteractor = createBookingRoomInteractor;
            this.updateBookingRoomInteractor = updateBookingRoomInteractor;
            this.cancelBookingRoomInteractor = cancelBookingRoomInteractor; 
            this.getAllBookingByHotelInteractor = getAllBookingByHotelInteractor;
        }
        [HttpGet("all")]
        [Authorize("owner")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest("Not authorized");
            }
            var result = await getAllBookingInteractor.GetAll(new IGetAllBookingInteractor.Request()
            {
                pageIndex = pageIndex,
                pageSize = pageSize
            });
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("hotel/{id}")]
        [Authorize("admin")]
        public async Task<IActionResult> GetByHotelId(int id)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest("Not authorized");
            }
            var result = await getAllBookingByHotelInteractor.GetAllByHotelIdAsync(id);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get(int? pageIndex, int? pageSize)
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
        public async Task<IActionResult> Post([FromBody] BookingDto bookingDto) 
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest("Not authorized");
            }
            var result = await createBookingRoomInteractor.CreateAsync(new ICreateBookingRoomInteractor.Request() 
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
        [HttpPut]
        [Authorize("user")]
        public async Task<IActionResult> Put([FromBody] BookingDto bookingDto) 
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest("Not authorized");
            }
            var result = await updateBookingRoomInteractor.UpdateAsync(new IUpdateBookingRoomInteractor.Request()
            {
                Booking = bookingDto,
                token = token
            });
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest("Not authorized");
            }
            var result = await cancelBookingRoomInteractor.CancelAsync(new ICancelBookingRoomInteractor.Request()
            {
                BookingId = id,
                token = token
            });
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
