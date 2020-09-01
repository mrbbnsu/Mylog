using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using MySql.Data;
using System.IO;
using TableEditor;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using Spire.Xls;
using Microsoft.VisualBasic;
namespace calander
{
    public partial class Form1 : Form
    {
        Point MouseLocation;//指示鼠标位置
        public DateTime CurrentDate=DateTime.Today;//获取当前时间
        public int year;
        public int month;
        static bool IsInitialized = false;//标志是否第一次运行
        List<DateTime> ListDate = new List<DateTime>();
        FormTable form = new FormTable();
        public DataTable ClickDateTab = new DataTable();
        private List<long> timelist = new List<long>();
        public Dictionary<string, double> budget = new Dictionary<string, double>();
        public class DateOnCalander
        {
            public DateTime dateOnCalander { get; set; }
            public  DateOnCalander(DateTime datetime):this()
            {
                dateOnCalander = datetime;
            }
            public DateOnCalander() { }
            public int x_pos { get; set; }
            public int y_pos { get; set; }
        }
        public  struct PanelPara
        {
           public int drawWidth;
           public int drawHeight;
           public int drawWidthDelta;
           public int drawHeightDelta;
           public List<Point> drawPoints;
        }
        PanelPara myPanelPara = new PanelPara();
       public CMysqlConnectoor myConnection = new CMysqlConnectoor();
       public DataTable datatable = new DataTable();
       public CmdAndAdp CNACom = new CmdAndAdp();// mycomsumption 表
       public CmdAndAdp CNASum = new CmdAndAdp();
        public CmdAndAdp CNADebt = new CmdAndAdp();
        public CmdAndAdp CNASumOfMoney = new CmdAndAdp();
        public CmdAndAdp BodyWeight = new CmdAndAdp();
        public CmdAndAdp CNASumOfMonth=new CmdAndAdp();
        //public CmdAndAdp DayDataView = new CmdAndAdp();

        public List<CmdAndAdp> AllTables = new List<CmdAndAdp>();
        
        public bool IsPaint = false;
       public struct PaintTablePara
       {
          public Color color;
         public  int deltax;
         public  int deltay;
         public int radius;
       }
       private Dictionary<string, PaintTablePara> PaintTabDic = new Dictionary<string, PaintTablePara>();
        public Dictionary<string, string> EngToChi = new Dictionary<string, string>();

       
       public Form1()
       {
           InitializeComponent();

           
       }
        private void MyInit()
        {
            //string pssswd = "284303";
            //bool startflag = false;
            //do
            //{
            //    if (startflag)
            //    {
            //        var result = MessageBox.Show("密码错误,是否重试", "提示", MessageBoxButtons.YesNo);
            //        if (result == System.Windows.Forms.DialogResult.No)
            //            Application.Exit();
            //    }

            //    pssswd = Interaction.InputBox("请输入密码", "密码", "passwd", 100, 100);
            //    startflag = true;
            //} while (pssswd != "284303");

            var g = panel1.CreateGraphics();
            //连接数据库
            #region
            
            myConnection.Connect();//时间长 250ms
            //myConnection.Select_DataBase("mylog_guest");
            CNACom = myConnection.SelectTable("mycomsumption");
            CNASum = myConnection.SelectTable("DayView");
            CNADebt = myConnection.SelectTable("debt");
            CNASumOfMonth = myConnection.SelectTable("MonthView");
            CNASumOfMoney = myConnection.SelectTable("sumofmoney");
            BodyWeight = myConnection.SelectTable("bodyweight");
            //DayDataView = myConnection.SelectTable("DayDataView");//时间长 104ms
            CNACom.table.DefaultView.Sort = "datadate desc";
            AllTables = myConnection.GetAllTables();//时间长 180ms
            AllTables.Add(CNACom);

            #endregion
            //显示次级界面
            #region
            //form.Visible = true;
            form.Show(this);
            form.Hide();
            // form.dataGridView1.DataSource = this.myConnection.ShowDayData(AllTables);
            // form.tabsum.DataSource = CNASum.table;
            // form.rbdayview.Checked = true;
            #endregion
            //给每个小分类着色
            #region

            //  PaintDayDeatil(CNASum);

            string path = Directory.GetCurrentDirectory() + @"\test.txt";//读取颜色文件
            string readline = " ";
            if (!File.Exists(path))
            {
                var file = File.Create(path);
                file.Close();
            }
            StreamReader stream = new StreamReader(path);
            //创建颜色与表类型对应的字典
            Dictionary<string, Color> mydic = new Dictionary<string, Color>();
            do
            {
                readline = stream.ReadLine();
                if (readline == null) break;
                string[] dic = readline.Split('\t');
                if (dic.Length > 1)
                {
                    Color color = Color.FromArgb(150, int.Parse(dic[1]), int.Parse(dic[2]), int.Parse(dic[3]));
                    mydic.Add(dic[0], color);
                }
            } while (readline != "");
            stream.Close();

            //读取预算文件
             path = Directory.GetCurrentDirectory() + @"\budget.txt";//读取颜色文件
             readline = " ";
            if (!File.Exists(path))
            {
                var file = File.Create(path);
                file.Close();
            }
             var budgetstream = new StreamReader(path);
            //创建颜色与表类型对应的字典
            
            do
            {
                readline = budgetstream.ReadLine();
                if (readline == null) break;
                string[] dic = readline.Split('\t');
                if (dic.Length > 1)
                {

                    budget.Add(dic[0], double.Parse( dic[1]));
                }
            } while (readline != "");
            budgetstream.Close();


            //着色结构体赋值
            int x = 0, y = 0;
            x = myPanelPara.drawWidthDelta;
            y = myPanelPara.drawHeightDelta * 3 / 4;
            //开始着色 消费记录单独处理
            var temp = AllTables.Find(i => { return i.tablenames.Contains("mycom"); });
            AllTables.Remove(temp);
            //遍历AllTables，得到表的数目
            foreach (CmdAndAdp cna in AllTables)
            {
                x -= myPanelPara.drawWidthDelta / AllTables.Count;
                Color color = new Color();
                if (mydic.ContainsKey(cna.tablenames))
                {
                    color = mydic[cna.tablenames];
                }
                else
                {
                    int r, ag, b;
                    Random ran = new Random();
                    do
                    {
                        r = ran.Next(0, 5) * 255 / 5;
                        ag = ran.Next(0, 5) * 255 / 5;
                        b = ran.Next(0, 5) * 255 / 5;
                    } while (mydic.ContainsValue(Color.FromArgb(150, r, ag, b)));
                    color = Color.FromArgb(150, r, ag, b);
                    mydic.Add(cna.tablenames, color);
                }
                PaintTablePara para = new PaintTablePara();
                para.color = color;
                para.deltax = x;
                para.deltay = y;
                para.radius = myPanelPara.drawWidthDelta / AllTables.Count / 2;
                PaintTabDic.Add(cna.tablenames, para);
            }
            //保存颜色文件
            path = Directory.GetCurrentDirectory() + @"\test.txt";//读取颜色文件
            var st = new StreamWriter(path, false);
            foreach (var i in mydic)
            {
                st.WriteLine("{0}\t{1}\t{2}\t{3}", i.Key, i.Value.R, i.Value.G, i.Value.B);
            }
            st.Close();
            //着色
            DrawCalander(CurrentDate, g);//115ms
            PaintLegendPanel(AllTables);//显示label
            AllTables.Add(temp);
           
            #endregion

            //读取字典文件（英文显示为中文）
            #region

            path = Directory.GetCurrentDirectory() + @"\dict.txt";
             readline = " ";
            if (!File.Exists(path))
            {
                var file = File.Create(path);
                file.Close();
            }
             stream = new StreamReader(path,System.Text.Encoding.Default);
            
            do
            {
                readline = stream.ReadLine();
                if (readline == null) break;
                string[] dic = readline.Split('\t');
                if (dic.Length > 1)
                {
                    
                    EngToChi.Add(dic[0], dic[1]);
                }
            } while (readline != "");
            stream.Close();

            #endregion

            //给类型选择颜色
            foreach (var cna in AllTables)
            {
                comboxtables.Items.Add(cna.tablenames);
            }

            #region
            //触发控件的程序
           ChangeBottunText(DateTime.Today.Year, DateTime.Today.Month);//月份前后按键显示具体月份
            ChangeComboxText(AllTables);//选择月份的下拉菜单
            #endregion

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var g = panel1.CreateGraphics();
            if(!IsPaint)
            {
                myPanelPara.drawWidth = panel1.Width - 1;
                myPanelPara.drawHeight = panel1.Height - 1;
                myPanelPara.drawWidthDelta = myPanelPara.drawWidth / 7;
                myPanelPara.drawHeightDelta = myPanelPara.drawHeight / 6;
                myPanelPara.drawPoints = new List<Point>();
                for (int i = 0; i < 7; i++)
                {
                    for (int k = 0; k < 6; k++)
                    {

                        myPanelPara.drawPoints.Add(new Point(i * myPanelPara.drawWidthDelta, k * myPanelPara.drawHeightDelta));
                    }
                }
                myPanelPara.drawPoints.Sort((x, y) =>
                {
                    return (x.Y * 8 + x.X - y.Y * 8 - y.X);
                });
            }
           
            //第一次画日历，默认为当前月份
            int j;

            var date = new DateOnCalander(CurrentDate);
            date.x_pos = myMethod.GetDayAndWeek(CurrentDate, out j);
            date.y_pos = j;
            if (IsPaint)
            {
                CurrentDate = DrawCalander(DateTime.Today, g);
            }
            year = CurrentDate.Year;
            month = CurrentDate.Month;
            if(!IsPaint)
            {
                IsInitialized = true;
                MyInit();
                IsPaint = true;
            }
           
        }

        private void DrawLine(Graphics g, DateTime date)//画方格线的函数
        {
            //六行七列
            int m = 6;
            int starti, startj, endi, endj, name, max_count;

            max_count = myMethod.GetDayOfMonth(date);//获取这个月有几天
            starti = myMethod.GetDayAndWeek(date.AddDays(-date.Day + 1), out startj);//获取当月1号在日历表中的位置
            endi = (starti + max_count % 7);
            endj = startj + max_count / 7 + 1;
           

            //MessageBox.Show(i.ToString() + "   " + j.ToString());

            g.DrawLine(new Pen(Color.Black), myPanelPara.drawWidthDelta*starti, myPanelPara.drawHeightDelta * startj, myPanelPara.drawWidthDelta * 7, myPanelPara.drawHeightDelta * startj);
            g.DrawLine(new Pen(Color.Black), 0, myPanelPara.drawHeightDelta * endj, myPanelPara.drawWidthDelta * endi, myPanelPara.drawHeightDelta * endj);
            for (int i = startj+1; i < endj ; i++)//画横线 startj 和 endj 控制画几根
            {
               
                g.DrawLine(new Pen(Color.Black), 0, myPanelPara.drawHeightDelta * i, myPanelPara.drawWidthDelta * 7, myPanelPara.drawHeightDelta * i);
                
            }
            m = 7;
            //竖线所有都要画，每一根长度不同
            for (int i = 0; i < m+1; i++)
            {
                int sy, ey;
                if (i >= starti)
                    sy = 1;
                sy = i >= starti ? startj : startj + 1;
                ey = i <= endi ? endj : endj-1;
                g.DrawLine(new Pen(Color.Black), myPanelPara.drawWidthDelta * i, myPanelPara.drawHeightDelta * sy, myPanelPara.drawWidthDelta * i, myPanelPara.drawHeightDelta * ey);
            }
        }
      /// <summary>
      /// 填充日期格子
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="g"></param>
        private void paint(int x,int y,Graphics g,Color color)
        {
            int x_loc = myPanelPara.drawWidthDelta * x;
            int y_loc = myPanelPara.drawHeightDelta * y;
            Rectangle rec = new Rectangle(x_loc, x_loc, myPanelPara.drawWidthDelta, myPanelPara.drawHeightDelta);
            Brush b = new SolidBrush(this.panel1.BackColor);
             g.FillRectangle(b, x_loc+1, y_loc+1, myPanelPara.drawWidthDelta-1, myPanelPara.drawHeightDelta-1);
            b = new SolidBrush(color);
            
            g.FillRectangle(b, x_loc+1, y_loc+1, myPanelPara.drawWidthDelta-1, myPanelPara.drawHeightDelta-1);
            
        }
        private void paint(DateTime datetime,Graphics g,Color color)
        {
            int i, j;
            i = myMethod.GetDayAndWeek(datetime, out j);
            paint(i, j,g, color);
        }
        private void paint(DateTime datetime, Graphics g)
        {
            paint(datetime, g, Color.FromArgb(50, Color.Yellow));

        }
        /// <summary>
        /// 写出格子对应的日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="g"></param>
        private void PaintDate(DateTime datetime,Graphics g)
        {
            Font f = new Font("Arial", 20f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Pixel);
            int i, j,count,name,print_count;
            i = myMethod.GetDayAndWeek(datetime.AddDays(-datetime.Day+1), out j);//获取当月1号在日历表中的位置
            //MessageBox.Show(i.ToString() + "   " + j.ToString());
            count = i + j*7;
            int x_loc = myPanelPara.drawWidthDelta * i;
            int y_loc = myPanelPara.drawHeightDelta * j;
            name = 0;
            print_count=0;
            foreach (var point in myPanelPara.drawPoints)
            {
                print_count++;
                if (print_count>count)
                {
                    name++;
                    g.DrawString(name.ToString(), f, Brushes.Black, point);
                    //g.DrawString(point.X.ToString()+"  "+point.Y.ToString(), f, Brushes.Black, point);
                    
                    if(name>=myMethod.GetDayOfMonth(datetime))
                    {
                        break;
                    }
                }
                
            }
           // g.DrawString("一", f, Brushes.Black, x_loc, x_loc);
        }
        /// <summary>
        /// 将日汇总填充到日期格子
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="sum"></param>
        /// <param name="g"></param>
        private void PaintDate(DateTime datetime, double sum, Graphics g)
        {
            //选择字体
            Font f = new Font("Arial", 12f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Pixel);
            //获取日期在日历表中的位置
            int i, j;
            i = myMethod.GetDayAndWeek(datetime, out j);
            int x_loc = myPanelPara.drawWidthDelta * i;
            int y_loc = myPanelPara.drawHeightDelta * j;
            int a = 20;
            a = (int)sum / 10 * 10+10;
            if (a > 255) a = 255;
            Color color = new Color();
            if(sum>0)
            color = Color.FromArgb(a, 255, 0, 0);
            if(sum<0)
                color= Color.FromArgb(50, 0, 0, 200);
            //按sum填充颜色
            paint(datetime, g, color);
            //写出sum
            Point point = new Point();
            point.X = myPanelPara.drawPoints[i + 7 * j].X + myPanelPara.drawWidthDelta * 7/10-(int)(Math.Log10(Math.Abs(sum)))*5;
            point.Y = myPanelPara.drawPoints[i + 7 * j].Y;
            double paintsum = Math.Round(sum, 1);
            if (sum > 100)
                paintsum = Math.Round(sum, 0);
            g.DrawString(paintsum.ToString(), f, Brushes.Black, point);
        }
        private void PaintDate(DateTime datetime,PaintTablePara para,Graphics g)
        {
            int i, j;
            i = myMethod.GetDayAndWeek(datetime, out j);
            int x_loc = myPanelPara.drawWidthDelta * i + para.deltax;
            int y_loc = myPanelPara.drawHeightDelta * j + para.deltay;

            Rectangle rec = new Rectangle(x_loc, y_loc, 3*para.radius/2, 3*para.radius/2);
            Brush b = new SolidBrush(para.color);
            g.FillEllipse(new SolidBrush(panel1.BackColor), rec);
            g.FillEllipse(b, rec);
            
        }
        private void PaintDayDeatil(Graphics g,params CmdAndAdp[] cnas)
        {
            if (cnas == null || cnas.Length == 0)
                return;

            foreach (CmdAndAdp cna in cnas)
            {
                if (cna.tablenames == null)
                    continue;
                if (cna.tablenames.Contains("sumof")|| cna.tablenames.ToLower().Contains("day"))
                {
                    
                    foreach (DataRow row in cna.table.Rows)
                    {

                        DateTime temp = Convert.ToDateTime(row[0]);
                        if (temp.Year == year && temp.Month == month)
                            this.PaintDate(Convert.ToDateTime(row[0]), (double)row[1], g);
                       
                    }
                }


            }

        }
        /// <summary>
        /// 画出月历
        /// </summary>画出月历
        /// <param name="datetime">日期</param>
        /// <param name="g"></param>
        private void PaintDayAll(DateTime datetime, Graphics g)
        {
            
            int i, j, count, name, print_count;
            
            i = myMethod.GetDayAndWeek(datetime.AddDays(-datetime.Day + 1), out j);//获取当月1号在日历表中的位置
            //MessageBox.Show(i.ToString() + "   " + j.ToString());
            count = i + j * 7;
            int x_loc = myPanelPara.drawWidthDelta * i;
            int y_loc = myPanelPara.drawHeightDelta * j;
            name = 0;
            print_count = 0;
            foreach (var point in myPanelPara.drawPoints)
            {
                print_count++;
                if (print_count > count)
                {
                    name++;
                    DateTime temp = new DateTime(datetime.Year, datetime.Month, name);
                    //g.DrawString(point.X.ToString()+"  "+point.Y.ToString(), f, Brushes.Black, point);
                    paint(temp, g,Color.FromArgb(20,0,80,0));
                    if (name >= myMethod.GetDayOfMonth(datetime))
                    {
                        break;
                    }
                }

            }
        }
        private void PaintDayDeatil(List<CmdAndAdp>list)
        {    
            foreach (CmdAndAdp cna in list)
            {
                if (cna.tablenames != null)
                {
                    if(!cna.tablenames.Contains("mycom"))
                    {
                        string columnname = "";
                        //找到时间对应的列
                        foreach (DataColumn column in cna.table.Columns)
                        {
                            if (column.DataType == typeof(DateTime))
                            {
                                columnname = column.ColumnName;
                                break;
                            }
                        }
                        foreach (DataRow row in cna.table.Rows)
                        {
                            if (cna.tablenames=="timelist")
                            {
                                continue;
                            }
                                DateTime temp = (DateTime)row[columnname]; 
                                if (temp.Year == year && temp.Month == month)
                                    this.PaintDate(temp, PaintTabDic[cna.tablenames], panel1.CreateGraphics());
                        }
                    }
                }
            }            
        }
        private void PaintLegendPanel(List<CmdAndAdp> list)
        {
            var templist = list;
            var temptab = list.Find(i => i.tablenames.Contains("mycom"));
            templist.RemoveAll(i => i.tablenames.Contains("mycom"));
            Panel PanelColor = new Panel();
            PanelColor.BackColor = Color.FromArgb(20,0,0,0);
            int count = templist.Count;
            int length = 30;
            int width = 180;
            Size size = new System.Drawing.Size(width, length * count);
            PanelColor.Size = size;
            PanelColor.Name = "PanelColor";
            PanelColor.Location = new Point(600, 50);
            Controls.Add(PanelColor);
            Pen pen = new Pen(Color.FromArgb(10,0,0,0));
            string bitmapfilename = Directory.GetCurrentDirectory() + "\\legend.jpg";
            Bitmap legendbitmap = new Bitmap(PanelColor.Size.Width, PanelColor.Size.Height);

            var g = Graphics.FromImage(legendbitmap);
            Font f = new Font("Arial", 14f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Pixel);
            Rectangle rect = new Rectangle(0, 0, PanelColor.Width - 1, PanelColor.Height - 1);
            g.DrawRectangle(pen, rect);
            g.Clear(Color.White);
            //PanelColor.BackColor = Color.Transparent;
            for (int i = 0; i < count; i++)
            {
                rect = new Rectangle(10, 10 + length * i, width / 3, length / 2);
                if (!PaintTabDic.Keys.Contains(templist[i].tablenames))
                    continue;
                Color color = PaintTabDic[templist[i].tablenames].color;
                Brush b = new SolidBrush(color);
                g.FillRectangle(b, rect);
                g.DrawString(templist[i].tablenames, f, Brushes.Black, width / 3 + 20, 10 + length * i);

            }
            //PanelColor.DrawToBitmap(legendbitmap,new Rectangle(0,0, PanelColor.Width, PanelColor.Height));
            legendbitmap.Save(bitmapfilename,System.Drawing.Imaging.ImageFormat.Jpeg);
            g.DrawImage(legendbitmap,0,0);
            g.Dispose();
            var h = PanelColor.CreateGraphics();
            h.Clear(Color.White);
            h.DrawImage(legendbitmap, 0, 0);
            legendbitmap.Dispose();
            if (temptab!=null)
            templist.Add(temptab);
        }
        public DateTime DrawCalander(DateTime datetime,Graphics g)
        {
            
            //panel1.BackColor = Color.FromArgb(150, 250, 255, 255);
            g.Clear(panel1.BackColor);
            DrawLine(g, datetime);
            // panel1.BackColor = Color.Transparent;
            // PaintDayAll(datetime, g);//画出月历的格子
            PaintDayDeatil(g,CNASum);//画出每日总额
            PaintDayDeatil(AllTables);//画出除消费外的记录
            PaintDate(datetime, g);//显示日期
            LabelDate.Width = 20;
            LabelDate.Text = datetime.ToShortDateString();
            return datetime;
        }
        /// <summary>
        /// 添加数据后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DrawCalander()
        {
            var g = panel1.CreateGraphics();
            PaintDayDeatil(g,CNASum);
            PaintDayDeatil(AllTables);
            PaintDate(CurrentDate, g);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           //Dictionary<string,Color> mydic=new Dictionary<string,Color>();
           // int r,g,b;
           // List<Color> listcolor=new List<Color>();
           // var ran = new Random();
           // foreach(CmdAndAdp cna in AllTables)
           // {
           //     Color color = new Color();
           //     do
           //     {
           //      r = ran.Next(0, 2) * 255/2;
           //     b = ran.Next(0, 2) * 255/2;
           //     g = ran.Next(0, 2) * 255/2;
           //     color = Color.FromArgb(r, g, b);
           //     } while (listcolor.Contains(color));
           //     listcolor.Add(color);
           //     mydic.Add(cna.tablenames, color);
           // }
           // string path = Directory.GetCurrentDirectory() + @"\test.txt";
            
           // if (!File.Exists(path))
           // {
           //     var file = File.Create(path);
           //     file.Close();
           // }

           // StreamWriter stream = new StreamWriter(path);
           // foreach (var i in mydic)
           // {
           //     stream.WriteLine("{0}\t{1}\t{2}\t{3}", i.Key, i.Value.R,i.Value.G,i.Value.B);
           // }
           //// StreamReader stream = new StreamReader(path);
           
           // stream.Close();
            form.Visible = !form.Visible;
            
        }
        

       

        private void panel1_Click(object sender, EventArgs e)
        {
            
            int loc_x, loc_y,delta;
            //将鼠标位置转化为格子位置
            loc_x = MouseLocation.X / myPanelPara.drawWidthDelta;
            loc_y = MouseLocation.Y / myPanelPara.drawHeightDelta;
            delta = loc_x + loc_y * 7 - myMethod.GetDayAndWeek(CurrentDate);
            DateTime Click_date = new DateTime();
            Click_date = CurrentDate.AddDays(delta);//得到点击处的日期
            if(Click_date.Month==CurrentDate.Month)
            {
                var formadd = new FormAddData(Click_date);
                //ClickDateTab = this.myConnection.ShowDayData(Click_date, AllTables);
                //formadd.dataGridView1.DataSource = ClickDateTab;
                formadd.Show(this);
            }
            
        
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            this.MouseLocation = e.Location;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("确认关闭？", "提示", MessageBoxButtons.OKCancel);
            if(result==System.Windows.Forms.DialogResult.OK)
            {
                form.Dispose();
                e.Cancel = false;
            }
            else if(result==System.Windows.Forms.DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (myConnection.conn.State == ConnectionState.Open)
                myConnection.CononectionClose();   
            }
            catch(Exception ex)
            {
                Console.WriteLine("button2_Click" + ex.Message);
            }
            
        }

      

        private void butnextmonth_Click(object sender, EventArgs e)
        {
            var g = panel1.CreateGraphics();
           // g.Clear(panel1.BackColor);
            CurrentDate = CurrentDate.AddMonths(1);
            year = CurrentDate.Year;
            month = CurrentDate.Month;
            
            CurrentDate = DrawCalander(CurrentDate, g);
            ChangeBottunText(year, month);
        }

        private void butlastmonth_Click(object sender, EventArgs e)
        {
            var g = panel1.CreateGraphics();
            //g.Clear(panel1.BackColor);
            CurrentDate = CurrentDate.AddMonths(-1);
            year = CurrentDate.Year;
            month = CurrentDate.Month;
            CurrentDate = DrawCalander(CurrentDate, g);
            ChangeBottunText(year, month);
        }

        private void butnextyear_Click(object sender, EventArgs e)
        {
            var g = panel1.CreateGraphics();
           // g.Clear(panel1.BackColor);
            CurrentDate = CurrentDate.AddYears(1);
            year = CurrentDate.Year;
            month = CurrentDate.Month;
            CurrentDate = DrawCalander(CurrentDate, g);
        }

        private void butlastyear_Click(object sender, EventArgs e)
        {
            var g = panel1.CreateGraphics();
            
           // g.Clear(panel1.BackColor);
            CurrentDate = CurrentDate.AddYears(-1);
            year = CurrentDate.Year;
            month = CurrentDate.Month;
            CurrentDate = DrawCalander(CurrentDate, g);
        }

        private void butconnecttomysql_Click(object sender, EventArgs e)
        {
            if(myConnection.conn==null)
            try
            {
                myConnection.Connect();
            }
            catch
            {

            }
            else
            {
                    myConnection.Connect();
            }
           
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                labconstate.Text = "state:" + "  " + myConnection.conn.State.ToString();
            }
            catch
            {
                labconstate.Text = "state:" + "  " + ConnectionState.Closed.ToString();
            }
        }

        private void cbselectyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbselectyear.SelectedItem != null && cbselectmonth.SelectedItem != null && IsInitialized)
            {
                var g = panel1.CreateGraphics();
                //g.Clear(panel1.BackColor);
                CurrentDate=  new DateTime((int)(cbselectyear.SelectedItem), (int)(cbselectmonth.SelectedItem), DateTime.Today.Day);
                year = CurrentDate.Year;
                month = CurrentDate.Month;
                CurrentDate = DrawCalander(CurrentDate, g);
                
            }
            
        }

        private void cbselectmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbselectyear.SelectedItem != null && cbselectmonth.SelectedItem != null && IsInitialized)
            {
                var g = panel1.CreateGraphics();
                //g.Clear(panel1.BackColor);
                CurrentDate = new DateTime((int)(cbselectyear.SelectedItem), (int)(cbselectmonth.SelectedItem), DateTime.Today.Day);
                year = CurrentDate.Year;
                month = CurrentDate.Month;
                CurrentDate = DrawCalander(CurrentDate, g);
            }
        }

       
        private void butcolor_Click(object sender, EventArgs e)
        {
            if(comboxtables.SelectedItem!=null)
            {
                if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string tableneme =comboxtables.SelectedItem.ToString();
                    var para = new PaintTablePara();
                    para = PaintTabDic[tableneme];
                    para.color = colorDialog1.Color;
                    PaintTabDic[tableneme] = para;
                }
                SaveTabParas();
                DrawCalander();
                
            }
        }

        private void SaveTabParas()
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + @"\test.txt";
                var st = new StreamWriter(path, false);


                foreach (var i in PaintTabDic)
                {
                    st.WriteLine("{0}\t{1}\t{2}\t{3}", i.Key, i.Value.color.R, i.Value.color.G, i.Value.color.B);
                }
                st.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + "savepara");
            }
           
        }

        public void MyUpdate()
        {
            form.ButtonUpdate.PerformClick();
        }

        private void comboxtables_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(comboxtables.SelectedItem);

        }
        public void UpdateClick()
        {
            form.UpDate();
        }
        public void ChangeBottunText(int year,int month)
        {
            int lastyear, nextyear, lastmonth, nextmonth;
            lastmonth = month - 1;
            nextmonth = month + 1;
            lastyear = nextyear = year;
            if (lastmonth == 0) { lastmonth = 12; lastyear--; }
            if (nextmonth == 13) { nextmonth = 1; nextyear++; }
            butnextmonth.Text = nextyear.ToString() + "\\" + nextmonth.ToString();
             butlastmonth.Text = lastyear.ToString() + "\\" + lastmonth.ToString();
        }
        public void ChangeComboxText(List<CmdAndAdp> alltable)
        {
            int maxy, miny, maxm, minm;
            var max = alltable.Select(i => i.table.AsEnumerable().Select(t=>t.Field<DateTime>("datadate")).Max()).Max();
            var min = alltable.Select(i => i.table.AsEnumerable().Select(t => t.Field<DateTime>("datadate")).Min()).Min();
            maxy = max.Year;
            miny = min.Year;
            maxm = max.Month;
            minm = min.Month;
            for (int i=miny; i <maxy+1; i++)
            {
                cbselectyear.Items.Add(i);
            }
            for (int i = 1; i < 13; i++)
            {
                cbselectmonth.Items.Add(i);
            }

            this.cbselectyear.SelectedIndexChanged -= new System.EventHandler(this.cbselectyear_SelectedIndexChanged);
            this.cbselectmonth.SelectedIndexChanged -= new System.EventHandler(this.cbselectmonth_SelectedIndexChanged);
            cbselectyear.SelectedItem = DateTime.Today.Year;
            cbselectmonth.SelectedItem = DateTime.Today.Month;
            this.cbselectyear.SelectedIndexChanged += new System.EventHandler(this.cbselectyear_SelectedIndexChanged);
            this.cbselectmonth.SelectedIndexChanged += new System.EventHandler(this.cbselectmonth_SelectedIndexChanged);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonShowDebt_Click(object sender, EventArgs e)
        {
            var formdebt = new FormDebt();
            formdebt.Show(this);
        }
    }

    
}
