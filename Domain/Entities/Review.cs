using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }

        // ⭐ Rating value (1–5)
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
        public string? ImageUrl { get; set; }

        // 🔹 Foreign Keys
        public int UserId { get; set; }
        public int ProductId { get; set; }

        // 🔹 Navigation Properties
        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
