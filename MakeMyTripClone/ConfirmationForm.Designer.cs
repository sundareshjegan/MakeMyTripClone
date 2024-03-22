namespace MakeMyTripClone
{
    partial class ConfirmationForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.confirmationCodeTB = new System.Windows.Forms.MaskedTextBox();
            this.confirmBtn = new System.Windows.Forms.Button();
            this.timerLabel = new System.Windows.Forms.Label();
            this.closePB = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.closePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "Enter the 6-digit code we sent to";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(74, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = " Enter Confirmation code";
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailLabel.Location = new System.Drawing.Point(108, 250);
            this.emailLabel.Margin = new System.Windows.Forms.Padding(30, 15, 3, 0);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(251, 25);
            this.emailLabel.TabIndex = 0;
            this.emailLabel.Text = "sundareshjegan@gmail.com";
            // 
            // confirmationCodeTB
            // 
            this.confirmationCodeTB.AsciiOnly = true;
            this.confirmationCodeTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.confirmationCodeTB.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmationCodeTB.Location = new System.Drawing.Point(153, 322);
            this.confirmationCodeTB.Margin = new System.Windows.Forms.Padding(60, 15, 3, 3);
            this.confirmationCodeTB.Mask = "0 0 0 0 0 0";
            this.confirmationCodeTB.Name = "confirmationCodeTB";
            this.confirmationCodeTB.PromptChar = '-';
            this.confirmationCodeTB.Size = new System.Drawing.Size(195, 47);
            this.confirmationCodeTB.TabIndex = 2;
            // 
            // confirmBtn
            // 
            this.confirmBtn.BackColor = System.Drawing.Color.DodgerBlue;
            this.confirmBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmBtn.ForeColor = System.Drawing.Color.White;
            this.confirmBtn.Location = new System.Drawing.Point(160, 393);
            this.confirmBtn.Margin = new System.Windows.Forms.Padding(60, 20, 3, 3);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(145, 44);
            this.confirmBtn.TabIndex = 3;
            this.confirmBtn.Text = "Confirm";
            this.confirmBtn.UseVisualStyleBackColor = false;
            this.confirmBtn.Click += new System.EventHandler(this.OnConfirmBtnClicked);
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerLabel.Location = new System.Drawing.Point(156, 287);
            this.timerLabel.Margin = new System.Windows.Forms.Padding(30, 15, 3, 0);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(149, 20);
            this.timerLabel.TabIndex = 0;
            this.timerLabel.Text = "Code Expires in : 3:00";
            // 
            // closePB
            // 
            this.closePB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closePB.Image = global::MakeMyTripClone.Properties.Resources.close;
            this.closePB.Location = new System.Drawing.Point(418, 28);
            this.closePB.Name = "closePB";
            this.closePB.Size = new System.Drawing.Size(27, 26);
            this.closePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.closePB.TabIndex = 4;
            this.closePB.TabStop = false;
            this.closePB.Click += new System.EventHandler(this.OnClosePBClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MakeMyTripClone.Properties.Resources.makemytrip_logo;
            this.pictureBox1.Location = new System.Drawing.Point(101, -27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(247, 199);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // ConfirmationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(466, 460);
            this.ControlBox = false;
            this.Controls.Add(this.closePB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.confirmationCodeTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.confirmBtn);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ConfirmationForm";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.closePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.MaskedTextBox confirmationCodeTB;
        private System.Windows.Forms.Button confirmBtn;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.PictureBox closePB;
    }
}