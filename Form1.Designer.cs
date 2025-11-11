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
            MenuButtons.SuspendLayout();
            SuspendLayout();
            // 
            // FinalizeSale
            // 
            FinalizeSale.Location = new Point(57, 223);
            FinalizeSale.Name = "FinalizeSale";
            FinalizeSale.RightToLeft = RightToLeft.Yes;
            FinalizeSale.Size = new Size(196, 42);
            FinalizeSale.TabIndex = 0;
            FinalizeSale.Text = "FINALIZE SALE";
            FinalizeSale.UseVisualStyleBackColor = true;
            FinalizeSale.Click += FinalizeSale_Click;
            // 
            // NewOrder
            // 
            NewOrder.Location = new Point(59, 176);
            NewOrder.Name = "NewOrder";
            NewOrder.Size = new Size(194, 29);
            NewOrder.TabIndex = 1;
            NewOrder.Text = "NEW ORDER";
            NewOrder.UseVisualStyleBackColor = true;
            // 
            // Discount
            // 
            Discount.Location = new Point(57, 132);
            Discount.Name = "Discount";
            Discount.Size = new Size(196, 38);
            Discount.TabIndex = 2;
            Discount.Text = "DISCOUNT";
            Discount.UseVisualStyleBackColor = true;
            Discount.Click += Discount_Click;
            // 
            // Reports
            // 
            Reports.Location = new Point(62, 101);
            Reports.Name = "Reports";
            Reports.Size = new Size(188, 24);
            Reports.TabIndex = 3;
            Reports.Text = "REPORTS";
            Reports.UseVisualStyleBackColor = true;
            // 
            // Total
            // 
            Total.AutoSize = true;
            Total.Location = new Point(300, 67);
            Total.Name = "Total";
            Total.Size = new Size(42, 20);
            Total.TabIndex = 4;
            Total.Text = "Total";
            // 
            // OrderItems
            // 
            OrderItems.AutoSize = true;
            OrderItems.Location = new Point(98, 39);
            OrderItems.Name = "OrderItems";
            OrderItems.Size = new Size(87, 20);
            OrderItems.TabIndex = 5;
            OrderItems.Text = "Order Items";
            // 
            // MenuItems
            // 
            MenuItems.Location = new Point(58, 39);
            MenuItems.Name = "MenuItems";
            MenuItems.Size = new Size(184, 47);
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
            MenuButtons.Location = new Point(95, 157);
            MenuButtons.Name = "MenuButtons";
            MenuButtons.Size = new Size(308, 283);
            MenuButtons.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(784, 473);
            Controls.Add(MenuButtons);
            Controls.Add(OrderItems);
            Controls.Add(Total);
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
    }
}
