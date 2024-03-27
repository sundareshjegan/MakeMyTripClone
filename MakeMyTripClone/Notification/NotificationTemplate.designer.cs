namespace MakeMyTripClone
{
    partial class NotificationTemplate
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.closeButton2 = new MakeMyTripClone.CloseButton();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.timePanel = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.seeMorePanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.closeButton2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(364, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(36, 175);
            this.panel1.TabIndex = 1;
            // 
            // closeButton2
            // 
            this.closeButton2.BackColor = System.Drawing.Color.Transparent;
            this.closeButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.closeButton2.Location = new System.Drawing.Point(0, 0);
            this.closeButton2.Name = "closeButton2";
            this.closeButton2.Size = new System.Drawing.Size(34, 34);
            this.closeButton2.TabIndex = 2;
            this.closeButton2.Click += new System.EventHandler(this.OnNotificationClosed);
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.Transparent;
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.headerPanel.Size = new System.Drawing.Size(364, 45);
            this.headerPanel.TabIndex = 2;
            this.headerPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnHeaderPanelPaint);
            // 
            // timePanel
            // 
            this.timePanel.BackColor = System.Drawing.Color.Transparent;
            this.timePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.timePanel.Location = new System.Drawing.Point(0, 151);
            this.timePanel.Name = "timePanel";
            this.timePanel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.timePanel.Size = new System.Drawing.Size(364, 24);
            this.timePanel.TabIndex = 3;
            this.timePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnTimePanelPaint);
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.Transparent;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 45);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.contentPanel.Size = new System.Drawing.Size(364, 93);
            this.contentPanel.TabIndex = 4;
            this.contentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnContentPanelPaint);
            // 
            // seeMorePanel
            // 
            this.seeMorePanel.BackColor = System.Drawing.Color.Transparent;
            this.seeMorePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.seeMorePanel.Location = new System.Drawing.Point(0, 138);
            this.seeMorePanel.Name = "seeMorePanel";
            this.seeMorePanel.Size = new System.Drawing.Size(364, 13);
            this.seeMorePanel.TabIndex = 5;
            this.seeMorePanel.Click += new System.EventHandler(this.OnFullContentViewed);
            this.seeMorePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.seeMorePanel_Paint);
            this.seeMorePanel.MouseEnter += new System.EventHandler(this.OnSeeMorePanelEnter);
            this.seeMorePanel.MouseLeave += new System.EventHandler(this.OnSeeMorePanelLeave);
            this.seeMorePanel.MouseHover += new System.EventHandler(this.OnSeeMorePanelHover);
            // 
            // NotificationTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 175);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.seeMorePanel);
            this.Controls.Add(this.timePanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotificationTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NotificationTemplate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFadeClose_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NotificationTemplate_FormClosed);
            this.Load += new System.EventHandler(this.NotificationTemplate_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.NotificationTemplate_Paint);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CloseButton closeButton1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel timePanel;
        private System.Windows.Forms.Panel contentPanel;
        private CloseButton closeButton2;
        private System.Windows.Forms.Panel seeMorePanel;
    }
}