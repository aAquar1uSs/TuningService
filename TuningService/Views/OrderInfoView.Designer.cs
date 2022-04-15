namespace TuningService.Views
{
    partial class OrderInfoView
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
            this.dataGridOrderView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOrderView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridOrderView
            // 
            this.dataGridOrderView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridOrderView.Location = new System.Drawing.Point(129, 12);
            this.dataGridOrderView.Name = "dataGridOrderView";
            this.dataGridOrderView.Size = new System.Drawing.Size(659, 426);
            this.dataGridOrderView.TabIndex = 0;
            // 
            // OrderInfoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridOrderView);
            this.Name = "OrderInfoView";
            this.Text = "OrderInfoView";
            this.Load += new System.EventHandler(this.OrderInfoView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOrderView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridOrderView;
    }
}