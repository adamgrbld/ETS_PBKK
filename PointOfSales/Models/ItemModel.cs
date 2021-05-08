using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointOfSales.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
    }
}