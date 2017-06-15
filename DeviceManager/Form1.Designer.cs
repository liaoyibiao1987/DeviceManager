namespace DeviceManager
{
    partial class Form1
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
            this.btnEnumerateDevices = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetNomarlDevice = new System.Windows.Forms.Button();
            this.btnGetAllDevice = new System.Windows.Forms.Button();
            this.btnGetHiddenDevice = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEnumerateDevices
            // 
            this.btnEnumerateDevices.Location = new System.Drawing.Point(2, 76);
            this.btnEnumerateDevices.Name = "btnEnumerateDevices";
            this.btnEnumerateDevices.Size = new System.Drawing.Size(157, 43);
            this.btnEnumerateDevices.TabIndex = 0;
            this.btnEnumerateDevices.Text = "EnumerateDevices";
            this.btnEnumerateDevices.UseVisualStyleBackColor = true;
            this.btnEnumerateDevices.Click += new System.EventHandler(this.btnEnumerateDevices_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // btnGetNomarlDevice
            // 
            this.btnGetNomarlDevice.Location = new System.Drawing.Point(182, 76);
            this.btnGetNomarlDevice.Name = "btnGetNomarlDevice";
            this.btnGetNomarlDevice.Size = new System.Drawing.Size(157, 43);
            this.btnGetNomarlDevice.TabIndex = 2;
            this.btnGetNomarlDevice.Text = "GetNomarlDevice";
            this.btnGetNomarlDevice.UseVisualStyleBackColor = true;
            this.btnGetNomarlDevice.Click += new System.EventHandler(this.btnGetNomarlDevice_Click);
            // 
            // btnGetAllDevice
            // 
            this.btnGetAllDevice.Location = new System.Drawing.Point(366, 76);
            this.btnGetAllDevice.Name = "btnGetAllDevice";
            this.btnGetAllDevice.Size = new System.Drawing.Size(157, 43);
            this.btnGetAllDevice.TabIndex = 2;
            this.btnGetAllDevice.Text = "GetAllDevice";
            this.btnGetAllDevice.UseVisualStyleBackColor = true;
            this.btnGetAllDevice.Click += new System.EventHandler(this.btnGetAllDevice_Click);
            // 
            // btnGetHiddenDevice
            // 
            this.btnGetHiddenDevice.Location = new System.Drawing.Point(2, 139);
            this.btnGetHiddenDevice.Name = "btnGetHiddenDevice";
            this.btnGetHiddenDevice.Size = new System.Drawing.Size(157, 43);
            this.btnGetHiddenDevice.TabIndex = 2;
            this.btnGetHiddenDevice.Text = "GetHiddenDevice";
            this.btnGetHiddenDevice.UseVisualStyleBackColor = true;
            this.btnGetHiddenDevice.Click += new System.EventHandler(this.btnGetHiddenDevice_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 262);
            this.Controls.Add(this.btnGetHiddenDevice);
            this.Controls.Add(this.btnGetAllDevice);
            this.Controls.Add(this.btnGetNomarlDevice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEnumerateDevices);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnumerateDevices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetNomarlDevice;
        private System.Windows.Forms.Button btnGetAllDevice;
        private System.Windows.Forms.Button btnGetHiddenDevice;
    }
}