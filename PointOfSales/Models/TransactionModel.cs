using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointOfSales.Models
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}