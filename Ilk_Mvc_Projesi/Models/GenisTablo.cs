using System;
using System.Collections.Generic;

#nullable disable

namespace Ilk_Mvc_Projesi.Models
{
    public partial class GenisTablo
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CusName { get; set; }
        public string ShipperName { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCity { get; set; }
        public string SaticiUlke { get; set; }
        public string City { get; set; }
        public string CustomerCountry { get; set; }
        public string SalesMan { get; set; }
    }
}
