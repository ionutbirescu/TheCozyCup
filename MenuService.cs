
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TheCozyCup;

namespace TheCozyCup
{
    
    public class MenuService
    {
        private const string MenuFilePath = "menu.json";
        private static readonly MenuService ins = new MenuService();
        public event StateChangedEventHandler MenuChanged;
        public List<MenuItem> menuItems = new List<MenuItem>();
        private MenuService()
        {
            if (!LoadMenuFile())
            {
                InitializeMenu();
            }
        }
        public static MenuService Instance
        {
            get { return ins; }
        }

        private void InitializeMenu()
        {
            menuItems.Add(new MenuItem("Espresso", 2.50m, "Beverage", "Strong and bold espresso shot."));
            menuItems.Add(new MenuItem("Cappuccino", 3.50m, "Beverage", "Espresso with steamed milk and foam."));
            menuItems.Add(new MenuItem("Latte", 4.00m, "Beverage", "Espresso with steamed milk."));
            menuItems.Add(new MenuItem("Blueberry Muffin", 2.00m, "Pastry", "Freshly baked muffin with blueberries."));
            menuItems.Add(new MenuItem("Bagel with Cream Cheese", 2.50m, "Pastry", "Toasted bagel served with cream cheese."));
        }

        /// Returns a read-only view of the menu.
        /// IReadOnlyList prevents external code (like the UI) from accidentally modifying the master list.
        public IReadOnlyList<MenuItem> GetAllItems()
        {
            return menuItems.AsReadOnly();
        }

        public MenuItem GetItemById(Guid id)
        {
            return menuItems.Find(item => item.Id == id);
        }

        public IReadOnlyCollection<MenuItem> GetItemsByCategory(string category)
        {
            var itemsInCategory = menuItems.FindAll(item => item.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            return itemsInCategory.AsReadOnly();
        }

        public void AddMenuItem(MenuItem newItem)
        {
            if (newItem == null)
            {
                throw new ArgumentNullException(nameof(newItem), "Menu item cannot be null.");
            }
            menuItems.Add(newItem);
            SaveMenuFile();
            OnMenuChanged();
        }
        private void Item_StateChanged(object sender, EventArgs e)
        {
            OnMenuChanged();
        }

        private void OnMenuChanged()
        {
            MenuChanged?.Invoke(this, EventArgs.Empty);
        }
        public void SaveMenuFile()
        {
            try
            {
                // Serialize the list of menu items to a JSON string
                var jsonString = JsonSerializer.Serialize(menuItems, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(MenuFilePath, jsonString);
            }
            catch (Exception ex)
            {
                // Log the error (e.g., to a console or log file) without crashing the app
                Console.WriteLine($"Error saving menu data: {ex.Message}");
            }
        }

        public bool LoadMenuFile()
        {
            try
            {
                if (!File.Exists(MenuFilePath))
                {
                    Console.WriteLine("Menu file not found. Initializing with default menu.");
                    return false;
                }
                var jsonString = File.ReadAllText(MenuFilePath);
                var loadedMenuItems = JsonSerializer.Deserialize<List<MenuItem>>(jsonString);
                if (loadedMenuItems != null)
                {
                    // Unsubscribe all current items to prevent duplicate event firing
                    foreach (var item in menuItems)
                    {
                        item.MenuItemChanged -= Item_StateChanged;
                    }

                    menuItems = new List<MenuItem>();

                    // Re-add and re-subscribe all loaded items
                    foreach (var item in loadedMenuItems)
                    {
                        AddMenuItem(item);
                    }

                    OnMenuChanged(); // Notify subscribers the menu has been loaded/reloaded
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading menu data: {ex.Message}");
                return false;
            }
        }
    }
}