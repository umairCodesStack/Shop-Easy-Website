﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductColor
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Color { get; set; }
        public Product Product { get; set; }
    }
}
