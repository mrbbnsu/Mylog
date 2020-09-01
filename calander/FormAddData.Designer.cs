namespace calander
{
    partial class FormAddData
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
            this.LabTableName = new System.Windows.Forms.Label();
            this.combox_add_y = new System.Windows.Forms.ComboBox();
            this.combox_add_m = new System.Windows.Forms.ComboBox();
            this.combox_add_d = new System.Windows.Forms.ComboBox();
            this.butupdate = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BodyWeight = new System.Windows.Forms.ToolStripMenuItem();
            this.SumOfMoneyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxpayment = new System.Windows.Forms.TextBox();
            this.textBoxnote = new System.Windows.Forms.TextBox();
            this.text_comsume = new System.Windows.Forms.TextBox();
            this.combox_item = new System.Windows.Forms.ComboBox();
            this.combox_category = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonlastday = new System.Windows.Forms.Button();
            this.buttonnextday = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.others = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.campuscard = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cash = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.debitcard = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.wechat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.alipay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonUpDate = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bodyweighttextbox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabTableName
            // 
            this.LabTableName.AutoSize = true;
            this.LabTableName.Location = new System.Drawing.Point(19, 44);
            this.LabTableName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabTableName.Name = "LabTableName";
            this.LabTableName.Size = new System.Drawing.Size(37, 15);
            this.LabTableName.TabIndex = 9;
            this.LabTableName.Text = "日期";
            // 
            // combox_add_y
            // 
            this.combox_add_y.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combox_add_y.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combox_add_y.FormattingEnabled = true;
            this.combox_add_y.Items.AddRange(new object[] {
            "2018",
            "2019",
            "2020",
            "2021"});
            this.combox_add_y.Location = new System.Drawing.Point(21, 66);
            this.combox_add_y.Margin = new System.Windows.Forms.Padding(4);
            this.combox_add_y.Name = "combox_add_y";
            this.combox_add_y.Size = new System.Drawing.Size(72, 23);
            this.combox_add_y.TabIndex = 1;
            this.combox_add_y.SelectedIndexChanged += new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            // 
            // combox_add_m
            // 
            this.combox_add_m.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combox_add_m.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combox_add_m.FormattingEnabled = true;
            this.combox_add_m.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.combox_add_m.Location = new System.Drawing.Point(103, 66);
            this.combox_add_m.Margin = new System.Windows.Forms.Padding(4);
            this.combox_add_m.Name = "combox_add_m";
            this.combox_add_m.Size = new System.Drawing.Size(51, 23);
            this.combox_add_m.TabIndex = 2;
            this.combox_add_m.SelectedIndexChanged += new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            // 
            // combox_add_d
            // 
            this.combox_add_d.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combox_add_d.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combox_add_d.FormattingEnabled = true;
            this.combox_add_d.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.combox_add_d.Location = new System.Drawing.Point(163, 66);
            this.combox_add_d.Margin = new System.Windows.Forms.Padding(4);
            this.combox_add_d.Name = "combox_add_d";
            this.combox_add_d.Size = new System.Drawing.Size(52, 23);
            this.combox_add_d.TabIndex = 3;
            this.combox_add_d.SelectedIndexChanged += new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            // 
            // butupdate
            // 
            this.butupdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butupdate.Location = new System.Drawing.Point(855, 64);
            this.butupdate.Margin = new System.Windows.Forms.Padding(4);
            this.butupdate.Name = "butupdate";
            this.butupdate.Size = new System.Drawing.Size(100, 29);
            this.butupdate.TabIndex = 9;
            this.butupdate.Text = "更新";
            this.butupdate.UseVisualStyleBackColor = true;
            this.butupdate.Click += new System.EventHandler(this.butupdate_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 240);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1059, 233);
            this.dataGridView1.TabIndex = 20;
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tablesToolStripMenuItem,
            this.controlToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1123, 28);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tablesToolStripMenuItem
            // 
            this.tablesToolStripMenuItem.Checked = true;
            this.tablesToolStripMenuItem.CheckOnClick = true;
            this.tablesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tablesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BodyWeight,
            this.SumOfMoneyToolStripMenuItem,
            this.toolStripSeparator1});
            this.tablesToolStripMenuItem.Name = "tablesToolStripMenuItem";
            this.tablesToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.tablesToolStripMenuItem.Text = "Tables";
            this.tablesToolStripMenuItem.Click += new System.EventHandler(this.tablesToolStripMenuItem_Click);
            // 
            // BodyWeight
            // 
            this.BodyWeight.Name = "BodyWeight";
            this.BodyWeight.Size = new System.Drawing.Size(114, 26);
            this.BodyWeight.Text = "体重";
            this.BodyWeight.Click += new System.EventHandler(this.BodyWeight_Click);
            // 
            // SumOfMoneyToolStripMenuItem
            // 
            this.SumOfMoneyToolStripMenuItem.Name = "SumOfMoneyToolStripMenuItem";
            this.SumOfMoneyToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.SumOfMoneyToolStripMenuItem.Text = "余额";
            this.SumOfMoneyToolStripMenuItem.Click += new System.EventHandler(this.SumOfMoneyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.nextToolStripMenuItem,
            this.lastToolStripMenuItem});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.controlToolStripMenuItem.Text = "control";
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.updateToolStripMenuItem.Text = "update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.saveToolStripMenuItem.Text = "save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // nextToolStripMenuItem
            // 
            this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
            this.nextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.nextToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.nextToolStripMenuItem.Text = "next";
            // 
            // lastToolStripMenuItem
            // 
            this.lastToolStripMenuItem.Name = "lastToolStripMenuItem";
            this.lastToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.lastToolStripMenuItem.Size = new System.Drawing.Size(201, 26);
            this.lastToolStripMenuItem.Text = "last";
            // 
            // textBoxpayment
            // 
            this.textBoxpayment.Location = new System.Drawing.Point(573, 66);
            this.textBoxpayment.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxpayment.Name = "textBoxpayment";
            this.textBoxpayment.Size = new System.Drawing.Size(132, 25);
            this.textBoxpayment.TabIndex = 7;
            // 
            // textBoxnote
            // 
            this.textBoxnote.Location = new System.Drawing.Point(715, 66);
            this.textBoxnote.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxnote.Name = "textBoxnote";
            this.textBoxnote.Size = new System.Drawing.Size(132, 25);
            this.textBoxnote.TabIndex = 8;
            // 
            // text_comsume
            // 
            this.text_comsume.Location = new System.Drawing.Point(413, 66);
            this.text_comsume.Margin = new System.Windows.Forms.Padding(4);
            this.text_comsume.Name = "text_comsume";
            this.text_comsume.Size = new System.Drawing.Size(132, 25);
            this.text_comsume.TabIndex = 6;
            // 
            // combox_item
            // 
            this.combox_item.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combox_item.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combox_item.FormattingEnabled = true;
            this.combox_item.Location = new System.Drawing.Point(321, 66);
            this.combox_item.Margin = new System.Windows.Forms.Padding(4);
            this.combox_item.Name = "combox_item";
            this.combox_item.Size = new System.Drawing.Size(83, 23);
            this.combox_item.TabIndex = 5;
            this.combox_item.SelectedIndexChanged += new System.EventHandler(this.combox_item_SelectedIndexChanged);
            this.combox_item.KeyDown += new System.Windows.Forms.KeyEventHandler(this.combox_enter_handler);
            // 
            // combox_category
            // 
            this.combox_category.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combox_category.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combox_category.FormattingEnabled = true;
            this.combox_category.Location = new System.Drawing.Point(224, 66);
            this.combox_category.Margin = new System.Windows.Forms.Padding(4);
            this.combox_category.Name = "combox_category";
            this.combox_category.Size = new System.Drawing.Size(88, 23);
            this.combox_category.TabIndex = 4;
            this.combox_category.SelectedIndexChanged += new System.EventHandler(this.combox_category_SelectedIndexChanged);
            this.combox_category.KeyDown += new System.Windows.Forms.KeyEventHandler(this.combox_enter_handler);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "category";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "item";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(571, 44);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "payment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(715, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "note";
            // 
            // buttonlastday
            // 
            this.buttonlastday.Location = new System.Drawing.Point(21, 100);
            this.buttonlastday.Margin = new System.Windows.Forms.Padding(4);
            this.buttonlastday.Name = "buttonlastday";
            this.buttonlastday.Size = new System.Drawing.Size(100, 29);
            this.buttonlastday.TabIndex = 17;
            this.buttonlastday.Text = "前一天";
            this.buttonlastday.UseVisualStyleBackColor = true;
            this.buttonlastday.Click += new System.EventHandler(this.buttonlastday_Click);
            // 
            // buttonnextday
            // 
            this.buttonnextday.Location = new System.Drawing.Point(129, 100);
            this.buttonnextday.Margin = new System.Windows.Forms.Padding(4);
            this.buttonnextday.Name = "buttonnextday";
            this.buttonnextday.Size = new System.Drawing.Size(100, 29);
            this.buttonnextday.TabIndex = 18;
            this.buttonnextday.Text = "后一天";
            this.buttonnextday.UseVisualStyleBackColor = true;
            this.buttonnextday.Click += new System.EventHandler(this.buttonnextday_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.others);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.campuscard);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cash);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.debitcard);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.wechat);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.alipay);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(22, 136);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1032, 45);
            this.panel1.TabIndex = 19;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(36, 37);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // others
            // 
            this.others.Location = new System.Drawing.Point(914, 17);
            this.others.Name = "others";
            this.others.Size = new System.Drawing.Size(100, 25);
            this.others.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(853, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 15);
            this.label10.TabIndex = 14;
            this.label10.Text = "others";
            // 
            // campuscard
            // 
            this.campuscard.Location = new System.Drawing.Point(752, 17);
            this.campuscard.Name = "campuscard";
            this.campuscard.Size = new System.Drawing.Size(100, 25);
            this.campuscard.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(691, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 15);
            this.label9.TabIndex = 12;
            this.label9.Text = "campus";
            // 
            // cash
            // 
            this.cash.Location = new System.Drawing.Point(583, 17);
            this.cash.Name = "cash";
            this.cash.Size = new System.Drawing.Size(100, 25);
            this.cash.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(522, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "cash";
            // 
            // debitcard
            // 
            this.debitcard.Location = new System.Drawing.Point(409, 17);
            this.debitcard.Name = "debitcard";
            this.debitcard.Size = new System.Drawing.Size(100, 25);
            this.debitcard.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(348, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "debit";
            // 
            // wechat
            // 
            this.wechat.Location = new System.Drawing.Point(238, 17);
            this.wechat.Name = "wechat";
            this.wechat.Size = new System.Drawing.Size(100, 25);
            this.wechat.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(177, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "wechat";
            // 
            // alipay
            // 
            this.alipay.Location = new System.Drawing.Point(69, 17);
            this.alipay.Name = "alipay";
            this.alipay.Size = new System.Drawing.Size(100, 25);
            this.alipay.TabIndex = 5;
            this.alipay.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "alipay";
            // 
            // buttonUpDate
            // 
            this.buttonUpDate.Location = new System.Drawing.Point(975, 64);
            this.buttonUpDate.Name = "buttonUpDate";
            this.buttonUpDate.Size = new System.Drawing.Size(90, 26);
            this.buttonUpDate.TabIndex = 10;
            this.buttonUpDate.Text = "保存";
            this.buttonUpDate.UseVisualStyleBackColor = true;
            this.buttonUpDate.Click += new System.EventHandler(this.buttonUpDate_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bodyweighttextbox);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(21, 184);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(493, 33);
            this.panel2.TabIndex = 21;
            // 
            // bodyweighttextbox
            // 
            this.bodyweighttextbox.Location = new System.Drawing.Point(81, 5);
            this.bodyweighttextbox.Name = "bodyweighttextbox";
            this.bodyweighttextbox.Size = new System.Drawing.Size(149, 25);
            this.bodyweighttextbox.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "体重";
            // 
            // FormAddData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 486);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonUpDate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonnextday);
            this.Controls.Add(this.buttonlastday);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_comsume);
            this.Controls.Add(this.combox_item);
            this.Controls.Add(this.combox_category);
            this.Controls.Add(this.textBoxnote);
            this.Controls.Add(this.textBoxpayment);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.butupdate);
            this.Controls.Add(this.combox_add_d);
            this.Controls.Add(this.combox_add_m);
            this.Controls.Add(this.combox_add_y);
            this.Controls.Add(this.LabTableName);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormAddData";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "0";
            this.Load += new System.EventHandler(this.FormAddData_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormAddData_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabTableName;
        private System.Windows.Forms.ComboBox combox_add_y;
        private System.Windows.Forms.ComboBox combox_add_m;
        private System.Windows.Forms.ComboBox combox_add_d;
        private System.Windows.Forms.Button butupdate;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tablesToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxpayment;
        private System.Windows.Forms.TextBox textBoxnote;
        private System.Windows.Forms.TextBox text_comsume;
        private System.Windows.Forms.ComboBox combox_item;
        private System.Windows.Forms.ComboBox combox_category;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonlastday;
        private System.Windows.Forms.Button buttonnextday;
        private System.Windows.Forms.ToolStripMenuItem SumOfMoneyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox campuscard;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox cash;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox debitcard;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox wechat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox alipay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox others;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonUpDate;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BodyWeight;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox bodyweighttextbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}