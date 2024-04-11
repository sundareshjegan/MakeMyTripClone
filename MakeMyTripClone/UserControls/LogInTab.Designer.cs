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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.beforeLogInTab = new System.Windows.Forms.TabPage();
            this.logInBtn = new System.Windows.Forms.Button();
            this.afterLogInTab = new System.Windows.Forms.TabPage();
            this.mailLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.profileImagePB = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.beforeLogInTab.SuspendLayout();
            this.afterLogInTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profileImagePB)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.beforeLogInTab);
            this.tabControl1.Controls.Add(this.afterLogInTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(246, 64);
            this.tabControl1.TabIndex = 0;
            // 
            // beforeLogInTab
            // 
            this.beforeLogInTab.Controls.Add(this.logInBtn);
            this.beforeLogInTab.Location = new System.Drawing.Point(4, 9);
            this.beforeLogInTab.Name = "beforeLogInTab";
            this.beforeLogInTab.Padding = new System.Windows.Forms.Padding(3);
            this.beforeLogInTab.Size = new System.Drawing.Size(238, 51);
            this.beforeLogInTab.TabIndex = 0;
            this.beforeLogInTab.UseVisualStyleBackColor = true;
            // 
            // logInBtn
            // 
            this.logInBtn.BackColor = System.Drawing.Color.DodgerBlue;
            this.logInBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logInBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logInBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logInBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logInBtn.ForeColor = System.Drawing.Color.White;
            this.logInBtn.Location = new System.Drawing.Point(3, 3);
            this.logInBtn.Name = "logInBtn";
            this.logInBtn.Size = new System.Drawing.Size(232, 45);
            this.logInBtn.TabIndex = 0;
            this.logInBtn.Text = "Login or Create Account";
            this.logInBtn.UseVisualStyleBackColor = false;
            this.logInBtn.Click += new System.EventHandler(this.OnLogInBtnClicked);
            // 
            // afterLogInTab
            // 
            this.afterLogInTab.Controls.Add(this.mailLabel);
            this.afterLogInTab.Controls.Add(this.nameLabel);
            this.afterLogInTab.Controls.Add(this.profileImagePB);
            this.afterLogInTab.Location = new System.Drawing.Point(4, 5);
            this.afterLogInTab.Name = "afterLogInTab";
            this.afterLogInTab.Padding = new System.Windows.Forms.Padding(3);
            this.afterLogInTab.Size = new System.Drawing.Size(238, 55);
            this.afterLogInTab.TabIndex = 1;
            this.afterLogInTab.UseVisualStyleBackColor = true;
            // 
            // mailLabel
            // 
            this.mailLabel.AutoSize = true;
            this.mailLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mailLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailLabel.Location = new System.Drawing.Point(48, 26);
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
            this.nameLabel.Location = new System.Drawing.Point(48, 3);
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
            this.profileImagePB.Location = new System.Drawing.Point(3, 3);
            this.profileImagePB.Name = "profileImagePB";
            this.profileImagePB.Padding = new System.Windows.Forms.Padding(8);
            this.profileImagePB.Size = new System.Drawing.Size(45, 49);
            this.profileImagePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profileImagePB.TabIndex = 0;
            this.profileImagePB.TabStop = false;
            // 
            // LogInTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "LogInTab";
            this.Size = new System.Drawing.Size(246, 64);
            this.tabControl1.ResumeLayout(false);
            this.beforeLogInTab.ResumeLayout(false);
            this.afterLogInTab.ResumeLayout(false);
            this.afterLogInTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profileImagePB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage beforeLogInTab;
        private System.Windows.Forms.Button logInBtn;
        private System.Windows.Forms.TabPage afterLogInTab;
        private System.Windows.Forms.PictureBox profileImagePB;
        private System.Windows.Forms.Label mailLabel;
        private System.Windows.Forms.Label nameLabel;
    }
}
