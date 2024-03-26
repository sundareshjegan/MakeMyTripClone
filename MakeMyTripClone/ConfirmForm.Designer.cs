namespace MakeMyTripClone
{
    partial class ConfirmForm
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
            this.confirmationCodeTB = new System.Windows.Forms.MaskedTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerLabel = new System.Windows.Forms.Label();
            this.closePB = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.confirmBtn = new MakeMyTripClone.RippleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closePB)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // confirmationCodeTB
            // 
            this.confirmationCodeTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.confirmationCodeTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.confirmationCodeTB.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmationCodeTB.Location = new System.Drawing.Point(133, 9);
            this.confirmationCodeTB.Mask = "0 0 0 0 0 0";
            this.confirmationCodeTB.Name = "confirmationCodeTB";
            this.confirmationCodeTB.PromptChar = '-';
            this.confirmationCodeTB.Size = new System.Drawing.Size(142, 39);
            this.confirmationCodeTB.TabIndex = 0;
            this.confirmationCodeTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.confirmationCodeTB.TextChanged += new System.EventHandler(this.OnConfirmationCodeTBTextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MakeMyTripClone.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(130, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(195, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailLabel.Location = new System.Drawing.Point(25, 198);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(179, 25);
            this.emailLabel.TabIndex = 4;
            this.emailLabel.Text = "sun*****@mail.com";
            this.emailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(387, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "We had just sent you a confirmation code over to\r\n";
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerLabel.ForeColor = System.Drawing.Color.Blue;
            this.timerLabel.Location = new System.Drawing.Point(147, 373);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(164, 21);
            this.timerLabel.TabIndex = 4;
            this.timerLabel.Text = "Code Expires in:  3 : 00";
            this.timerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // closePB
            // 
            this.closePB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closePB.Image = global::MakeMyTripClone.Properties.Resources.close;
            this.closePB.Location = new System.Drawing.Point(427, 12);
            this.closePB.Name = "closePB";
            this.closePB.Size = new System.Drawing.Size(20, 20);
            this.closePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.closePB.TabIndex = 6;
            this.closePB.TabStop = false;
            this.closePB.Click += new System.EventHandler(this.OnClosePBClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Complete Registration";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.confirmationCodeTB);
            this.panel1.Location = new System.Drawing.Point(27, 254);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 60);
            this.panel1.TabIndex = 7;
            // 
            // confirmBtn
            // 
            this.confirmBtn.BackColor = System.Drawing.Color.DodgerBlue;
            this.confirmBtn.Enabled = false;
            this.confirmBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirmBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmBtn.ForeColor = System.Drawing.Color.White;
            this.confirmBtn.Location = new System.Drawing.Point(27, 329);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(405, 41);
            this.confirmBtn.TabIndex = 8;
            this.confirmBtn.Text = "CONFIRM";
            this.confirmBtn.UseVisualStyleBackColor = false;
            this.confirmBtn.Click += new System.EventHandler(this.OnConfirmBtnClicked);
            // 
            // ConfirmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(459, 414);
            this.ControlBox = false;
            this.Controls.Add(this.confirmBtn);
            this.Controls.Add(this.closePB);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfirmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closePB)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox confirmationCodeTB;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.PictureBox closePB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private RippleButton confirmBtn;
    }
}