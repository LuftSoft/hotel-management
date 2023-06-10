using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api.APIs.Location.DTOs
{
    public class HomeletDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
    }
}
