using System;
using System.Windows.Forms;

namespace SpaceSaver
{
    partial class Form1
    {
        private FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog
        {
            RootFolder = Environment.SpecialFolder.MyComputer,
            ShowNewFolderButton = true
        };
        
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
            this.FolderTextBox = new System.Windows.Forms.TextBox();
            this.SelectFolderDialog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RunButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // FolderTextBox
            // 
            this.FolderTextBox.Location = new System.Drawing.Point(41, 14);
            this.FolderTextBox.Name = "FolderTextBox";
            this.FolderTextBox.Size = new System.Drawing.Size(214, 20);
            this.FolderTextBox.TabIndex = 0;
            // 
            // SelectFolderDialog
            // 
            this.SelectFolderDialog.Location = new System.Drawing.Point(261, 11);
            this.SelectFolderDialog.Name = "SelectFolderDialog";
            this.SelectFolderDialog.Size = new System.Drawing.Size(22, 23);
            this.SelectFolderDialog.TabIndex = 1;
            this.SelectFolderDialog.Text = "..";
            this.SelectFolderDialog.UseVisualStyleBackColor = true;
            this.SelectFolderDialog.Click += new System.EventHandler(this.SelectFolderDialog_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 34);
            this.label1.TabIndex = 2;
            this.label1.Text = "Root Path";
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(97, 40);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 3;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(183, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 76);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SelectFolderDialog);
            this.Controls.Add(this.FolderTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Space Saver";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ProgressBar progressBar1;

        private System.Windows.Forms.Button RunButton;

        private System.Windows.Forms.TextBox FolderTextBox;

        private System.Windows.Forms.Button SelectFolderDialog;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}