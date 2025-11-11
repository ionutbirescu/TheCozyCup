using System;
using System.Collections.Generic;
using System.Text;

namespace TheCozyCup
{
    public class SalesLogEntry
    {
        public Guid orderID { get; set; }
        public DateTime saleDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal FinalTotal { get; set; }
        public Dictionary<string, int> ItemsSold { get; set; }

        // Stores the items sold as a dictionary: Key=ItemName, Value=Quantity
        public SalesLogEntry(Order order)
        {
            if (!order.IsFinalized)
            {
                throw new InvalidOperationException("Cannot create a sales log entry for an unfinalized order.");
            }
            this.orderID = order.orderId;
            this.saleDate = order.orderDate;
            this.Subtotal = order.GetSubTotal();
            this.DiscountPercentage = order.DiscountPercentage;
            this.FinalTotal = order.FinalTotal;

            this.ItemsSold = order.lineItems.ToDictionary(li => li.Item.Name, li => li.Quantity);
        }


    }
}