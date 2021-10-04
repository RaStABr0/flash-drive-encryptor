using System.ComponentModel;

namespace lab2
{
    partial class DeviceForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceForm));
            this._notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this._encryptButton = new System.Windows.Forms.Button();
            this._ignoreCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // _notifyIcon
            // 
            this._notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_notifyIcon.Icon")));
            this._notifyIcon.Text = "lab2";
            this._notifyIcon.Visible = true;
            this._notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._notifyIcon_MouseDoubleClick);
            // 
            // _encryptButton
            // 
            this._encryptButton.Location = new System.Drawing.Point(28, 12);
            this._encryptButton.Name = "_encryptButton";
            this._encryptButton.Size = new System.Drawing.Size(138, 40);
            this._encryptButton.TabIndex = 0;
            this._encryptButton.Text = "Зашифровать";
            this._encryptButton.UseVisualStyleBackColor = true;
            this._encryptButton.Click += new System.EventHandler(this._encryptButton_Click);
            // 
            // _ignoreCheckBox
            // 
            this._ignoreCheckBox.Location = new System.Drawing.Point(28, 64);
            this._ignoreCheckBox.Name = "_ignoreCheckBox";
            this._ignoreCheckBox.Size = new System.Drawing.Size(138, 24);
            this._ignoreCheckBox.TabIndex = 1;
            this._ignoreCheckBox.Text = "Игнорировать";
            this._ignoreCheckBox.UseVisualStyleBackColor = true;
            this._ignoreCheckBox.CheckedChanged += new System.EventHandler(this._ignoreCheckBox_CheckedChanged);
            // 
            // DeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 100);
            this.Controls.Add(this._ignoreCheckBox);
            this.Controls.Add(this._encryptButton);
            this.Name = "DeviceForm";
            this.Text = "Устройство";
            this.Resize += new System.EventHandler(this.DeviceForm_Resize);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.CheckBox _ignoreCheckBox;

        private System.Windows.Forms.Button _encryptButton;
        
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        
        #endregion
    }
}