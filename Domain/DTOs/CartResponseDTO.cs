using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CartResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public UserSummaryDTO User { get; set; }

        public List<CartItemResponseDTO> Items { get; set; }

        public decimal TotalAmount =>
            Items?.Sum(i => i.TotalPrice) ?? 0;
    }

}
