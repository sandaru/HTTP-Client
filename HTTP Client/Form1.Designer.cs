namespace HTTP_Client
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
            this.components = new System.ComponentModel.Container();
            this.textNatType = new System.Windows.Forms.TextBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.textPublicIP = new System.Windows.Forms.TextBox();
            this.richTextBoxRespose = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textMachineName = new System.Windows.Forms.TextBox();
            this.textDataString = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // textNatType
            // 
            this.textNatType.BackColor = System.Drawing.Color.White;
            this.textNatType.Location = new System.Drawing.Point(126, 58);
            this.textNatType.Name = "textNatType";
            this.textNatType.ReadOnly = true;
            this.textNatType.Size = new System.Drawing.Size(166, 20);
            this.textNatType.TabIndex = 0;
            // 
            // textPort
            // 
            this.textPort.BackColor = System.Drawing.Color.White;
            this.textPort.Location = new System.Drawing.Point(126, 132);
            this.textPort.Name = "textPort";
            this.textPort.ReadOnly = true;
            this.textPort.Size = new System.Drawing.Size(166, 20);
            this.textPort.TabIndex = 1;
            // 
            // textPublicIP
            // 
            this.textPublicIP.BackColor = System.Drawing.Color.White;
            this.textPublicIP.Location = new System.Drawing.Point(126, 94);
            this.textPublicIP.Name = "textPublicIP";
            this.textPublicIP.ReadOnly = true;
            this.textPublicIP.Size = new System.Drawing.Size(166, 20);
            this.textPublicIP.TabIndex = 2;
            // 
            // richTextBoxRespose
            // 
            this.richTextBoxRespose.Location = new System.Drawing.Point(13, 209);
            this.richTextBoxRespose.Name = "richTextBoxRespose";
            this.richTextBoxRespose.Size = new System.Drawing.Size(279, 84);
            this.richTextBoxRespose.TabIndex = 3;
            this.richTextBoxRespose.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(71, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Send Request";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "NAT Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Public IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "PORT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Machine Name";
            // 
            // textMachineName
            // 
            this.textMachineName.BackColor = System.Drawing.Color.White;
            this.textMachineName.Location = new System.Drawing.Point(126, 21);
            this.textMachineName.Name = "textMachineName";
            this.textMachineName.ReadOnly = true;
            this.textMachineName.Size = new System.Drawing.Size(166, 20);
            this.textMachineName.TabIndex = 8;
            // 
            // textDataString
            // 
            this.textDataString.BackColor = System.Drawing.Color.White;
            this.textDataString.Location = new System.Drawing.Point(15, 183);
            this.textDataString.Name = "textDataString";
            this.textDataString.ReadOnly = true;
            this.textDataString.Size = new System.Drawing.Size(277, 20);
            this.textDataString.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Data String";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 338);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textDataString);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textMachineName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBoxRespose);
            this.Controls.Add(this.textPublicIP);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.textNatType);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textNatType;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.TextBox textPublicIP;
        private System.Windows.Forms.RichTextBox richTextBoxRespose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textMachineName;
        private System.Windows.Forms.TextBox textDataString;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
    }
}

