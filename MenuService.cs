using System;
using System.Collections.Generic;

// Avoid conflict with System.Windows.Forms.MenuItem
using MyMenuItem = TheCozyCup.MenuItem;

namespace TheCozyCup
{
    public class MenuService
    {
        private static readonly MenuService instance = new MenuService();
        public List<MyMenuItem> menuItems = new List<MyMenuItem>();

        private MenuService()
        {
            InitializeMenu();
        }

        public static MenuService Instance => instance;

        private void InitializeMenu()
        {
            menuItems.Add(new MyMenuItem("Espresso", 2.50m, "Beverage", "Strong and bold espresso shot."));
            menuItems.Add(new MyMenuItem("Cappuccino", 3.50m, "Beverage", "Espresso with steamed milk and foam."));
            menuItems.Add(new MyMenuItem("Latte", 4.00m, "Beverage", "Espresso with steamed milk."));
            menuItems.Add(new MyMenuItem("Blueberry Muffin", 2.00m, "Pastry", "Freshly baked muffin with blueberries."));
            menuItems.Add(new MyMenuItem("Bagel with Cream Cheese", 2.50m, "Pastry", "Toasted bagel served with cream cheese."));
        }

        // Returns all menu items (read-only)
        public IReadOnlyList<MyMenuItem> GetAllItems()
        {
            return menuItems.AsReadOnly();
        }
    }
}
