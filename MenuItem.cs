namespace TheCozyCup
{

    public class MenuItem
    {
        public event StateChangedEventHandler MenuItemChanged;

        public Guid Id { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Boolean IsAvailable { get; set; }

        public MenuItem(string name, decimal price, string category, string description)
        {
            if (string.IsNullOrWhiteSpace(name) || price <= 0)
            {
                throw new ArgumentException("Menu item must have a valid name and positive price.");
            }
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Category = category;
            Description = description;
            IsAvailable = true;
        }

        public override string ToString()
        {
            return $"{Name} - {Category} - ${Price:F2}\nDescription: {Description}\nAvailable: {IsAvailable}";
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }
            Price = newPrice;
            OnMenuItemChanged();
        }

        public void UpdateAvailability(bool availability)
        {
            IsAvailable = availability;
            OnMenuItemChanged();
        }

        //Helper method to trigger the MenuItemChanged event
        public void OnMenuItemChanged()
        {
            MenuItemChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}