﻿namespace TuningService.Views.Impl
{
    partial class MainView
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
            this.buttonAddNewOrder = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonShowOrder = new System.Windows.Forms.Button();
            this.buttonAddMaster = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteMaster = new System.Windows.Forms.Button();
            this.button_export = new System.Windows.Forms.Button();
            this.button_import = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelStatus = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAddNewOrder
            // 
            this.buttonAddNewOrder.Location = new System.Drawing.Point(135, 22);
            this.buttonAddNewOrder.Name = "buttonAddNewOrder";
            this.buttonAddNewOrder.Size = new System.Drawing.Size(91, 31);
            this.buttonAddNewOrder.TabIndex = 1;
            this.buttonAddNewOrder.Text = "Add Order";
            this.buttonAddNewOrder.UseVisualStyleBackColor = true;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(347, 22);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(89, 30);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_ClickAsync);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(232, 22);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(91, 30);
            this.buttonUpdate.TabIndex = 3;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonShowOrder);
            this.groupBox1.Controls.Add(this.buttonRemove);
            this.groupBox1.Controls.Add(this.buttonAddNewOrder);
            this.groupBox1.Controls.Add(this.buttonUpdate);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 64);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Order menu";
            // 
            // buttonShowOrder
            // 
            this.buttonShowOrder.Location = new System.Drawing.Point(6, 22);
            this.buttonShowOrder.Name = "buttonShowOrder";
            this.buttonShowOrder.Size = new System.Drawing.Size(91, 31);
            this.buttonShowOrder.TabIndex = 6;
            this.buttonShowOrder.Text = "Show Order";
            this.buttonShowOrder.UseVisualStyleBackColor = true;
            this.buttonShowOrder.Click += new System.EventHandler(this.buttonShowOrder_Click);
            // 
            // buttonAddMaster
            // 
            this.buttonAddMaster.Location = new System.Drawing.Point(6, 22);
            this.buttonAddMaster.Name = "buttonAddMaster";
            this.buttonAddMaster.Size = new System.Drawing.Size(91, 31);
            this.buttonAddMaster.TabIndex = 7;
            this.buttonAddMaster.Text = "Add ";
            this.buttonAddMaster.UseVisualStyleBackColor = true;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(756, 101);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(84, 29);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(128, 148);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(712, 542);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(128, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 28);
            this.label1.TabIndex = 6;
            this.label1.Text = "Search:";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(201, 105);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(549, 20);
            this.textBoxSearch.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonDeleteMaster);
            this.groupBox2.Controls.Add(this.buttonAddMaster);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.groupBox2.Location = new System.Drawing.Point(646, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 64);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Master menu";
            // 
            // buttonDeleteMaster
            // 
            this.buttonDeleteMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.buttonDeleteMaster.Location = new System.Drawing.Point(103, 22);
            this.buttonDeleteMaster.Name = "buttonDeleteMaster";
            this.buttonDeleteMaster.Size = new System.Drawing.Size(91, 31);
            this.buttonDeleteMaster.TabIndex = 8;
            this.buttonDeleteMaster.Text = "Delete";
            this.buttonDeleteMaster.UseVisualStyleBackColor = true;
            // 
            // button_export
            // 
            this.button_export.Location = new System.Drawing.Point(12, 148);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(97, 41);
            this.button_export.TabIndex = 9;
            this.button_export.Text = "Export";
            this.button_export.UseVisualStyleBackColor = true;
            this.button_export.Click += new System.EventHandler(this.ExportToCSV);
            // 
            // button_import
            // 
            this.button_import.Location = new System.Drawing.Point(12, 206);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(97, 39);
            this.button_import.TabIndex = 10;
            this.button_import.Text = "Import";
            this.button_import.UseVisualStyleBackColor = true;
            this.button_import.Click += new System.EventHandler(this.ImportFromCSV);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(128, 696);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(709, 23);
            this.progressBar.TabIndex = 11;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStatus.Location = new System.Drawing.Point(416, 722);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(122, 20);
            this.labelStatus.TabIndex = 12;
            this.labelStatus.Text = "Processing...0%";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(884, 761);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.button_import);
            this.Controls.Add(this.button_export);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.MaximumSize = new System.Drawing.Size(900, 800);
            this.MinimumSize = new System.Drawing.Size(900, 800);
            this.Name = "MainView";
            this.Text = "TuningService";
            this.Load += new System.EventHandler(this.Form1_LoadAsync);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button button_import;

        private System.Windows.Forms.Button button_export;

        private System.Windows.Forms.TextBox textBoxSearch;

        private System.Windows.Forms.Label label1;

        #endregion
        private System.Windows.Forms.Button buttonAddNewOrder;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonShowOrder;
        private System.Windows.Forms.Button buttonAddMaster;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonDeleteMaster;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}