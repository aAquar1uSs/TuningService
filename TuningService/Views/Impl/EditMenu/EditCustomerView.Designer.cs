namespace TuningService.Views.Impl.EditMenu
{
    partial class EditCustomerView
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonEditOwner = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxSurname = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxPhone);
            this.groupBox4.Controls.Add(this.textBoxLastName);
            this.groupBox4.Controls.Add(this.textBoxName);
            this.groupBox4.Controls.Add(this.textBoxSurname);
            this.groupBox4.Controls.Add(this.buttonEditOwner);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.groupBox4.Location = new System.Drawing.Point(17, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(248, 213);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Owner";
            // 
            // buttonEditOwner
            // 
            this.buttonEditOwner.Location = new System.Drawing.Point(58, 179);
            this.buttonEditOwner.Name = "buttonEditOwner";
            this.buttonEditOwner.Size = new System.Drawing.Size(108, 28);
            this.buttonEditOwner.TabIndex = 36;
            this.buttonEditOwner.Text = "Edit owner";
            this.buttonEditOwner.UseVisualStyleBackColor = true;
            this.buttonEditOwner.Click += new System.EventHandler(this.buttonEditOwner_Click);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(6, 132);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 19);
            this.label18.TabIndex = 30;
            this.label18.Text = "Phone:";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(6, 96);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 19);
            this.label17.TabIndex = 29;
            this.label17.Text = "Lastname:";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(6, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 19);
            this.label16.TabIndex = 28;
            this.label16.Text = "Surname:";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(6, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 19);
            this.label15.TabIndex = 27;
            this.label15.Text = "Name:";
            // 
            // textBoxSurname
            // 
            this.textBoxSurname.Location = new System.Drawing.Point(114, 28);
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.Size = new System.Drawing.Size(128, 23);
            this.textBoxSurname.TabIndex = 37;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(114, 63);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(128, 23);
            this.textBoxName.TabIndex = 38;
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(114, 96);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(128, 23);
            this.textBoxLastName.TabIndex = 39;
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Location = new System.Drawing.Point(114, 128);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(128, 23);
            this.textBoxPhone.TabIndex = 40;
            // 
            // EditCustomerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 226);
            this.Controls.Add(this.groupBox4);
            this.Name = "EditCustomerView";
            this.Text = "EditCustomerView";
            this.Load += new System.EventHandler(this.EditCustomerView_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxSurname;
        private System.Windows.Forms.Button buttonEditOwner;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
    }
}