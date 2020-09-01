namespace calander
{
    partial class FormTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTable));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ButtonUpdate = new System.Windows.Forms.Button();
            this.buttest = new System.Windows.Forms.Button();
            this.tabsum = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TableViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CategoryViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DateViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RankOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RankOrderAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RankOrderDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MonthViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CurrentMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LastMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NextMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AllMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SDateViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SMonthViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SSlectMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SCatViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SColumnViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SumOfMoneyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.swimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bodyweightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.历史ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SSelectMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SCurrentMonthToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.SLastMonthToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.SNextMonthToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.SAllMonthToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.SWholeMonthToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SEveryMonthToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.当前年ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.前一年ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.后一年ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自定义ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.labelsum = new System.Windows.Forms.Label();
            this.ChartSumDays = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartSplitContainer = new System.Windows.Forms.SplitContainer();
            this.labnum = new System.Windows.Forms.Label();
            this.LabelSSelectMonth = new System.Windows.Forms.Label();
            this.labelselect = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.numselect = new System.Windows.Forms.Label();
            this.buttonexport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabsum)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSumDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer)).BeginInit();
            this.ChartSplitContainer.Panel1.SuspendLayout();
            this.ChartSplitContainer.Panel2.SuspendLayout();
            this.ChartSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(974, 257);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_DataSourceChanged);
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            // 
            // ButtonUpdate
            // 
            this.ButtonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonUpdate.Location = new System.Drawing.Point(1165, 50);
            this.ButtonUpdate.Name = "ButtonUpdate";
            this.ButtonUpdate.Size = new System.Drawing.Size(75, 23);
            this.ButtonUpdate.TabIndex = 1;
            this.ButtonUpdate.Text = "更新";
            this.ButtonUpdate.UseVisualStyleBackColor = true;
            this.ButtonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // buttest
            // 
            this.buttest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttest.Location = new System.Drawing.Point(1164, 84);
            this.buttest.Name = "buttest";
            this.buttest.Size = new System.Drawing.Size(75, 23);
            this.buttest.TabIndex = 2;
            this.buttest.Text = "导出数据";
            this.buttest.UseVisualStyleBackColor = true;
            this.buttest.Click += new System.EventHandler(this.buttest_Click);
            // 
            // tabsum
            // 
            this.tabsum.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.tabsum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabsum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsum.Location = new System.Drawing.Point(0, 0);
            this.tabsum.Name = "tabsum";
            this.tabsum.RowTemplate.Height = 23;
            this.tabsum.Size = new System.Drawing.Size(66, 361);
            this.tabsum.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem,
            this.secondToolStripMenuItem,
            this.databseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1228, 25);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TableViewToolStripMenuItem,
            this.CategoryViewToolStripMenuItem,
            this.DateViewToolStripMenuItem,
            this.RankToolStripMenuItem,
            this.RankOrderToolStripMenuItem,
            this.MonthViewToolStripMenuItem});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.mainToolStripMenuItem.Text = "主窗格";
            // 
            // TableViewToolStripMenuItem
            // 
            this.TableViewToolStripMenuItem.Name = "TableViewToolStripMenuItem";
            this.TableViewToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.TableViewToolStripMenuItem.Text = "选择显示数据内容";
            // 
            // CategoryViewToolStripMenuItem
            // 
            this.CategoryViewToolStripMenuItem.Name = "CategoryViewToolStripMenuItem";
            this.CategoryViewToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.CategoryViewToolStripMenuItem.Text = "按类型显示";
            this.CategoryViewToolStripMenuItem.Click += new System.EventHandler(this.CategoryViewToolStripMenuItem_Click);
            // 
            // DateViewToolStripMenuItem
            // 
            this.DateViewToolStripMenuItem.Name = "DateViewToolStripMenuItem";
            this.DateViewToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.DateViewToolStripMenuItem.Text = "按日期显示";
            this.DateViewToolStripMenuItem.Click += new System.EventHandler(this.DateViewToolStripMenuItem_Click);
            // 
            // RankToolStripMenuItem
            // 
            this.RankToolStripMenuItem.Name = "RankToolStripMenuItem";
            this.RankToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.RankToolStripMenuItem.Text = "排序内容";
            // 
            // RankOrderToolStripMenuItem
            // 
            this.RankOrderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RankOrderAToolStripMenuItem,
            this.RankOrderDToolStripMenuItem});
            this.RankOrderToolStripMenuItem.Name = "RankOrderToolStripMenuItem";
            this.RankOrderToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.RankOrderToolStripMenuItem.Text = "排序方式";
            // 
            // RankOrderAToolStripMenuItem
            // 
            this.RankOrderAToolStripMenuItem.Checked = true;
            this.RankOrderAToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RankOrderAToolStripMenuItem.Name = "RankOrderAToolStripMenuItem";
            this.RankOrderAToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.RankOrderAToolStripMenuItem.Tag = "asc";
            this.RankOrderAToolStripMenuItem.Text = "升序";
            this.RankOrderAToolStripMenuItem.Click += new System.EventHandler(this.RankOrderClick);
            // 
            // RankOrderDToolStripMenuItem
            // 
            this.RankOrderDToolStripMenuItem.Name = "RankOrderDToolStripMenuItem";
            this.RankOrderDToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.RankOrderDToolStripMenuItem.Tag = "desc";
            this.RankOrderDToolStripMenuItem.Text = "降序";
            this.RankOrderDToolStripMenuItem.Click += new System.EventHandler(this.RankOrderClick);
            // 
            // MonthViewToolStripMenuItem
            // 
            this.MonthViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrentMonthToolStripMenuItem,
            this.LastMonthToolStripMenuItem,
            this.NextMonthToolStripMenuItem,
            this.AllMonthToolStripMenuItem});
            this.MonthViewToolStripMenuItem.Name = "MonthViewToolStripMenuItem";
            this.MonthViewToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.MonthViewToolStripMenuItem.Text = "按月显示";
            // 
            // CurrentMonthToolStripMenuItem
            // 
            this.CurrentMonthToolStripMenuItem.Name = "CurrentMonthToolStripMenuItem";
            this.CurrentMonthToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.CurrentMonthToolStripMenuItem.Tag = "0";
            this.CurrentMonthToolStripMenuItem.Text = "当前月";
            this.CurrentMonthToolStripMenuItem.Click += new System.EventHandler(this.MonthViewSelect);
            // 
            // LastMonthToolStripMenuItem
            // 
            this.LastMonthToolStripMenuItem.Name = "LastMonthToolStripMenuItem";
            this.LastMonthToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.LastMonthToolStripMenuItem.Tag = "-1";
            this.LastMonthToolStripMenuItem.Text = "前一月";
            this.LastMonthToolStripMenuItem.Click += new System.EventHandler(this.MonthViewSelect);
            // 
            // NextMonthToolStripMenuItem
            // 
            this.NextMonthToolStripMenuItem.Name = "NextMonthToolStripMenuItem";
            this.NextMonthToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.NextMonthToolStripMenuItem.Tag = "1";
            this.NextMonthToolStripMenuItem.Text = "后一月";
            this.NextMonthToolStripMenuItem.Click += new System.EventHandler(this.MonthViewSelect);
            // 
            // AllMonthToolStripMenuItem
            // 
            this.AllMonthToolStripMenuItem.Name = "AllMonthToolStripMenuItem";
            this.AllMonthToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.AllMonthToolStripMenuItem.Tag = "2";
            this.AllMonthToolStripMenuItem.Text = "所有";
            this.AllMonthToolStripMenuItem.Click += new System.EventHandler(this.MonthViewSelect);
            // 
            // secondToolStripMenuItem
            // 
            this.secondToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SDateViewToolStripMenuItem,
            this.SMonthViewToolStripMenuItem,
            this.SSlectMonthToolStripMenuItem,
            this.SCatViewToolStripMenuItem,
            this.SColumnViewToolStripMenuItem,
            this.SumOfMoneyToolStripMenuItem,
            this.swimToolStripMenuItem,
            this.bodyweightToolStripMenuItem,
            this.历史ToolStripMenuItem,
            this.SSelectMonthToolStripMenuItem});
            this.secondToolStripMenuItem.Name = "secondToolStripMenuItem";
            this.secondToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.secondToolStripMenuItem.Text = "副窗格";
            // 
            // SDateViewToolStripMenuItem
            // 
            this.SDateViewToolStripMenuItem.Name = "SDateViewToolStripMenuItem";
            this.SDateViewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.SDateViewToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SDateViewToolStripMenuItem.Tag = "day";
            this.SDateViewToolStripMenuItem.Text = "按日显示";
            this.SDateViewToolStripMenuItem.Click += new System.EventHandler(this.SecViewShow);
            // 
            // SMonthViewToolStripMenuItem
            // 
            this.SMonthViewToolStripMenuItem.Name = "SMonthViewToolStripMenuItem";
            this.SMonthViewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.SMonthViewToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SMonthViewToolStripMenuItem.Tag = "month";
            this.SMonthViewToolStripMenuItem.Text = "按月显示";
            this.SMonthViewToolStripMenuItem.Click += new System.EventHandler(this.SecViewShow);
            // 
            // SSlectMonthToolStripMenuItem
            // 
            this.SSlectMonthToolStripMenuItem.Name = "SSlectMonthToolStripMenuItem";
            this.SSlectMonthToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SSlectMonthToolStripMenuItem.Text = "选择日期";
            // 
            // SCatViewToolStripMenuItem
            // 
            this.SCatViewToolStripMenuItem.Name = "SCatViewToolStripMenuItem";
            this.SCatViewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.SCatViewToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SCatViewToolStripMenuItem.Tag = "pie";
            this.SCatViewToolStripMenuItem.Text = "饼状图";
            this.SCatViewToolStripMenuItem.Click += new System.EventHandler(this.SecViewShow);
            // 
            // SColumnViewToolStripMenuItem
            // 
            this.SColumnViewToolStripMenuItem.Name = "SColumnViewToolStripMenuItem";
            this.SColumnViewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.SColumnViewToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SColumnViewToolStripMenuItem.Tag = "column";
            this.SColumnViewToolStripMenuItem.Text = "柱状图";
            this.SColumnViewToolStripMenuItem.Click += new System.EventHandler(this.SecViewShow);
            // 
            // SumOfMoneyToolStripMenuItem
            // 
            this.SumOfMoneyToolStripMenuItem.Name = "SumOfMoneyToolStripMenuItem";
            this.SumOfMoneyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.SumOfMoneyToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SumOfMoneyToolStripMenuItem.Tag = "balance";
            this.SumOfMoneyToolStripMenuItem.Text = "余额";
            this.SumOfMoneyToolStripMenuItem.Click += new System.EventHandler(this.SecViewShow);
            // 
            // swimToolStripMenuItem
            // 
            this.swimToolStripMenuItem.Name = "swimToolStripMenuItem";
            this.swimToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.swimToolStripMenuItem.Tag = "swim";
            this.swimToolStripMenuItem.Text = "运动数据";
            this.swimToolStripMenuItem.Click += new System.EventHandler(this.SecViewShow);
            // 
            // bodyweightToolStripMenuItem
            // 
            this.bodyweightToolStripMenuItem.Name = "bodyweightToolStripMenuItem";
            this.bodyweightToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.bodyweightToolStripMenuItem.Tag = "weight";
            this.bodyweightToolStripMenuItem.Text = "体重数据";
            this.bodyweightToolStripMenuItem.Click += new System.EventHandler(this.SecViewShow);
            // 
            // 历史ToolStripMenuItem
            // 
            this.历史ToolStripMenuItem.Name = "历史ToolStripMenuItem";
            this.历史ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.历史ToolStripMenuItem.Tag = "His";
            this.历史ToolStripMenuItem.Text = "历史";
            this.历史ToolStripMenuItem.Click += new System.EventHandler(this.SecViewShow);
            // 
            // SSelectMonthToolStripMenuItem
            // 
            this.SSelectMonthToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SCurrentMonthToolStripMenuItem3,
            this.SLastMonthToolStripMenuItem3,
            this.SNextMonthToolStripMenuItem3,
            this.SAllMonthToolStripMenuItem2,
            this.SWholeMonthToolStripMenuItem1,
            this.SEveryMonthToolStripMenuItem1,
            this.当前年ToolStripMenuItem,
            this.前一年ToolStripMenuItem,
            this.后一年ToolStripMenuItem,
            this.自定义ToolStripMenuItem});
            this.SSelectMonthToolStripMenuItem.Name = "SSelectMonthToolStripMenuItem";
            this.SSelectMonthToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SSelectMonthToolStripMenuItem.Text = "选择月份";
            // 
            // SCurrentMonthToolStripMenuItem3
            // 
            this.SCurrentMonthToolStripMenuItem3.Name = "SCurrentMonthToolStripMenuItem3";
            this.SCurrentMonthToolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.SCurrentMonthToolStripMenuItem3.Size = new System.Drawing.Size(213, 22);
            this.SCurrentMonthToolStripMenuItem3.Tag = "Current";
            this.SCurrentMonthToolStripMenuItem3.Text = "当前月";
            this.SCurrentMonthToolStripMenuItem3.Click += new System.EventHandler(this.SecViewSelectMonth);
            // 
            // SLastMonthToolStripMenuItem3
            // 
            this.SLastMonthToolStripMenuItem3.Name = "SLastMonthToolStripMenuItem3";
            this.SLastMonthToolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Left)));
            this.SLastMonthToolStripMenuItem3.Size = new System.Drawing.Size(213, 22);
            this.SLastMonthToolStripMenuItem3.Tag = "Last";
            this.SLastMonthToolStripMenuItem3.Text = "前一月";
            this.SLastMonthToolStripMenuItem3.Click += new System.EventHandler(this.SecViewSelectMonth);
            // 
            // SNextMonthToolStripMenuItem3
            // 
            this.SNextMonthToolStripMenuItem3.Name = "SNextMonthToolStripMenuItem3";
            this.SNextMonthToolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Right)));
            this.SNextMonthToolStripMenuItem3.Size = new System.Drawing.Size(213, 22);
            this.SNextMonthToolStripMenuItem3.Tag = "Add";
            this.SNextMonthToolStripMenuItem3.Text = "后一月";
            this.SNextMonthToolStripMenuItem3.Click += new System.EventHandler(this.SecViewSelectMonth);
            // 
            // SAllMonthToolStripMenuItem2
            // 
            this.SAllMonthToolStripMenuItem2.Name = "SAllMonthToolStripMenuItem2";
            this.SAllMonthToolStripMenuItem2.Size = new System.Drawing.Size(213, 22);
            this.SAllMonthToolStripMenuItem2.Tag = "All";
            this.SAllMonthToolStripMenuItem2.Text = "所有";
            this.SAllMonthToolStripMenuItem2.Click += new System.EventHandler(this.SecViewSelectMonth);
            // 
            // SWholeMonthToolStripMenuItem1
            // 
            this.SWholeMonthToolStripMenuItem1.Name = "SWholeMonthToolStripMenuItem1";
            this.SWholeMonthToolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
            this.SWholeMonthToolStripMenuItem1.Tag = "Minus";
            this.SWholeMonthToolStripMenuItem1.Text = "最近一月";
            this.SWholeMonthToolStripMenuItem1.Click += new System.EventHandler(this.SecViewSelectMonth);
            // 
            // SEveryMonthToolStripMenuItem1
            // 
            this.SEveryMonthToolStripMenuItem1.Name = "SEveryMonthToolStripMenuItem1";
            this.SEveryMonthToolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
            this.SEveryMonthToolStripMenuItem1.Tag = "Every";
            this.SEveryMonthToolStripMenuItem1.Text = "EveryMonth";
            this.SEveryMonthToolStripMenuItem1.Click += new System.EventHandler(this.SecViewSelectMonth);
            // 
            // 当前年ToolStripMenuItem
            // 
            this.当前年ToolStripMenuItem.Name = "当前年ToolStripMenuItem";
            this.当前年ToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.当前年ToolStripMenuItem.Tag = "Currentyear";
            this.当前年ToolStripMenuItem.Text = "今年";
            this.当前年ToolStripMenuItem.Click += new System.EventHandler(this.SecViewSelectMonth);
            // 
            // 前一年ToolStripMenuItem
            // 
            this.前一年ToolStripMenuItem.Name = "前一年ToolStripMenuItem";
            this.前一年ToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.前一年ToolStripMenuItem.Tag = "Lastyear";
            this.前一年ToolStripMenuItem.Text = "上一年";
            this.前一年ToolStripMenuItem.Click += new System.EventHandler(this.SecViewSelectMonth);
            // 
            // 后一年ToolStripMenuItem
            // 
            this.后一年ToolStripMenuItem.Name = "后一年ToolStripMenuItem";
            this.后一年ToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.后一年ToolStripMenuItem.Tag = "Nextyear";
            this.后一年ToolStripMenuItem.Text = "下一年";
            this.后一年ToolStripMenuItem.Click += new System.EventHandler(this.SecViewSelectMonth);
            // 
            // 自定义ToolStripMenuItem
            // 
            this.自定义ToolStripMenuItem.Name = "自定义ToolStripMenuItem";
            this.自定义ToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.自定义ToolStripMenuItem.Tag = "Customlize";
            this.自定义ToolStripMenuItem.Text = "自定义";
            this.自定义ToolStripMenuItem.Click += new System.EventHandler(this.SecViewSelectMonth);
            // 
            // databseToolStripMenuItem
            // 
            this.databseToolStripMenuItem.Name = "databseToolStripMenuItem";
            this.databseToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.databseToolStripMenuItem.Text = "修改数据";
            this.databseToolStripMenuItem.Click += new System.EventHandler(this.databseToolStripMenuItem_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 23);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(126, 21);
            this.dateTimePicker1.TabIndex = 11;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(167, 23);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(125, 21);
            this.dateTimePicker2.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "至";
            // 
            // labelsum
            // 
            this.labelsum.AutoSize = true;
            this.labelsum.Location = new System.Drawing.Point(810, 29);
            this.labelsum.Name = "labelsum";
            this.labelsum.Size = new System.Drawing.Size(53, 12);
            this.labelsum.TabIndex = 14;
            this.labelsum.Text = "当前总和";
            // 
            // ChartSumDays
            // 
            chartArea1.Name = "ChartArea1";
            this.ChartSumDays.ChartAreas.Add(chartArea1);
            this.ChartSumDays.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend1.Name = "Legend1";
            this.ChartSumDays.Legends.Add(legend1);
            this.ChartSumDays.Location = new System.Drawing.Point(0, 2);
            this.ChartSumDays.Margin = new System.Windows.Forms.Padding(2);
            this.ChartSumDays.Name = "ChartSumDays";
            this.ChartSumDays.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ChartSumDays.Series.Add(series1);
            this.ChartSumDays.Size = new System.Drawing.Size(1159, 359);
            this.ChartSumDays.TabIndex = 15;
            this.ChartSumDays.Text = "chart1";
            // 
            // ChartSplitContainer
            // 
            this.ChartSplitContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChartSplitContainer.Location = new System.Drawing.Point(0, 321);
            this.ChartSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.ChartSplitContainer.Name = "ChartSplitContainer";
            // 
            // ChartSplitContainer.Panel1
            // 
            this.ChartSplitContainer.Panel1.Controls.Add(this.tabsum);
            // 
            // ChartSplitContainer.Panel2
            // 
            this.ChartSplitContainer.Panel2.Controls.Add(this.ChartSumDays);
            this.ChartSplitContainer.Size = new System.Drawing.Size(1228, 361);
            this.ChartSplitContainer.SplitterDistance = 66;
            this.ChartSplitContainer.SplitterWidth = 3;
            this.ChartSplitContainer.TabIndex = 16;
            // 
            // labnum
            // 
            this.labnum.AutoSize = true;
            this.labnum.Location = new System.Drawing.Point(709, 29);
            this.labnum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labnum.Name = "labnum";
            this.labnum.Size = new System.Drawing.Size(53, 12);
            this.labnum.TabIndex = 17;
            this.labnum.Text = "记录数目";
            // 
            // LabelSSelectMonth
            // 
            this.LabelSSelectMonth.AutoSize = true;
            this.LabelSSelectMonth.Location = new System.Drawing.Point(1069, 294);
            this.LabelSSelectMonth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelSSelectMonth.Name = "LabelSSelectMonth";
            this.LabelSSelectMonth.Size = new System.Drawing.Size(107, 12);
            this.LabelSSelectMonth.TabIndex = 18;
            this.LabelSSelectMonth.Text = "LabelSSelectMonth";
            // 
            // labelselect
            // 
            this.labelselect.AutoSize = true;
            this.labelselect.Location = new System.Drawing.Point(626, 29);
            this.labelselect.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelselect.Name = "labelselect";
            this.labelselect.Size = new System.Drawing.Size(41, 12);
            this.labelselect.TabIndex = 19;
            this.labelselect.Text = "label2";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(996, 110);
            this.monthCalendar1.Margin = new System.Windows.Forms.Padding(7);
            this.monthCalendar1.MaxSelectionCount = 500;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 20;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // numselect
            // 
            this.numselect.AutoSize = true;
            this.numselect.Location = new System.Drawing.Point(560, 29);
            this.numselect.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.numselect.Name = "numselect";
            this.numselect.Size = new System.Drawing.Size(41, 12);
            this.numselect.TabIndex = 21;
            this.numselect.Text = "label2";
            // 
            // buttonexport
            // 
            this.buttonexport.Location = new System.Drawing.Point(1088, 85);
            this.buttonexport.Margin = new System.Windows.Forms.Padding(2);
            this.buttonexport.Name = "buttonexport";
            this.buttonexport.Size = new System.Drawing.Size(70, 22);
            this.buttonexport.TabIndex = 22;
            this.buttonexport.Text = "export";
            this.buttonexport.UseVisualStyleBackColor = true;
            this.buttonexport.Click += new System.EventHandler(this.buttonexport_Click);
            // 
            // FormTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 682);
            this.Controls.Add(this.buttonexport);
            this.Controls.Add(this.numselect);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.labelselect);
            this.Controls.Add(this.LabelSSelectMonth);
            this.Controls.Add(this.labnum);
            this.Controls.Add(this.ChartSplitContainer);
            this.Controls.Add(this.labelsum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.buttest);
            this.Controls.Add(this.ButtonUpdate);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTable";
            this.Text = "FormTable";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTable_FormClosing);
            this.Shown += new System.EventHandler(this.FormTable_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabsum)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSumDays)).EndInit();
            this.ChartSplitContainer.Panel1.ResumeLayout(false);
            this.ChartSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartSplitContainer)).EndInit();
            this.ChartSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttest;
        public System.Windows.Forms.DataGridView tabsum;
        public System.Windows.Forms.Button ButtonUpdate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TableViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CategoryViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DateViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SMonthViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SDateViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SSelectMonthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SSlectMonthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MonthViewToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem RankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RankOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RankOrderAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RankOrderDToolStripMenuItem;
        private System.Windows.Forms.Label labelsum;
        private System.Windows.Forms.ToolStripMenuItem CurrentMonthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LastMonthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NextMonthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AllMonthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SCatViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SColumnViewToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartSumDays;
        private System.Windows.Forms.SplitContainer ChartSplitContainer;
        private System.Windows.Forms.ToolStripMenuItem SumOfMoneyToolStripMenuItem;
        private System.Windows.Forms.Label labnum;
        private System.Windows.Forms.ToolStripMenuItem SCurrentMonthToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem SLastMonthToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem SNextMonthToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem SAllMonthToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem SWholeMonthToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SEveryMonthToolStripMenuItem1;
        private System.Windows.Forms.Label LabelSSelectMonth;
        private System.Windows.Forms.Label labelselect;
        private System.Windows.Forms.ToolStripMenuItem 当前年ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 前一年ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 后一年ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自定义ToolStripMenuItem;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label numselect;
        private System.Windows.Forms.Button buttonexport;
        private System.Windows.Forms.ToolStripMenuItem swimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bodyweightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 历史ToolStripMenuItem;
    }
} 