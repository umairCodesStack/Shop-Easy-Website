using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class GetStoreDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public string? LogoUrl { get; set; }

        public string? BannerUrl { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }
        public string? ApprovalStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public int OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerEmail { get; set; }
        public int TotalProducts { get; set; }
        public double AverageRating { get; set; }

    }

}
