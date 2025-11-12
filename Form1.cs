using Microsoft.VisualBasic;
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

        private void FinalizeSale_Click(object sender, EventArgs e)
        {
            // Safety check: ensure we actually have items in the order before finalizing
            if (_currentOrder == null || _currentOrder.lineItems.Count == 0)
            {
                MessageBox.Show("Cannot finalize an empty order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Call the business logic method
                _currentOrder.FinalizeOrder();

                // 2. Display the confirmation and final total
                MessageBox.Show(
                    $"Sale Finalized!\nTotal Amount Paid: {_currentOrder.FinalTotal.ToString("C")}",
                    "Transaction Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // 3. Reset the UI and start a new order
                ResetOrderUI();

            }
            catch (InvalidOperationException ex)
            {
                // This catches the exception from your Order class if it's already finalized
                MessageBox.Show($"Error: {ex.Message}", "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Discount_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the discount percentage (0-100):", // Prompt
                "Apply Discount",                         // Title
                _currentOrder.DiscountPercentage.ToString() // Default value (uses current discount)
                );

            // Check if the user clicked Cancel or didn't enter anything
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            // 1. Validate and Parse the Input
            if (decimal.TryParse(input, out decimal discountPercentage))
            {
                try
                {
                    // 2. Call the ApplyDiscount method on the Order object
                    // The method implements ITransaction.ApplyDiscount
                    // Note: Use ITransaction interface call to match the definition, if needed, 
                    // otherwise, simply call the public/private ApplyDiscount method.

                    // Calling the explicit interface implementation using a cast:
                    ITransaction transactionOrder = _currentOrder;
                    transactionOrder.ApplyDiscount(discountPercentage);

                    // 3. Update the UI to show the new breakdown
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
            // A. Unsubscribe the old order's event handler (prevents memory leaks/duplicate calls)
            if (_currentOrder != null)
            {
                _currentOrder.OrderUpdated -= OnOrderTotalUpdated;
            }

            // B. Clear the visual elements
            listView1.Items.Clear();        // Clear the items list
            textBox1.Text = "$0.00";        // Reset the total display (assuming textBox1 is the total)
                                            // Assuming you have a txtDiscount for input:
                                            // txtDiscount.Text = "";        // Clear the discount input area 

            // C. Create a brand new order object
            _currentOrder = new Order();

            // D. Subscribe the new order's event handler
            _currentOrder.OrderUpdated += OnOrderTotalUpdated;
        }
    }

}
