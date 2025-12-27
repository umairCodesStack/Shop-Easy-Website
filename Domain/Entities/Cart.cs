using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Domain.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
        public User User { get; set; }
    }
}
