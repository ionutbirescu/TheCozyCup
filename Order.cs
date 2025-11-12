using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace TheCozyCup
{
    public class Order : ITransaction
    {
        public event OrderUpdatedEventHandler OrderUpdated;
        public Guid orderId { get; set; }
        public List<OrderLineItem> lineItems { get; set; }
        public DateTime orderDate { get; set; }

        public decimal DiscountPercentage { get; set; }
        public decimal FinalTotal { get; set; }
        public bool IsFinalized { get; set; }

        public Order()
        {
            orderId = Guid.NewGuid();
            lineItems = new List<OrderLineItem>();
            orderDate = DateTime.Now;
        }

        // Order Management Methods

        //adds a menu item to the order, consolidating if it already exists
        public void AddMenuItem(MenuItem item, int quantity)
        {
            if (IsFinalized)
            {
                throw new InvalidOperationException("Cannot modify a finalized order.");
            }
            var existingLine = lineItems.FirstOrDefault(li => li.Item.Id == item.Id); // Check if item already exists in order
            if (existingLine != null)
            {
                existingLine.IncreaseQuantity(quantity);
            }
            else
            {
                SubscribeToLineItemEvents(new OrderLineItem(item, quantity));
                lineItems.Add(new OrderLineItem(item, quantity));
                OnOrderUpdated();
            }
        }
        public void RemoveMenuItem(Guid itemId)
        {
            if (IsFinalized)
            {
                throw new InvalidOperationException("Cannot modify a finalized order.");
            }
            var lineItem = lineItems.FirstOrDefault(li => li.Item.Id == itemId);
            if (lineItem != null)
            {
                UnsubscribeFromLineItemEvents(lineItem);
                lineItems.Remove(lineItem);
                OnOrderUpdated();
            }
            else
            {
                throw new ArgumentException("Item not found in order.");
            }
        }

        private void LineItem_StateChanged(object sender, EventArgs e)
        {
            // When any line item changes (e.g., quantity increase), recalculate and notify.
            OnOrderUpdated();
        }

        /// Handles the event when a line item requests removal (e.g., quantity decreased to zero).
        private void LineItem_RequestedRemoval(object sender, Guid itemId)
        {
            var itemsToRemove = lineItems.Where(li => li.Quantity == 0).ToList();
            foreach (var lineItem in itemsToRemove)
            {
                UnsubscribeFromLineItemEvents(lineItem);
                lineItems.Remove(lineItem);
            }
            OnOrderUpdated();
        }
        private void SubscribeToLineItemEvents(OrderLineItem item)
        {
            item.OrderLineItemChanged += LineItem_StateChanged;
            item.OrderLineItemRemoved += LineItem_RequestedRemoval;
        }


        // Unsubscribes the Order from a line item's events before disposal/removal. CRITICAL to prevent memory leaks.
        private void UnsubscribeFromLineItemEvents(OrderLineItem item)
        {
            item.OrderLineItemChanged -= LineItem_StateChanged;
            item.OrderLineItemRemoved -= LineItem_RequestedRemoval;
        }

        // ITransaction Interface Implementations

        decimal ITransaction.ApplyDiscount(decimal discountPercentage)
        {
            if (IsFinalized)
            {
                throw new InvalidOperationException("Cannot apply discount to a finalized order.");
            }
            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentOutOfRangeException("Discount percentage must be between 0 and 100.");
            }

            this.DiscountPercentage = discountPercentage;
            decimal total = CalculateFinalTotal();
            OnOrderUpdated();
            return total;
        }

        decimal CalculateFinalTotal()
        {
            decimal subTotal = GetSubTotal();
            decimal discountAmount = subTotal * (DiscountPercentage / 100);
            return subTotal - discountAmount;
        }

        public decimal GetSubTotal()
        {
            return lineItems.Sum(li => li.CalculateTotalPrice());
        }

        decimal ITransaction.GetSubTotal()
        {
            return GetSubTotal();
        }

        decimal ITransaction.CalculateFinalTotal()
        {
            return CalculateFinalTotal();
        }

        public void FinalizeOrder()
        {
            if (IsFinalized)
            {
                throw new InvalidOperationException("Order is already finalized.");
            }
            FinalTotal = CalculateFinalTotal();
            IsFinalized = true;
            OnOrderUpdated();
        }

        private void OnOrderUpdated()
        {
            // calculate the total before invoking the event
            decimal newTotal = CalculateFinalTotal();
            OrderUpdated?.Invoke(this, newTotal);
        }

    }
}