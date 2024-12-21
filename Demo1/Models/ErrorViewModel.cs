using System;
using System.Collections.Generic;

namespace Demo1.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class Cart
    {
        public string Name { get; set; }
        public List<CartLine> Lines { get; set; }
    }

    public class CartLine
    {
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
