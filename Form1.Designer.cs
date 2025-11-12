namespace TheCozyCup
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            FinalizeSale = new Button();
            NewOrder = new Button();
            Discount = new Button();
            Reports = new Button();
            Total = new Label();
            OrderItems = new Label();
            MenuItems = new Button();
            MenuButtons = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            MenuButtons.SuspendLayout();
            SuspendLayout();
            // 
            // FinalizeSale
            // 
            FinalizeSale.Location = new Point(71, 279);
            FinalizeSale.Margin = new Padding(4, 4, 4, 4);
            FinalizeSale.Name = "FinalizeSale";
            FinalizeSale.RightToLeft = RightToLeft.Yes;
            FinalizeSale.Size = new Size(245, 52);
            FinalizeSale.TabIndex = 0;
            FinalizeSale.Text = "FINALIZE SALE";
            FinalizeSale.UseVisualStyleBackColor = true;
            FinalizeSale.Click += FinalizeSale_Click;
            // 
            // NewOrder
            // 
            NewOrder.Location = new Point(74, 220);
            NewOrder.Margin = new Padding(4, 4, 4, 4);
            NewOrder.Name = "NewOrder";
            NewOrder.Size = new Size(242, 36);
            NewOrder.TabIndex = 1;
            NewOrder.Text = "NEW ORDER";
            NewOrder.UseVisualStyleBackColor = true;
            // 
            // Discount
            // 
            Discount.Location = new Point(71, 165);
            Discount.Margin = new Padding(4, 4, 4, 4);
            Discount.Name = "Discount";
            Discount.Size = new Size(245, 48);
            Discount.TabIndex = 2;
            Discount.Text = "DISCOUNT";
            Discount.UseVisualStyleBackColor = true;
            Discount.Click += Discount_Click;
            // 
            // Reports
            // 
            Reports.Location = new Point(78, 126);
            Reports.Margin = new Padding(4, 4, 4, 4);
            Reports.Name = "Reports";
            Reports.Size = new Size(235, 30);
            Reports.TabIndex = 3;
            Reports.Text = "REPORTS";
            Reports.UseVisualStyleBackColor = true;
            // 
            // Total
            // 
            Total.AutoSize = true;
            Total.Location = new Point(375, 84);
            Total.Margin = new Padding(4, 0, 4, 0);
            Total.Name = "Total";
            Total.Size = new Size(49, 25);
            Total.TabIndex = 4;
            Total.Text = "Total";
            Total.Click += Total_Click;
            // 
            // OrderItems
            // 
            OrderItems.AutoSize = true;
            OrderItems.Location = new Point(122, 49);
            OrderItems.Margin = new Padding(4, 0, 4, 0);
            OrderItems.Name = "OrderItems";
            OrderItems.Size = new Size(107, 25);
            OrderItems.TabIndex = 5;
            OrderItems.Text = "Order Items";
            // 
            // MenuItems
            // 
            MenuItems.Location = new Point(72, 49);
            MenuItems.Margin = new Padding(4, 4, 4, 4);
            MenuItems.Name = "MenuItems";
            MenuItems.Size = new Size(230, 59);
            MenuItems.TabIndex = 6;
            MenuItems.Text = "Menu Items";
            MenuItems.UseVisualStyleBackColor = true;
            // 
            // MenuButtons
            // 
            MenuButtons.Controls.Add(MenuItems);
            MenuButtons.Controls.Add(Reports);
            MenuButtons.Controls.Add(Discount);
            MenuButtons.Controls.Add(NewOrder);
            MenuButtons.Controls.Add(FinalizeSale);
            MenuButtons.Location = new Point(80, 155);
            MenuButtons.Margin = new Padding(4, 4, 4, 4);
            MenuButtons.Name = "MenuButtons";
            MenuButtons.Size = new Size(385, 354);
            MenuButtons.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(577, 155);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(300, 308);
            flowLayoutPanel1.TabIndex = 8;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(980, 591);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(MenuButtons);
            Controls.Add(OrderItems);
            Controls.Add(Total);
            Margin = new Padding(4, 4, 4, 4);
            Name = "Form1";
            Text = "The Cozy Cup";
            Load += Form1_Load;
            MenuButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button FinalizeSale;
        private Button NewOrder;
        private Button Discount;
        private Button Reports;
        private Label Total;
        private Label OrderItems;
        private Button MenuItems;
        private Panel MenuButtons;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
