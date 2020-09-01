using Spire.Xls;
using Spire.Xls.Core.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calander
{
    public partial class FormAddData : Form
    {
        public FormAddData()
        {
            InitializeComponent();
           
        }
        public FormAddData(DateTime date):this()
        {
            defaultdate = date;
            CurrentDate = date;
        }
        DateTime defaultdate = DateTime.Today;
        List<TextBox> textboxlist = new List<TextBox>();
        bool IsLoaded = false;
        DateTime CurrentDate = DateTime.Today;
        string SelectTable="";//选择的表，添加的数据到这里
        private List<ToolStripMenuItem> TableList = new List<ToolStripMenuItem>();
        private List<Panel> AddPanel = new List<Panel>();
        private void FormAddData_Load(object sender, EventArgs e)
        {
            LabTableName.Text = (this.Owner as Form1).myConnection.CurrentTable.Translate();
            My_Init();
        }
        /// <summary>
        /// form的初始化
        /// </summary>
        private void My_Init()
        {
            
            ///给combox添加选项
            ///默认为com表
            foreach (var con in this.Controls)
            {
                if (con is Label)
                    (con as Label).Text = (con as Label).Text.Translate();
            }
            foreach (var con in panel1.Controls)
            {
                if (con is Label)
                    (con as Label).Text = (con as Label).Text.Translate();
            }
            //for (int i = 2010; i < 2030; i++)
            //{
            //    combox_add_y.Items.Add(i);
            //}
            //for (int i = 1; i <= 12; i++)
            //{
            //    combox_add_m.Items.Add(i);                
            //}
            //for (int i = 1; i <32; i++)
            //{
            //    combox_add_d.Items.Add(i);
            //}
            combox_add_y.SelectedItem = combox_add_y.Items[defaultdate.Year-2018];
            combox_add_m.SelectedItem = combox_add_m.Items[defaultdate.Month-1];
            combox_add_d.SelectedItem = combox_add_d.Items[defaultdate.Day-1];

            //foreach (DataRow row in (this.Owner as Form1).CNA.table.Rows)
            //{
            //    combox_category.Items.distinctadd(row["category"]);
            //    combox_item.Items.distinctadd(row["item"]);
            //}
            TableList.Clear();
             foreach (var item in (this.Owner as Form1).myConnection.TablesInSelectedDataBase)
            {
                 if(!item.Contains("sumof"))
                 {
                    ToolStripMenuItem newitem=new ToolStripMenuItem();
                     newitem.Name=item;
                     newitem.Text=item.Translate();
                     newitem.Click += ItemClick;
                    tablesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {newitem});
                    TableList.Add(newitem);
                    if (item.Contains("comsumption"))
                    {
                        newitem.Checked = true;
                        SelectTable = LabTableName.Text = item;
                        
                        newitem.PerformClick();
                        
                    }
                     
                 } 
            }
            IsLoaded = true;
            textboxlist.Add(alipay);
            textboxlist.Add(wechat);
            textboxlist.Add(campuscard);
            textboxlist.Add(others);
            textboxlist.Add(cash);
            textboxlist.Add(debitcard);
            combox_category.Focus();
            
        }


        protected void combox_enter_handler(object sender, KeyEventArgs e)
        {
           
            if(e.KeyCode==Keys.Enter)
            {
               
                 ((ComboBox)sender).Items.distinctadd(((ComboBox)sender).Text);
                ((ComboBox)sender).SelectedItem = ((ComboBox)sender).Text;
                SendKeys.Send("{TAB}");
                //MessageBox.Show(e.KeyCode.ToString());
            }
            
           
        }
       /// <summary>
       /// 更新按钮
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        public void butupdate_Click(object sender, EventArgs e)
        {
            if(!SelectTable.Contains("sumof")&&(!SelectTable.Contains("body")))
            {
                try
                {
                    var owner = this.Owner as Form1;
                    //找到表
                    CmdAndAdp cna = owner.AllTables.Find(i => { return i.tablenames == SelectTable; });
                    DataRow dr = cna.table.NewRow();
                    ///数据匹配
                    ///
                    DateTime date = new DateTime(int.Parse(combox_add_y.SelectedItem.ToString()), int.Parse(combox_add_m.SelectedItem.ToString()), int.Parse(combox_add_d.SelectedItem.ToString()));
                    dr["datadate"] = date;
                    if (combox_category.Visible)
                    {
                        dr["category"] = combox_category.SelectedItem.ToString();
                    }
                    if (combox_item.Visible)
                    {
                        dr["item"] = combox_item.SelectedItem.ToString();
                    }

                    string columnname = "";
                    if (cna.tablenames.Contains("sport"))
                    {
                        columnname = "time";
                    }

                    else
                    {
                        foreach (DataColumn column in cna.table.Columns)
                        {
                            if (column.DataType == typeof(double))
                            {
                                columnname = column.ColumnName;
                                dr[columnname] = double.Parse(text_comsume.Text);
                                break;
                            }
                        }
                    }
                    int countmax = 0;

                    //if (cna.table.Columns.Contains("datacount"))
                    //{
                    //    var restult = from t
                    //               in cna.table.AsEnumerable()
                    //                  select t.Field<int>("datacount");
                    //    var reslist = restult.ToList();
                    //    if (reslist.Count != 0)
                    //        countmax = reslist.Max();
                    //    else
                    //        countmax = 0;
                    //}

                    if (cna.table.Columns.Contains("datacount"))
                        dr["datacount"] = countmax + 1;
                    if (cna.table.Columns.Contains("payment"))
                        dr["payment"] = textBoxpayment.Text;
                    if (cna.table.Columns.Contains("note"))
                        dr["note"] = textBoxnote.Text;
                    if (cna.table.Columns.Contains("writer"))
                        columnname = "writer";
                    if(columnname!="")
                        dr[columnname] = text_comsume.Text;
                    cna.table.Rows.Add(dr);

                    
                    this.dataGridView1.DataSource = owner.myConnection.ShowDayData(date, owner.AllTables).Translate();


                    //更新报销记录表
                    if(cna.tablenames.Contains("mycom"))
                    {
                        if (dr["category"].ToString().Equals("科研"))
                            if(MessageBox.Show("是否记录？", "记录", MessageBoxButtons.YesNo)==DialogResult.Yes)
                            {
                                Workbook workbook = new Workbook();
                                workbook.LoadFromFile(@"F:\财务\个人汇总.xlsx");
                                Worksheet worksheet = workbook.Worksheets[0];
                                //worksheet.InsertRow(worksheet.Rows.Count());
                                int row = worksheet.LastRow+1;
                                //worksheet 计数从1开始
                                DateTime dd = (DateTime)dr["datadate"];
                                worksheet.SetCellValue(row, 1, dd.ToString("d"));
                                worksheet.SetCellValue(row, 2, dr["item"].ToString());
                                worksheet.SetCellValue(row, 3, dr[columnname].ToString());
                                worksheet.SetCellValue(row, 7," " );
                                worksheet.SetCellValue(row, 8, "苏宇杰");
                                worksheet.SetCellValue(row, 10, dr["payment"].ToString());
                                workbook.Save();
                                workbook.Dispose();
                            }                     
                    }

                    else if (dr["category"].ToString().Equals("报销")&&(cna.tablenames.Contains("income")))
                        if (MessageBox.Show("是否记录？", "记录", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Workbook workbook = new Workbook();
                            workbook.LoadFromFile(@"F:\财务\个人汇总.xlsx");
                            Worksheet worksheet = workbook.Worksheets[0];
                            double money = (double)dr["income"];
                            int row = worksheet.LastRow;
                            List<int> count = new List<int>();
                            List<double>mom=new List<double>();
                            for (int i = row; i >=4 ; i--)
                            {
                                if (worksheet.Range[i, 11].Value2.ToString().Equals("")&& worksheet.Range[i, 12].Value2.ToString().Equals("是")&& !worksheet.Range[i, 3].Value2.ToString().Equals(""))
                                {
                                    count.Add(i);
                                    mom.Add((double)worksheet.Range[i, 3].Value2);
                                }
                            }
                           
                            //共有2的N次方种可能
                            int N = (int)Math.Pow(2, count.Count);
                            for (int i = 0; i < N; i++)
                            {
                                double sum = 0;
                                for (int j = 0; j < count.Count; j++)
                                {
                                    sum += mom[j] * ((i>>j)&0x01);
                                }
                                if (Math.Abs(sum-money)<=0.01)
                                {
                                    List<int> ans = new List<int>();
                                    Console.WriteLine("found it");
                                    //找到了，将i分解
                                    for (int j = 0; j < count.Count; j++)
                                    {
                                        if (((i >> j) & 0x01) == 1)
                                            ans.Add(j);
                                    }

                                    if (ans.Count > 0)
                                    {
                                        string s = "";
                                        foreach (var item in ans)
                                        {
                                            int num = count[item];
                                            s += (worksheet.Range[num, 2].Value2.ToString());

                                            s += "金额:" + (worksheet.Range[num, 3].Value2.ToString());

                                            s += "\r\n";
                                        }
                                        if(MessageBox.Show("找到了\r\n" + s, "是否写入", MessageBoxButtons.YesNo)==DialogResult.Yes)
                                        {
                                            foreach (var item in ans)
                                            {
                                                worksheet.SetCellValue(count[item], 11, "是");
                                            }
                                            break;

                                        }
                                    }

                                }
                                    
                            }
                            
                            //worksheet.InsertRow(worksheet.Rows.Count() + 1);
                            //int row = worksheet.LastRow;
                            ////worksheet 计数从1开始
                            //worksheet.SetCellValue(row, 1, dr["datadate"].ToString());
                            //worksheet.SetCellValue(row, 2, dr["item"].ToString());
                            //worksheet.SetCellValue(row, 3, dr[columnname].ToString());
                            //worksheet.SetCellValue(row, 7, "淘宝");
                            //worksheet.SetCellValue(row, 8, "苏宇杰");
                            workbook.Save();
                            workbook.Dispose();

                        }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if(SelectTable.Contains("money"))
            {
                bool flag = true;
                var cna = (this.Owner as Form1).CNASumOfMoney;
                var row = cna.table.NewRow();
                if ((DateTime)cna.table.Rows[cna.table.Rows.Count - 1]["datadate"] == DateTime.Today)
                {
                    flag = false;
                    row = cna.table.Rows[cna.table.Rows.Count - 1];
                }
                    


                UInt32 countmax = 0;
                if (cna.table.Columns.Contains("datacount"))
                {
                    var type = cna.table.Columns["datacount"].DataType;
                    var restult = from t
                               in cna.table.AsEnumerable()
                                  select t.Field<UInt32>("datacount");
                    var reslist = restult.ToList();
                    if (reslist.Count == 0)
                        countmax = 0;
                    else 
                    countmax = reslist.Max();
                }

                row["datacount"] = countmax + 1;
                row["datadate"] = DateTime.Today;
                row["alipay"] = double.Parse(alipay.Text);
                row["wechat"] = double.Parse(wechat.Text);
                row["debitcard"] = double.Parse(debitcard.Text);
                row["cash"] = double.Parse(cash.Text);
                row["campuscard"]= double.Parse(campuscard.Text);
                row["others"] = double.Parse(others.Text);
                row["sumofmoney"] = Math.Round((double)row["alipay"] +(double)row["wechat"] + (double)row["debitcard"] + (double)row["cash"] + (double)row["others"]+ (double)row["campuscard"],2);
                if (flag)
                    cna.table.Rows.Add(row);
                
            }
            else if(SelectTable.Contains("body"))
            {
                bool flag = true;
                var cna = (this.Owner as Form1).BodyWeight;
                var row = cna.table.NewRow();
                if ((DateTime)cna.table.Rows[cna.table.Rows.Count - 1]["datadate"] == DateTime.Today)
                {
                    flag = false;
                    row = cna.table.Rows[cna.table.Rows.Count - 1];
                }

                row["datadate"] = DateTime.Today;
                row["weight"] = bodyweighttextbox.Text;
              if (flag)
                    cna.table.Rows.Add(row);
            }
            combox_category.Focus();
            
        }
        private void buttonUpDate_Click(object sender, EventArgs e)
        {
            (this.Owner as Form1).UpdateClick();
        }


        private void combox_item_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!combox_category.Visible)
                return;
            string cat = combox_category.SelectedItem.ToString();
            string item = combox_item.SelectedItem.ToString();
            var temptab = (this.Owner as Form1).AllTables.Find(i => i.tablenames.Equals(SelectTable)).table;
            if(temptab.Columns.Contains("Table"))temptab.Columns.Remove("Table");
            string name1 = temptab.Columns[2].ColumnName;
            string name2 = temptab.Columns[3].ColumnName;
            string name3 = temptab.Columns[4].ColumnName;
            
            var type = temptab.Rows[0][4].GetType();
            if(type.FullName.ToLower().Equals("system.double"))
            {
                var list = (from t
                      in temptab.AsEnumerable()
                            where t.Field<string>(name1).Equals(cat) && t.Field<string>(name2).Equals(item)&&t.Field<DateTime>("datadate")>=DateTime.Today.AddMonths(-2)
                            group t
                            by t.Field<double>(name3)
                       into row
                            orderby row.Count()
                            select new { item = row.Key, count = row.Count() }).ToList();
                if (list.Count == 0)
                    return; 
                list.Reverse();
                text_comsume.Text = list[0].item.ToString();
            }
            else if (type.FullName.ToLower().Equals("system.string"))
            {
                var list = (from t
                      in temptab.AsEnumerable()
                            where t.Field<string>(name1).Equals(cat) && t.Field<string>(name2).Equals(item)
                            group t
                            by t.Field<string>(name3)
                       into row
                            orderby row.Count()
                            select new { item = row.Key, count = row.Count() }).ToList();
                list.Reverse();
                if(list.Count>0)
                text_comsume.Text = list[0].item.ToString();
            }
            
            

        }

        private void combox_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            combox_item.Items.Clear();
            try
            {
                //得到category后，整理得到相应的值
                string s = combox_category.SelectedItem.ToString();
                var temptab = (this.Owner as Form1).AllTables.Find(i => i.tablenames.Equals(SelectTable)).table;
                var list = (from t
                           in temptab.AsEnumerable()
                            where t.Field<string>("category").Equals(s) && t.Field<DateTime>("datadate")> CurrentDate.AddMonths(-2)
                            group t
                            by t.Field<string>("item")
                          into row 
                            orderby row.Count()
                           select new { item=row.Key, count=row.Count() }).ToList();
                list.Reverse();
                list.ForEach(i => { if (i.count >= list.Sum(j=>j.count)/30) combox_item.Items.distinctadd(i.item); });
                
                combox_item.SelectedItem = list[0].item;
                //foreach (var row in (this.Owner as Form1).CNA.table.Select("category='" + s + "'"))
                //{
                //    combox_category.Items.distinctadd(row["category"]);
                //    combox_item.Items.distinctadd(row["item"]);
                    
                //}
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            
        }

        private void combox_add_date_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsLoaded)
            {

                int year = int.Parse(combox_add_y.SelectedItem.ToString());
                int month = int.Parse(combox_add_m.SelectedItem.ToString());
                int day = int.Parse(combox_add_d.SelectedItem.ToString());
                DateTime date = new DateTime(year, month, day);
               
                CurrentDate = date;
                this.dataGridView1.DataSource = (this.Owner as Form1).myConnection.ShowDayData(date, (this.Owner as Form1).AllTables).Translate();
            }
        
        }

        private void tablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private class MyEventArgs<T>:EventArgs
        {
            public MyEventArgs(T t)
            {
                data=t;
            }
            public T data{get;set;}
        }
       // EventHandler<MyEventArgs<string>> ItemClick;


        /// <summary>
        /// 选中表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemClick(object sender, EventArgs e)
        {
            var owner = (this.Owner as Form1);
            SumOfMoneyToolStripMenuItem.Checked = false;
            panel1.Visible = false;
            panel2.Visible = false;
            ToolStripMenuItem Menu = sender as ToolStripMenuItem;   
            TableList.ForEach(i => i.Checked = false);
            Menu.Checked = true;
            
            SelectTable = LabTableName.Text = Menu.Name;
            LabTableName.Text = Menu.Name.Translate();
            var list = (this.Owner as Form1).AllTables;
            //Console.WriteLine(list.Find(i => { return i.tablenames == munu.Name; }));
            
            CmdAndAdp cna =list.Find(i => { return i.tablenames == Menu.Name; });
            this.dataGridView1.DataSource = owner.myConnection.ShowDayData(CurrentDate, owner.AllTables).Translate();//78ms
            if (dataGridView1.Columns.Contains("expense"))
            {
                dataGridView1.Columns["expense"].SortMode = DataGridViewColumnSortMode.Programmatic;
                CustomSort("expense", "desc");
            }
            if (dataGridView1.Columns.Contains("金额"))
            {
                dataGridView1.Columns["金额"].SortMode = DataGridViewColumnSortMode.Programmatic;
                CustomSort("金额", "desc");
            }
                
            LoadSelectTable(cna);
            
        }
        /// <summary>
        /// 选择的表修改以后重载combobox
        /// </summary>
        /// <param name="cna"></param>
        private void LoadSelectTable(CmdAndAdp cna)
        {
            LoadSelectTable(cna.table);
        }
        private void LoadSelectTable(DataTable table)
        {
            if (!table.Columns.Contains("category"))//不存在'category'列
            {
                combox_category.Visible = false;
                label1.Visible = false;
            }
            else
            {
                combox_category.Visible = true;
                combox_category.Items.Clear();
                
                var list =(from t 
                          in table.AsEnumerable()
                          where t.Field<DateTime>("datadate")>CurrentDate.AddDays(-180)
                          group t 
                          by new{t1=t.Field<string>("category")}
                          into m
                          select new{cat =m.Key.t1,count=m.Count()}
                          
                          ).OrderByDescending(i=>i.count).ToList();
                foreach (var item in list)
                {
                    combox_category.Items.distinctadd(item.cat);
                    if (combox_category.Items.Count >= 10)
                        break;
                }
                //list.ForEach(i => { if (i.count > list.Sum(j => j.count) / 30)combox_category.Items.(i.cat); });//46ms
                label1.Visible = true;
            }

            if (!table.Columns.Contains("item"))
            {
                combox_item.Visible = false;
            }
            else
            {
                combox_item.Visible = true;
                combox_item.Items.Clear();
                //foreach (DataRow row in table.Rows)
                //{
                //    combox_item.Items.distinctadd(row["item"]);
                //} 
            }
            if (!table.Columns.Contains("payment"))
            {
                textBoxpayment.Visible = false;
                label3.Visible = false;
            }
            else
            {
                textBoxpayment.Visible = true;
                label3.Visible = true;
            }
            if (!table.Columns.Contains("note"))
            {
                textBoxnote.Visible = false;
                label4.Visible = false;
            }
            else
            {
                textBoxnote.Visible = true;
                label4.Visible = true;
            }

           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonlastday_Click(object sender, EventArgs e)
        {
            int year = int.Parse( combox_add_y.SelectedItem.ToString());
            int month = int.Parse(combox_add_m.SelectedItem.ToString());
            int day = int.Parse(combox_add_d.SelectedItem.ToString());
            DateTime date = new DateTime(year, month, day);
            var newdate = date.AddDays(-1);
            this.combox_add_y.SelectedIndexChanged -= new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            this.combox_add_m.SelectedIndexChanged -= new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            this.combox_add_d.SelectedIndexChanged -= new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            combox_add_y.SelectedItem = combox_add_y.Items[newdate.Year - 2018];
            combox_add_d.SelectedItem = combox_add_d.Items[newdate.Day - 1];
            combox_add_m.SelectedItem = combox_add_m.Items[newdate.Month - 1];
            combox_add_date_SelectedIndexChanged(sender, e);
            this.combox_add_y.SelectedIndexChanged += new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            this.combox_add_m.SelectedIndexChanged += new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            this.combox_add_d.SelectedIndexChanged += new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
        }

        private void buttonnextday_Click(object sender, EventArgs e)
        {
            int year = int.Parse(combox_add_y.SelectedItem.ToString());
            int month = int.Parse(combox_add_m.SelectedItem.ToString());
            int day = int.Parse(combox_add_d.SelectedItem.ToString());
            DateTime date = new DateTime(year, month, day);
            var newdate = date.AddDays(1);
            this.combox_add_y.SelectedIndexChanged -= new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            this.combox_add_m.SelectedIndexChanged -= new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            this.combox_add_d.SelectedIndexChanged -= new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            combox_add_y.SelectedItem = combox_add_y.Items[newdate.Year - 2018];
            combox_add_d.SelectedItem = combox_add_d.Items[newdate.Day - 1];
            combox_add_m.SelectedItem = combox_add_m.Items[newdate.Month - 1];
            combox_add_date_SelectedIndexChanged(sender, e);
            this.combox_add_y.SelectedIndexChanged += new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            this.combox_add_m.SelectedIndexChanged += new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
            this.combox_add_d.SelectedIndexChanged += new System.EventHandler(this.combox_add_date_SelectedIndexChanged);
        }
        /// <summary>
        /// 选中余额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SumOfMoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            TableList.ForEach(i => i.Checked = false);
            SumOfMoneyToolStripMenuItem.Checked = true;
            panel1.Visible = true;
            panel2.Visible = false;
            SelectTable = (this.Owner as Form1).CNASumOfMoney.tablenames;
            var tab= (this.Owner as Form1).CNASumOfMoney.table;
            tab.DefaultView.Sort = "datadate desc";
            tab = tab.DefaultView.ToTable();
            dataGridView1.DataSource = tab.Translate();
            var row = tab.AsEnumerable().First();
            textboxlist.ForEach(i => i.Text = row[i.Name].ToString());

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormAddData_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine(e.KeyChar);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonUpDate.PerformClick();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonUpDate.PerformClick();
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
        Func<DataRow, string, decimal> comparer = (DataRow p1, string columnname) =>
        {
            decimal k;
            if (!decimal.TryParse(p1[columnname].ToString(), out k)) return 0;
            return Convert.ToDecimal(p1[columnname]);
        };
        public void CustomSort(string columnBindingName, string sortMode)
        {
            DataTable dt = this.dataGridView1.DataSource as DataTable;
            DataView dv = dt.DefaultView;
            List<int> list = new List<int>();
            if (dt.Rows.Count == 0)
                return;
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
                    dtsort = dt.Rows.Cast<DataRow>().OrderBy(i => comparer(i, columnname)).CopyToDataTable();
                //dtsort = dt.Rows.Cast<DataRow>().OrderBy(delegate(DataRow p1,DataRow p2) { return Convert.ToDecimal(p1[columnname]) - Convert.ToDecimal(p2[columnname]); })).CopyToDataTable();
                if (sortMode == "desc")
                    dtsort = dt.Rows.Cast<DataRow>().OrderByDescending(i => comparer(i, columnname)).CopyToDataTable();
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
        //选中体重
        private void BodyWeight_Click(object sender, EventArgs e)
        {
            TableList.ForEach(i => i.Checked = false);
            BodyWeight.Checked = true;
            SumOfMoneyToolStripMenuItem.Checked = false;
            panel1.Visible = false;
            panel2.Visible = true;
            SelectTable = (this.Owner as Form1).BodyWeight.tablenames;
            var tab = (this.Owner as Form1).BodyWeight.table;
            tab.DefaultView.Sort = "datadate desc";
            tab = tab.DefaultView.ToTable();
            dataGridView1.DataSource = tab.Translate();
            var row = tab.AsEnumerable().First();
            bodyweighttextbox.Text = row[1].ToString();
        }

        
    }
}
