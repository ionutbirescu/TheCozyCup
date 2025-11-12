namespace TheCozyCup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            MenuService menuService = MenuService.Instance;

            foreach (var item in menuService.menuItems)

            {

                Button btn = new Button();
                btn.Text = item.Name;          // textul afisat
                btn.Width = 120;               // latimea butonului (optional)
                btn.Height = 40;               // inaltimea butonului (optional)
                btn.Tag = item;                // salvăm obiectul complet în Tag pentru mai târzi

                btn.Click += (s, ev) =>

                {

                    MenuItem clickedItem = (MenuItem)((Button)s).Tag;
                    MessageBox.Show($"Ai selectat: {clickedItem.Name}");

                };


                flowLayoutPanel1.Controls.Add(btn);

            }

        }

        private void FinalizeSale_Click(object sender, EventArgs e)
        {

        }

        private void Discount_Click(object sender, EventArgs e)
        {

        }

        private void Total_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
