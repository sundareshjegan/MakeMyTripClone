namespace MakeMyTripClone
{
    partial class Address
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
            this.stopnamelabel = new System.Windows.Forms.Label();
            this.datetimelabel = new System.Windows.Forms.Label();
            this.addresslabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // stopnamelabel
            // 
            this.stopnamelabel.AutoSize = true;
            this.stopnamelabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopnamelabel.Location = new System.Drawing.Point(5, 19);
            this.stopnamelabel.Name = "stopnamelabel";
            this.stopnamelabel.Size = new System.Drawing.Size(65, 25);
            this.stopnamelabel.TabIndex = 0;
            this.stopnamelabel.Text = "label1";
            this.stopnamelabel.Click += new System.EventHandler(this.AddressClick);
            // 
            // datetimelabel
            // 
            this.datetimelabel.AutoSize = true;
            this.datetimelabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datetimelabel.Location = new System.Drawing.Point(8, 2);
            this.datetimelabel.Name = "datetimelabel";
            this.datetimelabel.Size = new System.Drawing.Size(50, 20);
            this.datetimelabel.TabIndex = 1;
            this.datetimelabel.Text = "label2";
            this.datetimelabel.Click += new System.EventHandler(this.AddressClick);
            // 
            // addresslabel
            // 
            this.addresslabel.AutoSize = true;
            this.addresslabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addresslabel.Location = new System.Drawing.Point(8, 45);
            this.addresslabel.Name = "addresslabel";
            this.addresslabel.Size = new System.Drawing.Size(38, 13);
            this.addresslabel.TabIndex = 1;
            this.addresslabel.Text = "label2";
            this.addresslabel.Click += new System.EventHandler(this.AddressClick);
            // 
            // Address
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScrollMinSize = new System.Drawing.Size(100, 0);
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.addresslabel);
            this.Controls.Add(this.datetimelabel);
            this.Controls.Add(this.stopnamelabel);
            this.Name = "Address";
            this.Size = new System.Drawing.Size(198, 68);
            this.Click += new System.EventHandler(this.AddressClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label stopnamelabel;
        private System.Windows.Forms.Label datetimelabel;
        private System.Windows.Forms.Label addresslabel;
    }
}
