﻿
namespace HAPCTracker
{
    partial class ConfigForm
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
            this.UiUrl = new System.Windows.Forms.TextBox();
            this.UiToken = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uiCancel = new System.Windows.Forms.Button();
            this.uiSave = new System.Windows.Forms.Button();
            this.uiTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UiAfkTime = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UiAfkTime)).BeginInit();
            this.SuspendLayout();
            // 
            // UiUrl
            // 
            this.UiUrl.Location = new System.Drawing.Point(134, 17);
            this.UiUrl.Name = "UiUrl";
            this.UiUrl.Size = new System.Drawing.Size(311, 23);
            this.UiUrl.TabIndex = 0;
            // 
            // UiToken
            // 
            this.UiToken.Location = new System.Drawing.Point(134, 65);
            this.UiToken.Name = "UiToken";
            this.UiToken.Size = new System.Drawing.Size(311, 23);
            this.UiToken.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 52);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.uiCancel);
            this.panel2.Controls.Add(this.uiSave);
            this.panel2.Controls.Add(this.uiTest);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(145, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(322, 52);
            this.panel2.TabIndex = 3;
            // 
            // uiCancel
            // 
            this.uiCancel.Location = new System.Drawing.Point(214, 11);
            this.uiCancel.Name = "uiCancel";
            this.uiCancel.Size = new System.Drawing.Size(101, 27);
            this.uiCancel.TabIndex = 5;
            this.uiCancel.Text = "Cancel";
            this.uiCancel.UseVisualStyleBackColor = true;
            this.uiCancel.Click += new System.EventHandler(this.UiCancel_Click);
            // 
            // uiSave
            // 
            this.uiSave.Location = new System.Drawing.Point(107, 11);
            this.uiSave.Name = "uiSave";
            this.uiSave.Size = new System.Drawing.Size(101, 27);
            this.uiSave.TabIndex = 4;
            this.uiSave.Text = "Save";
            this.uiSave.UseVisualStyleBackColor = true;
            this.uiSave.Click += new System.EventHandler(this.UiSave_Click);
            // 
            // uiTest
            // 
            this.uiTest.Enabled = false;
            this.uiTest.Location = new System.Drawing.Point(0, 11);
            this.uiTest.Name = "uiTest";
            this.uiTest.Size = new System.Drawing.Size(101, 27);
            this.uiTest.TabIndex = 3;
            this.uiTest.Text = "Test";
            this.uiTest.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "HomeAssistant URL:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Access Token:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UiAfkTime
            // 
            this.UiAfkTime.Location = new System.Drawing.Point(134, 107);
            this.UiAfkTime.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.UiAfkTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UiAfkTime.Name = "UiAfkTime";
            this.UiAfkTime.Size = new System.Drawing.Size(60, 23);
            this.UiAfkTime.TabIndex = 5;
            this.UiAfkTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Away After:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(197, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "minutes";
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.uiSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancel;
            this.ClientSize = new System.Drawing.Size(467, 222);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UiAfkTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.UiToken);
            this.Controls.Add(this.UiUrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ConfigForm";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UiAfkTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UiUrl;
        private System.Windows.Forms.TextBox UiToken;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button uiCancel;
        private System.Windows.Forms.Button uiSave;
        private System.Windows.Forms.Button uiTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown UiAfkTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
