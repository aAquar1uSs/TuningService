namespace TuningService.Views.Impl.EditMenu
{
    partial class EditOrderView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.textBoxEndDate = new System.Windows.Forms.TextBox();
            this.buttonChangeOrder = new System.Windows.Forms.Button();
            this.checkBoxIsDone = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.orderDescription = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Price:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "End date:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.labelStartDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxPrice);
            this.groupBox1.Controls.Add(this.textBoxEndDate);
            this.groupBox1.Controls.Add(this.buttonChangeOrder);
            this.groupBox1.Controls.Add(this.checkBoxIsDone);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.orderDescription);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(649, 276);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information about order";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.label6.Location = new System.Drawing.Point(6, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 31;
            this.label6.Text = "format: yyyy-dd-mm";
            // 
            // labelStartDate
            // 
            this.labelStartDate.Location = new System.Drawing.Point(118, 42);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(100, 19);
            this.labelStartDate.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 28;
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Location = new System.Drawing.Point(121, 117);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(122, 23);
            this.textBoxPrice.TabIndex = 27;
            // 
            // textBoxEndDate
            // 
            this.textBoxEndDate.Location = new System.Drawing.Point(121, 81);
            this.textBoxEndDate.Name = "textBoxEndDate";
            this.textBoxEndDate.Size = new System.Drawing.Size(122, 23);
            this.textBoxEndDate.TabIndex = 26;
            // 
            // buttonChangeOrder
            // 
            this.buttonChangeOrder.Location = new System.Drawing.Point(90, 209);
            this.buttonChangeOrder.Name = "buttonChangeOrder";
            this.buttonChangeOrder.Size = new System.Drawing.Size(85, 33);
            this.buttonChangeOrder.TabIndex = 24;
            this.buttonChangeOrder.Text = "Edit ";
            this.buttonChangeOrder.UseVisualStyleBackColor = true;
            this.buttonChangeOrder.Click += new System.EventHandler(this.buttonChangeOrder_Click);
            // 
            // checkBoxIsDone
            // 
            this.checkBoxIsDone.Location = new System.Drawing.Point(121, 155);
            this.checkBoxIsDone.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxIsDone.Name = "checkBoxIsDone";
            this.checkBoxIsDone.Size = new System.Drawing.Size(15, 20);
            this.checkBoxIsDone.TabIndex = 20;
            this.checkBoxIsDone.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(0, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 19);
            this.label10.TabIndex = 19;
            this.label10.Text = " Done:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(250, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Description:";
            // 
            // orderDescription
            // 
            this.orderDescription.Location = new System.Drawing.Point(253, 42);
            this.orderDescription.Name = "orderDescription";
            this.orderDescription.Size = new System.Drawing.Size(374, 200);
            this.orderDescription.TabIndex = 6;
            this.orderDescription.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Start date:";
            // 
            // EditOrderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 301);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditOrderView";
            this.Text = "EditOrderView";
            this.Load += new System.EventHandler(this.EditOrderView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.TextBox textBoxEndDate;
        private System.Windows.Forms.Button buttonChangeOrder;
        private System.Windows.Forms.CheckBox checkBoxIsDone;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox orderDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.Label label6;
    }
}