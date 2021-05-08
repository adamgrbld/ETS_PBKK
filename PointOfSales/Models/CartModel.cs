using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointOfSales.Models
{
    public class CartModel
    {
        public int CartId { get; set; }
        public ItemModel Item { get; set; }
        public TransactionModel Transaction { get; set; }
        public int Amount { get; set; }
    }
}