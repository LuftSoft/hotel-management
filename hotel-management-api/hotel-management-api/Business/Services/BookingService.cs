using hotel_management_api.APIs.Booking.DTOs;
using hotel_management_api.Business.Interactor.Booking;
using hotel_management_api.Database.Model;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;

namespace hotel_management_api.Business.Services
{
    public class BookingService : IBookingService
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IUserService userService;
        private readonly IRoomRepository roomRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly IBookingRepository bookingRepository;

        public BookingService(
            IJwtUtil jwtUtil,
            IUserService userService,
            IHotelRepository hotelRepository,
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository
            )
        {
            this.jwtUtil = jwtUtil;
            this.userService = userService;
            this.hotelRepository = hotelRepository;
            this.roomRepository = roomRepository;
            this.bookingRepository = bookingRepository;
        }
        public async Task<IGetAllBookingByHotelInteractor.Response> GetByHotelIdAsync(int hotelId)
        {
            try
            {
                List<BookingListDto> bookingListDtos = new List<BookingListDto>();
                var bookings = await bookingRepository.GetByHotelId(hotelId);
                foreach (var booking in bookings)
                {
                    var room = await roomRepository.GetByIdAsync(booking.RoomId);
                    var hotel = await hotelRepository.getOne(room.HotelId);
                    if (room == null || hotel == null) continue;
                    bookingListDtos.Add(new BookingListDto()
                    {
                        Id = booking.Id,
                        RoomId = booking.RoomId,
                        RoomSize = room.NumOfPeope,
                        RoomName = room.Name,
                        Status = booking.Status,
                        Returned = booking.Returned,
                        HotelId = hotel.Id,
                        HotelName = hotel.Name,
                        HotelImage = hotel.LogoLink,
                        FromDate = booking.FromDate,
                        ToDate = booking.ToDate
                    });
                }
                return new IGetAllBookingByHotelInteractor.Response()
                {
                    Success = true,
                    Message = "Get list success",
                    BookingList = bookingListDtos
                };
            }
            catch (Exception ex)
            {
                return new IGetAllBookingByHotelInteractor.Response()
                {
                    Success = false,
                    Message = $"Get list failed. Error: {ex}"
                };
            }
        }
        public async Task<IGetAllBookingByUserInteractor.Response> GetByUserIdAsync(IGetAllBookingByUserInteractor.Request request)
        {
            try
            {
                string userId = await userService.GetUserIdFromToken(request.token);
                if (userId == null)
                {
                    return new IGetAllBookingByUserInteractor.Response()
                    {
                        Success = false,
                        Message = "Get list failed"
                    };
                }
                List<BookingListDto> bookingListDtos = new List<BookingListDto>();
                var bookings = await bookingRepository.GetByUserId(userId);
                foreach (var booking in bookings)
                {
                    var room = await roomRepository.GetByIdAsync(booking.RoomId);
                    var hotel = await hotelRepository.getOne(room.HotelId);
                    if(room == null || hotel == null) continue;
                    bookingListDtos.Add(new BookingListDto()
                    {
                        Id = booking.Id,
                        RoomId = booking.RoomId,
                        RoomSize = room.NumOfPeope,
                        RoomName = room.Name,
                        Status = booking.Status,
                        Returned = booking.Returned,
                        HotelId = hotel.Id,
                        HotelName = hotel.Name,
                        HotelImage = hotel.LogoLink,
                        FromDate = booking.FromDate,
                        ToDate = booking.ToDate
                    });
                }
                return new IGetAllBookingByUserInteractor.Response()
                {
                    Success = true,
                    Message = "Get list success",
                    BookingList = bookingListDtos
                };
            }
            catch (Exception ex)
            {
                return new IGetAllBookingByUserInteractor.Response()
                {
                    Success = false,
                    Message = $"Get list failed. Error: {ex}"
                };
            }
        }
        public async Task<ICreateBookingRoomInteractor.Response> CreateAsync(ICreateBookingRoomInteractor.Request request)
        {
            try
            {
                string userId = await userService.GetUserIdFromToken(request.token);
                if (userId == null)
                {
                    return new ICreateBookingRoomInteractor.Response()
                    {
                        Success = false,
                        Message = "Booking failed"
                    };
                }
                var bookingDto = request.Booking;
                Booking booking = new Booking();
                booking.UserId = userId;
                booking.FromDate = bookingDto.FromDate;
                booking.ToDate = bookingDto.ToDate;
                booking.RoomId = bookingDto.RoomId;
                booking.Status = bookingDto.Status;
                booking.CreateDate = DateTime.Now;
                booking.CreateDate = DateTime.Now;
                booking.Returned = false;
                var isSuccess = await bookingRepository.Create(booking);
                if (isSuccess)
                {
                    return new ICreateBookingRoomInteractor.Response()
                    {
                        Success = true,
                        Message = "Booking success"
                    };
                }
                return new ICreateBookingRoomInteractor.Response()
                {
                    Success = false,
                    Message = "Booking failed"
                };
            }
            catch (Exception ex)
            {
                return new ICreateBookingRoomInteractor.Response()
                {
                    Success = false,
                    Message = $"Error occur when booking. Error: {ex}"
                };
            }
        }
        public async Task<IUpdateBookingRoomInteractor.Response> UpdateAsync(IUpdateBookingRoomInteractor.Request request)
        {
            try
            {
                BookingDto requestBooking = request.Booking;
                string userId = await userService.GetUserIdFromToken(request.token);
                Booking booking = await bookingRepository.GetOne(request.Booking.Id);
                if (userId == null || booking == null || userId != booking.UserId)
                {
                    return new IUpdateBookingRoomInteractor.Response()
                    {
                        Success = false,
                        Message = "Update booking information failed"
                    };
                }
                booking.UpdateDate = DateTime.Now;
                booking.Returned = requestBooking.Returned;
                booking.FromDate = requestBooking.FromDate;
                booking.ToDate = requestBooking.ToDate;
                await bookingRepository.Update(booking);
                return new IUpdateBookingRoomInteractor.Response()
                {
                    Success = true,
                    Message = "Update booking information success"
                };
            } catch(Exception ex)
            {
                return new IUpdateBookingRoomInteractor.Response()
                {
                    Success = false,
                    Message = "Exception occur when update booking information"
                };
            }
        }
        public async Task<ICancelBookingRoomInteractor.Response> CancelAsync(ICancelBookingRoomInteractor.Request request)
        {
            try
            {
                string userId = await userService.GetUserIdFromToken(request.token);
                Booking booking = await bookingRepository.GetOne(request.BookingId);
                if (userId == null || booking == null || userId != booking.UserId)
                {
                    return new ICancelBookingRoomInteractor.Response()
                    {
                        Success = false,
                        Message = "Update booking information failed"
                    };
                }
                booking.Returned = true;
                await bookingRepository.Update(booking);
                return new ICancelBookingRoomInteractor.Response()
                {
                    Success = true,
                    Message = "Update booking information success"
                };
            }
            catch(Exception ex) 
            {
                return new ICancelBookingRoomInteractor.Response()
                {
                    Success = false,
                    Message = "Error occur when cancel booking information"
                };
            }
        }
        public async Task<IGetAllBookingInteractor.Response> GetAll(IGetAllBookingInteractor.Request request)
        {
            try
            {
                if (request.pageSize == 0) request.pageSize = 10;
                List<BookingListDto> bookingListDtos = new List<BookingListDto>();
                var bookings = await bookingRepository.GetAll();
                bookings = bookings.Skip(request.pageIndex * request.pageSize).Take(request.pageSize);
                foreach (var booking in bookings)
                {
                    var room = await roomRepository.GetByIdAsync(booking.RoomId);
                    var hotel = await hotelRepository.getOne(room.HotelId);
                    if (room == null || hotel == null) continue;
                    bookingListDtos.Add(new BookingListDto()
                    {
                        Id = booking.Id,
                        RoomId = booking.RoomId,
                        RoomSize = room.NumOfPeope,
                        RoomName = room.Name,
                        Status = booking.Status,
                        Returned = booking.Returned,
                        HotelId = hotel.Id,
                        UserId = booking.UserId,
                        HotelName = hotel.Name,
                        HotelImage = hotel.LogoLink,
                        FromDate = booking.FromDate,
                        ToDate = booking.ToDate
                    });
                }
                return new IGetAllBookingInteractor.Response()
                {
                    Success = true,
                    Message = "Error occur when get list  booking",
                    BookingList = bookingListDtos
                };
            }
            catch(Exception ex)
            {
                return new IGetAllBookingInteractor.Response()
                {
                    Success = false,
                    Message = "Error occur when get list  booking"
                };
            }
        }
    }
}
