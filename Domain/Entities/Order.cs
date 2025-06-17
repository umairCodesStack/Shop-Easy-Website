using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int CartId { get; set; }
        [ForeignKey ("CartId")]
        public Cart Cart { get; set; }

        
    }
}
