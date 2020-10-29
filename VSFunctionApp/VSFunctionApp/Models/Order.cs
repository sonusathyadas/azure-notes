using System;
using System.Collections.Generic;
using System.Text;

namespace VSFunctionApp.Models
{
    public class Order
    {
        public string OrderId { get; set; }

        public string CustomerName { get; set; }

        public double Amount { get; set; }

        public DateTime OrderDate { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerMobile { get; set; }

        public string Status { get; set; }
    }
}
