﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace hotel_management_api.Database.Model
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Avatar { get; set; }
        public string? ResetPasswordToken { get; set; }
        public string? RefreshToken { get; set; }
        [DefaultValue(false)]
        public bool IsBlock { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
    }
}
