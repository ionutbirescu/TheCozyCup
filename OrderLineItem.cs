namespace TheCozyCup
{
    public class OrderLineItem
    {
        public event StateChangedEventHandler OrderLineItemChanged;
        public event OrderLineItemRemovedEventHandler OrderLineItemRemoved;

        public MenuItem Item { get; set; }
        public int Quantity { get; set; }

        public decimal CalculateTotalPrice()
        {
            return Item.Price * Quantity;
        }

        public OrderLineItem(MenuItem item, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }
            this.Item = item;
            this.Quantity = quantity;
        }

        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Increase amount must be greater than zero.");
            }
            Quantity += amount;
            OnOrderLineItemChanged();
        }

        public void DecreaseQuantity(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Decrease amount must be greater than zero.");
            }
            if (Quantity - amount <= 0)
            {
                throw new ArgumentException("Quantity cannot be less than or equal to zero.");
            }
            Quantity -= amount;
            OnOrderLineItemChanged();
        }

        private void OnOrderLineItemChanged()
        {
            OrderLineItemChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnOrderLineItemRemoved()
        {
            OrderLineItemRemoved?.Invoke(this, Item.Id);
        }

        public override string ToString()
        {
            return $"{Item.Name} x {Quantity} - Total: ${CalculateTotalPrice():F2}";
        }


    }
}