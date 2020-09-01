namespace calander
{
    partial class FormDebt
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.textboxmonth = new System.Windows.Forms.TextBox();
            this.textboxlastmonth = new System.Windows.Forms.TextBox();
            this.textboxbill = new System.Windows.Forms.TextBox();
            this.button_update = new System.Windows.Forms.Button();
            this.dataGridViewdebt = new System.Windows.Forms.DataGridView();
            this.textboxitem = new System.Windows.Forms.TextBox();
            this.chartdebt = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chartsum = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewdebt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartdebt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartsum)).BeginInit();
            this.SuspendLayout();
            // 
            // textboxmonth
            // 
            this.textboxmonth.Location = new System.Drawing.Point(98, 83);
            this.textboxmonth.Margin = new System.Windows.Forms.Padding(2);
            this.textboxmonth.Name = "textboxmonth";
            this.textboxmonth.Size = new System.Drawing.Size(76, 21);
            this.textboxmonth.TabIndex = 1;
            // 
            // textboxlastmonth
            // 
            this.textboxlastmonth.Location = new System.Drawing.Point(178, 83);
            this.textboxlastmonth.Margin = new System.Windows.Forms.Padding(2);
            this.textboxlastmonth.Name = "textboxlastmonth";
            this.textboxlastmonth.Size = new System.Drawing.Size(76, 21);
            this.textboxlastmonth.TabIndex = 2;
            // 
            // textboxbill
            // 
            this.textboxbill.Location = new System.Drawing.Point(257, 83);
            this.textboxbill.Margin = new System.Windows.Forms.Padding(2);
            this.textboxbill.Name = "textboxbill";
            this.textboxbill.Size = new System.Drawing.Size(76, 21);
            this.textboxbill.TabIndex = 3;
            // 
            // button_update
            // 
            this.button_update.Location = new System.Drawing.Point(337, 82);
            this.button_update.Margin = new System.Windows.Forms.Padding(2);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(70, 20);
            this.button_update.TabIndex = 3;
            this.button_update.Text = "更新";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // dataGridViewdebt
            // 
            this.dataGridViewdebt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewdebt.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewdebt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewdebt.Location = new System.Drawing.Point(19, 121);
            this.dataGridViewdebt.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewdebt.Name = "dataGridViewdebt";
            this.dataGridViewdebt.RowTemplate.Height = 27;
            this.dataGridViewdebt.Size = new System.Drawing.Size(394, 380);
            this.dataGridViewdebt.TabIndex = 4;
            // 
            // textboxitem
            // 
            this.textboxitem.Location = new System.Drawing.Point(19, 83);
            this.textboxitem.Margin = new System.Windows.Forms.Padding(2);
            this.textboxitem.Name = "textboxitem";
            this.textboxitem.Size = new System.Drawing.Size(76, 21);
            this.textboxitem.TabIndex = 0;
            // 
            // chartdebt
            // 
            chartArea1.Name = "ChartArea1";
            this.chartdebt.ChartAreas.Add(chartArea1);
            this.chartdebt.Dock = System.Windows.Forms.DockStyle.Left;
            legend1.Name = "Legend1";
            this.chartdebt.Legends.Add(legend1);
            this.chartdebt.Location = new System.Drawing.Point(0, 0);
            this.chartdebt.Margin = new System.Windows.Forms.Padding(2);
            this.chartdebt.Name = "chartdebt";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartdebt.Series.Add(series1);
            this.chartdebt.Size = new System.Drawing.Size(866, 500);
            this.chartdebt.TabIndex = 6;
            this.chartdebt.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "项目";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "开始时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "持续时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(255, 56);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "每笔金额";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitContainer1.Location = new System.Drawing.Point(417, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chartdebt);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chartsum);
            this.splitContainer1.Size = new System.Drawing.Size(666, 620);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 11;
            // 
            // chartsum
            // 
            chartArea2.Name = "ChartArea1";
            this.chartsum.ChartAreas.Add(chartArea2);
            this.chartsum.Dock = System.Windows.Forms.DockStyle.Left;
            legend2.Name = "Legend1";
            this.chartsum.Legends.Add(legend2);
            this.chartsum.Location = new System.Drawing.Point(0, 0);
            this.chartsum.Margin = new System.Windows.Forms.Padding(2);
            this.chartsum.Name = "chartsum";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartsum.Series.Add(series2);
            this.chartsum.Size = new System.Drawing.Size(866, 117);
            this.chartsum.TabIndex = 0;
            this.chartsum.Text = "chart1";
            // 
            // FormDebt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 620);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textboxitem);
            this.Controls.Add(this.dataGridViewdebt);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.textboxbill);
            this.Controls.Add(this.textboxlastmonth);
            this.Controls.Add(this.textboxmonth);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormDebt";
            this.Text = "FormDebt";
            this.Shown += new System.EventHandler(this.FormDebt_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewdebt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartdebt)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartsum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxmonth;
        private System.Windows.Forms.TextBox textboxlastmonth;
        private System.Windows.Forms.TextBox textboxbill;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.DataGridView dataGridViewdebt;
        private System.Windows.Forms.TextBox textboxitem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartdebt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartsum;
    }
}