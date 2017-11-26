namespace Server
{
    partial class Server
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            this.lblServerLog = new System.Windows.Forms.Label();
            this.txtServerLog = new System.Windows.Forms.TextBox();
            this.btnPrivate = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnBradcast = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listName = new System.Windows.Forms.ListBox();
            this.listIp = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblServerLog
            // 
            this.lblServerLog.AutoSize = true;
            this.lblServerLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerLog.Location = new System.Drawing.Point(99, 9);
            this.lblServerLog.Name = "lblServerLog";
            this.lblServerLog.Size = new System.Drawing.Size(96, 20);
            this.lblServerLog.TabIndex = 0;
            this.lblServerLog.Text = "Server Log";
            // 
            // txtServerLog
            // 
            this.txtServerLog.Location = new System.Drawing.Point(227, 69);
            this.txtServerLog.Multiline = true;
            this.txtServerLog.Name = "txtServerLog";
            this.txtServerLog.ReadOnly = true;
            this.txtServerLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServerLog.Size = new System.Drawing.Size(190, 171);
            this.txtServerLog.TabIndex = 1;
            this.txtServerLog.TextChanged += new System.EventHandler(this.txtServerLog_TextChanged);
            // 
            // btnPrivate
            // 
            this.btnPrivate.Location = new System.Drawing.Point(12, 346);
            this.btnPrivate.Name = "btnPrivate";
            this.btnPrivate.Size = new System.Drawing.Size(131, 23);
            this.btnPrivate.TabIndex = 4;
            this.btnPrivate.Text = "Private Message";
            this.btnPrivate.UseVisualStyleBackColor = true;
            this.btnPrivate.Click += new System.EventHandler(this.btnPrivate_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(12, 422);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(131, 23);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnBradcast
            // 
            this.btnBradcast.Location = new System.Drawing.Point(12, 384);
            this.btnBradcast.Name = "btnBradcast";
            this.btnBradcast.Size = new System.Drawing.Size(131, 23);
            this.btnBradcast.TabIndex = 6;
            this.btnBradcast.Text = "Bradcast Message";
            this.btnBradcast.UseVisualStyleBackColor = true;
            this.btnBradcast.Click += new System.EventHandler(this.btnBradcast_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(158, 372);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(259, 73);
            this.txtMessage.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Message";
            // 
            // listName
            // 
            this.listName.FormattingEnabled = true;
            this.listName.Location = new System.Drawing.Point(12, 69);
            this.listName.Name = "listName";
            this.listName.Size = new System.Drawing.Size(101, 264);
            this.listName.TabIndex = 9;
            this.listName.SelectedIndexChanged += new System.EventHandler(this.listName_SelectedIndexChanged);
            // 
            // listIp
            // 
            this.listIp.FormattingEnabled = true;
            this.listIp.Location = new System.Drawing.Point(120, 69);
            this.listIp.Name = "listIp";
            this.listIp.Size = new System.Drawing.Size(101, 264);
            this.listIp.TabIndex = 10;
            this.listIp.SelectedIndexChanged += new System.EventHandler(this.listIp_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(242, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Server Chat Log";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(289, 271);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(92, 20);
            this.textBox1.TabIndex = 12;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(289, 297);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(92, 20);
            this.textBox2.TabIndex = 12;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 457);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listIp);
            this.Controls.Add(this.listName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnBradcast);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnPrivate);
            this.Controls.Add(this.txtServerLog);
            this.Controls.Add(this.lblServerLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Server";
            this.Text = "Server Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Server_FormClosing);
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServerLog;
        private System.Windows.Forms.TextBox txtServerLog;
        private System.Windows.Forms.Button btnPrivate;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnBradcast;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listName;
        private System.Windows.Forms.ListBox listIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}

