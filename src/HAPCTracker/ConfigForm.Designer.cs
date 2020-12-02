
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
            this.UiMqttServer = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.UiCancel = new System.Windows.Forms.Button();
            this.UiSave = new System.Windows.Forms.Button();
            this.UiTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.UiAfkTime = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.UiUpdateSeconds = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UiAfkTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UiUpdateSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // UiMqttServer
            // 
            this.UiMqttServer.Location = new System.Drawing.Point(134, 17);
            this.UiMqttServer.Name = "UiMqttServer";
            this.UiMqttServer.Size = new System.Drawing.Size(311, 23);
            this.UiMqttServer.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 52);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.UiCancel);
            this.panel2.Controls.Add(this.UiSave);
            this.panel2.Controls.Add(this.UiTest);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(145, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(322, 52);
            this.panel2.TabIndex = 3;
            // 
            // UiCancel
            // 
            this.UiCancel.Location = new System.Drawing.Point(214, 11);
            this.UiCancel.Name = "UiCancel";
            this.UiCancel.Size = new System.Drawing.Size(101, 27);
            this.UiCancel.TabIndex = 5;
            this.UiCancel.Text = "Cancel";
            this.UiCancel.UseVisualStyleBackColor = true;
            this.UiCancel.Click += new System.EventHandler(this.UiCancel_Click);
            // 
            // UiSave
            // 
            this.UiSave.Location = new System.Drawing.Point(107, 11);
            this.UiSave.Name = "UiSave";
            this.UiSave.Size = new System.Drawing.Size(101, 27);
            this.UiSave.TabIndex = 4;
            this.UiSave.Text = "Save";
            this.UiSave.UseVisualStyleBackColor = true;
            this.UiSave.Click += new System.EventHandler(this.UiSave_Click);
            // 
            // UiTest
            // 
            this.UiTest.Enabled = false;
            this.UiTest.Location = new System.Drawing.Point(0, 11);
            this.UiTest.Name = "UiTest";
            this.UiTest.Size = new System.Drawing.Size(101, 27);
            this.UiTest.TabIndex = 3;
            this.UiTest.Text = "Test";
            this.UiTest.UseVisualStyleBackColor = true;
            this.UiTest.Click += new System.EventHandler(this.UiTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "MQTT Server:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UiAfkTime
            // 
            this.UiAfkTime.Location = new System.Drawing.Point(134, 51);
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
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Away After:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(197, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "minutes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(197, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "seconds";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Update Frequency:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UiUpdateSeconds
            // 
            this.UiUpdateSeconds.Location = new System.Drawing.Point(134, 84);
            this.UiUpdateSeconds.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
            this.UiUpdateSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UiUpdateSeconds.Name = "UiUpdateSeconds";
            this.UiUpdateSeconds.Size = new System.Drawing.Size(60, 23);
            this.UiUpdateSeconds.TabIndex = 8;
            this.UiUpdateSeconds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.UiSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.UiCancel;
            this.ClientSize = new System.Drawing.Size(467, 171);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.UiUpdateSeconds);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UiAfkTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.UiMqttServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ConfigForm";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UiAfkTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UiUpdateSeconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UiMqttServer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button UiCancel;
        private System.Windows.Forms.Button UiSave;
        private System.Windows.Forms.Button UiTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown UiAfkTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown UiUpdateSeconds;
    }
}

