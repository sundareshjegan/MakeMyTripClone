namespace MakeMyTripClone.HomePage
{
    partial class HomePageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomePageForm));
            this.navBar1 = new MakeMyTripClone.NavBar();
            this.SuspendLayout();
            // 
            // navBar1
            // 
            this.navBar1.BackColor = System.Drawing.Color.White;
            this.navBar1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("navBar1.BackgroundImage")));
            this.navBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBar1.Location = new System.Drawing.Point(0, 0);
            this.navBar1.Name = "navBar1";
            this.navBar1.Size = new System.Drawing.Size(1364, 596);
            this.navBar1.TabIndex = 0;
            // 
            // HomePageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 596);
            this.Controls.Add(this.navBar1);
            this.Name = "HomePageForm";
            this.Text = "HomePageForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private MakeMyTripClone.NavBar navBar1;
    }
}