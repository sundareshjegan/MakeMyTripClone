namespace MakeMyTripClone
{
    partial class LoaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoaderForm));
            this.loaderGifPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loaderGifPB)).BeginInit();
            this.SuspendLayout();
            // 
            // loaderGifPB
            // 
            this.loaderGifPB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loaderGifPB.Image = ((System.Drawing.Image)(resources.GetObject("loaderGifPB.Image")));
            this.loaderGifPB.Location = new System.Drawing.Point(0, 0);
            this.loaderGifPB.Name = "loaderGifPB";
            this.loaderGifPB.Size = new System.Drawing.Size(80, 80);
            this.loaderGifPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loaderGifPB.TabIndex = 0;
            this.loaderGifPB.TabStop = false;
            // 
            // LoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(80, 80);
            this.Controls.Add(this.loaderGifPB);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(80, 80);
            this.MinimumSize = new System.Drawing.Size(80, 80);
            this.Name = "LoaderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestForm";
            this.TransparencyKey = System.Drawing.Color.White;
            ((System.ComponentModel.ISupportInitialize)(this.loaderGifPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox loaderGifPB;
    }
}