using System.ComponentModel;

namespace TuningService.Views.Impl;

partial class ImportMenuView
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        this.buttonAccept = new System.Windows.Forms.Button();
        this.dtgView = new System.Windows.Forms.DataGridView();
        this.buttonSelectFile = new System.Windows.Forms.Button();
        this.buttonClose = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dtgView)).BeginInit();
        this.SuspendLayout();
        // 
        // buttonAccept
        // 
        this.buttonAccept.Location = new System.Drawing.Point(301, 479);
        this.buttonAccept.Name = "buttonAccept";
        this.buttonAccept.Size = new System.Drawing.Size(118, 49);
        this.buttonAccept.TabIndex = 0;
        this.buttonAccept.Text = "Accept";
        this.buttonAccept.UseVisualStyleBackColor = true;
        this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
        // 
        // dtgView
        // 
        this.dtgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dtgView.Location = new System.Drawing.Point(12, 88);
        this.dtgView.Name = "dtgView";
        this.dtgView.RowHeadersWidth = 51;
        this.dtgView.RowTemplate.Height = 24;
        this.dtgView.Size = new System.Drawing.Size(999, 373);
        this.dtgView.TabIndex = 2;
        // 
        // buttonSelectFile
        // 
        this.buttonSelectFile.Location = new System.Drawing.Point(438, 12);
        this.buttonSelectFile.Name = "buttonSelectFile";
        this.buttonSelectFile.Size = new System.Drawing.Size(127, 55);
        this.buttonSelectFile.TabIndex = 4;
        this.buttonSelectFile.Text = "Select file";
        this.buttonSelectFile.UseVisualStyleBackColor = true;
        this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
        // 
        // buttonClose
        // 
        this.buttonClose.Location = new System.Drawing.Point(553, 479);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(118, 49);
        this.buttonClose.TabIndex = 3;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // ImportMenuView
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(1023, 540);
        this.Controls.Add(this.buttonSelectFile);
        this.Controls.Add(this.buttonClose);
        this.Controls.Add(this.dtgView);
        this.Controls.Add(this.buttonAccept);
        this.Location = new System.Drawing.Point(15, 15);
        this.Name = "ImportMenuView";
        this.Load += new System.EventHandler(this.ImportMenuView_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dtgView)).EndInit();
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Button buttonClose;

    #endregion

    private System.Windows.Forms.Button buttonAccept;
    private System.Windows.Forms.DataGridView dtgView;
    private System.Windows.Forms.Button buttonSelectFile;
}