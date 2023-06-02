namespace SVMember
{
    partial class Frm_SeminarDialog
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lb_Condi1 = new System.Windows.Forms.Label();
            this.lb_Condi2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
          //  this.pictureBox1.Image = global::SVMember.Properties.Resources.not;
            this.pictureBox1.Location = new System.Drawing.Point(273, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(164, 178);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lb_Condi1
            // 
            this.lb_Condi1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Condi1.ForeColor = System.Drawing.Color.Yellow;
            this.lb_Condi1.Location = new System.Drawing.Point(12, 208);
            this.lb_Condi1.Name = "lb_Condi1";
            this.lb_Condi1.Size = new System.Drawing.Size(698, 86);
            this.lb_Condi1.TabIndex = 1;
            this.lb_Condi1.Text = "dsfsdfsdf";
            this.lb_Condi1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Condi2
            // 
            this.lb_Condi2.AutoSize = true;
            this.lb_Condi2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Condi2.ForeColor = System.Drawing.Color.Yellow;
            this.lb_Condi2.Location = new System.Drawing.Point(175, 306);
            this.lb_Condi2.Name = "lb_Condi2";
            this.lb_Condi2.Size = new System.Drawing.Size(384, 46);
            this.lb_Condi2.TabIndex = 2;
            this.lb_Condi2.Text = "ท่านไม่เป็นผู้แทนสมาชิก";
            this.lb_Condi2.Click += new System.EventHandler(this.lb_Condi2_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Frm_SeminarDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(722, 415);
            this.Controls.Add(this.lb_Condi2);
            this.Controls.Add(this.lb_Condi1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Frm_SeminarDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_SeminarDialog";
            this.Load += new System.EventHandler(this.Frm_SeminarDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label lb_Condi1;
        private System.Windows.Forms.Label lb_Condi2;
        private System.Windows.Forms.Timer timer1;
    }
}