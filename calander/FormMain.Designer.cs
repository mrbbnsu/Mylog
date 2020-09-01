namespace calander
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.butnextmonth = new System.Windows.Forms.Button();
            this.butlastmonth = new System.Windows.Forms.Button();
            this.butnextyear = new System.Windows.Forms.Button();
            this.butlastyear = new System.Windows.Forms.Button();
            this.LabelDate = new System.Windows.Forms.Label();
            this.cbselectyear = new System.Windows.Forms.ComboBox();
            this.cbselectmonth = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.butcolor = new System.Windows.Forms.Button();
            this.comboxtables = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.butconnecttomysql = new System.Windows.Forms.Button();
            this.labconstate = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonShowDebt = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 450);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(670, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Show";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // butnextmonth
            // 
            this.butnextmonth.BackColor = System.Drawing.Color.Transparent;
            this.butnextmonth.Location = new System.Drawing.Point(670, 332);
            this.butnextmonth.Name = "butnextmonth";
            this.butnextmonth.Size = new System.Drawing.Size(75, 23);
            this.butnextmonth.TabIndex = 5;
            this.butnextmonth.Text = "nextmonth";
            this.butnextmonth.UseVisualStyleBackColor = false;
            this.butnextmonth.Click += new System.EventHandler(this.butnextmonth_Click);
            // 
            // butlastmonth
            // 
            this.butlastmonth.BackColor = System.Drawing.Color.Transparent;
            this.butlastmonth.Location = new System.Drawing.Point(758, 332);
            this.butlastmonth.Name = "butlastmonth";
            this.butlastmonth.Size = new System.Drawing.Size(75, 23);
            this.butlastmonth.TabIndex = 6;
            this.butlastmonth.Text = "lastmonth";
            this.butlastmonth.UseVisualStyleBackColor = false;
            this.butlastmonth.Click += new System.EventHandler(this.butlastmonth_Click);
            // 
            // butnextyear
            // 
            this.butnextyear.Location = new System.Drawing.Point(606, 433);
            this.butnextyear.Name = "butnextyear";
            this.butnextyear.Size = new System.Drawing.Size(75, 23);
            this.butnextyear.TabIndex = 7;
            this.butnextyear.Text = "nextyear";
            this.butnextyear.UseVisualStyleBackColor = true;
            this.butnextyear.Visible = false;
            this.butnextyear.Click += new System.EventHandler(this.butnextyear_Click);
            // 
            // butlastyear
            // 
            this.butlastyear.Location = new System.Drawing.Point(606, 476);
            this.butlastyear.Name = "butlastyear";
            this.butlastyear.Size = new System.Drawing.Size(75, 23);
            this.butlastyear.TabIndex = 8;
            this.butlastyear.Text = "lastyear";
            this.butlastyear.UseVisualStyleBackColor = true;
            this.butlastyear.Visible = false;
            this.butlastyear.Click += new System.EventHandler(this.butlastyear_Click);
            // 
            // LabelDate
            // 
            this.LabelDate.AutoSize = true;
            this.LabelDate.BackColor = System.Drawing.Color.Transparent;
            this.LabelDate.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelDate.Location = new System.Drawing.Point(12, 9);
            this.LabelDate.Name = "LabelDate";
            this.LabelDate.Size = new System.Drawing.Size(47, 19);
            this.LabelDate.TabIndex = 9;
            this.LabelDate.Text = "日期";
            // 
            // cbselectyear
            // 
            this.cbselectyear.FormattingEnabled = true;
            this.cbselectyear.Location = new System.Drawing.Point(698, 307);
            this.cbselectyear.Name = "cbselectyear";
            this.cbselectyear.Size = new System.Drawing.Size(48, 20);
            this.cbselectyear.TabIndex = 12;
            this.cbselectyear.SelectedIndexChanged += new System.EventHandler(this.cbselectyear_SelectedIndexChanged);
            // 
            // cbselectmonth
            // 
            this.cbselectmonth.FormattingEnabled = true;
            this.cbselectmonth.Location = new System.Drawing.Point(758, 307);
            this.cbselectmonth.Name = "cbselectmonth";
            this.cbselectmonth.Size = new System.Drawing.Size(44, 20);
            this.cbselectmonth.TabIndex = 13;
            this.cbselectmonth.SelectedIndexChanged += new System.EventHandler(this.cbselectmonth_SelectedIndexChanged);
            // 
            // butcolor
            // 
            this.butcolor.Location = new System.Drawing.Point(165, 20);
            this.butcolor.Name = "butcolor";
            this.butcolor.Size = new System.Drawing.Size(88, 23);
            this.butcolor.TabIndex = 15;
            this.butcolor.Text = "更改颜色";
            this.butcolor.UseVisualStyleBackColor = true;
            this.butcolor.Click += new System.EventHandler(this.butcolor_Click);
            // 
            // comboxtables
            // 
            this.comboxtables.FormattingEnabled = true;
            this.comboxtables.Location = new System.Drawing.Point(39, 20);
            this.comboxtables.Name = "comboxtables";
            this.comboxtables.Size = new System.Drawing.Size(121, 20);
            this.comboxtables.TabIndex = 16;
            this.comboxtables.SelectedIndexChanged += new System.EventHandler(this.comboxtables_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "view";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(606, 505);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "disconnect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // butconnecttomysql
            // 
            this.butconnecttomysql.Location = new System.Drawing.Point(606, 403);
            this.butconnecttomysql.Name = "butconnecttomysql";
            this.butconnecttomysql.Size = new System.Drawing.Size(75, 23);
            this.butconnecttomysql.TabIndex = 10;
            this.butconnecttomysql.Text = "connect";
            this.butconnecttomysql.UseVisualStyleBackColor = true;
            this.butconnecttomysql.Visible = false;
            this.butconnecttomysql.Click += new System.EventHandler(this.butconnecttomysql_Click);
            // 
            // labconstate
            // 
            this.labconstate.AutoSize = true;
            this.labconstate.Location = new System.Drawing.Point(606, 388);
            this.labconstate.Name = "labconstate";
            this.labconstate.Size = new System.Drawing.Size(41, 12);
            this.labconstate.TabIndex = 11;
            this.labconstate.Text = "label1";
            this.labconstate.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.butcolor);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.comboxtables);
            this.panel2.Location = new System.Drawing.Point(288, 505);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(262, 60);
            this.panel2.TabIndex = 18;
            this.panel2.Visible = false;
            // 
            // buttonShowDebt
            // 
            this.buttonShowDebt.Location = new System.Drawing.Point(758, 372);
            this.buttonShowDebt.Margin = new System.Windows.Forms.Padding(2);
            this.buttonShowDebt.Name = "buttonShowDebt";
            this.buttonShowDebt.Size = new System.Drawing.Size(75, 28);
            this.buttonShowDebt.TabIndex = 19;
            this.buttonShowDebt.Text = "debt";
            this.buttonShowDebt.UseVisualStyleBackColor = true;
            this.buttonShowDebt.Click += new System.EventHandler(this.buttonShowDebt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(839, 521);
            this.Controls.Add(this.buttonShowDebt);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cbselectmonth);
            this.Controls.Add(this.cbselectyear);
            this.Controls.Add(this.labconstate);
            this.Controls.Add(this.butconnecttomysql);
            this.Controls.Add(this.LabelDate);
            this.Controls.Add(this.butlastyear);
            this.Controls.Add(this.butnextyear);
            this.Controls.Add(this.butlastmonth);
            this.Controls.Add(this.butnextmonth);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "MyLog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button butnextmonth;
        private System.Windows.Forms.Button butlastmonth;
        private System.Windows.Forms.Button butnextyear;
        private System.Windows.Forms.Button butlastyear;
        private System.Windows.Forms.Label LabelDate;
        private System.Windows.Forms.ComboBox cbselectyear;
        private System.Windows.Forms.ComboBox cbselectmonth;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button butcolor;
        private System.Windows.Forms.ComboBox comboxtables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button butconnecttomysql;
        private System.Windows.Forms.Label labconstate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonShowDebt;
    }
}

