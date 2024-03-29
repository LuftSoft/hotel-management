﻿using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace hotel_management_api.Database.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext appDbContext;
        public CommentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Comment?> FindByIdAsync(int id)
        {
            try
            {
                return await appDbContext.Comments.Where(c => c.Id == id)
                .Include(c => c.Booking)
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Comment repository FindByIdAsync -> {ex.Message}");
                return null;
            }
        }
        public async Task<IEnumerable<CommentDto>?> FindByHotelId(int hotelId)
        {
            try
            {
                return await appDbContext.Comments
                .Include(c => c.Booking)
                .ThenInclude(b => b.User)
                .Where(c => c.Booking.Room.HotelId == hotelId)
                .Select(c => new CommentDto()
                {
                    Id = c.Id,
                    BookingId = c.BookingId,
                    UserId = c.Booking.UserId,
                    Content = c.Content,
                    CreateDate = c.CreateDate,
                    LastChange = c.LastChange,
                    Rating = c.Rating,
                    UserName = $"{c.Booking.User.FirstName} {c.Booking.User.LastName}"
                })
                .ToListAsync();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Comment repository FindByIdAsync -> {ex.Message}");
                return null;
            }
        }
        public async Task<Comment> FindByBookingId(int id)
        {
            return await appDbContext.Comments.FirstOrDefaultAsync(c => c.BookingId == id);
        }

        public async Task<Comment?> CreateAsync(Comment comment)
        {
            try
            {
                appDbContext.Comments.Add(comment);
                await this.appDbContext.SaveChangesAsync();
                return comment;
            }catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Comment repository createasync -> {ex.Message}");
                return null;
            }
        }
        public async Task<bool> UpdateAsync(Comment comment)
        {
            try
            {
                appDbContext.Comments.Update(comment);
                await this.appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Comment repository updateasync -> {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var comment = await appDbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
                if (comment == null)
                    return false;
                appDbContext.Comments.Remove(comment);
                await this.appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Comment repository delete comment -> {ex.Message}");
                return false;
            }
        }
    }
}
