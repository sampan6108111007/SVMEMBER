namespace SVMember
{
    partial class Frm_Approve2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle121 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle122 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle123 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle124 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle125 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle126 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle127 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle128 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle129 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle130 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle131 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle132 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle133 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle134 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle135 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.card_personn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Appv_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addr_mobilephonec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addr_mobilephonen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sYES = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sNO = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column3,
            this.Column1,
            this.Column6,
            this.card_personn,
            this.Column2,
            this.Column10,
            this.Appv_date,
            this.Column5,
            this.Column7,
            this.Column8,
            this.addr_mobilephonec,
            this.addr_mobilephonen,
            this.sYES,
            this.sNO});
            this.dataGridView1.Location = new System.Drawing.Point(12, 108);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 50;
            this.dataGridView1.RowTemplate.Height = 50;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1168, 448);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick_1);
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "appl_docno";
            dataGridViewCellStyle121.Font = new System.Drawing.Font("TH Chakra Petch", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Column4.DefaultCellStyle = dataGridViewCellStyle121;
            this.Column4.HeaderText = "คำขอ";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "app_date";
            dataGridViewCellStyle122.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            dataGridViewCellStyle122.Format = "D";
            dataGridViewCellStyle122.NullValue = null;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle122;
            this.Column3.HeaderText = "วันที่ขอ";
            this.Column3.Name = "Column3";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "member_no";
            dataGridViewCellStyle123.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle123.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            this.Column1.DefaultCellStyle = dataGridViewCellStyle123;
            this.Column1.HeaderText = "เลขทะเบียน";
            this.Column1.Name = "Column1";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "card_personc";
            dataGridViewCellStyle124.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            this.Column6.DefaultCellStyle = dataGridViewCellStyle124;
            this.Column6.HeaderText = "เลขที่บัตรประชาชน ปัจจุบัน";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 200;
            // 
            // card_personn
            // 
            this.card_personn.DataPropertyName = "card_personn";
            dataGridViewCellStyle125.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            this.card_personn.DefaultCellStyle = dataGridViewCellStyle125;
            this.card_personn.HeaderText = "เลขที่บัตรประชาชน ใหม่";
            this.card_personn.Name = "card_personn";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "MbnameC";
            dataGridViewCellStyle126.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            this.Column2.DefaultCellStyle = dataGridViewCellStyle126;
            this.Column2.HeaderText = "ชื่อ-สกุล ปัจจุบัน";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "MbnameN";
            dataGridViewCellStyle127.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            this.Column10.DefaultCellStyle = dataGridViewCellStyle127;
            this.Column10.HeaderText = "ชื่อ-สกุล ใหม่";
            this.Column10.Name = "Column10";
            // 
            // Appv_date
            // 
            this.Appv_date.DataPropertyName = "Cur_Add";
            dataGridViewCellStyle128.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle128.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            dataGridViewCellStyle128.Format = "d";
            dataGridViewCellStyle128.NullValue = null;
            this.Appv_date.DefaultCellStyle = dataGridViewCellStyle128;
            this.Appv_date.HeaderText = "ที่อยู่ปัจจุบัน";
            this.Appv_date.Name = "Appv_date";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "New_Add";
            dataGridViewCellStyle129.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle129.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            this.Column5.DefaultCellStyle = dataGridViewCellStyle129;
            this.Column5.HeaderText = "ที่อยู่ ใหม่";
            this.Column5.Name = "Column5";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "birth_datec";
            dataGridViewCellStyle130.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            this.Column7.DefaultCellStyle = dataGridViewCellStyle130;
            this.Column7.HeaderText = "วันเกิด ปัจจุบัน";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "birth_daten";
            dataGridViewCellStyle131.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            this.Column8.DefaultCellStyle = dataGridViewCellStyle131;
            this.Column8.HeaderText = "วันเกิด ใหม่";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // addr_mobilephonec
            // 
            this.addr_mobilephonec.DataPropertyName = "addr_mobilephonec";
            dataGridViewCellStyle132.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            this.addr_mobilephonec.DefaultCellStyle = dataGridViewCellStyle132;
            this.addr_mobilephonec.HeaderText = "เบอร์โทรศัพท์ ปัจจุบัน";
            this.addr_mobilephonec.Name = "addr_mobilephonec";
            // 
            // addr_mobilephonen
            // 
            this.addr_mobilephonen.DataPropertyName = "addr_mobilephonen";
            dataGridViewCellStyle133.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            this.addr_mobilephonen.DefaultCellStyle = dataGridViewCellStyle133;
            this.addr_mobilephonen.HeaderText = "เบอร์โทรศัพท์ ใหม่";
            this.addr_mobilephonen.Name = "addr_mobilephonen";
            // 
            // sYES
            // 
            this.sYES.DataPropertyName = "sYES";
            dataGridViewCellStyle134.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle134.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            dataGridViewCellStyle134.NullValue = false;
            dataGridViewCellStyle134.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sYES.DefaultCellStyle = dataGridViewCellStyle134;
            this.sYES.HeaderText = "อนุมัติ";
            this.sYES.Name = "sYES";
            this.sYES.Width = 50;
            // 
            // sNO
            // 
            this.sNO.DataPropertyName = "sNO";
            dataGridViewCellStyle135.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle135.Font = new System.Drawing.Font("TH Chakra Petch", 18F);
            dataGridViewCellStyle135.NullValue = false;
            dataGridViewCellStyle135.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.sNO.DefaultCellStyle = dataGridViewCellStyle135;
            this.sNO.HeaderText = "ไม่อนุมัติ";
            this.sNO.Name = "sNO";
            this.sNO.Width = 60;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.Location = new System.Drawing.Point(14, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 43);
            this.button1.TabIndex = 18;
            this.button1.Text = "บันทึก";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(14, 85);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(87, 17);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "อนุมัติทั้งหมด";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(114, 85);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(100, 17);
            this.checkBox2.TabIndex = 20;
            this.checkBox2.Text = "ไม่อนุมัติทั้งหมด";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // Frm_Approve2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 568);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Frm_Approve2";
            this.Text = "Frm_Approve2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Approve2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn card_personn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Appv_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn addr_mobilephonec;
        private System.Windows.Forms.DataGridViewTextBoxColumn addr_mobilephonen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn sYES;
        private System.Windows.Forms.DataGridViewCheckBoxColumn sNO;
    }
}