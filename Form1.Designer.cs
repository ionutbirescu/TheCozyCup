

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
            Discount = new Button();
            Reports = new Button();
            MenuButtons = new Panel();
            NewOrder = new Button();
            Total = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            listView1 = new ListView();
            textBox1 = new TextBox();
            RemoveItem = new Button();
            MenuButtons.SuspendLayout();
            SuspendLayout();
            // 
            // FinalizeSale
            // 
            FinalizeSale.Location = new Point(74, 282);
            FinalizeSale.Margin = new Padding(4);
            FinalizeSale.Name = "FinalizeSale";
            FinalizeSale.RightToLeft = RightToLeft.Yes;
            FinalizeSale.Size = new Size(245, 52);
            FinalizeSale.TabIndex = 0;
            FinalizeSale.Text = "FINALIZE SALE";
            FinalizeSale.UseVisualStyleBackColor = true;
            FinalizeSale.Click += FinalizeSale_Click;
            // 
            // Discount
            // 
            Discount.Location = new Point(74, 112);
            Discount.Margin = new Padding(4);
            Discount.Name = "Discount";
            Discount.Size = new Size(245, 58);
            Discount.TabIndex = 2;
            Discount.Text = "DISCOUNT";
            Discount.UseVisualStyleBackColor = true;
            Discount.Click += Discount_Click;
            // 
            // Reports
            // 
            Reports.Location = new Point(300, 488);
            Reports.Margin = new Padding(4);
            Reports.Name = "Reports";
            Reports.Size = new Size(281, 52);
            Reports.TabIndex = 3;
            Reports.Text = "REPORTS";
            Reports.UseVisualStyleBackColor = true;
            Reports.Click += Reports_Click;
            // 
            // MenuButtons
            // 
            MenuButtons.Controls.Add(RemoveItem);
            MenuButtons.Controls.Add(NewOrder);
            MenuButtons.Controls.Add(Discount);
            MenuButtons.Controls.Add(FinalizeSale);
            MenuButtons.Location = new Point(1159, 268);
            MenuButtons.Margin = new Padding(4);
            MenuButtons.Name = "MenuButtons";
            MenuButtons.Size = new Size(397, 374);
            MenuButtons.TabIndex = 7;
            MenuButtons.Paint += MenuButtons_Paint;
            // 
            // NewOrder
            // 
            NewOrder.Location = new Point(72, 201);
            NewOrder.Name = "NewOrder";
            NewOrder.Size = new Size(247, 55);
            NewOrder.TabIndex = 4;
            NewOrder.Text = "NEW ORDER";
            NewOrder.UseVisualStyleBackColor = true;
            NewOrder.Click += NewOrder_Click;
            // 
            // Total
            // 
            Total.AutoSize = true;
            Total.Font = new Font("Segoe UI", 20F);
            Total.Location = new Point(695, 645);
            Total.Margin = new Padding(4, 0, 4, 0);
            Total.Name = "Total";
            Total.Size = new Size(107, 54);
            Total.TabIndex = 4;
            Total.Text = "Total";
            Total.Click += Total_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(300, 296);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(281, 156);
            flowLayoutPanel1.TabIndex = 8;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // listView1
            // 
            listView1.Location = new Point(695, 255);
            listView1.Name = "listView1";
            listView1.Size = new Size(361, 387);
            listView1.TabIndex = 9;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 18F);
            textBox1.Location = new Point(803, 645);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(253, 55);
            textBox1.TabIndex = 12;
            // 
            // RemoveItem
            // 
            RemoveItem.Location = new Point(72, 28);
            RemoveItem.Name = "RemoveItem";
            RemoveItem.Size = new Size(247, 53);
            RemoveItem.TabIndex = 13;
            RemoveItem.Text = "REMOVE";
            RemoveItem.UseVisualStyleBackColor = true;
            RemoveItem.Click += RemoveItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1924, 1170);
            Controls.Add(textBox1);
            Controls.Add(Reports);
            Controls.Add(listView1);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(MenuButtons);
            Controls.Add(Total);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "The Cozy Cup";
            Load += Form1_Load;
            MenuButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private Button FinalizeSale;
        private Button Discount;
        private Button Reports;
        private Panel MenuButtons;
        private Label Total;
        private FlowLayoutPanel flowLayoutPanel1;
        private ListView listView1;
        private TextBox textBox1;
        private Button NewOrder;
        private Button RemoveItem;
    }
}
