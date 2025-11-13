using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
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
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "menu.json");

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var items = JsonSerializer.Deserialize<List<MyMenuItem>>(json);

                    if (items != null)
                        menuItems = items;
                }
                else
                {
                    Console.WriteLine($"menu.json not found at {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading menu data: {ex.Message}");
            }
        }

        // Returns all menu items (read-only)
        public IReadOnlyList<MyMenuItem> GetAllItems()
        {
            return menuItems.AsReadOnly();
        }
    }
}
