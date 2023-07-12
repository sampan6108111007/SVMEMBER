namespace SVMember
{
    partial class MDI
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.เปลยนแปลงขอมลสมาชกToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.เสยบบตรToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.กรอกขอมลToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLabelUserID = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.เปลยนแปลงขอมลสมาชกToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1263, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // เปลยนแปลงขอมลสมาชกToolStripMenuItem
            // 
            this.เปลยนแปลงขอมลสมาชกToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.เสยบบตรToolStripMenuItem,
            this.กรอกขอมลToolStripMenuItem});
            this.เปลยนแปลงขอมลสมาชกToolStripMenuItem.Name = "เปลยนแปลงขอมลสมาชกToolStripMenuItem";
            this.เปลยนแปลงขอมลสมาชกToolStripMenuItem.Size = new System.Drawing.Size(130, 20);
            this.เปลยนแปลงขอมลสมาชกToolStripMenuItem.Text = "เปลี่ยนแปลงข้อมูลสมาชิก";
            // 
            // เสยบบตรToolStripMenuItem
            // 
            this.เสยบบตรToolStripMenuItem.Name = "เสยบบตรToolStripMenuItem";
            this.เสยบบตรToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.เสยบบตรToolStripMenuItem.Text = "เสียบบัตร";
            this.เสยบบตรToolStripMenuItem.Click += new System.EventHandler(this.เสยบบตรToolStripMenuItem_Click);
            // 
            // กรอกขอมลToolStripMenuItem
            // 
            this.กรอกขอมลToolStripMenuItem.Name = "กรอกขอมลToolStripMenuItem";
            this.กรอกขอมลToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.กรอกขอมลToolStripMenuItem.Text = "กรอกข้อมูล";
            this.กรอกขอมลToolStripMenuItem.Click += new System.EventHandler(this.กรอกขอมลToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripLabelUserID});
            this.statusStrip.Location = new System.Drawing.Point(0, 576);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1263, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(66, 17);
            this.toolStripStatusLabel.Text = "ผู้ใช้งานระบบ";
            // 
            // toolStripLabelUserID
            // 
            this.toolStripLabelUserID.Name = "toolStripLabelUserID";
            this.toolStripLabelUserID.Size = new System.Drawing.Size(22, 17);
            this.toolStripLabelUserID.Text = "sss";
            // 
            // MDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 598);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MDI";
            this.Text = "MDI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MDI_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem เปลยนแปลงขอมลสมาชกToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem เสยบบตรToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem กรอกขอมลToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLabelUserID;
    }
}



