using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace calander
{
    public partial class FormTable : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        /// <summary>
        /// 显示表格下拉菜单选项
        /// </summary>
        private List<ToolStripMenuItem> TableList = new List<ToolStripMenuItem>();
        /// <summary>
        /// 按类型显示下拉菜单选项
        /// </summary>
        private List<ToolStripMenuItem> CategoryList = new List<ToolStripMenuItem>();
        /// <summary>
        /// 排序内容下拉菜单选项
        /// </summary>
        private List<ToolStripMenuItem> RankList = new List<ToolStripMenuItem>();
        string SelectMainTable = "";//选择的表，添加的数据到这里
        DataTable MainTable = new DataTable();
        /// <summary>
        /// 特定类型的表
        /// </summary>
        DataTable CatTable = new DataTable();
        DataTable Alltable = new DataTable();
        /// <summary>
        /// 默认的副窗格显示时间
        /// </summary>
        SMonthAdd ViewMonth = SMonthAdd.Current;
        SMonthAdd LastViewMonth = SMonthAdd.Current;
        /// <summary>
        /// 默认副窗格显示类型
        /// </summary>
        SViewType ViewType = SViewType.day;
        ToolStripMenuItem LastView = new ToolStripMenuItem();
        string RankOrder = "DESC";
        /// <summary>
        /// 选择的日期，datetimepicker1对应的日期
        /// </summary>
       public  DateTime SelectDate = new DateTime();
        /// <summary>
        /// 选择的日期对应的月份
        /// </summary>
       public DateTime SelectMonth = new DateTime();
       private DateTime LastDateTime1 = new DateTime();
       private DateTime LastDateTime2 = new DateTime();
       private DateTime SSelectMonth = new DateTime();
        private Dictionary<string, string> Dic = new Dictionary<string, string>();
        #region 
        //Form相关
        public FormTable()//构造函数
        {
            InitializeComponent();
        }
        private void FormTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }
        private void FormTable_Shown(object sender, EventArgs e)
        {
            Myinit();
        }      
        private void Myinit()
        {
            //获取字典
            Dic = (this.Owner as Form1).EngToChi;
            StringTrans.Dic = Dic;
            var obj = (this.Owner as Form1);
            //获得总表
            DataTable alltab = obj.myConnection.ShowDayData(obj.AllTables);
            //获取时间
            SelectDate = DateTime.Today;
            SelectMonth = SelectDate.ThisMonth();
            dateTimePicker1.Value = SelectMonth;
            dateTimePicker2.Value = SelectMonth.AddMonths(1);
            //为每个表添加对应的下拉选项--ItemClick
            obj.AllTables.ForEach(i => TableViewToolStripMenuItem.AddItem(TableList, i.tablenames, ItemClick));
            obj.AllTables.ForEach(i => SMonthViewToolStripMenuItem.AddItem(TableList, i.tablenames, SecViewShow));
            //为统计表添加对应的下拉选项
            TableViewToolStripMenuItem.AddItem(TableList, alltab.TableName, ItemClick,false);
            //转到ItemClick          
            // TableList.Find(i => i.Name.Contains("mycomsum")).PerformClick();
            //获取mycomsumption中本月的内容
            var temp = obj.AllTables.Find(i => i.tablenames.Contains("mycom")).table.Select("datadate >= '" + DateTime.Today.ThisMonth() + "'");
            var temptest = obj.CNACom.table.Select("datadate >= '" + DateTime.Today.ThisMonth() + "'");
            var tempdt = obj.AllTables.Find(i => i.tablenames.Contains("mycom")).table.Clone();
            foreach (var item in temp)
            {
                tempdt.ImportRow(item);
            }
            tempdt.Columns.Remove("datacount");
            tempdt.DefaultView.Sort = "datadate desc";
            tempdt = tempdt.DefaultView.ToTable();
            dataGridView1.DataSource = tempdt.Translate();
            if(DateTime.Today.Day==1)
            {
                SDateViewToolStripMenuItem.Checked = true;
                var temptab = SSelectTable(SMonthAdd.Minus);
                ViewMonth = SMonthAdd.Minus;
                SDateViewToolStripMenuItem.PerformClick();
                tabsum.DataSource = temptab.Translate();
            }
            else
            {
                SDateViewToolStripMenuItem.Checked = true;
                //SDateViewToolStripMenuItem.PerformClick();
                var temptab = SSelectTable(SMonthAdd.Current);
                SDateViewToolStripMenuItem.PerformClick();
                tabsum.DataSource = temptab.Translate();
            }
            
            
        }
        #endregion

        /// <summary>
        /// 主窗格-选择显示数据内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemClick(object sender, EventArgs e)
        {
            var obj = this.Owner as Form1;
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            //勾选相应选项
            TableList.ForEach(i => i.Checked = false);
            menu.Checked = true;
            //获取表的名字
            SelectMainTable = menu.Name;
            var list = (this.Owner as Form1).AllTables;
            //更新显示内容
            if (SelectMainTable == "AllTables")
            {
                var temp = obj.myConnection.ShowDayData(list).Copy();
                temp.DefaultView.Sort = "datadate DESC";
                temp = temp.DefaultView.ToTable();
                if (temp.Columns.Contains("datacount"))
                    temp.Columns.Remove("datacount");
                MainTable = temp;
                //dataGridView1.DataSource = MainTable.Translate();
                Alltable = MainTable;
            }
            else
            {
                var cna = list.Find(i => { return i.tablenames == SelectMainTable; });
                var temp = cna.table.Copy();
                temp.Columns.Remove("datacount");
                
                MainTable = temp;
                MainTable.DefaultView.Sort = "datadate DESC";
                MainTable = MainTable.DefaultView.ToTable();
               // dataGridView1.DataSource = MainTable.Translate();

            }
            //选择表格中含有的类型
            if (MainTable.Columns.Contains("category"))
            {
                CategoryViewToolStripMenuItem.DropDownItems.Clear();
                CategoryList.Clear();
                CategoryViewToolStripMenuItem.Enabled = true;
                //对类型按照数据数目排序
                var mostcategoty = (from row
                                    in MainTable.AsEnumerable()
                                    where row.Field<DateTime>("datadate")>=SelectDate.ThisMonth()
                                    group row by new { t1 = row.Field<string>("category") }
                                        into m
                                    select new { category = m.Key.t1, count = m.Count() }).OrderByDescending(i => i.count).ToList();

                // mostcategoty.ForEach(i => { Console.WriteLine(i.category + "   " + i.count); });
                mostcategoty.ForEach(i => { if (i.count > 5) CategoryViewToolStripMenuItem.AddItem(CategoryList, i.category, CatItemClick); });
                //foreach (var cat in mostcategoty)
                //{
                //    if (cat.count >= 5)
                //    {
                //        ToolStripMenuItem newitem = new ToolStripMenuItem();
                //        newitem.Name = cat.category;
                //        newitem.Text = cat.category.Translate();
                //        newitem.Click += CatItemClick;
                //        CategoryList.Add(newitem);
                //        CategoryViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newitem });
                //    }
                //}
                CategoryViewToolStripMenuItem.AddItem(CategoryList, "AllCategories", CatItemClick, true);
                //ToolStripMenuItem allcat = new ToolStripMenuItem();
                //allcat.Text = "所有类型";
                //allcat.Name = "AllCategories";
                //allcat.Click += CatItemClick;
                //CategoryList.Add(allcat);
                //CategoryViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { allcat });
                //allcat.Checked = true;
                //allcat.PerformClick();
            }
            else//如果不包含类型，直接显示表
            {
                dataGridView1.DataSource = MainTable.Translate();
                CategoryViewToolStripMenuItem.Enabled = false;
            }
        }
        /// <summary>
        /// 主窗格-按类型显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //按类型显示，默认选择类型最多的
            if (CategoryList.Count > 1)
                CategoryList[0].PerformClick();
        }
        /// <summary>
        /// 主窗格-按类型显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CatItemClick(object sender, EventArgs e)
        {

            //选择对应的类型显示
            var obj = this.Owner as Form1;
            ToolStripMenuItem category = sender as ToolStripMenuItem;
            CategoryList.ForEach(i => { i.Checked = false; });
            category.Checked = true;
            if (category.Name == "AllCategories")
            {
                var tab = from row
                      in MainTable.AsEnumerable()
                          select row;
                var temp = MainTable.Clone();
                temp.TableName = category.Text;
                foreach (var item in tab)
                {
                    temp.ImportRow(item);
                }
                if (temp.Columns.Contains("datacount"))
                    temp.Columns.Remove("datacount");
                CatTable = temp;
                //dataGridView1.DataSource = CatTable.Translate();
            }
            else
            {
                var tab = from row
                      in MainTable.AsEnumerable()
                          where Enumerable.SequenceEqual(Encoding.UTF8.GetBytes(row.Field<string>("category")), Encoding.UTF8.GetBytes(category.Text))
                          select row;
                var temp = MainTable.Clone();
                temp.TableName = category.Text;
                foreach (var item in tab)
                {
                    temp.ImportRow(item);
                }
                CatTable = temp;
                if (temp.Columns.Contains("datacount"))
                    temp.Columns.Remove("datacount");
                //dataGridView1.DataSource = CatTable.Translate();
            }
            //提取所有行，加入Rank
            RankList.Clear();
            RankToolStripMenuItem.DropDownItems.Clear();
            foreach (DataColumn column in CatTable.Columns)
            {
                RankToolStripMenuItem.AddItem(RankList, column.ColumnName,RankItemClick,column.ColumnName.Contains("datad"));
            }
            //默认排序为日期升序
            //提取最大最小日期
            string name = "";
            //获取列名，数据类型是datetime
            foreach (DataColumn column in CatTable.Columns)
            {
                if (column.DataType == typeof(DateTime))
                {
                    name = column.ColumnName;
                    break;
                }
            }
            var datelist = (from row
                       in CatTable.AsEnumerable()
                            select row.Field<DateTime>(name)).ToList();
            ShowDateView();
        }
        /// <summary>
        /// 主窗格-排序内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RankItemClick(object sender, EventArgs e)
        {
            var obj = this.Owner as Form1;
            ToolStripMenuItem Rank = sender as ToolStripMenuItem;
            RankList.ForEach(i => { i.Checked = false; });
            Rank.Checked = true;
            //Rank代表列名
            CatTable.DefaultView.Sort = Rank.Name + " " + RankOrder;
            CatTable = CatTable.DefaultView.ToTable();
            dataGridView1.DataSource = CatTable.Translate();
        }
        /// <summary>
        /// 主窗格-排序方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RankOrderClick(object sender, EventArgs e)
        {
            var obj = sender as ToolStripMenuItem;
            var objowner = obj.OwnerItem as ToolStripMenuItem;

            RankOrder = obj.Tag.ToString();
            foreach (ToolStripMenuItem item in objowner.DropDownItems)
            {
                if (item.Equals(obj))
                {
                    // item.PerformClick();
                    item.Checked = true;

                }
                else
                {
                    item.Checked = false;
                }
            }
            //执行上一次排序
            RankList.Find(i => i.Checked).PerformClick();
        }
        /// <summary>
        /// 主窗格-按日期显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDateView();
        }
        /// <summary>
        /// 显示选中时间段内数据
        /// </summary>
        private void ShowDateView()
        {          
            string name = "";
            var table = CatTable;
            DateTime datetime1 = dateTimePicker1.Value;
            DateTime datetime2 = dateTimePicker2.Value;
            foreach (DataColumn column in table.Columns)
            {
                if (column.DataType == typeof(DateTime))
                {
                    name = column.ColumnName;
                    break;
                }
            }
            var dateview = from row
                           in CatTable.AsEnumerable()
                           where row.Field<DateTime>(name) >= datetime1 && row.Field<DateTime>(name) <= datetime2
                           select row;
            var temp = table.Clone();
            dateview.ToList().ForEach(i => { temp.ImportRow(i); });
            if (temp.Columns.Contains("datacount"))
                temp.Columns.Remove("datacount");
            dataGridView1.DataSource = temp.Translate();            
        }
        /// <summary>
        /// 主窗格数据改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            Console.WriteLine("data change");
            var dt = dataGridView1.DataSource as DataTable;
            string columnname="";
            if (dt.Columns.Contains("expense"))
            {
                columnname = "expense";
            }
            else if (dt.Columns.Contains("expense".Translate()))
            {
                columnname = "expense".Translate();
            }
           
            if (columnname!="")
            {
                
                
                try
                {
                    if (dt.Columns[columnname].DataType != typeof(double))
                    {
                        var sum = dt.AsEnumerable().Sum(i => { if (i[columnname].GetType() != typeof(System.DBNull)) return double.Parse(i[columnname] as string); else return 0; });
                        labelsum.Text = "当前总和：" + sum;
                    }
                    else
                        labelsum.Text = "当前总和：" + dt.AsEnumerable().Sum(i => i.Field<double>(columnname));

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            }
            else
                labelsum.Text = "当前总和：";
            labnum.Text = "记录数目:" + dt.Rows.Count.ToString();

            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.Columns[e.ColumnIndex].SortMode == DataGridViewColumnSortMode.Programmatic)
            {
                string columnBindingName = dgv.Columns[e.ColumnIndex].DataPropertyName;
                switch (dgv.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection)
                {
                    case System.Windows.Forms.SortOrder.None:
                    case System.Windows.Forms.SortOrder.Ascending:
                        CustomSort(columnBindingName, "desc");
                        dgv.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                        break;
                    case System.Windows.Forms.SortOrder.Descending:
                        CustomSort(columnBindingName, "asc");
                        dgv.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                        break;
                }
            }
        }
        private static int sortfunc(DataRow p1,DataRow p2)
        {
            return 0;
        }
        Func<DataRow,string,decimal> comparer = (DataRow p1,string columnname) => 
        {
            decimal k;
            if (!decimal.TryParse(p1[columnname].ToString(),out k)) return 0;
            return Convert.ToDecimal( p1[columnname]);
        };
        
        private void CustomSort(string columnBindingName, string sortMode)
        {
            DataTable dt = this.dataGridView1.DataSource as DataTable;
            DataView dv = dt.DefaultView;
            List < int > list= new List<int>();
            
            var columnname = columnBindingName;
            if (columnname.Equals("expense"))
                columnname = "expense";
            else if (columnname.Equals("expense".Translate()))
            {
                columnname = "expense".Translate();
            }
            else
            {
                columnname = "";
            }
            if (!columnname.Equals(""))
            {
                var dtsort = dt.Clone();
                if (sortMode == "asc")
                    dtsort = dt.Rows.Cast<DataRow>().OrderBy(i=>comparer(i,columnname)).CopyToDataTable();
                //dtsort = dt.Rows.Cast<DataRow>().OrderBy(delegate(DataRow p1,DataRow p2) { return Convert.ToDecimal(p1[columnname]) - Convert.ToDecimal(p2[columnname]); })).CopyToDataTable();
                if (sortMode == "desc")
                    dtsort = dt.Rows.Cast<DataRow>().OrderByDescending(i=>comparer(i,columnname)).CopyToDataTable();
                this.dataGridView1.DataSource = dtsort;
                this.dataGridView1.Refresh();
            }
            else
            {

                dv.Sort = columnBindingName + " " + sortMode;
                this.dataGridView1.DataSource = dv.ToTable();
                this.dataGridView1.Refresh();
            }

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            var obj = sender as DataGridView;
            double sum = 0;
            int selectnum = 0;
            string columnname = "";
            if (obj.Columns.Contains("expense"))
                columnname = "expense";
            if (obj.Columns.Contains("expense".Translate()))
                columnname = "expense".Translate();
            if (columnname != "")
            {
                int index = dataGridView1.Columns[columnname].Index;
                foreach (DataGridViewCell item in obj.SelectedCells)
                {
                    if (item.ColumnIndex == index)
                    {
                        selectnum++;
                        sum += Convert.ToDouble(item.Value);
                    }
                        
                    //Console.WriteLine(item.ColumnIndex.ToString() + item.Value.ToString());
                }
            }
            if (sum > 0)
            {
                labelselect.Text = sum.ToString();
                numselect.Text = selectnum.ToString();
            }

            else
            {
                labelselect.Text = "";
                numselect.Text = "";
            }
                


        }
        /// <summary>
        /// 更新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUpdate_Click(object sender, EventArgs e)//更新数据库
        {
            UpDate();
            
        }
        public void UpDate()
        {
            var obj = (this.Owner as Form1);
            // (this.Owner as Form1).myConnection.UpdateData((this.Owner as Form1).CNA);//更新comsumption 
            var changes = obj.AllTables.Find(i => i.tablenames.Contains("mycom")).table.GetChanges();
            obj.AllTables.ForEach(i => { if (!i.tablenames.Contains("sumof")) obj.myConnection.UpdateData(i); });
            
            if(true)
            {
                //DataTable temp = obj.myConnection.CalSumOfDay(obj.CNASum.table, obj.AllTables.Find(i => { return i.tablenames.Contains("mycom"); }).table, obj.AllTables.Find(i => { return i.tablenames.Contains("income"); }).table);//计算日期的和
                //                                                                                                                                                                                                                       ///将和添加到Sum
                //foreach (DataRow item in temp.Rows)
                //{
                //    int i = 0;
                //    foreach (DataRow row in obj.CNASum.table.Rows)
                //    {
                //        if (row[0].Equals(item[0]))
                //        {
                //            row[1] = item[1];
                //            row[2] = item[2];
                //            i = 1;
                //            break;
                //        }
                //    }
                //    if (i == 0)
                //        obj.CNASum.table.Rows.Add(item[0], item[1], item[2]);
                //}
                //更新Sum
                //obj.myConnection.UpdateData(obj.CNASum);
                obj.CNASum= obj.myConnection.SelectTable("DayView");
                //temp.Clear();
                //计算月和
                obj.CNASumOfMonth = obj.myConnection.SelectTable("MonthView");
                //var temp = obj.myConnection.CalSumofMonth(obj.CNASumOfMonth.table, obj.CNASum.table);
                /////将和添加到SumOfMonth
                //foreach (DataRow item in temp.Rows)
                //{
                //    int i = 0;
                //    foreach (DataRow row in obj.CNASumOfMonth.table.Rows)
                //    {
                //        if (row[0].Equals(item[0]))
                //        {
                //            row[1] = item[1];
                //            row[2] = item[2];
                //            i = 1;
                //            break;
                //        }
                //    }
                //    if (i == 0)
                //        obj.CNASumOfMonth.table.Rows.Add(item[0], item[1], item[2]);
                //}
            }
           

            obj.myConnection.UpdateData(obj.BodyWeight);
            //更新SumOfMonth
            //obj.myConnection.UpdateData(obj.CNASumOfMonth);
            //更新余额
            obj.myConnection.UpdateData(obj.CNASumOfMoney);

            //更新数据后显示所有数据
            //MainTable = obj.myConnection.ShowDayData(obj.AllTables);
            //dataGridView1.DataSource = MainTable;

            TableList.Find(i => { return i.Name.ToLower().Contains("all"); }).PerformClick();
            obj.DrawCalander();
            LastView.PerformClick();
        }
     
        private void databseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formmodify = new FormModify();
            formmodify.AddSelectMenu((this.Owner as Form1).AllTables);
            formmodify.Show();
        }

        public void Export(DataTable dt)
        {
            Workbook workbook = new Workbook();
            dt.AsEnumerable().OrderBy(i => i.Field<string>("Table"));
            
            string date = string.Format("{0:yyyyMM}", SelectMonth);
            string path = @"G:\桌面\test\calander_xls\记录" + "/" + date  + ".xls";
            for (int i = 0; i < 5; i++)
            {
                Worksheet sheet = workbook.Worksheets[i];
                sheet.InsertDataTable(dt,true,1,1);
                if (dt.Rows.Count <= 199)
                    break;
                for (int j = 0; j < 199; j++)
                {
                    dt.Rows.RemoveAt(0);
                }
            }
            
            
            workbook.SaveToFile(path);

        }
        public void Export(DataTable dt,string path)
        {
            Workbook workbook = new Workbook();
            dt.AsEnumerable().OrderBy(i => i.Field<string>("Table"));
            Worksheet sheet = workbook.Worksheets[0];
            int t = 0;
            foreach (DataRow item in dt.Rows)
            {
                sheet.SetValue(++t, 1, item[0].ToString());
                for (int i = 1  ; i < dt.Columns.Count; i++)
                {
                    sheet.SetValue(t, i+1, item[i].ToString());
                }
                
            }

            workbook.SaveToFile(path);

        }
        private void buttest_Click(object sender, EventArgs e)
        {
            //if(SelectMainTable== "AllTables")//选择的是AllCategory，根据月份
            //{
            //    int isall = 0;
            //    foreach (ToolStripMenuItem item in MonthViewToolStripMenuItem.DropDownItems)
            //    {
            //        if (item.Checked)
            //            isall =(int) item.Tag;
            //    }
            //    if(isall==2)//选择了所有时间
            //    {

            //    }
            //    else//不是选择所有时间，选择了一个月
            //    {
            //        var alltab = (this.Owner as Form1).AllTables;
            //        var comtab = alltab.Find(i => i.tablenames.Contains("mycomsum")).table;

            //    }
            //}
            if(true)
            {
                Export(dataGridView1.DataSource as DataTable);
            }
            
        }
        /// <summary>
        /// 主窗格-按月显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="s"></param>
        private void MonthViewSelect(Object sender,EventArgs s)
        {
            foreach (ToolStripMenuItem item in MonthViewToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }
            var SelectItem = sender as ToolStripMenuItem;
            SelectItem.Checked = true;
            int tag = Convert.ToInt16(SelectItem.Tag);
            if (tag<2)
            {
                var date1 = SelectMonth.AddMonths(tag);
                var date2 = SelectMonth.AddMonths(tag+1);
                dateTimePicker1.Value = date1;
                dateTimePicker2.Value = date2.AddDays(-1);
                ShowDateView();
                LastDateTime1 = date1;
                LastDateTime2 = date2;             
            }
            else if(tag==2)
            {              
                var datelist = (from t
                            in MainTable.AsEnumerable()
                                select t.Field<DateTime>("datadate")).ToList();

                var date1 = datelist.Min();
                var date2 = datelist.Max();
                dateTimePicker1.Value = date1;
                dateTimePicker2.Value = date2;
                ShowDateView();
                LastDateTime1 = date1;
                LastDateTime2 = date2;
                SelectMonth = DateTime.Today.ThisMonth();
            }
        }

        public void DrawChartPoint(DataTable sports)
        {
            tabsum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ChartSplitContainer.SplitterDistance = 300;
            List<string> Cat = new List<string>();
            for (int i = 0; i < sports.Rows.Count; i++)
            {
                string cat = sports.Rows[i][1].ToString();
                if (!Cat.Contains(cat))
                    Cat.Add(cat);
            }
            int CatNum = Cat.Count;
            if (sports != null && sports.Rows.Count != 0)
            {
                string name1 = "";
                string name2 = "";
                foreach (DataColumn column in sports.Columns)
                {
                    if (column.DataType == typeof(DateTime))
                        name1 = column.ColumnName;
                }
                if (ChartSumDays.Series.Count == 0)
                {

                }
                else if (ChartSumDays.Series.Count >= 1)
                {
                    ChartSumDays.Series.Clear();
                }
                string columnname = sports.Columns[2].ColumnName;
                for (int k = 0; k < CatNum; k++)
                {
                    var temp = sports.Clone();
                    var templist
                       = (from row
                         in sports.AsEnumerable()
                          where row.Field<string>("category") == Cat[k]
                          select new { date = row[0], cat = row[1], dis = row[2] }).ToList();
                    templist.ForEach(i => { temp.Rows.Add(i.date, i.cat, i.dis); });
                    name2 = temp.Rows[0][1].ToString();
                    Series dataTable1Series = new Series(name2.Translate());
                    dataTable1Series.Points.DataBind(temp.AsEnumerable(), name1,columnname , "");
                    dataTable1Series.XValueType = ChartValueType.DateTime; //设置X轴类型为时间
                    dataTable1Series.ChartType = SeriesChartType.Point;  //设置Y轴为散点

                    dataTable1Series.MarkerSize = 8;
                    dataTable1Series.ToolTip = "#VALX" + name2 + ":#VALY";
                    dataTable1Series.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
                    ChartSumDays.Series.Add(dataTable1Series);
 
                }
                ChartSumDays.Legends[0].Docking = Docking.Top;
                //ChartSumDays.Legends[0]. = 20;
                ChartSumDays.ChartAreas[0].AxisY2.Enabled=AxisEnabled.False;
                ChartSumDays.ChartAreas[0].AxisY.Minimum = -double.NaN;
                ChartSumDays.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
                ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                ChartSumDays.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                ChartSumDays.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                ChartSumDays.ChartAreas[0].AxisY.Title = columnname;
                ChartSumDays.ChartAreas[0].AxisY.TextOrientation=TextOrientation.Auto;
                ChartSumDays.ChartAreas[0].AxisY.Maximum = Math.Ceiling(sports.AsEnumerable().Select(i => i.Field<double>(columnname)).Max()*1.1/100)*100;
                ChartSumDays.ChartAreas[0].AxisY.Minimum = 0;
                ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                ChartSumDays.ChartAreas[0].AxisX.Minimum = sports.AsEnumerable().Select(i => i.Field<DateTime>(name1)).Min().ToOADate();
                ChartSumDays.ChartAreas[0].AxisX.Maximum = sports.AsEnumerable().Select(i => i.Field<DateTime>(name1)).Max().ToOADate();


            }
            else if (sports.Rows.Count == 0 && DateTime.Today.Day == 1)//表格没有内容,当月的第一天
            {

            }
        }

        public void DrawChartHis(DataTable sports,DataTable BodyWeight)
        {
            tabsum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ChartSplitContainer.SplitterDistance = 300;
            List<string> Cat = new List<string>();
            for (int i = 0; i < sports.Rows.Count; i++)
            {
                string cat = sports.Rows[i][1].ToString();
                if (!Cat.Contains(cat))
                    Cat.Add(cat);
            }
            int CatNum = Cat.Count;
            if (sports != null && sports.Rows.Count != 0)
            {
                string name1 = "";
                string name2 = "";
                foreach (DataColumn column in sports.Columns)
                {
                    if (column.DataType == typeof(DateTime))
                        name1 = column.ColumnName;
                }
                if (ChartSumDays.Series.Count == 0)
                {

                }
                else if (ChartSumDays.Series.Count >= 1)
                {
                    ChartSumDays.Series.Clear();
                }
                string columnname = sports.Columns[2].ColumnName;
                for (int k = 0; k < CatNum; k++)
                {
                    var temp = sports.Clone();
                    var templist
                       = (from row
                         in sports.AsEnumerable()
                          where row.Field<string>("category") == Cat[k]
                          select new { date = row[0], cat = row[1], dis = row[2] }).ToList();
                    templist.ForEach(i => { temp.Rows.Add(i.date, i.cat, i.dis); });
                    name2 = temp.Rows[0][1].ToString();
                    Series dataTable1Series = new Series(name2.Translate());
                    dataTable1Series.Points.DataBind(temp.AsEnumerable(), name1, columnname, "");
                    dataTable1Series.XValueType = ChartValueType.DateTime; //设置X轴类型为时间
                    dataTable1Series.ChartType = SeriesChartType.Point;  //设置Y轴为散点

                    dataTable1Series.MarkerSize = 5;
                    dataTable1Series.ToolTip = "#VALX" + name2 + ":#VALY";
                    dataTable1Series.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
                    ChartSumDays.Series.Add(dataTable1Series);

                }
                ChartSumDays.Legends[0].Docking = Docking.Top;
                ChartSumDays.ChartAreas[0].AxisY.Minimum = -double.NaN;
                ChartSumDays.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
                ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                ChartSumDays.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                ChartSumDays.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                ChartSumDays.ChartAreas[0].AxisY.Title = columnname;
                ChartSumDays.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Auto;
                ChartSumDays.ChartAreas[0].AxisY.Maximum = Math.Ceiling(sports.AsEnumerable().Select(i => i.Field<double>(columnname)).Max() * 1.1 / 100) * 100;
                ChartSumDays.ChartAreas[0].AxisY.Minimum = 0;
                ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                ChartSumDays.ChartAreas[0].AxisX.Minimum = sports.AsEnumerable().Select(i => i.Field<DateTime>(name1)).Min().AddDays(-10).ToOADate();
                DateTime fdate = sports.AsEnumerable().Select(i => i.Field<DateTime>(name1)).Min().AddDays(-10);
                DateTime edate = sports.AsEnumerable().Select(i => i.Field<DateTime>(name1)).Max();

                var tempw = BodyWeight.Clone();
                var tempwlist
                   = (from row
                     in BodyWeight.AsEnumerable()
                      where row.Field<DateTime>("datadate") >=fdate
                      select new { date = row[0],  w = row[1] }).ToList();
                tempwlist.ForEach(i => { tempw.Rows.Add(i.date, i.w); });

                name1 = tempw.Columns[0].ColumnName;
                columnname = tempw.Columns[1].ColumnName;
                Series Series1 = new Series(columnname.Translate());

                Series1.Points.DataBind(tempw.AsEnumerable(), name1, columnname, "");
                Series1.XValueType = ChartValueType.DateTime; //设置X轴类型为时间
                Series1.ChartType = SeriesChartType.FastLine;  //设置Y轴为散点
                Series1.MarkerSize = 20;
                Series1.Color = Color.Red;
                Series1.ToolTip = "#VALX" + columnname + ":#VALY";
                Series1.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                ChartSumDays.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;

                ChartSumDays.Series.Add(Series1);

                ChartSumDays.ChartAreas[0].AxisY2.Minimum = -double.NaN;
                ChartSumDays.ChartAreas[0].AxisY2.IntervalAutoMode = IntervalAutoMode.VariableCount;

                ChartSumDays.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
                ChartSumDays.ChartAreas[0].AxisY2.Title = columnname;
                ChartSumDays.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Auto;
                ChartSumDays.ChartAreas[0].AxisY2.Maximum = Math.Round(tempw.AsEnumerable().Select(i => i.Field<double>(columnname)).Max() +0.2 ,1) ;
                ChartSumDays.ChartAreas[0].AxisY2.Minimum = Math.Round(tempw.AsEnumerable().Select(i => i.Field<double>(columnname)).Min() -0.2, 1); ;


            }
            else if (sports.Rows.Count == 0 && DateTime.Today.Day == 1)//表格没有内容,当月的第一天
            {

            }
            
        }


        /// <summary>
        /// 画折线图
        /// </summary>
        /// <param name="dt"></param>折线图数据
        public void DrawChartLine(DataTable dt)
        {
            tabsum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            ChartSplitContainer.SplitterDistance = 300;
          
            if (dt != null&&dt.Rows.Count!=0)
            {
                string name1 = "";
                string name2 = "";
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.DataType == typeof(DateTime))
                        name1 = column.ColumnName;
                }
                if (ChartSumDays.Series.Count == 0)
                {

                }
                else if (ChartSumDays.Series.Count >= 1)
                {
                    ChartSumDays.Series.Clear();
                }
                name2 = dt.Columns[1].ColumnName;
                Series dataTable1Series = new Series(name2.Translate());
               
                dataTable1Series.Points.DataBind(dt.AsEnumerable(), name1, name2, "");
                dataTable1Series.XValueType = ChartValueType.DateTime; //设置X轴类型为时间
                dataTable1Series.ChartType = SeriesChartType.Line;  //设置Y轴为折线
                dataTable1Series.Color = Color.OrangeRed;
                dataTable1Series.ToolTip = "#VALX"+ ":#VALY";
                //dataTable1Series.Label =  "#VALY";
                

                dataTable1Series.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
                ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
                ChartSumDays.ChartAreas[0].AxisX.Minimum= dt.AsEnumerable().Select(i=>i.Field<DateTime>(name1)).Min().AddDays(-2).ToOADate();
                ChartSumDays.Legends[0].Docking = Docking.Top;

                ChartSumDays.Series.Add(dataTable1Series);
                if (dt.Columns.Contains("comsum"))//统计消费
                {
                    ChartSumDays.Series.Remove(dataTable1Series);
                    Series dataTable1Series1 = new Series("comsum".Translate());
                    dataTable1Series1.Points.DataBind(dt.AsEnumerable(), name1, "comsum", "");
                    dataTable1Series1.XValueType = ChartValueType.DateTime; //设置X轴类型为时间
                    dataTable1Series1.ChartType = SeriesChartType.Line;  //设置Y轴为折线
                    dataTable1Series1.LegendText = "总支出";
                    dataTable1Series1.ToolTip = "总支出:"+"#VALY";
                    dataTable1Series1.Color = Color.Blue;
                    ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                    ChartSumDays.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
                    if (ViewType == SViewType.month)
                        ChartSumDays.ChartAreas[0].AxisX.Interval = 0;
                    ChartSumDays.Legends[0].Docking = Docking.Top;
                    dataTable1Series1.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                    ChartSumDays.Series.Add(dataTable1Series1);                    
                    ChartSumDays.ChartAreas[0].AxisY2.Minimum = 0;
                    ChartSumDays.ChartAreas[0].AxisY2.Maximum = dt.AsEnumerable().Select(i => i.Field<double>("comsum")).Max();
                    ChartSumDays.ChartAreas[0].AxisY.Maximum = dt.AsEnumerable().Select(i => i.Field<double>("sum")).Max();
                    ChartSumDays.ChartAreas[0].AxisY.Minimum = dt.AsEnumerable().Select(i => i.Field<double>("sum")).Min(); ;
                    ChartSumDays.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                    ChartSumDays.ChartAreas[0].AxisY2.IntervalAutoMode = IntervalAutoMode.VariableCount;
                    ChartSumDays.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
                    ChartSumDays.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    ChartSumDays.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                    ChartSumDays.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
                    //ChartSumDays.ChartAreas[0].AxisY2.Title = "总支出";
                    //ChartSumDays.ChartAreas[0].AxisY.Title = "净支出";
                    ChartSumDays.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
                    ChartSumDays.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Horizontal;


                    //找表中的最大值，最小值，最近一天的值
                    var AnotherTable = dt.Clone();
                    var list1
                        = (from row
                          in dt.AsEnumerable()
                           orderby row.Field<double>("comsum") descending
                           select row);
                    AnotherTable.ImportRow(list1.Last());
                    AnotherTable.ImportRow(list1.First());
                    var list2
                        = (from row
                          in dt.AsEnumerable()
                           orderby row.Field<DateTime>(name1) descending
                           select row);
                    AnotherTable.ImportRow(list2.First());
                    Series AnotherSeries = new Series("Another".Translate());
                    AnotherSeries.Points.DataBind(AnotherTable.AsEnumerable(), name1, "comsum", "");
                    AnotherSeries.ChartType = SeriesChartType.FastPoint;
                    AnotherSeries.Label = "#VALY";
                    AnotherSeries.LegendText = "统计";
                    AnotherSeries.MarkerStyle = MarkerStyle.Circle;
                    AnotherSeries.MarkerSize = 5;
                    AnotherSeries.MarkerColor = Color.Orange;
                    AnotherSeries.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                    ChartSumDays.Series.Add(AnotherSeries);


                    //计算平均值
                    double ave = dt.AsEnumerable().Average(i => i.Field<double>("comsum"));
                    Series aveseries = new Series("Average".Translate());
                    DataTable avetable = new DataTable();
                    avetable.Columns.Add(name1, Type.GetType("System.DateTime"));
                    avetable.Columns.Add("Average", Type.GetType("System.Double"));
                    avetable.Rows.Add(dt.AsEnumerable().Select(i => i.Field<DateTime>(name1)).Min(), ave);
                    avetable.Rows.Add(dt.AsEnumerable().Select(i => i.Field<DateTime>(name1)).Max(), ave);
                    aveseries.Points.DataBind(avetable.AsEnumerable(), name1, "Average", "");
                    aveseries.XValueType = ChartValueType.DateTime; //设置X轴类型为时间
                    aveseries.ChartType = SeriesChartType.Line;  //设置Y轴为折线
                    aveseries.Color = Color.Yellow;
                    aveseries.LegendText = "平均值";
                    aveseries.ToolTip = "平均值:" + "#VALY";
                    aveseries.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                    ChartSumDays.Series.Add(aveseries);
                }

                else if(dt.Columns.Contains("sumofday"))//统计消费
                {
                    ChartSumDays.Series.Remove(dataTable1Series);
                    Series dataTable1Series1 = new Series("sumofday".Translate());
                    dataTable1Series1.Points.DataBind(dt.AsEnumerable(), name1, "sumofday", "");
                    dataTable1Series1.XValueType = ChartValueType.DateTime; //设置X轴类型为时间
                    dataTable1Series1.ChartType = SeriesChartType.Line;  //设置Y轴为折线
                    dataTable1Series1.LegendText = "总支出";
                    dataTable1Series1.ToolTip = "总支出:" + "#VALY";
                    dataTable1Series1.Color = Color.Blue;
                    ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                    ChartSumDays.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
                    if (ViewType == SViewType.month)
                        ChartSumDays.ChartAreas[0].AxisX.Interval = 0;
                    ChartSumDays.Legends[0].Docking = Docking.Top;
                    dataTable1Series1.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                    ChartSumDays.Series.Add(dataTable1Series1);
                    ChartSumDays.ChartAreas[0].AxisY2.Minimum = 0;
                    ChartSumDays.ChartAreas[0].AxisY2.Maximum = dt.AsEnumerable().Select(i => i.Field<double>("sumofday")).Max();
                    ChartSumDays.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                    ChartSumDays.ChartAreas[0].AxisY2.IntervalAutoMode = IntervalAutoMode.VariableCount;
                    ChartSumDays.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
                    ChartSumDays.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    ChartSumDays.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                    ChartSumDays.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
                    //ChartSumDays.ChartAreas[0].AxisY2.Title = "总支出";
                    //ChartSumDays.ChartAreas[0].AxisY.Title = "净支出";
                    ChartSumDays.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
                    ChartSumDays.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Horizontal;


                    //找表中的最大值，最小值，最近一天的值
                    var AnotherTable = dt.Clone();
                    var list1
                        = (from row
                          in dt.AsEnumerable()
                           orderby row.Field<double>("sumofday") descending
                           select row);
                    AnotherTable.ImportRow(list1.Last());
                    AnotherTable.ImportRow(list1.First());
                    var list2
                        = (from row
                          in dt.AsEnumerable()
                           orderby row.Field<DateTime>(name1) descending
                           select row);
                    AnotherTable.ImportRow(list2.First());
                    Series AnotherSeries = new Series("Another".Translate());
                    AnotherSeries.Points.DataBind(AnotherTable.AsEnumerable(), name1, "sumofday", "");
                    AnotherSeries.ChartType = SeriesChartType.FastPoint;
                    AnotherSeries.Label = "#VALY";
                    AnotherSeries.LegendText = "统计";
                    AnotherSeries.MarkerStyle = MarkerStyle.Circle;
                    AnotherSeries.MarkerSize = 5;
                    AnotherSeries.MarkerColor = Color.Orange;
                    AnotherSeries.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                    ChartSumDays.Series.Add(AnotherSeries);


                    //计算平均值
                    double ave = dt.AsEnumerable().Average(i => i.Field<double>("sumofday"));
                    Series aveseries = new Series("Average".Translate());
                    DataTable avetable = new DataTable();
                    avetable.Columns.Add(name1, Type.GetType("System.DateTime"));
                    avetable.Columns.Add("Average", Type.GetType("System.Double"));
                    avetable.Rows.Add(dt.AsEnumerable().Select(i => i.Field<DateTime>(name1)).Min(), ave);
                    avetable.Rows.Add(dt.AsEnumerable().Select(i => i.Field<DateTime>(name1)).Max(), ave);
                    aveseries.Points.DataBind(avetable.AsEnumerable(), name1, "Average", "");
                    aveseries.XValueType = ChartValueType.DateTime; //设置X轴类型为时间
                    aveseries.ChartType = SeriesChartType.Line;  //设置Y轴为折线
                    aveseries.Color = Color.Red;
                    aveseries.LegendText = "平均值";
                    aveseries.ToolTip = "平均值:" + "#VALY";
                    aveseries.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                    ChartSumDays.Series.Add(aveseries);
                }
                else//不是统计消费的
                {
                    //找表中的最大值，最小值，最近一天的值
                    var AnotherTable = dt.Clone();
                    var list1
                        = (from row
                          in dt.AsEnumerable()
                           orderby row.Field<double>(name2) descending
                           select row);
                    AnotherTable.ImportRow(list1.Last());
                    AnotherTable.ImportRow(list1.First());
                    var list2
                        = (from row
                          in dt.AsEnumerable()
                           orderby row.Field<DateTime>(name1) descending
                           select row);
                    AnotherTable.ImportRow(list2.First());
                    Series AnotherSeries = new Series("Another".Translate());
                    AnotherSeries.Points.DataBind(AnotherTable.AsEnumerable(), name1, name2, "");
                    AnotherSeries.ChartType = SeriesChartType.FastPoint;
                    
                    AnotherSeries.MarkerStyle = MarkerStyle.Circle;
                    AnotherSeries.MarkerSize = 5;
                    AnotherSeries.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
                    AnotherSeries.Label = "#VALY";
                    AnotherSeries.ToolTip = "#VALY";
                    AnotherSeries.LegendText = "统计";
                    ChartSumDays.Series.Add(AnotherSeries);

                    ChartSumDays.ChartAreas[0].AxisY2.Minimum = 0;
                    ChartSumDays.ChartAreas[0].AxisY.Minimum = -double.NaN;
                    ChartSumDays.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                    int sumcount= (int)Math.Round(dt.AsEnumerable().Select(i => i.Field<double>(name2)).Max()-dt.AsEnumerable().Select(i => i.Field<double>(name2)).Min(), 1);
                    ChartSumDays.ChartAreas[0].AxisY2.IntervalAutoMode = IntervalAutoMode.VariableCount;
                    ChartSumDays.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
                    ChartSumDays.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            
                    ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                    
                    ChartSumDays.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    ChartSumDays.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                    ChartSumDays.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
                    ChartSumDays.ChartAreas[0].AxisY.Title = name2;
                    ChartSumDays.ChartAreas[0].AxisY.Maximum = Math.Round( dt.AsEnumerable().Select(i => i.Field<double>(name2)).Max(),1)+1;
                    ChartSumDays.ChartAreas[0].AxisY.Minimum = Math.Round(dt.AsEnumerable().Select(i => i.Field<double>(name2)).Min(), 1)-1;


                }

                if(ViewMonth==SMonthAdd.Current&& ViewType == SViewType.day)
                {
                    //增加平均值
                    var bud = (this.Owner as Form1).budget;
                    double sum = bud.Sum(i => i.Value)/myMethod.GetDayOfMonth(DateTime.Today);
                    Series budseries = new Series("budget".Translate());
                    DataTable budtable = new DataTable();
                    budtable.Columns.Add("datadate", Type.GetType("System.DateTime"));
                    budtable.Columns.Add("sum", Type.GetType("System.Double"));
                    budtable.Rows.Add(dt.AsEnumerable().Select(i => i.Field<DateTime>("datadate")).Min(), sum);
                    budtable.Rows.Add(dt.AsEnumerable().Select(i => i.Field<DateTime>("datadate")).Max(), sum);
                    budseries.Points.DataBind(budtable.AsEnumerable(), "datadate", "sum", "");

                    budseries.XValueType = ChartValueType.DateTime; //设置X轴类型为时间
                    budseries.ChartType = SeriesChartType.Line;  //设置Y轴为折线
                    budseries.Color = Color.Yellow;
                    budseries.LegendText = "预算";
                    budseries.ToolTip = "预算:" + "#VALY";
                    budseries.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                    ChartSumDays.Series.Add(budseries);

                }
            }
            else if(dt.Rows.Count==0&&DateTime.Today.Day==1)//表格没有内容,当月的第一天
            {

            }
        }

        /// <summary>
        /// 副窗格显示月统计折线图
        /// </summary>
        private void ShowMonthView(string tabname)
        {
            if (tabname ==  "SMonthViewToolStripMenuItem" )  
                tabname = "mycomsumption";
            var temp = (this.Owner as Form1).AllTables.Find(i => i.tablenames == tabname).table;
            tabsum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (ToolStripMenuItem item in secondToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }
            
            SMonthViewToolStripMenuItem.Checked = true;
            string datetimecolum = "";
            if (tabname.Contains("mycom"))
            {
                temp = (this.Owner as Form1).CNASumOfMonth.table;
                var newtab = new DataTable();
                newtab.Columns.Add("Month", Type.GetType("System.DateTime"));
                newtab.Columns.Add("SumOfMonth", Type.GetType("System.Double"));
                foreach (DataRow  row in temp.Rows)
                {
                    newtab.Rows.Add(new DateTime((int)row[0], (int)row[1], 1), (double)row[2]);
                }
                for (int i = 0; i < newtab.Columns.Count; i++)
                {
                    if (newtab.Columns[i].DataType == Type.GetType("System.DateTime"))
                        datetimecolum = newtab.Columns[i].ColumnName;

                }
                newtab.DefaultView.Sort = datetimecolum + " desc";
                newtab = newtab.DefaultView.ToTable();
                tabsum.DataSource = newtab.Translate();
                DrawChartLine(newtab);
            }
            else
            {
                for (int i = 0; i < temp.Columns.Count; i++)
                {
                    if (temp.Columns[i].DataType == Type.GetType("System.DateTime"))
                        datetimecolum = temp.Columns[i].ColumnName;
                }
                temp.DefaultView.Sort = datetimecolum + " desc";
                temp = temp.DefaultView.ToTable();
                //按月统计temp
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    var d = (DateTime)temp.Rows[i][datetimecolum];
                    temp.Rows[i][datetimecolum] = d.ThisMonth();
                }
                var sumlist
                            = (from row
                              in temp.AsEnumerable()
                               group row by new { t1 = row.Field<DateTime>(datetimecolum) }
                               into m
                               select new { datamonth = m.Key.t1, count = m.Count() }).OrderByDescending(i => { return i.datamonth; }).ToList();
                DataTable objtable = new DataTable();
                objtable.Columns.Add("datamonth", Type.GetType("System.DateTime"));
                objtable.Columns.Add("count", Type.GetType("System.Double"));
                sumlist.ForEach(i => { objtable.Rows.Add(i.datamonth, i.count); });
                tabsum.DataSource = objtable.Translate();

                DrawChartLine(objtable);
            }
           
        }
        
        public void DrawChartPie(DataTable dt)
        {
            tabsum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ChartSplitContainer.SplitterDistance = 250;
            foreach (ToolStripMenuItem item in secondToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }
            SCatViewToolStripMenuItem.Checked = true;
            if (dt != null)
            {
                var catlist = GetSortTable(dt);
                tabsum.DataSource = catlist;
                Series dataTable1Series = new Series(dt.TableName);

                if (ChartSumDays.Series.Count == 0)
                {

                }
                else if (ChartSumDays.Series.Count >= 1)
                {

                    ChartSumDays.Series.Clear();
                }
                dataTable1Series.Points.DataBind(catlist.AsEnumerable(), "category", "sum", "");

                dataTable1Series.XValueType = ChartValueType.String; //设置X轴类型为时间
                dataTable1Series.ChartType = SeriesChartType.Pie;  //设置Y轴为饼状图
                dataTable1Series["PieLabelStyle"] = "Outside";
                dataTable1Series.LegendText = "#VALX";
                dataTable1Series.Label = "#VAL" + "   " + "#PERCENT";
                ChartSumDays.Legends[0].Docking = Docking.Top;
                ChartSumDays.Series.Add(dataTable1Series);
            }
        }          
        public void DrawChartColumn(DataTable dt)
        {
            tabsum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ChartSplitContainer.SplitterDistance = 200;
            foreach (ToolStripMenuItem item in secondToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }
           SColumnViewToolStripMenuItem.Checked = true;
            if (dt != null)
            {
                var catlist = GetSortTable(dt,true);
                tabsum.DataSource = catlist;
                if (ChartSumDays.Series.Count == 0)
                {

                }
                else if (ChartSumDays.Series.Count >= 1)
                {
                    ChartSumDays.Series.Clear();
                }
                DataColumn column = new DataColumn("budget", Type.GetType("System.Double"));
                catlist.Columns.Add(column);
                for (int i = 0; i < catlist.Rows.Count; i++)
                {
                    string cat = catlist.Rows[i]["category"].ToString();
                    double b = 0;
                    if ((this.Owner as Form1).budget.Keys.Contains(cat))
                        b = (this.Owner as Form1).budget[cat];
                    catlist.Rows[i]["budget"] = b;

                }
                
                //foreach (DataRow row in catlist.Rows)
                //{
                    Series dataTable1Series = new Series(dt.TableName);
                    //DataTable temp = catlist.Clone();
                    //temp.TableName = row["category"].ToString();
                    //temp.ImportRow(row);
                    dataTable1Series.Points.DataBind(catlist.AsEnumerable(), "category", "sum", "");
                    
                    dataTable1Series.XValueType = ChartValueType.String; //设置X轴类型为时间
                    dataTable1Series.ChartType = SeriesChartType.Column;  //设置Y轴为饼状图
                
                    dataTable1Series["PieLabelStyle"] = "Outside";
                //dataTable1Series.LegendText = row["category"].ToString();
                //dataTable1Series["PointWidth"] = "0.2";
                dataTable1Series.ToolTip = "#VALY";
                    dataTable1Series.Label = "";
                int pointnum = dataTable1Series.Points.Count;
                //for (int i = 0; i < pointnum; i++)
                //{
                //    dataTable1Series.Points[i].Color = Color.FromArgb(255, i * 15, 255-i * 15, i * 15);
                //}
                    dataTable1Series.IsValueShownAsLabel = false ;
                    ChartSumDays.Series.Add(dataTable1Series);
               // }
               
                ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
                ChartSumDays.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
                ChartSumDays.ChartAreas[0].AxisX.Interval = 1 ;
                ChartSumDays.ChartAreas[0].AxisX.Minimum = 0;
                ChartSumDays.ChartAreas[0].AxisY.Maximum = catlist.AsEnumerable().Max(i=>(double)i[1]);
                ChartSumDays.ChartAreas[0].AxisY.Minimum = 0;
                ChartSumDays.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                ChartSumDays.ChartAreas[0].AxisY.Title = "sum";
                ChartSumDays.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Auto;
                ChartSumDays.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                ChartSumDays.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                //chartofsumofday.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
                ChartSumDays.Legends[0].Docking = Docking.Top;
                
            }
        }
        public void DrawChartColumnEveryMonth()
        {
            int maxitem = 10;
            tabsum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            List<DataTable> dt=new List<DataTable>();
            DateTime td = SSelectMonth;
            SSelectMonth = SSelectMonth = DateTime.Today.AddDays(-DateTime.Today.Day + 1).AddMonths(1);
            DataTable SumDt = new DataTable();
            DataTable temp = new DataTable();
            while( (temp=SSelectTable("mycom",SMonthAdd.Last)).Rows.Count>0)
            {
                dt.Add(temp);
            }

            ////处理选项的钩
            //foreach (ToolStripMenuItem item in secondToolStripMenuItem.DropDownItems)
            //{
            //    item.Checked = false;
            //}
            //SColumnViewToolStripMenuItem.Checked = true;
            if (ChartSumDays.Series.Count == 0)
            {

            }
            else if (ChartSumDays.Series.Count >= 1)
            {

                ChartSumDays.Series.Clear();
            }
            //处理表格
            if (dt != null)
            {
                string name1 = "";
                string name2 = "";
                //找到时间和金额的数据项
                foreach (DataColumn column in dt[1].Columns)
                {
                    if (column.DataType == typeof(DateTime))
                        name1 = column.ColumnName;
                    if (column.DataType == typeof(double))
                        name2 = column.ColumnName;
                }
                SumDt.Columns.Add("Month");
                SumDt.Columns["Month"].DataType = Type.GetType("System.String");
                //每个月分类进行统计
                foreach (DataTable everydt in dt)
                {
                    //分类进行累加
                    var catlist 
                        = (from row
                          in everydt.AsEnumerable()
                          where row.Field<string>("category")!="科研"&&row.Field<string>("category") != "还款"&& row.Field<string>("payment") != "花呗" || (row.Field<string>("note").Contains("true"))
                        group row by new { t1 = row.Field<string>("category") }
                           into m
                       select new { category = m.Key.t1, sum = Math.Round(m.Sum(i => { return i.Field<double>(name2); }), 2) }).OrderByDescending(i => { return i.sum; }).ToList();
                    //double sumofother = 0;
                    //double compare = 0;
                    //得到分类统计的list
                    //catlist.Add(new { category = "其他", sum = sumofother });
                    //catlist.RemoveAll(i => i.sum < compare);
                    //获得当前表格月份
                    DateTime SeriesMonth= everydt.Rows[0].Field<DateTime>(name1);
                    string SeriesName = SeriesMonth.Year.ToString() + "." + SeriesMonth.Month.ToString();
                    //将分类统计的list整理为datatable
                    //扩展表格
                    catlist.ForEach(i => { if (!SumDt.Columns.Contains(i.category)) 
                    { SumDt.Columns.Add(i.category); } });
                    //将数据放入表格
                    DataRow Row = SumDt.NewRow();
                    Row["Month"] = SeriesName;
                    catlist.ForEach(i => Row[i.category] = i.sum);
                    SumDt.Rows.Add(Row);
                }
                //没有数据的内容设置为0，插入统计行
                DataRow datarow = SumDt.NewRow();
                datarow["Month"] = "AllMonth";
                for (int i = 1; i < SumDt.Columns.Count; i++)
                {
                    double d = 0;
                    for (int j = 0; j < SumDt.Rows.Count; j++)
                    {
                        if (SumDt.Rows[j][i].GetType() == typeof(System.DBNull))
                            SumDt.Rows[j][i] = 0;
                        d += Convert.ToDouble(SumDt.Rows[j][i]);
                    }
                    datarow[i] = d;
                }
                SumDt.Rows.Add(datarow);
                
                //按列的总和排序,排到一定数目就合并
                for (int i = 1; i < SumDt.Columns.Count; i++)
                {
                    double max = Double.MinValue;
                    int m = 1;
                    for (int j = i; j < SumDt.Columns.Count; j++)
                    {
                        if (Convert.ToDouble(SumDt.Rows[SumDt.Rows.Count - 1][j]) > max)
                        {
                            max = Convert.ToDouble(SumDt.Rows[SumDt.Rows.Count - 1][j]);
                            m = j;
                        }
                    }
                    SumDt.Columns[m].SetOrdinal(i);
                }
                //合并小项目
                for (int j = 0; j < SumDt.Rows.Count; j++)
                {
                    double sum = 0;
                    for (int i = maxitem; i < SumDt.Columns.Count; i++)
                    {
                        sum += Convert.ToDouble( SumDt.Rows[j][i]);
                    }
                    SumDt.Rows[j][maxitem] = sum;
                }
                //移除小项目对应的列
                do
                {
                    int i = SumDt.Columns.Count - 1;
                    SumDt.Columns.RemoveAt(i);
                } while (SumDt.Columns.Count>maxitem+1);
                
                SumDt.Columns[maxitem].ColumnName = "其他";
                dataGridView1.DataSource = SumDt.Translate();
                //移除统计列
                SumDt.Rows.RemoveAt(SumDt.Rows.Count-1);

                foreach (DataColumn item in SumDt.Columns)
                {
                    if(item.ColumnName!="Month")
                    {
                        Series dataTable1Series = new Series(item.ColumnName);
                        dataTable1Series.Points.DataBind(SumDt.AsEnumerable(), "Month", item.ColumnName, "");
                        dataTable1Series.XValueType = ChartValueType.Auto; //设置X轴类型为时间
                        dataTable1Series.ChartType = SeriesChartType.Column;  //设置Y轴为饼状图
                        dataTable1Series["PieLabelStyle"] = "Outside";
                        dataTable1Series["PointWidth"] = "1";
                        dataTable1Series.LegendText = item.ColumnName;
                        
                        dataTable1Series.ToolTip = "#SERIESNAME:#VALY";
                        dataTable1Series.Label = "";
                       
                        dataTable1Series.IsValueShownAsLabel = false;


                        ChartSumDays.Series.Add(dataTable1Series);
                    }
                  
                }
                ChartSumDays.Series[0].Color = Color.Red;
                ChartSumDays.Series[ChartSumDays.Series.Count-1].Color = Color.Blue;
                ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                ChartSumDays.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
                ChartSumDays.ChartAreas[0].AxisY.Maximum = 2000;
                ChartSumDays.ChartAreas[0].AxisY.Minimum = 0;
                ChartSumDays.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                ChartSumDays.ChartAreas[0].AxisY.Title = "s\nu\nm";
                ChartSumDays.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
                ChartSumDays.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
                ChartSumDays.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                ChartSumDays.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                ChartSumDays.Legends[0].Docking = Docking.Top; 
                tabsum.DataSource = new DataTable();
                ChartSplitContainer.SplitterDistance = 1;
            }
            SSelectMonth = td;
        }


        public void ShowMonthViewEvery()
        {
            int maxitem = 10;
            tabsum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            List<DataTable> dt = new List<DataTable>();
            DateTime td = SSelectMonth;
            SSelectMonth = SSelectMonth = DateTime.Today.AddDays(-DateTime.Today.Day + 1).AddMonths(1);
            DataTable SumDt = new DataTable();
            DataTable temp = new DataTable();
            while ((temp = SSelectTable("mycom", SMonthAdd.Last)).Rows.Count > 0)
            {
                dt.Add(temp);
            }

            ////处理选项的钩
            //foreach (ToolStripMenuItem item in secondToolStripMenuItem.DropDownItems)
            //{
            //    item.Checked = false;
            //}
            //SColumnViewToolStripMenuItem.Checked = true;
            if (ChartSumDays.Series.Count == 0)
            {

            }
            else if (ChartSumDays.Series.Count >= 1)
            {

                ChartSumDays.Series.Clear();
            }
            //处理表格
            if (dt != null)
            {
                string name1 = "";
                string name2 = "";
                //找到时间和金额的数据项
                foreach (DataColumn column in dt[1].Columns)
                {
                    if (column.DataType == typeof(DateTime))
                        name1 = column.ColumnName;
                    if (column.DataType == typeof(double))
                        name2 = column.ColumnName;
                }
                SumDt.Columns.Add("Month");
                SumDt.Columns["Month"].DataType = Type.GetType("System.String");
                //每个月分类进行统计
                foreach (DataTable everydt in dt)
                {
                    //分类进行累加
                    var catlist
                        = (from row
                          in everydt.AsEnumerable()
                           where row.Field<string>("category") != "科研" && row.Field<string>("category") != "还款" && row.Field<string>("payment") != "花呗" || (row.Field<string>("note").Contains("true"))
                           group row by new { t1 = row.Field<string>("category") }
                           into m
                           select new { category = m.Key.t1, sum = Math.Round(m.Sum(i => { return i.Field<double>(name2); }), 2) }).OrderByDescending(i => { return i.sum; }).ToList();
                    //double sumofother = 0;
                    //double compare = 0;
                    //得到分类统计的list
                    //catlist.Add(new { category = "其他", sum = sumofother });
                    //catlist.RemoveAll(i => i.sum < compare);
                    //获得当前表格月份
                    DateTime SeriesMonth = everydt.Rows[0].Field<DateTime>(name1);
                    string SeriesName = SeriesMonth.Year.ToString() + "." + SeriesMonth.Month.ToString();
                    //将分类统计的list整理为datatable
                    //扩展表格
                    catlist.ForEach(i => {
                        if (!SumDt.Columns.Contains(i.category))
                        { SumDt.Columns.Add(i.category); }
                    });
                    //将数据放入表格
                    DataRow Row = SumDt.NewRow();
                    Row["Month"] = SeriesName;
                    catlist.ForEach(i => Row[i.category] = i.sum);
                    SumDt.Rows.Add(Row);
                }
                //没有数据的内容设置为0，插入统计行
                DataRow datarow = SumDt.NewRow();
                datarow["Month"] = "AllMonth";
                for (int i = 1; i < SumDt.Columns.Count; i++)
                {
                    double d = 0;
                    for (int j = 0; j < SumDt.Rows.Count; j++)
                    {
                        if (SumDt.Rows[j][i].GetType() == typeof(System.DBNull))
                            SumDt.Rows[j][i] = 0;
                        d += Convert.ToDouble(SumDt.Rows[j][i]);
                    }
                    datarow[i] = d;
                }
                SumDt.Rows.Add(datarow);

                //按列的总和排序,排到一定数目就合并
                for (int i = 1; i < SumDt.Columns.Count; i++)
                {
                    double max = Double.MinValue;
                    int m = 1;
                    for (int j = i; j < SumDt.Columns.Count; j++)
                    {
                        if (Convert.ToDouble(SumDt.Rows[SumDt.Rows.Count - 1][j]) > max)
                        {
                            max = Convert.ToDouble(SumDt.Rows[SumDt.Rows.Count - 1][j]);
                            m = j;
                        }
                    }
                    SumDt.Columns[m].SetOrdinal(i);
                }
                //合并小项目
                for (int j = 0; j < SumDt.Rows.Count; j++)
                {
                    double sum = 0;
                    for (int i = maxitem; i < SumDt.Columns.Count; i++)
                    {
                        sum += Convert.ToDouble(SumDt.Rows[j][i]);
                    }
                    SumDt.Rows[j][maxitem] = sum;
                }
                //移除小项目对应的列
                do
                {
                    int i = SumDt.Columns.Count - 1;
                    SumDt.Columns.RemoveAt(i);
                } while (SumDt.Columns.Count > maxitem + 1);

                SumDt.Columns[maxitem].ColumnName = "其他";
                dataGridView1.DataSource = SumDt.Translate();
                //移除统计列
                SumDt.Rows.RemoveAt(SumDt.Rows.Count - 1);

                foreach (DataColumn item in SumDt.Columns)
                {
                    if (item.ColumnName != "Month")
                    {
                        Series dataTable1Series = new Series(item.ColumnName);
                        dataTable1Series.Points.DataBind(SumDt.AsEnumerable(), "Month", item.ColumnName, "");
                        dataTable1Series.XValueType = ChartValueType.Auto; //设置X轴类型为时间
                        dataTable1Series.ChartType = SeriesChartType.Line;  //设置Y轴为饼状图
                        dataTable1Series["PieLabelStyle"] = "Outside";
                        dataTable1Series["PointWidth"] = "1";
                        dataTable1Series.LegendText = item.ColumnName;

                        dataTable1Series.ToolTip = "#SERIESNAME:#VALY";
                        dataTable1Series.Label = "";

                        dataTable1Series.IsValueShownAsLabel = false;


                        ChartSumDays.Series.Add(dataTable1Series);
                    }

                }
                ChartSumDays.Series[0].Color = Color.Red;
                ChartSumDays.Series[ChartSumDays.Series.Count - 1].Color = Color.Blue;
                ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                ChartSumDays.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
                ChartSumDays.ChartAreas[0].AxisY.Maximum = 3000;
                ChartSumDays.ChartAreas[0].AxisY.Minimum = 0;
                ChartSumDays.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                ChartSumDays.ChartAreas[0].AxisY.Title = "s\nu\nm";
                ChartSumDays.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
                ChartSumDays.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
                ChartSumDays.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                ChartSumDays.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                ChartSumDays.ChartAreas[0].AxisX.Minimum = 0;
                ChartSumDays.ChartAreas[0].AxisX.Maximum = SumDt.Rows.Count + 1;
                ChartSumDays.Legends[0].Docking = Docking.Top;
                tabsum.DataSource = new DataTable();
                ChartSplitContainer.SplitterDistance = 1;
            }
            SSelectMonth = td;
        }

        //public void ShowMonthViewEvery()
        //{
        //    int maxitem = 10;
        //    tabsum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //    List<DataTable> dt = new List<DataTable>();
        //    DateTime td = SSelectMonth;
        //    SSelectMonth = SSelectMonth = DateTime.Today.AddDays(-DateTime.Today.Day + 1).AddMonths(1);
        //    DataTable SumDt = new DataTable();
        //    DataTable temp = new DataTable();
        //    while ((temp = SSelectTable("mycom", SMonthAdd.Last)).Rows.Count > 0)
        //    {
        //        dt.Add(temp);
        //    }

        //    ////处理选项的钩
        //    //foreach (ToolStripMenuItem item in secondToolStripMenuItem.DropDownItems)
        //    //{
        //    //    item.Checked = false;
        //    //}
        //    //SColumnViewToolStripMenuItem.Checked = true;
        //    if (ChartSumDays.Series.Count == 0)
        //    {

        //    }
        //    else if (ChartSumDays.Series.Count >= 1)
        //    {

        //        ChartSumDays.Series.Clear();
        //    }
        //    //处理表格
        //    if (dt != null)
        //    {
        //        string name1 = "";
        //        string name2 = "";
        //        //找到时间和金额的数据项
        //        foreach (DataColumn column in dt[1].Columns)
        //        {
        //            if (column.DataType == typeof(DateTime))
        //                name1 = column.ColumnName;
        //            if (column.DataType == typeof(double))
        //                name2 = column.ColumnName;
        //        }
        //        SumDt.Columns.Add("Category");
        //        SumDt.Columns["Category"].DataType = Type.GetType("System.String");
        //        List<string> Category = new List<string>();
        //        //每个月分类进行统计
        //        foreach (DataTable everydt in dt)
        //        {
        //            //分类进行累加
        //            var catlist
        //                = (from row
        //                  in everydt.AsEnumerable()
        //                   where row.Field<string>("category") != "科研" && row.Field<string>("category") != "还款" && row.Field<string>("payment") != "花呗" || (row.Field<string>("note").Contains("true"))
        //                   group row by new { t1 = row.Field<string>("category") }
        //                   into m
        //                   select new { category = m.Key.t1, sum = Math.Round(m.Sum(i => { return i.Field<double>(name2); }), 2) }).OrderByDescending(i => { return i.sum; }).ToList();
        //            //double sumofother = 0;
        //            //double compare = 0;
        //            //得到分类统计的list
        //            //catlist.Add(new { category = "其他", sum = sumofother });
        //            //catlist.RemoveAll(i => i.sum < compare);
        //            //获得当前表格月份
        //            DateTime SeriesMonth = everydt.Rows[0].Field<DateTime>(name1);
        //            string SeriesName = SeriesMonth.Year.ToString() + "." + SeriesMonth.Month.ToString();
        //            SumDt.Columns.Add(SeriesName);
        //            //SumDt.Columns[SeriesName].DataType = Type.GetType("System.Double");
        //            //将分类统计的list整理为datatable
        //            //扩展表格 行扩展
        //            catlist.ForEach(i => {
        //                if (!Category.Contains(i.category))
        //                { SumDt.Rows.Add(i.category); Category.Add(i.category); }
        //            });
        //            //将数据放入表格
        //            foreach (var item in catlist)
        //            {
        //                string c = item.category;
        //                double d = item.sum;
        //                for (int i = 0; i < SumDt.Rows.Count; i++)
        //                {
        //                    if (SumDt.Rows[i][0].ToString() == c)
        //                        SumDt.Rows[i][SeriesName] = d;

        //                }
        //            }

        //        }
        //        //没有数据的内容设置为0，插入统计行
        //        SumDt.Columns.Add("AllMonth", Type.GetType("System.Double"));

        //        for (int i = 0; i < SumDt.Rows.Count; i++)
        //        {
        //            double d = 0;
        //            for (int j = 1; j < SumDt.Columns.Count; j++)
        //            {
        //                if (SumDt.Rows[i][j].GetType() == typeof(System.DBNull))
        //                    SumDt.Rows[i][j] = 0;
        //                d += Convert.ToDouble(SumDt.Rows[i][j]);
        //            }
        //            SumDt.Rows[i]["AllMonth"] = d;
        //        }


        //        //按列的总和排序,排到一定数目就合并
        //        SumDt.DefaultView.Sort = "AllMonth DESC";//按AllMonth倒序
        //        //合并小项目
        //        for (int j = 1; j < SumDt.Columns.Count; j++)
        //        {
        //            double sum = 0;
        //            for (int i = maxitem; i < SumDt.Rows.Count; i++)
        //            {
        //                sum += Convert.ToDouble(SumDt.Rows[i][j]);
        //            }
        //            SumDt.Rows[maxitem][j] = sum;
        //        }
        //        //移除小项目对应的列
        //        do
        //        {
        //            int i = SumDt.Rows.Count - 1;
        //            SumDt.Rows.RemoveAt(i);
        //        } while (SumDt.Rows.Count > maxitem + 1);

        //        SumDt.Rows[maxitem][0] = "其他";
        //        dataGridView1.DataSource = SumDt.Translate();
        //        //移除统计列
        //        SumDt.Columns.RemoveAt(SumDt.Columns.Count - 1);

        //        foreach (DataRow row in SumDt.Rows)
        //        {

        //                Series dataTable1Series = new Series(row[0].ToString());
        //                dataTable1Series.Points.DataBind(row, "Category", item.ColumnName, "");
        //                dataTable1Series.XValueType = ChartValueType.String; //设置X轴类型为时间
        //                dataTable1Series.ChartType = SeriesChartType.Column;  //设置Y轴为饼状图
        //                dataTable1Series["PieLabelStyle"] = "Outside";
        //                dataTable1Series["PointWidth"] = "1";
        //                dataTable1Series.LegendText = item.ColumnName;

        //                dataTable1Series.ToolTip = "#SERIESNAME:#VALY";
        //                dataTable1Series.Label = "";

        //                dataTable1Series.IsValueShownAsLabel = false;


        //                ChartSumDays.Series.Add(dataTable1Series);


        //        }
        //        ChartSumDays.Series[0].Color = Color.Red;
        //        ChartSumDays.Series[ChartSumDays.Series.Count - 1].Color = Color.Blue;
        //        ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        //        ChartSumDays.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Auto;
        //        ChartSumDays.ChartAreas[0].AxisY.Maximum = 2000;
        //        ChartSumDays.ChartAreas[0].AxisY.Minimum = 0;
        //        ChartSumDays.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
        //        ChartSumDays.ChartAreas[0].AxisY.Title = "s\nu\nm";
        //        ChartSumDays.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
        //        ChartSumDays.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
        //        ChartSumDays.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //        ChartSumDays.ChartAreas[0].AxisX.Minimum = 0;
        //        ChartSumDays.ChartAreas[0].AxisX.Maximum = SumDt.Rows.Count+1;
        //        ChartSumDays.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
        //        ChartSumDays.Legends[0].Docking = Docking.Top;
        //        tabsum.DataSource = new DataTable();
        //        ChartSplitContainer.SplitterDistance = 1;
        //    }
        //    SSelectMonth = td;
        //}

        public void DrawChartSumOfMoney(DataTable dt)
        {
            tabsum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            ChartSplitContainer.SplitterDistance = 510;
            if (dt != null)
            {
                
                var temp = dt.Clone();
                ChartSumDays.Series.Clear();
                 //生成多个系列
                 foreach(DataColumn column in dt.Columns)
                {
                    if(column.DataType==typeof(double))
                    {
                        Series dataTable1Series = new Series(column.ColumnName);

                        dataTable1Series.Points.DataBind(dt.AsEnumerable(), "datadate", column.ColumnName, "");
                        dataTable1Series.XValueType = ChartValueType.DateTime; //设置X轴类型为时间
                        
                        dataTable1Series.ChartType = SeriesChartType.Line;  //设置Y轴为折线
                        dataTable1Series.LegendText = column.ColumnName.Translate();
                        if (column.ColumnName == "sum")
                            dataTable1Series.LegendText = "总余额";
                        dataTable1Series.ToolTip = column.ColumnName+":"+ "#VALX" + ' ' + "#VALY";
                        ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                        ChartSumDays.Legends[0].Docking = Docking.Top;
                        ChartSumDays.ChartAreas[0].AxisX.Minimum = dt.AsEnumerable().Select(i => i.Field<DateTime>("datadate")).Min().ToOADate();
                        ChartSumDays.ChartAreas[0].AxisX.Maximum = dt.AsEnumerable().Select(i => i.Field<DateTime>("datadate")).Max().ToOADate();

                        ChartSumDays.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                        dataTable1Series.LegendText = column.ColumnName.Translate();

                        ChartSumDays.Series.Add(dataTable1Series);
                    }
                    
                }
                ChartSumDays.ChartAreas[0].AxisY.Maximum = dt.AsEnumerable().Select(i => i.Field<double>("sumofmoney")).Max();
                ChartSumDays.ChartAreas[0].AxisY.Minimum = 0;
                ChartSumDays.ChartAreas[0].AxisY.MajorGrid.Enabled=false ;
                ChartSumDays.ChartAreas[0].AxisY.Title = "余额";
                ChartSumDays.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;

    
                ChartSumDays.ChartAreas[0].Area3DStyle.Enable3D = false;
                ChartSumDays.ChartAreas[0].Area3DStyle.LightStyle = LightStyle.Realistic;
                ChartSumDays.ChartAreas[0].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(165)), ((System.Byte)(191)), ((System.Byte)(228)));
                ChartSumDays.ChartAreas[0].CursorX.AutoScroll = false;
                ChartSumDays.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
                ChartSumDays.ChartAreas[0].CursorX.IsUserEnabled = false;
                ChartSumDays.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                ChartSumDays.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
                ChartSumDays.ChartAreas[0].AxisX.ScaleView.Position = ChartSumDays.ChartAreas[0].AxisX.Minimum;
                ChartSumDays.ChartAreas[0].AxisX.ScaleView.Size = Math.Min(ChartSumDays.ChartAreas[0].AxisX.Maximum-ChartSumDays.ChartAreas[0].AxisX.Minimum ,10000);
                ChartSumDays.ChartAreas[0].AxisX.ScrollBar.ButtonColor = System.Drawing.Color.White;
                ChartSumDays.ChartAreas[0].AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
            }
        }
        /// <summary>
        /// 显示余额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecViewShow(object sender,EventArgs e)
        {
            var obj = sender as ToolStripMenuItem;
            //if(LastView.Equals(obj)&&LastViewMonth.Equals(ViewMonth)&&(!LastView.Equals(SDateViewToolStripMenuItem)))
            if(false)
            {
                Console.WriteLine("View Equal!");
            }
            else
            {
                var objowner = obj.OwnerItem as ToolStripMenuItem;
                foreach (ToolStripMenuItem item in objowner.DropDownItems)
                {
                    if (item.Equals(obj))
                        obj.Checked = true;
                    else
                    {
                        item.Checked = false;
                    }
                }
                ViewType = (SViewType)Enum.Parse(typeof(SViewType), obj.Tag.ToString());

                switch (ViewType)
                {
                    case SViewType.day:
                        DrawChartLine(SSelectTable(ViewMonth));
                        break;
                    case SViewType.pie:
                        DrawChartPie(SSelectTable("mycom", ViewMonth));
                        break;
                    case SViewType.column:
                        if (ViewMonth.Equals(SMonthAdd.Every))
                            DrawChartColumnEveryMonth();
                        else
                            DrawChartColumn(SSelectTable("mycom", ViewMonth));
                        break;
                    case SViewType.balance:
                        var tab = (this.Owner as Form1).CNASumOfMoney.table;
                        tabsum.DataSource = SSelectTable(tab, ViewMonth).Translate();
                        DrawChartSumOfMoney(SSelectTable(tab, ViewMonth));
                        break;
                    case SViewType.month:
                        if(ViewMonth.Equals(SMonthAdd.Every))
                            ShowMonthViewEvery();
                        else
                        {
                           
                            
                           ShowMonthView(obj.Name);
                        }
                        
                        break;
                    case SViewType.columnall:
                        if (ViewMonth.Equals(SMonthAdd.Every))
                            DrawChartColumnEveryMonth();
                        else
                            DrawChartColumn(SSelectTable("mycom", ViewMonth));
                        break;
                    case SViewType.swim:
                        DrawChartPoint(GetSportsData(SSelectTable("sports", ViewMonth)));
                        tabsum.DataSource = GetSportsData(SSelectTable("sports", ViewMonth));
                        break;
                    case SViewType.weight:
                        DrawChartLine(SSelectTable("body", ViewMonth));
                        tabsum.DataSource = (this.Owner as Form1).BodyWeight.table;
                        break;

                    case SViewType.His:
                        DrawChartHis(GetSportsData(SSelectTable("sports", ViewMonth)), SSelectTable("body", SMonthAdd.All));
                        tabsum.DataSource = (this.Owner as Form1).BodyWeight.table;
                        break;
                    default:
                        break;
                    
                }
               SSelectMonthCheck(ViewType, SSelectMonth);
                LastView = obj;
                LastViewMonth = ViewMonth;
            }
            
        }
        private void SecViewSelectMonth(object sender, EventArgs e)
        {
            var obj = sender as ToolStripMenuItem;
            var objowner = obj.OwnerItem as ToolStripMenuItem;
            ViewMonth = (SMonthAdd)Enum.Parse(typeof(SMonthAdd), obj.Tag.ToString());
            LastView.PerformClick();
        }
        enum SMonthAdd { Add=1,Minus=-1,Current=0,Last=2,All=3,Every=4,Currentyear=6,Lastyear=5,Nextyear=7,Customlize=8 };
        enum SViewType { day,pie,column,balance,month,columnall,swim,weight,His};
        private DataTable SSelectTable( SMonthAdd add)
        {
            if (SSelectMonth.Year==1) SSelectMonth = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
            //根据add决定选择的月份
            switch((int)add)
            {
                case 0: SSelectMonth = DateTime.Today.ThisMonth(); break;//当前月
                case 1: SSelectMonth = SSelectMonth.AddMonths(1); break;//下个月
                case -1: SSelectMonth = DateTime.Today.AddMonths(-1); break;//最近一月
                case 2: SSelectMonth  = SSelectMonth.AddMonths(-1); break;//上一个月
                case 3: 
                case 6: SSelectMonth = DateTime.Today.ThisMonth(); break;
                case 5:
                case 4:
                case 8:
                case 7:break;
            }
            //克隆原始表格
            var temp = (this.Owner as Form1).CNASum.table;
            var selecttab = temp.Clone();
            //选择特定的数据,数据内容为一个月内
            if ((int)add<3)
            {
                
                
                (from row
                         in temp.AsEnumerable()
                 where row.Field<DateTime>("datadate") >= SSelectMonth && row.Field<DateTime>("datadate") <= SSelectMonth.AddMonths(1)
                 orderby row.Field<DateTime>("datadate") descending
                 select row ).ToList().ForEach(i => selecttab.ImportRow(i));
            }
            //数据内容超过一个月,但不是全部
            //SSelectMonth修改为当天
            else if ((int)add>=4)
            {
                int diff = (int)add - 6;

                DateTime date1 = new DateTime(SSelectMonth.Year + diff, 1, 1);
                DateTime date2 = date1.AddYears(1).AddDays(-1);
                if (add == SMonthAdd.Customlize)
                {
                   
                    monthCalendar1.Visible = true;
                    date1 = monthCalendar1.SelectionStart;
                    date2 = monthCalendar1.SelectionEnd;
                }
                
                
                (from row
                         in temp.AsEnumerable()
                 where row.Field<DateTime>("datadate") >= date1 && row.Field<DateTime>("datadate") <= date2
                 orderby row.Field<DateTime>("datadate") descending
                 select row).ToList().ForEach(i => selecttab.ImportRow(i));
                SSelectMonth = date1;
            }
            else
            {
                selecttab = temp;
            }
            
           
            
            
            tabsum.DataSource = selecttab.Translate();
            SSelectMonth = SSelectMonth.ThisMonth();
            LabelSSelectMonth.Text = SSelectMonth.ToString();
            return selecttab;
        }
        private DataTable SSelectTable(string TableName,SMonthAdd add)
        {
            //选择日期
            if (SSelectMonth.Year == 1) SSelectMonth = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
            switch ((int)add)
            {
                case 0: SSelectMonth = DateTime.Today.ThisMonth(); break;//当前月
                case 1: SSelectMonth = SSelectMonth.AddMonths(1); break;//下个月
                case -1: SSelectMonth = DateTime.Today.AddMonths(-1); break;//最近一月
                case 2: SSelectMonth  = SSelectMonth.AddMonths(-1); break;//上一个月
                case 4: SSelectMonth = DateTime.Today.ThisMonth(); break;
                case 6: SSelectMonth = DateTime.Today.ThisMonth(); break;
                case 5:
                case 3:
                case 7:
                case 8:break;
                
            }

            //获取mycomsumption表
            DataTable temp;
            if ((this.Owner as Form1).AllTables.Exists(i => i.tablenames.Contains(TableName)))
                temp = (this.Owner as Form1).AllTables.Find(i => i.tablenames.Contains(TableName)).table;
            else 
            temp = (this.Owner as Form1).BodyWeight.table;
            var selecttab = temp.Clone();
            if (add < SMonthAdd.All)
            {
                (from row
                         in temp.AsEnumerable()
                 where row.Field<DateTime>("datadate") >= SSelectMonth && row.Field<DateTime>("datadate") <SSelectMonth.AddMonths(1)
                 orderby row.Field<DateTime>("datadate") descending
                 select row).ToList().ForEach(i => selecttab.ImportRow(i));
            }
            else if(add>SMonthAdd.All) 
            {
                int diff = (int)add - 6;
                
                DateTime date1 = new DateTime(SSelectMonth.Year + diff, 1, 1);
                
                DateTime date2 = date1.AddYears(1).AddDays(-1);
                if (add == SMonthAdd.Customlize)
                {
                    
                    monthCalendar1.Visible = true;
                    date1 = monthCalendar1.SelectionStart;
                    date2 = monthCalendar1.SelectionEnd;
                }
               
                
                (from row
                         in temp.AsEnumerable()
                 where row.Field<DateTime>("datadate") >= date1 && row.Field<DateTime>("datadate") <= date2
                 orderby row.Field<DateTime>("datadate") descending
                 select row).ToList().ForEach(i => selecttab.ImportRow(i));
                
                SSelectMonth = date1;
                
            }
            else
            {
                selecttab = temp;
            }
            //更改时间
            SSelectMonth = SSelectMonth.ThisMonth();
            LabelSSelectMonth.Text = SSelectMonth.ToString();
            for (int i = 0; i < selecttab.Columns.Count; i++)
            {
                if(selecttab.Columns[i].DataType==Type.GetType("System.Double"))
                    for (int j = 0; j < selecttab.Rows.Count; j++)
                    {
                        selecttab.Rows[j][i] = Math.Round((double)selecttab.Rows[j][i], 2);
                    }
            }
            return selecttab;
        }
        private DataTable SSelectTable(DataTable stable, SMonthAdd add)
        {
            if (SSelectMonth.Year == 1) SSelectMonth = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
            switch ((int)add)
            {
                case 0: SSelectMonth = DateTime.Today.ThisMonth(); break;//当前月
                case 1: SSelectMonth = SSelectMonth.AddMonths(1); break;//下个月
                case -1: SSelectMonth = DateTime.Today.AddMonths(-1); break;//最近一月
                case 2: SSelectMonth = SSelectMonth.AddMonths(-1); break;//上一个月
                case 6:
                case 3: SSelectMonth = DateTime.Today.ThisMonth(); break;
            }
            var temp = stable;
            var selecttab = temp.Clone();
            if (add < SMonthAdd.All)
            {
                (from row
                         in temp.AsEnumerable()
                 where row.Field<DateTime>("datadate") >= SSelectMonth && row.Field<DateTime>("datadate") <= SSelectMonth.AddMonths(1)
                 orderby row.Field<DateTime>("datadate") descending
                 select row).ToList().ForEach(i => selecttab.ImportRow(i));
            }
            else if(add>SMonthAdd.All)
            {
                int diff = (int)add - 6;

                DateTime date1 = new DateTime(SSelectMonth.Year + diff, 1, 1);

                DateTime date2 = date1.AddYears(1).AddDays(-1);
                if (add == SMonthAdd.Customlize)
                {
                    
                   // monthCalendar1.Visible = true;
                    date1 = monthCalendar1.SelectionStart;
                    date2 = monthCalendar1.SelectionEnd;
                }
                
                
                (from row
                         in temp.AsEnumerable()
                 where row.Field<DateTime>("datadate") >= date1 && row.Field<DateTime>("datadate") <= date2
                 orderby row.Field<DateTime>("datadate") descending
                 select row).ToList().ForEach(i => selecttab.ImportRow(i));
                SSelectMonth = date1;
            }
            else
            {

                selecttab = temp;
            }
            SSelectMonth = SSelectMonth.ThisMonth();
            LabelSSelectMonth.Text = SSelectMonth.ToString();
            if(selecttab.Rows.Count!=0)
            return selecttab;
            else
            {
                return SSelectTable(stable, add);
            }
        }    
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            SelectDate = dateTimePicker1.Value;
            SelectMonth = SelectDate.ThisMonth();
        }
       private void SSelectMonthCheck(SViewType viewtype,DateTime sdate)
        {
            foreach (ToolStripMenuItem item in SSelectMonthToolStripMenuItem.DropDownItems)
            {
                if( item.Tag.ToString().Equals(SMonthAdd.Every.ToString()))
                {
                    if (viewtype == SViewType.column|| viewtype == SViewType.month)
                        item.Enabled = true;
                    else
                    {
                        item.Enabled = false;
                    }
                }
                else if (SSelectMonth.Month.Equals(DateTime.Today.Month)&& item.Tag.ToString().Equals(SMonthAdd.Add.ToString()))
                {
                    item.Enabled = false;
                }
                else if (SSelectMonth.Year.Equals(DateTime.Today.Year) && item.Tag.ToString().Equals(SMonthAdd.Nextyear.ToString()))
                {
                    item.Enabled = false;
                }
                else
                {
                    item.Enabled = true;
                }
            }
          
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            
            Console.WriteLine(monthCalendar1.SelectionStart.ToString());
            Console.WriteLine(monthCalendar1.SelectionEnd.ToString());
        }


        private DataTable GetSortTable(DataTable dt,bool isall=false)
        {
            string name1 = "";
            string name2 = "";
            foreach (DataColumn column in dt.Columns)
            {
                if (column.DataType == typeof(DateTime))
                    name1 = column.ColumnName;
                if (column.DataType == typeof(double))
                    name2 = column.ColumnName;
            }
            double sumofall = (double)dt.AsEnumerable().Sum(i => (double)i[name2]);
            var catlist = (from row
                           in dt.AsEnumerable()
                           where ((row["category"].ToString()!= "科研")&& (row["category"].ToString() != "还款")&&(row["payment"]).ToString()!="花呗")|| (row["note"].ToString() == "true")
                           group row by new { t1 = row.Field<string>("category") }
                           into m
                           
                           select new { category = m.Key.t1, sum = Math.Round(m.Sum(i => { return i.Field<double>(name2); }), 2), per = Math.Round(m.Sum(i => { return i.Field<double>(name2); }) / sumofall, 4) }).OrderByDescending(i => { return i.sum; }).ToList();
            if (false)
            {
                catlist.Remove(catlist.Find(i => i.category.Equals("科研")));
                catlist.Remove(catlist.Find(i =>  i.category.Equals("还款")));
                
            }
            sumofall = catlist.Sum(i => i.sum);
            if (isall)
            {
                var redt = new DataTable();
                DataColumn c1 = new DataColumn("category", catlist[0].category.GetType());
                redt.Columns.Add(c1);
                c1 = new DataColumn("sum", catlist[0].sum.GetType()); redt.Columns.Add(c1);
                c1 = new DataColumn("per", catlist[0].category.GetType()); redt.Columns.Add(c1);
                foreach (var item in catlist)
                {
                    var row = redt.NewRow();
                    row[0] = item.category;
                    row[1] = item.sum;
                    row[2] = Math.Round( item.sum/sumofall,4).ToString("P");
                    redt.Rows.Add(row);
                    
                }
                return redt;
            }
            else
            {
                double sumofleft = sumofall;
                if (catlist.Count <= 9) sumofleft = 0;
                else
                {

                    for (int i = 0; i < catlist.Count-1; i++)
                    {
                        sumofleft -= catlist[i].sum;
                        if (i - 1 < 0)
                            continue;
                        if (sumofleft <= catlist[i].sum)//|| sumofleft / sumofall<=0.05
                        {
                            //sumofleft -= Math.Round( catlist[i].sum,2);
                            break;
                        }
                    }

                }

                //foreach (var item in catlist)
                //{
                //    if (item.sum < sumofleft)
                //        sumofleft += item.sum;

                //}
                catlist.RemoveAll(i => i.sum < sumofleft);
                if (sumofleft != 0)
                    catlist.Add(new { category = "其他", sum = sumofleft, per = (sumofleft / sumofall) });

                var redt = new DataTable();
                DataColumn c1 = new DataColumn("category", catlist[0].category.GetType());
                redt.Columns.Add(c1);
                c1 = new DataColumn("sum", catlist[0].sum.GetType()); redt.Columns.Add(c1);
                c1 = new DataColumn("per", catlist[0].category.GetType()); redt.Columns.Add(c1);
                foreach (var item in catlist)
                {
                    var row = redt.NewRow();
                    row[0] = item.category;
                    row[1] = Math.Round( item.sum,2);
                    row[2] = Math.Round( item.sum/sumofall,4).ToString("P");
                    redt.Rows.Add(row);

                }
                return redt;
            }
           
            
        }

        private void buttonexport_Click(object sender, EventArgs e)
        {

            string path = @"G:\桌面\test\calander_xls\记录\test.xls";
            Export(tabsum.DataSource as DataTable ,  path);
        }

        private DataTable GetSportsData(DataTable Allsports)
        {
            
            DataTable sports = new DataTable("sports");
            sports.Columns.Add("datadate", Type.GetType("System.DateTime"));
            sports.Columns.Add("Category", Type.GetType("System.String"));
            sports.Columns.Add("Distance", Type.GetType("System.Double"));
            List<string> sportscat = new List<string>();
            for (int i = 0; i < Allsports.Rows.Count; i++)
            {
                string cat = Allsports.Rows[i][2].ToString();
                if (!sportscat.Contains(cat))
                    sportscat.Add(cat);
            }
            for (int k = 0; k < sportscat.Count; k++)
            {
                for (int i = 0; i < Allsports.Rows.Count; i++)
                {
                    string cat = Allsports.Rows[i][2].ToString();
                    if (cat != sportscat[k])
                        continue;
                    DateTime d = (DateTime)Allsports.Rows[i][1];
                    double dis = 0;
                    if (double.TryParse(Allsports.Rows[i][4].ToString(), out dis))
                    {
                        sports.Rows.Add(d, cat, dis);
                        
                    }
                }
            }
            return sports;

        }

        
    }
    
    

}
