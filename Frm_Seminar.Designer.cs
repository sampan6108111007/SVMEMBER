namespace SVMember
{
    partial class Frm_Seminar
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
            this.timer_AutoRead = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.txt_MbNo = new System.Windows.Forms.Label();
            this.lb_IDCard_Exits = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lb_time = new System.Windows.Forms.Label();
            this.Chk_AutoPrn = new System.Windows.Forms.CheckBox();
            this.lb_ISSUE_NUM = new System.Windows.Forms.Label();
            this.lb_Tsurname = new System.Windows.Forms.TextBox();
            this.lb_Tname = new System.Windows.Forms.TextBox();
            this.lb_TFN = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Chk_AutoSave = new System.Windows.Forms.CheckBox();
            this.Chk_AutoRead = new System.Windows.Forms.CheckBox();
            this.m_ListReaderCard = new System.Windows.Forms.ComboBox();
            this.lb_SeqNo = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lb_Regis = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.m_picPhoto = new System.Windows.Forms.PictureBox();
            this.lb_group2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_group = new System.Windows.Forms.Label();
            this.lbID_Card = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lb_Tname2 = new System.Windows.Forms.Label();
            this.lb_xYear = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // timer_AutoRead
            // 
            this.timer_AutoRead.Interval = 7000;
            this.timer_AutoRead.Tick += new System.EventHandler(this.timer_AutoRead_Tick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.txt_MbNo);
            this.panel2.Controls.Add(this.lb_IDCard_Exits);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.lb_time);
            this.panel2.Controls.Add(this.Chk_AutoPrn);
            this.panel2.Controls.Add(this.lb_ISSUE_NUM);
            this.panel2.Controls.Add(this.lb_Tsurname);
            this.panel2.Controls.Add(this.lb_Tname);
            this.panel2.Controls.Add(this.lb_TFN);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.Chk_AutoSave);
            this.panel2.Controls.Add(this.Chk_AutoRead);
            this.panel2.Controls.Add(this.m_ListReaderCard);
            this.panel2.Controls.Add(this.lb_SeqNo);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.lb_Regis);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.m_picPhoto);
            this.panel2.Controls.Add(this.lb_group2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lb_group);
            this.panel2.Controls.Add(this.lbID_Card);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.lb_Tname2);
            this.panel2.Controls.Add(this.lb_xYear);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(12, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1014, 793);
            this.panel2.TabIndex = 179;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(990, 767);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(21, 23);
            this.button3.TabIndex = 209;
            this.button3.Text = ".";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txt_MbNo
            // 
            this.txt_MbNo.BackColor = System.Drawing.Color.White;
            this.txt_MbNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_MbNo.Location = new System.Drawing.Point(547, 310);
            this.txt_MbNo.Name = "txt_MbNo";
            this.txt_MbNo.Size = new System.Drawing.Size(91, 23);
            this.txt_MbNo.TabIndex = 208;
            this.txt_MbNo.Visible = false;
            // 
            // lb_IDCard_Exits
            // 
            this.lb_IDCard_Exits.BackColor = System.Drawing.Color.White;
            this.lb_IDCard_Exits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_IDCard_Exits.Location = new System.Drawing.Point(356, 310);
            this.lb_IDCard_Exits.Name = "lb_IDCard_Exits";
            this.lb_IDCard_Exits.Size = new System.Drawing.Size(189, 23);
            this.lb_IDCard_Exits.TabIndex = 207;
            this.lb_IDCard_Exits.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(167, 293);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(553, 347);
            this.panel1.TabIndex = 195;
            this.panel1.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label16.ForeColor = System.Drawing.Color.Blue;
            this.label16.Location = new System.Drawing.Point(194, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(207, 31);
            this.label16.TabIndex = 1;
            this.label16.Text = "กำลังรับข้อมูล.......";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(8, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(534, 323);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lb_time
            // 
            this.lb_time.BackColor = System.Drawing.Color.White;
            this.lb_time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_time.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_time.Font = new System.Drawing.Font("Arial", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_time.ForeColor = System.Drawing.Color.Red;
            this.lb_time.Location = new System.Drawing.Point(33, 219);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(936, 99);
            this.lb_time.TabIndex = 206;
            this.lb_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Chk_AutoPrn
            // 
            this.Chk_AutoPrn.AutoSize = true;
            this.Chk_AutoPrn.Checked = true;
            this.Chk_AutoPrn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_AutoPrn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Chk_AutoPrn.Location = new System.Drawing.Point(238, 697);
            this.Chk_AutoPrn.Name = "Chk_AutoPrn";
            this.Chk_AutoPrn.Size = new System.Drawing.Size(89, 21);
            this.Chk_AutoPrn.TabIndex = 205;
            this.Chk_AutoPrn.Text = "Auto Print";
            this.Chk_AutoPrn.UseVisualStyleBackColor = true;
            this.Chk_AutoPrn.CheckedChanged += new System.EventHandler(this.Chk_AutoPrn_CheckedChanged);
            // 
            // lb_ISSUE_NUM
            // 
            this.lb_ISSUE_NUM.BackColor = System.Drawing.Color.White;
            this.lb_ISSUE_NUM.Location = new System.Drawing.Point(704, 695);
            this.lb_ISSUE_NUM.Name = "lb_ISSUE_NUM";
            this.lb_ISSUE_NUM.Size = new System.Drawing.Size(190, 23);
            this.lb_ISSUE_NUM.TabIndex = 204;
            this.lb_ISSUE_NUM.Text = "lb_ISSUE_NUM";
            this.lb_ISSUE_NUM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Tsurname
            // 
            this.lb_Tsurname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_Tsurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Tsurname.Location = new System.Drawing.Point(601, 370);
            this.lb_Tsurname.Name = "lb_Tsurname";
            this.lb_Tsurname.ReadOnly = true;
            this.lb_Tsurname.Size = new System.Drawing.Size(44, 38);
            this.lb_Tsurname.TabIndex = 203;
            this.lb_Tsurname.Visible = false;
            // 
            // lb_Tname
            // 
            this.lb_Tname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_Tname.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Tname.Location = new System.Drawing.Point(547, 370);
            this.lb_Tname.Name = "lb_Tname";
            this.lb_Tname.ReadOnly = true;
            this.lb_Tname.Size = new System.Drawing.Size(52, 38);
            this.lb_Tname.TabIndex = 202;
            this.lb_Tname.Visible = false;
            // 
            // lb_TFN
            // 
            this.lb_TFN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_TFN.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_TFN.Location = new System.Drawing.Point(494, 370);
            this.lb_TFN.Name = "lb_TFN";
            this.lb_TFN.ReadOnly = true;
            this.lb_TFN.Size = new System.Drawing.Size(51, 38);
            this.lb_TFN.TabIndex = 201;
            this.lb_TFN.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(487, 668);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 50);
            this.button2.TabIndex = 200;
            this.button2.Text = "บันทึกข้อมูล";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(342, 667);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 51);
            this.button1.TabIndex = 199;
            this.button1.Text = "เรียกข้อมูล";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // Chk_AutoSave
            // 
            this.Chk_AutoSave.AutoSize = true;
            this.Chk_AutoSave.Checked = true;
            this.Chk_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_AutoSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Chk_AutoSave.Location = new System.Drawing.Point(139, 697);
            this.Chk_AutoSave.Name = "Chk_AutoSave";
            this.Chk_AutoSave.Size = new System.Drawing.Size(92, 21);
            this.Chk_AutoSave.TabIndex = 198;
            this.Chk_AutoSave.Text = "Auto Save";
            this.Chk_AutoSave.UseVisualStyleBackColor = true;
            this.Chk_AutoSave.CheckedChanged += new System.EventHandler(this.Chk_AutoSave_CheckedChanged);
            // 
            // Chk_AutoRead
            // 
            this.Chk_AutoRead.AutoSize = true;
            this.Chk_AutoRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Chk_AutoRead.Location = new System.Drawing.Point(41, 697);
            this.Chk_AutoRead.Name = "Chk_AutoRead";
            this.Chk_AutoRead.Size = new System.Drawing.Size(94, 21);
            this.Chk_AutoRead.TabIndex = 197;
            this.Chk_AutoRead.Text = "Auto Read";
            this.Chk_AutoRead.UseVisualStyleBackColor = true;
            this.Chk_AutoRead.CheckedChanged += new System.EventHandler(this.Chk_AutoRead_CheckedChanged);
            // 
            // m_ListReaderCard
            // 
            this.m_ListReaderCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_ListReaderCard.FormattingEnabled = true;
            this.m_ListReaderCard.Location = new System.Drawing.Point(41, 670);
            this.m_ListReaderCard.Name = "m_ListReaderCard";
            this.m_ListReaderCard.Size = new System.Drawing.Size(296, 21);
            this.m_ListReaderCard.TabIndex = 196;
            // 
            // lb_SeqNo
            // 
            this.lb_SeqNo.BackColor = System.Drawing.Color.White;
            this.lb_SeqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_SeqNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_SeqNo.Location = new System.Drawing.Point(342, 568);
            this.lb_SeqNo.Name = "lb_SeqNo";
            this.lb_SeqNo.Size = new System.Drawing.Size(296, 89);
            this.lb_SeqNo.TabIndex = 194;
            this.lb_SeqNo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label15.Location = new System.Drawing.Point(342, 513);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(296, 55);
            this.label15.TabIndex = 193;
            this.label15.Text = "ลำดับผู้แทน";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lb_Regis
            // 
            this.lb_Regis.BackColor = System.Drawing.Color.White;
            this.lb_Regis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_Regis.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Regis.Location = new System.Drawing.Point(40, 568);
            this.lb_Regis.Name = "lb_Regis";
            this.lb_Regis.Size = new System.Drawing.Size(296, 89);
            this.lb_Regis.TabIndex = 192;
            this.lb_Regis.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(40, 513);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(296, 55);
            this.label12.TabIndex = 191;
            this.label12.Text = "จุดลงทะเบียน";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // m_picPhoto
            // 
            this.m_picPhoto.BackColor = System.Drawing.Color.White;
            this.m_picPhoto.Location = new System.Drawing.Point(656, 321);
            this.m_picPhoto.Name = "m_picPhoto";
            this.m_picPhoto.Size = new System.Drawing.Size(313, 372);
            this.m_picPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_picPhoto.TabIndex = 190;
            this.m_picPhoto.TabStop = false;
            // 
            // lb_group2
            // 
            this.lb_group2.BackColor = System.Drawing.Color.White;
            this.lb_group2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_group2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_group2.Location = new System.Drawing.Point(230, 461);
            this.lb_group2.Name = "lb_group2";
            this.lb_group2.Size = new System.Drawing.Size(418, 46);
            this.lb_group2.TabIndex = 189;
            this.lb_group2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(33, 319);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(262, 48);
            this.label4.TabIndex = 182;
            this.label4.Text = "เลขที่บัตร ปชช.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lb_group
            // 
            this.lb_group.BackColor = System.Drawing.Color.White;
            this.lb_group.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_group.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_group.Location = new System.Drawing.Point(230, 414);
            this.lb_group.Name = "lb_group";
            this.lb_group.Size = new System.Drawing.Size(418, 46);
            this.lb_group.TabIndex = 188;
            this.lb_group.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbID_Card
            // 
            this.lbID_Card.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbID_Card.Enabled = false;
            this.lbID_Card.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbID_Card.Location = new System.Drawing.Point(230, 319);
            this.lbID_Card.Mask = "#-####-#####-##-#";
            this.lbID_Card.Name = "lbID_Card";
            this.lbID_Card.ReadOnly = true;
            this.lbID_Card.Size = new System.Drawing.Size(418, 45);
            this.lbID_Card.TabIndex = 183;
            this.lbID_Card.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(33, 461);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 46);
            this.label8.TabIndex = 187;
            this.label8.Text = "กลุ่ม";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(33, 366);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 46);
            this.label5.TabIndex = 184;
            this.label5.Text = "ชื่อ-สกุล";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(33, 414);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(193, 46);
            this.label7.TabIndex = 186;
            this.label7.Text = "สังกัด";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lb_Tname2
            // 
            this.lb_Tname2.BackColor = System.Drawing.Color.White;
            this.lb_Tname2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_Tname2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_Tname2.Location = new System.Drawing.Point(230, 367);
            this.lb_Tname2.Name = "lb_Tname2";
            this.lb_Tname2.Size = new System.Drawing.Size(418, 46);
            this.lb_Tname2.TabIndex = 185;
            this.lb_Tname2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lb_xYear
            // 
            this.lb_xYear.AutoSize = true;
            this.lb_xYear.BackColor = System.Drawing.Color.White;
            this.lb_xYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_xYear.Location = new System.Drawing.Point(758, 98);
            this.lb_xYear.Name = "lb_xYear";
            this.lb_xYear.Size = new System.Drawing.Size(151, 63);
            this.lb_xYear.TabIndex = 181;
            this.lb_xYear.Text = "2563";
            this.lb_xYear.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(300, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(724, 46);
            this.label3.TabIndex = 180;
            this.label3.Text = "หอประชุมจันทน์ผา มหาวิทยาลัยราชภัฎลำปาง";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(33, 30);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(182, 177);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 179;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(263, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(665, 63);
            this.label2.TabIndex = 178;
            this.label2.Text = "การประชุมใหญ่สามัญ ประจำปี";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(395, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 60);
            this.label1.TabIndex = 177;
            this.label1.Text = "ระบบลงทะเบียน";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Frm_Seminar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 819);
            this.Controls.Add(this.panel2);
            this.Name = "Frm_Seminar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ลงทะเบียนเข้าประชุม";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Seminar_Load);
            this.SizeChanged += new System.EventHandler(this.Frm_Seminar_SizeChanged);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_AutoRead;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lb_xYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txt_MbNo;
        private System.Windows.Forms.Label lb_IDCard_Exits;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lb_time;
        private System.Windows.Forms.CheckBox Chk_AutoPrn;
        private System.Windows.Forms.Label lb_ISSUE_NUM;
        private System.Windows.Forms.TextBox lb_Tsurname;
        private System.Windows.Forms.TextBox lb_Tname;
        private System.Windows.Forms.TextBox lb_TFN;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox Chk_AutoSave;
        private System.Windows.Forms.CheckBox Chk_AutoRead;
        private System.Windows.Forms.ComboBox m_ListReaderCard;
        private System.Windows.Forms.Label lb_SeqNo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lb_Regis;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox m_picPhoto;
        private System.Windows.Forms.Label lb_group2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_group;
        private System.Windows.Forms.MaskedTextBox lbID_Card;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lb_Tname2;
        private System.Windows.Forms.Button button3;

    }
}