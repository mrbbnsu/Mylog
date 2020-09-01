using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using Spire.Xls;
using System.IO;
namespace calander
{
    public  class CMysqlConnectoor
    {
        public MySqlConnection conn;
        public DataTable data;
        public DataTable sumofday;
        public MySqlDataAdapter da;
        public MySqlCommandBuilder cb;
        public string CurrentTable = "";
        public List<string> AllDataBase = new List<string>();
        public string CurrentDataBase = "";
        public List<string> TablesInSelectedDataBase = new List<string>();
        public List<CmdAndAdp>AllTabCNA=new List<CmdAndAdp>();
        public CmdAndAdp DayDataView = new CmdAndAdp();
        /// <summary>
        /// 连接默认数据库
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            string myPassword = "123456";
            bool MysqlConnectionInfo = false;
            string connStr = String.Format("server={0};port=3306;user id={1}; password={2}; database=mylog; pooling=false",
            "rm-bp1u45y587y51559lfo.mysql.rds.aliyuncs.com", "abc", myPassword);
            
                conn = new MySqlConnection(connStr);
                if(conn.State==ConnectionState.Closed)
                 conn.Open();
                MysqlConnectionInfo = true;
            
            //catch (MySqlException ex)
            //{
            //    MysqlConnectionInfo = false;
            //    MessageBox.Show("Error connecting to the server: " + ex.Message);
            //    conn = null;
            //}
            data = new DataTable();
            //var tempstrlist=this.GetDatabases();
            this.GetTables();
            DayDataView = this.SelectTable("DayDataView");//时间长 104ms
            return MysqlConnectionInfo;
        }
        /// <summary>
        /// 获得数据库中的database
        /// </summary>
        public List<string> GetDatabases()
        {
            MySqlDataReader reader = null;//读取command返回值的
         //   if(conn.State==ConnectionState.Closed)
            
            using ( MySqlCommand cmd = new MySqlCommand("SHOW DATABASES", conn))
            {
                try
                {
                   
                    reader = cmd.ExecuteReader();
                    AllDataBase.Clear();
                    while (reader.Read())
                    {
                        AllDataBase.Add(reader.GetString(0));
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Failed to populate database list: " + ex.Message);
                }
                finally
                {
                    if (reader != null) reader.Close();
                }
            }
            
            return AllDataBase;
        }

        public void CononectionClose()
        {
            conn.Close();
        }

        public bool Select_DataBase(string s)
        {
            
            //if (this.AllDataBase.Any(i => { return i == s; }))
            //{
                
                //conn.ChangeDatabase(s);
                CurrentDataBase = s;
                var temp=this.GetTables();
                return true;
            //}
            //return false;
        }

        public List<string>GetTables()
        {
            MySqlDataReader reader = null;//读取command返回值的
            using (MySqlCommand cmd = new MySqlCommand("SHOW TABLES;", conn))
            {
                try
                {
                    reader = cmd.ExecuteReader();
                    TablesInSelectedDataBase.Clear();
                    while (reader.Read())
                    {
                        if(!reader.GetString(0).ToLower().Contains("view"))
                        TablesInSelectedDataBase.Add(reader.GetString(0));
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Failed to populate table list: " + ex.Message);
                }
                finally
                {
                    if (reader != null) reader.Close();
                }
            }

            return TablesInSelectedDataBase;
        }

        /// <summary>
        /// 获取一个database中所有表的名字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<string>GetTables(string s)
        {
            List<string> templist = new List<string>();
            string temp = CurrentDataBase;
            Select_DataBase(s);
           templist=GetTables();
           Select_DataBase(temp);
           return templist;
        }
        /// <summary>
        /// 显示某一个表
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public DataTable ShowTables(string tab)
        {
            if (this.TablesInSelectedDataBase.Any(i => { return i == tab; }))
            {
                da = new MySqlDataAdapter("SELECT * FROM " + tab, conn);
                cb = new MySqlCommandBuilder(da);
                da.Fill(data);
                CurrentTable = tab;
            }
            else
                data = null;
            return data;
        }
        /// <summary>
        /// 选择某一个表
        /// </summary>
        /// 
        public CmdAndAdp SelectTable(string tab,bool isFull=true)
        {
            //if (tab.Contains("View")|| tab.Contains("view")) return null;
                
            CmdAndAdp cna = new CmdAndAdp();
            CmdAndAdp tempcna = new CmdAndAdp();
            //if (this.TablesInSelectedDataBase.Any(i => { return i == tab; }))//验证存在
            if(true)
            {
                string[] nolist= { "debt","sumofmonth"};
                string tempcmd = @"select COLUMN_NAME from information_schema.columns where table_name = '" + tab + "'";
                tempcna.da = new MySqlDataAdapter(tempcmd, conn);
                tempcna.cb = new MySqlCommandBuilder(tempcna.da);
                tempcna.da.Fill(tempcna.table);
                string useless = " where DATE_SUB(CURDATE(), INTERVAL 30 DAY) <= date(datadate)";
                string cmd = "  ";
                string columnnanecmd = "";
                for (int i = 0; i < tempcna.table.Rows.Count-1; i++)
                {
                    columnnanecmd += tempcna.table.Rows[i][0].ToString()+",";
                }
                columnnanecmd += tempcna.table.Rows[tempcna.table.Rows.Count-1][0].ToString() ;
                if (nolist.Contains(tab))
                    cmd = "";
                cna.tablenames = tab;
                columnnanecmd = " * ";
                string cnacmd = "SELECT " + columnnanecmd + " FROM " + tab + cmd;
                if(!isFull)
                    cnacmd = "SELECT " + columnnanecmd + " FROM " + tab + useless;
                cna.da = new MySqlDataAdapter(cnacmd, conn);
                cna.cb = new MySqlCommandBuilder(cna.da);
                cna.da.Fill(cna.table);
                
                cna.table.TableName = tab;
            }
            else
                data = null;
            return cna;
        }

        public CmdAndAdp SelectTable(string tab,DateTime date)
        {

            CmdAndAdp cna = new CmdAndAdp();
            //if (this.TablesInSelectedDataBase.Any(i => { return i == tab; }))//验证存在
            if (true)
            {
                string[] nolist = { "debt", "sumofmonth" };
                string tempcmd = @"select COLUMN_NAME from information_schema.columns where table_name = '" + tab + "'";
                
                string datestring = date.ToString("yyyy-MM-dd");
                string cmd = " where datadate  = \' "+ datestring+ " \' ";
           

                if (nolist.Contains(tab))
                    cmd = "";
                cna.tablenames = tab;
               
                string cnacmd = "SELECT * "  + " FROM " + tab + cmd;
                cna.da = new MySqlDataAdapter(cnacmd, conn);
                cna.cb = new MySqlCommandBuilder(cna.da);
                cna.da.Fill(cna.table);

                cna.table.TableName = tab;
            }
            else
                data = null;
            return cna;
        }

        /// <summary>
        /// 获取database所有的表，表示成datatable
        /// </summary>
        public List<CmdAndAdp>GetAllTables()
        {

            foreach(var tab in TablesInSelectedDataBase)
            {
                
                if((!tab.Contains("sum"))&& (!tab.Contains("debt")) && (!tab.Contains("body")) && (!tab.Contains("View")) && (!tab.Contains("TIME")) && (!tab.Contains("time")))
                {
                    CmdAndAdp cna = SelectTable(tab);
                  
                    AllTabCNA.Add(cna);
                }
            }
            return AllTabCNA;
        }
        public void UpdateData()
        {
            DataTable changes = data.GetChanges();
            try
            {
                da.Update(changes);
                data.AcceptChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UpdateData(CmdAndAdp cna)
        {
            DataTable changes = cna.table.GetChanges();
            if (changes != null)
            {
                try
                {
                    cna.da.Update(changes);
                    
                    cna.table.AcceptChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                   
                }
            }
        }
        public void CalSumOfDay()
        {
            sumofday = this.ShowTables("sumofday");
            DataTable changes = data.GetChanges();
            sumofday = this.CalSumOfDay(changes);
            

        }
        /// <summary>
        /// 根据记录计算这些记录中的和
        /// </summary>
        /// 每个日期对应一个记录
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataRow[] CalSumOfDay(DataRow[] rows)
        {
            //将行变成表的格式
            DataTable dt = new DataTable();
            dt.Columns.Add("date", typeof(DateTime));
            dt.Columns.Add("expense", typeof(string));
           
            foreach(DataRow row in rows)
            {
                dt.Rows.Add(row["datadate"], row["expense"]);
            }
            //对表排序
            dt.DefaultView.Sort = "datadate asc";
            dt = dt.DefaultView.ToTable();

            //找到不同的日子的数量
            int count=0;
            List<DateTime> list = new List<DateTime>();
            foreach (DataRow row in dt.Rows)
            {
                if(!list.Contains((DateTime)row["date"]))
                {
                    list.Add((DateTime) row["date"]);
                    count++;
                }
            }
            
           DataRow[] datarows=new DataRow[5];

            
           return datarows;
        }
        public DataTable CalSumOfDay(DataTable table)
        {
          
            //对表排序
            if(table.Columns.Contains("datadate"))
            {
                table.DefaultView.Sort = "datadate asc";
            }
            else if (table.Columns.Contains("dataday"))
            {
                table.DefaultView.Sort = "dataday asc";
            }
            
            table = table.DefaultView.ToTable();

            //找到不同的日子的数量
            
            List<DateTime> list = new List<DateTime>();
            List<double> sumlist = new List<double>();
            foreach (DataRow row in table.Rows)
            {
                if (!list.Contains((DateTime)row["datadate"]))//第一次遇到这个值
                {
                    //这时日期与sum是一一对应的
                    list.Add((DateTime)row["datadate"]);
                    sumlist.Add((double)row["expense"]);
                }
                else
                {
                    sumlist[sumlist.Count-1] += (double)row["expense"];
                }
            }
            
            DataTable result = new DataTable();
            result.Columns.Add("datadate", typeof(DateTime));
            result.Columns.Add("sum", typeof(double));
            for (int i = 0; i <list.Count; i++)
            {
                result.Rows.Add(list[i], sumlist[i]);
            }
            return result;
        }
      /// <summary>
      /// 由每日表和日汇总表得到新的日汇总表
      /// </summary>
      /// <param name="table1"></param>
      /// <param name="table2"></param>
      /// <returns></returns>
        public DataTable  CalSumOfDay(DataTable tabsum,DataTable tabday,DataTable tabincome)
        {
            DataTable tempsum = tabsum.Clone();
            string nameofdateintabday = "";
            string nameofexpenseintabday = "";
            //获得对应的列的名字
            foreach (DataColumn column in tabday.Columns)
            {
                if (column.DataType.Equals(typeof(DateTime)))
                    nameofdateintabday = column.ColumnName;
                if (column.DataType.Equals(typeof(double)))
                    nameofexpenseintabday = column.ColumnName;
            }
            if (nameofdateintabday != "" && nameofexpenseintabday != "")
            {
                //将日表复制到总表
                foreach (DataRow row in tabday.Rows)
                {
                    bool equal = false;
                    if (row["payment"].GetType() != typeof(System.DBNull))
                    {
                        byte[] utf81 = Encoding.UTF8.GetBytes((string)row["category"]);
                        byte[] utf82 = Encoding.UTF8.GetBytes("科研");
                        byte[] utf83 = Encoding.UTF8.GetBytes("还款");
                        byte[] utf84 = Encoding.UTF8.GetBytes("借款");
                        bool equal1 = Enumerable.SequenceEqual(utf81, utf82)| Enumerable.SequenceEqual(utf81, utf84);
                        bool equal2 = Enumerable.SequenceEqual(utf81, utf83);
                        bool equal3 = row["note"].ToString().Contains("true");
                        bool equal4 = row["payment"].ToString().Equals("花呗") || row["payment"].ToString().Equals("白条");
                        equal = equal1 | equal2| equal4;
                        if (equal3)//只要有true，一定记入
                            equal = false;
                        if (Enumerable.SequenceEqual(utf81, utf84))
                            Console.WriteLine("eq");
                        //equal = false;
                    }
                    
                    if(!equal)
                    {
                        tempsum.Rows.Add(row[nameofdateintabday], row[nameofexpenseintabday]);
                    }
                    
                   // tempsum.ImportRow(row);
                }
                ///计算汇总表
                nameofdateintabday = tempsum.Columns[0].ColumnName;
                nameofexpenseintabday = tempsum.Columns[1].ColumnName;
                var myTable = from t in tempsum.AsEnumerable()
                              group t by new { myDH = t.Field<DateTime>(nameofdateintabday) }
                                  into x
                                  select new
                                  {
                                      x.Key.myDH,
                                      y = Math.Round(x.Sum(k => k.Field<double>(nameofexpenseintabday)),3)
                                  };
                DataTable result = tempsum.Clone();
                Dictionary<DateTime, double> mydict = new Dictionary<DateTime, double>();
                foreach(DataRow row in tabincome.Rows)
                {
                    if((row["note"].ToString().Contains("true")))
                    {
                        DateTime date = (DateTime)row["datadate"];
                        if (mydict.Keys.Contains(date))
                            mydict[date] += (double)row["income"];
                        else
                            mydict.Add(date, (double)row["income"]);
                    }
                   
                   
                    
                }
                foreach (var item in myTable)
                {
                    double income = 0;
                    if (mydict.Keys.Contains((DateTime)item.myDH))
                        income = mydict[(DateTime)item.myDH];
                    result.Rows.Add(item.myDH, Math.Round( item.y-income,3), Math.Round(item.y, 3));
                }
                
                return result;
            }
            else
            {
                Console.WriteLine("wrong！");
                return tabsum;
            }
        }
        public DataTable CalSumofMonth(DataTable tabsum,DataTable tabday)
        {
            DataTable tempsum = tabsum.Clone();
            string nameofdateintabday = "";
            string nameofexpenseintabday = "";
            //获得对应的列的名字
            foreach (DataColumn column in tabday.Columns)
            {
                if (column.DataType.Equals(typeof(DateTime)))
                    nameofdateintabday = column.ColumnName;
                if (column.DataType.Equals(typeof(double)))
                    nameofexpenseintabday = column.ColumnName;
            }
            if (nameofdateintabday != "" && nameofexpenseintabday != "")
            {
                ////将日表复制到汇总表
                //foreach (DataRow row in tabday.Rows)
                //{
                //    tempsum.Rows.Add(row[nameofdateintabday], row[nameofexpenseintabday]);
                //}
                ///计算汇总表
                nameofdateintabday = tempsum.Columns[0].ColumnName;
                nameofexpenseintabday = tempsum.Columns[1].ColumnName;
                List<DateTime> list = new List<DateTime>();
                ///遍历tabday，获得所有月份
                foreach (DataRow row in tabday.Rows)
                {
                    //判断这个数据属于那一个月
                    int i = ((DateTime)row[0]).Month;
                    int j = ((DateTime)row[0]).Year;
                    if (!list.Exists(k => { return (k.Month == i && k.Year == j); }))
                    {
                        list.Add(new DateTime(j, i, 1));
                    }
                }
                foreach (DateTime date in list)
                {
                    double obj = (double)tabday.Compute("sum(sum)", "datadate>='" + date + "'and datadate<'" + date.AddMonths(1) + "'");
                    obj = Math.Round(obj, 2);
                    
                    double obj2 = (double)tabday.Compute("sum(comsum)", "datadate>='" + date + "'and datadate<'" + date.AddMonths(1) + "'");
                    obj2 = Math.Round(obj2, 2);
                    var lll = tabday.AsEnumerable().Count(i => { if (i.Field<DateTime>("datadate") >= date && i.Field<DateTime>("datadate") < date.AddMonths(1)) return true; else return false; });
                    int obj3 = lll;
                    //if(obj3<myMethod.GetDayOfMonth(date))
                    //{
                    //    obj2 = obj2 * myMethod.GetDayOfMonth(date) / lll;
                    //    obj3 = obj3 * myMethod.GetDayOfMonth(date) / lll;
                    //}
                    tempsum.Rows.Add(date, obj,obj2);
                }


                return tempsum;
            }
            else
            {
                Console.WriteLine("wrong！");
                return tabsum;
            }  
        }
        public DataTable ShowDayData(DateTime datetime,CmdAndAdp cna)
        {
            DataTable dt = cna.table.Clone();
            DataRow[] rows = cna.table.Select("datadate='"+datetime+"'");
            foreach(var row in rows)
            {
                dt.ImportRow(row);
            }
            if(dt.Columns.Contains("datacount"))
            {
                dt.Columns.Remove("datacount");
            }
            
            return dt;
            
        }
        public DataTable ShowDayData(DateTime datetime, List<CmdAndAdp> list)
        {
       
            DayDataView = this.SelectTable("DayDataView", datetime);//时间长 104ms
            var temp = DayDataView.table;
            DataTable dt = temp.Clone();
            DataRow[] rows = temp.Select("datadate='" + datetime + "'");
            foreach (var row in rows)
            {
                dt.ImportRow(row);
            }
            if (dt.Columns.Contains("datacount"))
            {
                dt.Columns.Remove("datacount");
            }
            return dt;
            //List<DataTable> listdt = new List<DataTable>();
            //foreach(CmdAndAdp cna in list)
            //{
            //    listdt.Add(this.ShowDayData(datetime, cna));
            //}
            //DataTable dt = listdt[0].Clone();
            //foreach(DataTable i in listdt)
            //{
            //    ///保证两个表可以相加
            //    foreach(DataColumn column in i.Columns)
            //    {
            //        if(!dt.Columns.Contains(column.ColumnName))
            //        {
            //            dt.Columns.Add(column.ColumnName);
            //        }
            //    }
            //    foreach(DataRow row in i.Rows)
            //    {
            //        dt.ImportRow(row);
            //    }
            //}
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    if(dt.Columns[i].DataType==typeof(double))
            //    {
            //        dt.Columns[dt.Columns[i].ColumnName].SetOrdinal(dt.Columns.Count - 4);
            //    }
            //    if (dt.Columns[i].DataType == typeof(int))
            //    {
            //        dt.Columns[dt.Columns[i].ColumnName].SetOrdinal(dt.Columns.Count - 3);
            //    }
            //}
          
            //return dt;
        }
        /// <summary>
        /// 把所有表汇总显示
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataTable ShowDayData(List<CmdAndAdp> list)
        {
            List<DataTable> listdt = new List<DataTable>();
            list.ForEach(i => listdt.Add(i.table));
            DataTable dt = listdt[0].Clone();
            //if(!dt.Columns.Contains("Table"))
            //{
            //    DataColumn newcolumn = new DataColumn("Table", Type.GetType("System.String"));
            //    dt.Columns.Add(newcolumn);
            //    dt.Columns["Table"].SetOrdinal(0);
            //}
            
            Dictionary<string, int> mydict = new Dictionary<string, int>();
            foreach (DataTable i in listdt)
            {
                ///保证两个表可以相加
                foreach (DataColumn column in i.Columns)
                {
                    if (!dt.Columns.Contains(column.ColumnName))
                    {
                        dt.Columns.Add(column.ColumnName);
                    }
                }
                //if(!i.Columns.Contains("Table"))
                //{
                //    DataColumn addcolumn = new DataColumn("Table", Type.GetType("System.String"));
                //    i.Columns.Add(addcolumn);
                //    i.Columns["Table"].SetOrdinal(0);
                //    foreach (DataRow row in i.Rows)
                //    {
                //        row[addcolumn] = i.TableName;
                //    }
                //}
               
                
                //保证没有空值
                DataTable tempdt = i.Clone();
                foreach(DataRow row in i.Rows)
                {
                    //if (tempdt.Columns.Contains("note"))
                    //    if(!row["note"].ToString().Contains(i.TableName.Translate()))
                    //    row["note"]  +=" "+i.TableName.Translate();
                    tempdt.ImportRow(row);
                }
                
                foreach(DataColumn column in dt.Columns)
                {

                    if(column.DataType==typeof(string)&&column.ColumnName!="writer")
                    {
                        if (!i.Columns.Contains(column.ColumnName))
                        {

                            tempdt.Columns.Add(column.ColumnName, typeof(string));
                            
                            if(column.ColumnName=="category"||column.ColumnName=="item")
                            {
                                foreach (DataRow row in tempdt.Rows)
                                    row[column.ColumnName] = i.TableName;
                            }
                            
                        }
                    }
                }
                foreach (DataRow row in tempdt.Rows)
                {
                    dt.ImportRow(row);
                }
            }
          
            //改变列的次序
           
            if (dt.Columns.Contains("datacount"))
            {
                dt.Columns.Remove("datacount");
            }
            foreach (DataColumn column in dt.Columns)
            {
                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row[column].GetType() != typeof(DBNull)&&(!row[column].ToString().Equals(null)))
                        count++;
                }
                mydict.Add(column.ColumnName, count);
            }
            var templist = mydict.ToList();
            templist = templist.OrderBy(i => i.Value).ToList();
            //templist.ForEach(i => Console.WriteLine(i.Value));
            foreach(var item in templist)
            {
                int order =templist.IndexOf(item);
                if(item.Value!=mydict.Values.Max())
                dt.Columns[item.Key].SetOrdinal(dt.Columns.Count -order-1);
            }
           
            
            dt.DefaultView.Sort = "datadate desc";
            dt.TableName = "AllTables";
            
            return dt;
        }
        
    }

    public  class CmdAndAdp
    {
       public string tablenames;
       public MySqlDataAdapter da = new MySqlDataAdapter();
       public MySqlCommandBuilder cb = new MySqlCommandBuilder();
       public DataTable table=new DataTable();
    }

    
}


