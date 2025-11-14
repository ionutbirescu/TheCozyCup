using Microsoft.VisualBasic;
using System.Text;
namespace TheCozyCup
{
    public partial class Form1 : Form
    {
        private Order _currentOrder;
        public Form1()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile("C:\\Users\\I769044\\source\\repos\\TheCozyCup\\pixel_art.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            ResetOrderUI();
            listView1.Columns.Clear();

            listView1.Columns.Add("Item Name", 400, HorizontalAlignment.Left);
            listView1.Columns.Add("Price", 100, HorizontalAlignment.Right);
            listView1.Columns.Add("Quantity", 100, HorizontalAlignment.Right);
            flowLayoutPanel1.Controls.Clear();
            MenuService menuService = MenuService.Instance;
            var items = menuService.GetAllItems();

            if (items == null || items.Count == 0)
            {
                MessageBox.Show(
                    "No menu items found. Please ensure 'menu.json' exists and contains valid data.",
                    "Menu Not Loaded",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            _currentOrder = new Order();
            _currentOrder.OrderUpdated += OnOrderTotalUpdated;
            foreach (var item in menuService.menuItems)

            {

                Button btn = new Button();
                btn.Text = item.Name;
                btn.Width = 140;
                btn.Height = 80;
                btn.Tag = item;

                btn.Click += (s, ev) =>
                {
                    MenuItem clickedItem = (MenuItem)((Button)s).Tag;

                    // Add or update in the order
                    _currentOrder.AddMenuItem(clickedItem, 1);

                    // Check if the item is already in the ListView
                    var existingListViewItem = listView1.Items
                        .Cast<ListViewItem>()
                        .FirstOrDefault(lvi => lvi.Text == clickedItem.Name);

                    if (existingListViewItem != null)
                    {
                        // Update quantity and price
                        var lineItem = _currentOrder.lineItems.First(li => li.Item.Id == clickedItem.Id);
                        existingListViewItem.SubItems[1].Text = clickedItem.Price.ToString("C"); // Price per unit
                        existingListViewItem.SubItems[2].Text = lineItem.Quantity.ToString();    // Quantity
                    }
                    else
                    {
                        // Add new row
                        var lineItem = _currentOrder.lineItems.First(li => li.Item.Id == clickedItem.Id);
                        listView1.Items.Add(new ListViewItem(new string[]
                        {
                            clickedItem.Name,
                            clickedItem.Price.ToString("C"),
                            lineItem.Quantity.ToString()
                        }));
                    }

                    // Update total
                    textBox1.Text = _currentOrder.CalculateFinalTotal().ToString("C");
                };

                flowLayoutPanel1.Controls.Add(btn);

            }

        }

        private void OnOrderTotalUpdated(object sender, decimal newTotal)
        {
            textBox1.Text = newTotal.ToString("C");
        }

        private async void FinalizeSale_Click(object sender, EventArgs e)
        {
            // Safety check: ensure we actually have items in the order before finalizing
            if (!_currentOrder.lineItems.Any())
            {
                MessageBox.Show("Cannot finalize an empty order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _currentOrder.FinalizeOrder();
                var salesLogEntry = new SalesLogEntry(_currentOrder);
                await FileLoggerService.Instance.AppendTransactionAsync(salesLogEntry);
                MessageBox.Show(
                    $"Sale Finalized!\nTotal Amount Paid: {_currentOrder.FinalTotal.ToString("C")}",
                    "Transaction Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                ResetOrderUI();

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Discount_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the discount percentage (0-100):",
                "Apply Discount",
                _currentOrder.DiscountPercentage.ToString()
                );
            if (string.IsNullOrEmpty(input))
            {
                return;
            }
            if (decimal.TryParse(input, out decimal discountPercentage))
            {
                try
                {
                    ITransaction transactionOrder = _currentOrder;
                    transactionOrder.ApplyDiscount(discountPercentage);
                    MessageBox.Show($"Discount of {discountPercentage}% applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Discount Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Order Finalized", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number for the discount percentage (0-100).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Total_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MenuButtons_Paint(object sender, PaintEventArgs e)
        {

        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void ResetOrderUI()
        {
            if (_currentOrder != null)
            {
                _currentOrder.OrderUpdated -= OnOrderTotalUpdated;
            }

            listView1.Items.Clear();
            textBox1.Text = "$0.00";

            _currentOrder = new Order();
            _currentOrder.OrderUpdated += OnOrderTotalUpdated;
        }

        private async void Reports_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox(
                "Enter the date for the sales report (YYYY-MM-DD):",
                "Generate Sales Report",
                DateTime.Today.ToString("yyyy-MM-dd")
            );

            if (string.IsNullOrEmpty(input))
                return;

            if (!DateTime.TryParse(input, out DateTime reportDate))
            {
                MessageBox.Show(
                    "Invalid date format. Please use YYYY-MM-DD.",
                    "Input Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                var allLogs = await FileLoggerService.Instance.ReadAllTransactionsAsync();
                DateTime startDate = reportDate.Date;
                DateTime endDate = startDate.AddDays(1);

                var dailySales = allLogs
                    .Where(log => log.saleDate >= startDate && log.saleDate < endDate)
                    .ToList();

                if (!dailySales.Any())
                {
                    MessageBox.Show(
                        $"No sales found for {reportDate:yyyy-MM-dd}.",
                        "Report Empty",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                var report = SalesReportingService.Instance.GenerateSummary(dailySales);
                report.ReportDate = reportDate.Date;

                // Project transactions into a display-friendly format
                var displayData = dailySales.Select(log => new
                {
                    TransactionID = log.orderID,
                    SaleDate = log.saleDate,
                    Subtotal = log.Subtotal,
                    Discount = log.DiscountPercentage,
                    FinalTotal = log.FinalTotal,
                    ItemsSold = string.Join(", ", log.ItemsSold.Select(item => item.ToString()))
                }).ToList();

                // Create a new form for the report
                Form reportForm = new Form()
                {
                    Text = $"Sales Summary for {reportDate:yyyy-MM-dd}",
                    Width = 800,
                    Height = 600,
                    StartPosition = FormStartPosition.CenterParent
                };

                // Summary label
                Label summaryLabel = new Label()
                {
                    Text = $"Report Date: {report.ReportDate:yyyy-MM-dd}\n" +
                           $"Total Sales: {report.TotalRevenue:C}\n" +
                           $"Number of Transactions: {report.TotalTransactions}",
                    Dock = DockStyle.Top,
                    Height = 60,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };

                // DataGridView for detailed transactions
                DataGridView dgv = new DataGridView()
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AutoGenerateColumns = true,
                    DataSource = displayData
                };

                reportForm.Controls.Add(dgv);
                reportForm.Controls.Add(summaryLabel);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                reportForm.ShowDialog();
            }
            catch (IOException ex)
            {
                MessageBox.Show(
                    $"File Error: Could not load sales data. Please ensure 'saleslog.json' exists and is not locked.\n\nDetails: {ex.Message}",
                    "Data Access Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An unexpected error occurred while generating the report: {ex.Message}",
                    "System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        /*private async void BestSellers_Click(object sender, EventArgs e)
        {
            try
            {
                var allLogs = await FileLoggerService.Instance.ReadAllTransactionsAsync();

                if (allLogs == null || !allLogs.Any())
                {
                    MessageBox.Show("No sales data found.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var report = SalesReportingService.Instance.GenerateSummary(allLogs);

                if (report.BestSellers == null || !report.BestSellers.Any())
                {
                    MessageBox.Show("No best sellers found.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 3. Build message text for display
                var sb = new StringBuilder();
                sb.AppendLine($"BEST SELLERS REPORT - {DateTime.Now:yyyy-MM-dd}");
                sb.AppendLine("---------------------------------------");

                int rank = 1;
                foreach (var item in report.BestSellers)
                {
                    sb.AppendLine($"{rank}. {item.ItemName} - {item.TotalQuantitySold} sold");
                    rank++;
                }

                MessageBox.Show(
                    sb.ToString(),
                    "Top Selling Items",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (IOException ex)
            {
                MessageBox.Show($"File access error: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/

        private void NewOrder_Click(object sender, EventArgs e)
        {
            ResetOrderUI();
        }

        private void RemoveItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Assuming each row corresponds to a MenuItem's Name
                var selectedItem = listView1.SelectedItems[0];
                string itemName = selectedItem.Text;

                // Find the MenuItem in the current order
                var lineItem = _currentOrder.lineItems.FirstOrDefault(li => li.Item.Name == itemName);
                if (lineItem != null)
                {
                    _currentOrder.RemoveMenuItem(lineItem.Item.Id);

                    // Remove from the ListView
                    listView1.Items.Remove(selectedItem);

                    // Update total
                    textBox1.Text = _currentOrder.CalculateFinalTotal().ToString("C");
                }
                else
                {
                    MessageBox.Show("Item not found in the current order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DecreaseQuantity_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item to decrease.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = listView1.SelectedItems[0];
            string itemName = selectedItem.Text;

            // Find backend line item
            var lineItem = _currentOrder.lineItems
                .FirstOrDefault(li => li.Item.Name == itemName);

            if (lineItem == null)
                return;

            // Decrease quantity internally
            lineItem.DecreaseQuantity(1);

            // ✔ Remove from ListView if quantity is now 0
            if (lineItem.Quantity == 0)
            {
                listView1.Items.Remove(selectedItem);
            }
            else
            {
                // Otherwise update quantity
                selectedItem.SubItems[2].Text = lineItem.Quantity.ToString();
            }

            // Update total display
            textBox1.Text = _currentOrder.CalculateFinalTotal().ToString("C");
        }


    }
}