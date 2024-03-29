namespace MakeMyTripClone.UserControls
{
    partial class Traveller_Details
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Traveller_Details));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.femaleBtn = new System.Windows.Forms.Button();
            this.maleBtn = new System.Windows.Forms.Button();
            this.nameWarningLabel = new System.Windows.Forms.Label();
            this.ageWarningLabel = new System.Windows.Forms.Label();
            this.namePanel = new System.Windows.Forms.Panel();
            this.agePanel = new System.Windows.Forms.Panel();
            this.ageTB = new System.Windows.Forms.TextBox();
            this.namePanel.SuspendLayout();
            this.agePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seat 3G";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(88, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name*";
            // 
            // nameTB
            // 
            this.nameTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameTB.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTB.Location = new System.Drawing.Point(2, 2);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(228, 30);
            this.nameTB.TabIndex = 1;
            this.nameTB.TextChanged += new System.EventHandler(this.OnNameTBTextChanged);
            this.nameTB.Enter += new System.EventHandler(this.OnTextBoxActive);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(325, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Age*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(460, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Gender";
            // 
            // femaleBtn
            // 
            this.femaleBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.femaleBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.femaleBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.femaleBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.femaleBtn.Image = global::MakeMyTripClone.Properties.Resources.female;
            this.femaleBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.femaleBtn.Location = new System.Drawing.Point(558, 37);
            this.femaleBtn.Name = "femaleBtn";
            this.femaleBtn.Size = new System.Drawing.Size(100, 33);
            this.femaleBtn.TabIndex = 2;
            this.femaleBtn.Text = "      Female";
            this.femaleBtn.UseVisualStyleBackColor = true;
            this.femaleBtn.Click += new System.EventHandler(this.OnGenderBtnClicked);
            // 
            // maleBtn
            // 
            this.maleBtn.BackColor = System.Drawing.Color.White;
            this.maleBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.maleBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.maleBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maleBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maleBtn.Image = ((System.Drawing.Image)(resources.GetObject("maleBtn.Image")));
            this.maleBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.maleBtn.Location = new System.Drawing.Point(464, 37);
            this.maleBtn.Name = "maleBtn";
            this.maleBtn.Size = new System.Drawing.Size(95, 33);
            this.maleBtn.TabIndex = 2;
            this.maleBtn.Text = "    Male";
            this.maleBtn.UseVisualStyleBackColor = false;
            this.maleBtn.Click += new System.EventHandler(this.OnGenderBtnClicked);
            // 
            // nameWarningLabel
            // 
            this.nameWarningLabel.AutoSize = true;
            this.nameWarningLabel.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.nameWarningLabel.Location = new System.Drawing.Point(94, 71);
            this.nameWarningLabel.Name = "nameWarningLabel";
            this.nameWarningLabel.Size = new System.Drawing.Size(0, 17);
            this.nameWarningLabel.TabIndex = 0;
            // 
            // ageWarningLabel
            // 
            this.ageWarningLabel.AutoSize = true;
            this.ageWarningLabel.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.ageWarningLabel.Location = new System.Drawing.Point(330, 71);
            this.ageWarningLabel.Name = "ageWarningLabel";
            this.ageWarningLabel.Size = new System.Drawing.Size(0, 17);
            this.ageWarningLabel.TabIndex = 0;
            // 
            // namePanel
            // 
            this.namePanel.BackColor = System.Drawing.Color.White;
            this.namePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.namePanel.Controls.Add(this.nameTB);
            this.namePanel.Location = new System.Drawing.Point(91, 34);
            this.namePanel.Name = "namePanel";
            this.namePanel.Padding = new System.Windows.Forms.Padding(2);
            this.namePanel.Size = new System.Drawing.Size(232, 34);
            this.namePanel.TabIndex = 3;
            this.namePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // agePanel
            // 
            this.agePanel.BackColor = System.Drawing.Color.White;
            this.agePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.agePanel.Controls.Add(this.ageTB);
            this.agePanel.Location = new System.Drawing.Point(329, 34);
            this.agePanel.Name = "agePanel";
            this.agePanel.Padding = new System.Windows.Forms.Padding(2);
            this.agePanel.Size = new System.Drawing.Size(118, 34);
            this.agePanel.TabIndex = 3;
            this.agePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ageTB
            // 
            this.ageTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ageTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ageTB.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageTB.Location = new System.Drawing.Point(2, 2);
            this.ageTB.Name = "ageTB";
            this.ageTB.Size = new System.Drawing.Size(114, 30);
            this.ageTB.TabIndex = 1;
            this.ageTB.TextChanged += new System.EventHandler(this.OnAgeTBTextChanged);
            this.ageTB.Enter += new System.EventHandler(this.OnTextBoxActive);
            // 
            // Traveller_Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.agePanel);
            this.Controls.Add(this.namePanel);
            this.Controls.Add(this.femaleBtn);
            this.Controls.Add(this.maleBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ageWarningLabel);
            this.Controls.Add(this.nameWarningLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Traveller_Details";
            this.Size = new System.Drawing.Size(669, 101);
            this.Click += new System.EventHandler(this.OnTextBoxActive);
            this.Enter += new System.EventHandler(this.OnTextBoxActive);
            this.namePanel.ResumeLayout(false);
            this.namePanel.PerformLayout();
            this.agePanel.ResumeLayout(false);
            this.agePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button maleBtn;
        public System.Windows.Forms.Button femaleBtn;
        private System.Windows.Forms.Label nameWarningLabel;
        private System.Windows.Forms.Label ageWarningLabel;
        private System.Windows.Forms.Panel namePanel;
        private System.Windows.Forms.Panel agePanel;
        private System.Windows.Forms.TextBox ageTB;
    }
}
