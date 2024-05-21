namespace MakeMyTripClone
{
    partial class LogInTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mailLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.profileImagePB = new System.Windows.Forms.PictureBox();
            this.afterLogInPanel = new System.Windows.Forms.Panel();
            this.beforeLogInPanel = new System.Windows.Forms.Panel();
            this.logInBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.profileImagePB)).BeginInit();
            this.afterLogInPanel.SuspendLayout();
            this.beforeLogInPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mailLabel
            // 
            this.mailLabel.AutoSize = true;
            this.mailLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mailLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.mailLabel.Location = new System.Drawing.Point(49, 23);
            this.mailLabel.Name = "mailLabel";
            this.mailLabel.Size = new System.Drawing.Size(124, 17);
            this.mailLabel.TabIndex = 2;
            this.mailLabel.Text = "sundar@gmail.com";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.nameLabel.Location = new System.Drawing.Point(49, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.nameLabel.Size = new System.Drawing.Size(47, 23);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Guest";
            // 
            // profileImagePB
            // 
            this.profileImagePB.Dock = System.Windows.Forms.DockStyle.Left;
            this.profileImagePB.Image = global::MakeMyTripClone.Properties.Resources.user;
            this.profileImagePB.Location = new System.Drawing.Point(0, 0);
            this.profileImagePB.Name = "profileImagePB";
            this.profileImagePB.Padding = new System.Windows.Forms.Padding(8);
            this.profileImagePB.Size = new System.Drawing.Size(49, 36);
            this.profileImagePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profileImagePB.TabIndex = 0;
            this.profileImagePB.TabStop = false;
            // 
            // afterLogInPanel
            // 
            this.afterLogInPanel.BackColor = System.Drawing.Color.Transparent;
            this.afterLogInPanel.Controls.Add(this.mailLabel);
            this.afterLogInPanel.Controls.Add(this.nameLabel);
            this.afterLogInPanel.Controls.Add(this.profileImagePB);
            this.afterLogInPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.afterLogInPanel.Location = new System.Drawing.Point(0, 0);
            this.afterLogInPanel.Name = "afterLogInPanel";
            this.afterLogInPanel.Size = new System.Drawing.Size(180, 36);
            this.afterLogInPanel.TabIndex = 1;
            // 
            // beforeLogInPanel
            // 
            this.beforeLogInPanel.Controls.Add(this.logInBtn);
            this.beforeLogInPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.beforeLogInPanel.Location = new System.Drawing.Point(0, 0);
            this.beforeLogInPanel.Name = "beforeLogInPanel";
            this.beforeLogInPanel.Size = new System.Drawing.Size(180, 36);
            this.beforeLogInPanel.TabIndex = 4;
            // 
            // logInBtn
            // 
            this.logInBtn.BackColor = System.Drawing.Color.DodgerBlue;
            this.logInBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logInBtn.FlatAppearance.BorderSize = 0;
            this.logInBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logInBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logInBtn.ForeColor = System.Drawing.Color.White;
            this.logInBtn.Location = new System.Drawing.Point(0, 0);
            this.logInBtn.Name = "logInBtn";
            this.logInBtn.Size = new System.Drawing.Size(180, 36);
            this.logInBtn.TabIndex = 0;
            this.logInBtn.Text = "Login or Create Account";
            this.logInBtn.UseVisualStyleBackColor = false;
            this.logInBtn.Click += new System.EventHandler(this.OnLogInBtnClicked);
            // 
            // LogInTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.beforeLogInPanel);
            this.Controls.Add(this.afterLogInPanel);
            this.Name = "LogInTab";
            this.Size = new System.Drawing.Size(180, 36);
            ((System.ComponentModel.ISupportInitialize)(this.profileImagePB)).EndInit();
            this.afterLogInPanel.ResumeLayout(false);
            this.afterLogInPanel.PerformLayout();
            this.beforeLogInPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox profileImagePB;
        private System.Windows.Forms.Label mailLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Panel afterLogInPanel;
        private System.Windows.Forms.Panel beforeLogInPanel;
        private System.Windows.Forms.Button logInBtn;
    }
}
