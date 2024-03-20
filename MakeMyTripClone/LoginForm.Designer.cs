namespace MakeMyTripClone
{
    partial class LoginForm
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
            this.emailTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.submitBtn = new System.Windows.Forms.Button();
            this.viewHideBtn = new System.Windows.Forms.Button();
            this.passwordUnderLinePanel = new System.Windows.Forms.Panel();
            this.emailUnderLinePanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.inputPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // emailTB
            // 
            this.emailTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.emailTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.emailTB.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.emailTB.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailTB.Location = new System.Drawing.Point(74, 123);
            this.emailTB.Name = "emailTB";
            this.emailTB.Size = new System.Drawing.Size(393, 28);
            this.emailTB.TabIndex = 2;
            this.emailTB.TextChanged += new System.EventHandler(this.OnEmailTBTextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(69, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Email - ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(69, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // passwordTB
            // 
            this.passwordTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.passwordTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passwordTB.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.passwordTB.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordTB.Location = new System.Drawing.Point(74, 220);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(393, 28);
            this.passwordTB.TabIndex = 4;
            this.passwordTB.UseSystemPasswordChar = true;
            // 
            // inputPanel
            // 
            this.inputPanel.BackColor = System.Drawing.Color.White;
            this.inputPanel.Controls.Add(this.label3);
            this.inputPanel.Controls.Add(this.panel1);
            this.inputPanel.Controls.Add(this.viewHideBtn);
            this.inputPanel.Controls.Add(this.passwordUnderLinePanel);
            this.inputPanel.Controls.Add(this.emailUnderLinePanel);
            this.inputPanel.Controls.Add(this.label2);
            this.inputPanel.Controls.Add(this.label4);
            this.inputPanel.Controls.Add(this.label1);
            this.inputPanel.Controls.Add(this.emailTB);
            this.inputPanel.Controls.Add(this.passwordTB);
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputPanel.Location = new System.Drawing.Point(397, 0);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(571, 571);
            this.inputPanel.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(70, 276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(217, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Don\'t have an account ?";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.submitBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 360);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(571, 211);
            this.panel1.TabIndex = 7;
            // 
            // submitBtn
            // 
            this.submitBtn.Font = new System.Drawing.Font("Sitka Text Semibold", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitBtn.Location = new System.Drawing.Point(232, 27);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(128, 41);
            this.submitBtn.TabIndex = 0;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.MouseEnter += new System.EventHandler(this.OnSubmitBtnMouseHover);
            this.submitBtn.MouseHover += new System.EventHandler(this.OnSubmitBtnMouseHover);
            // 
            // viewHideBtn
            // 
            this.viewHideBtn.BackgroundImage = global::MakeMyTripClone.Properties.Resources.viewPassword;
            this.viewHideBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.viewHideBtn.FlatAppearance.BorderSize = 0;
            this.viewHideBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewHideBtn.Location = new System.Drawing.Point(473, 223);
            this.viewHideBtn.Name = "viewHideBtn";
            this.viewHideBtn.Size = new System.Drawing.Size(39, 37);
            this.viewHideBtn.TabIndex = 1;
            this.viewHideBtn.UseVisualStyleBackColor = true;
            this.viewHideBtn.Click += new System.EventHandler(this.OnViewHideBtnClicked);
            // 
            // passwordUnderLinePanel
            // 
            this.passwordUnderLinePanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.passwordUnderLinePanel.Location = new System.Drawing.Point(74, 254);
            this.passwordUnderLinePanel.Name = "passwordUnderLinePanel";
            this.passwordUnderLinePanel.Size = new System.Drawing.Size(393, 1);
            this.passwordUnderLinePanel.TabIndex = 6;
            // 
            // emailUnderLinePanel
            // 
            this.emailUnderLinePanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.emailUnderLinePanel.Location = new System.Drawing.Point(74, 157);
            this.emailUnderLinePanel.Name = "emailUnderLinePanel";
            this.emailUnderLinePanel.Size = new System.Drawing.Size(393, 1);
            this.emailUnderLinePanel.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(243, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Sign - In";
            // 
            // imagePanel
            // 
            this.imagePanel.BackgroundImage = global::MakeMyTripClone.Properties.Resources.LoginPageCarousel1;
            this.imagePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imagePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.imagePanel.Location = new System.Drawing.Point(0, 0);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(397, 571);
            this.imagePanel.TabIndex = 0;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 571);
            this.ControlBox = false;
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.imagePanel);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.Button viewHideBtn;
        private System.Windows.Forms.TextBox emailTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.Panel emailUnderLinePanel;
        private System.Windows.Forms.Panel passwordUnderLinePanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}