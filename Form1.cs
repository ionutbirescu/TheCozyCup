using Microsoft.VisualBasic;
using System.Text;
using System.Threading.Tasks;
namespace TheCozyCup
{
    public partial class Form1 : Form
    {
        private Order _currentOrder;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.Columns.Clear();

            listView1.Columns.Add("Item Name", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Price", 70, HorizontalAlignment.Right);
            flowLayoutPanel1.Controls.Clear();

            MenuService menuService = MenuService.Instance;
            _currentOrder = new Order();
            _currentOrder.OrderUpdated += OnOrderTotalUpdated;
            foreach (var item in menuService.menuItems)

            {

                Button btn = new Button();
                btn.Text = item.Name;
                btn.Width = 120;
                btn.Height = 40;
                btn.Tag = item;

                btn.Click += (s, ev) =>
                {

                    MenuItem clickedItem = (MenuItem)((Button)s).Tag;
                    listView1.Items.Add(new ListViewItem(new string[] { clickedItem.Name, clickedItem.Price.ToString("C") }));
                    _currentOrder.AddMenuItem(clickedItem, 1);
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
            {
                return;
            }

            if (DateTime.TryParse(input, out DateTime reportDate))
            {
                try
                {
                    DateTime startDate = reportDate.Date;
                    DateTime endDate = reportDate.Date.AddDays(1);
                    var allLogs = await FileLoggerService.Instance.ReadAllTransactionsAsync();

                    var dailySales = allLogs
                        .Where(log => log.saleDate >= startDate && log.saleDate <= endDate)
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
                    MessageBox.Show(
                        report.ToString(),
                        $"Sales Summary for {reportDate:yyyy-MM-dd}",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    try
                    {
                        string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "saleslog.json");
                        if (File.Exists(logFilePath))
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                            {
                                FileName = logFilePath,
                                UseShellExecute = true
                            });
                        }
                        else
                        {
                            MessageBox.Show("The saleslog.json file was not found.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to open saleslog.json: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"File Error: Could not load sales data. Please ensure 'saleslog.json' exists and is not locked.\n\nDetails: {ex.Message}",
                        "Data Access Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred while generating the report: {ex.Message}",
                        "System Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid date format. Please use YYYY-MM-DD.",
                    "Input Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private async void BestSellers_Click(object sender, EventArgs e)
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
        }
    }
}