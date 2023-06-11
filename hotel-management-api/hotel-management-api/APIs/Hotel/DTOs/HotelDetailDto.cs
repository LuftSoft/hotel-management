using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace hotel_management_api.APIs.Hotel.DTOs
{
    public class HotelDetailDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Star { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? LogoLink { get; set; }
        public string? Slug { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? USerId { get; set; }
        public HotelCategory? HotelCategory { set; get; }
        public HotelBenefit? HotelBenefit { set; get; }
        public int HomeletId { set; get; }
        public List<RoomDetailDto> Rooms { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
