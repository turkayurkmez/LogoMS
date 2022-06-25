﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
