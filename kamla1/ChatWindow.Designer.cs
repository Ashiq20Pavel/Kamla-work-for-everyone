namespace kamla1
{
    partial class ChatWindow
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
            this.ChatLog = new System.Windows.Forms.TextBox();
            this.SendButton = new MetroFramework.Controls.MetroButton();
            this.ChatBox = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // ChatLog
            // 
            this.ChatLog.BackColor = System.Drawing.Color.Gainsboro;
            this.ChatLog.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ChatLog.Location = new System.Drawing.Point(12, 18);
            this.ChatLog.Multiline = true;
            this.ChatLog.Name = "ChatLog";
            this.ChatLog.ReadOnly = true;
            this.ChatLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatLog.Size = new System.Drawing.Size(270, 237);
            this.ChatLog.TabIndex = 10;
            // 
            // SendButton
            // 
            this.SendButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.SendButton.Location = new System.Drawing.Point(207, 270);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 9;
            this.SendButton.Text = "Send";
            this.SendButton.UseCustomBackColor = true;
            this.SendButton.UseCustomForeColor = true;
            this.SendButton.UseSelectable = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ChatBox
            // 
            this.ChatBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            // 
            // 
            // 
            this.ChatBox.CustomButton.Image = null;
            this.ChatBox.CustomButton.Location = new System.Drawing.Point(154, 1);
            this.ChatBox.CustomButton.Name = "";
            this.ChatBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.ChatBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.ChatBox.CustomButton.TabIndex = 1;
            this.ChatBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ChatBox.CustomButton.UseSelectable = true;
            this.ChatBox.CustomButton.Visible = false;
            this.ChatBox.Lines = new string[0];
            this.ChatBox.Location = new System.Drawing.Point(12, 270);
            this.ChatBox.MaxLength = 32767;
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.PasswordChar = '\0';
            this.ChatBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ChatBox.SelectedText = "";
            this.ChatBox.SelectionLength = 0;
            this.ChatBox.SelectionStart = 0;
            this.ChatBox.ShortcutsEnabled = true;
            this.ChatBox.Size = new System.Drawing.Size(176, 23);
            this.ChatBox.TabIndex = 8;
            this.ChatBox.UseSelectable = true;
            this.ChatBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ChatBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.ChatBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatBox_KeyDown);
            // 
            // ChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 311);
            this.Controls.Add(this.ChatLog);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ChatBox);
            this.Name = "ChatWindow";
            this.Text = "ChatWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatWindow_FormClosing);
            this.Load += new System.EventHandler(this.ChatWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChatLog;
        private MetroFramework.Controls.MetroButton SendButton;
        private MetroFramework.Controls.MetroTextBox ChatBox;
    }
}