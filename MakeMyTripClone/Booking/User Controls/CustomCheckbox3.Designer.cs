namespace MakeMyTripClone
{
    partial class CustomCheckBox3
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
            this.checkBoxs = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxs
            // 
            this.checkBoxs.AutoSize = true;
            this.checkBoxs.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxs.Location = new System.Drawing.Point(19, 15);
            this.checkBoxs.Name = "checkBoxs";
            this.checkBoxs.Size = new System.Drawing.Size(127, 29);
            this.checkBoxs.TabIndex = 2;
            this.checkBoxs.Text = "checkBox1";
            this.checkBoxs.UseVisualStyleBackColor = true;
            this.checkBoxs.Click += new System.EventHandler(this.CustomCheckboxClick);
            // 
            // CustomCheckBox3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.checkBoxs);
            this.Name = "CustomCheckBox3";
            this.Size = new System.Drawing.Size(425, 46);
            this.Click += new System.EventHandler(this.CustomCheckboxClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox checkBoxs;
    }
}
