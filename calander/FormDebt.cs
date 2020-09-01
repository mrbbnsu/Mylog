using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace calander
{
    public partial class FormDebt : Form
    {
        
        public FormDebt()
        {
            InitializeComponent();
           
        }

        private void Init()
        {
            DateTime datetime = DateTime.Today;
            int today = datetime.Year * 100 + datetime.Month;
            textboxmonth.Text = today.ToString();
            var tb = CalDebt((this.Owner as Form1).CNADebt.table);
            var row = tb.NewRow();
            //row[0] = "每项汇总";
            for (int i = 1; i < tb.Columns.Count-1; i++)
            {
                double sum = 0;
                for (int j = 0; j < tb.Rows.Count-1; j++)
                {
                    sum += (double)tb.Rows[j][i];
                }
                row[i] =  sum.ToString("f2");
            }
            tb.Rows.Add(row);
            for (int i = 0; i < tb.Rows.Count-1; i++)
            {
                int month = (int)tb.Rows[i][0];
                month = (month%100-1) % 12+1 + (month / 100 + (month%100-1) / 12)*100;
                tb.Rows[i][0] = month.ToString();
            }
            dataGridViewdebt.DataSource = tb.Translate();

        }

        private void button_update_Click(object sender, EventArgs e)
        {
            var owner = this.Owner as Form1;
            var obj = owner.CNADebt;
            int month = Convert.ToInt32(textboxmonth.Text);
            int lasttime = Convert.ToInt32(textboxlastmonth.Text);
            double bill = Convert.ToDouble(textboxbill.Text);
            DataRow row = obj.table.NewRow();
            row[0] = textboxitem.Text;
            row[1] = month;
            row[2] = lasttime;
            row[3] = bill;
            obj.table.Rows.Add(row);
            owner.myConnection.UpdateData(obj);
        }

        private void FormDebt_Shown(object sender, EventArgs e)
        {
            Init();
        }

        private int CalCompare(int startmonth,int lastmonth,int now)
        {
            //提取开始年，开始月，现在年，现在月
            int sy = startmonth / 100;
            int sm = startmonth % 100;
            int ny= now / 100;
            int nm = now % 100;
            int com = (-ny+  sy) * 12 + (-nm +sm) + lastmonth;
            return com;
        }
        private DataTable CalDebt(DataTable dt)
        {
            int typeint=0;
            double typedouble=0;
            DateTime today = DateTime.Today;
            int inttoday = today.Year * 100 + today.Month-0;
            DataTable re = new DataTable();
            re.Columns.Add("month", typeint.GetType());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(!re.Columns.Contains(dt.Rows[i][0] as string))
                {
                    re.Columns.Add(dt.Rows[i][0] as string, typedouble.GetType());
                }
            }
            re.Columns.Add("sumofmonth", typedouble.GetType());
            for (int i = inttoday+1; ; i++)
            {
               
                DataRow newrow = re.NewRow();
                
                int columncount = re.Columns.Count;
                for (int j = 1; j < columncount; j++)
                {
                    newrow[j] = 0;
                }
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string item = dt.Rows[j][0] as string;
                    int com1 = (int)dt.Rows[j][1];
                    int com2 = (int)dt.Rows[j][2];
                    int compare = CalCompare(com1, com2, i);
                    int com3 = (int)dt.Rows[j][1];
                    if (compare >= 0&&com3 <i)
                        newrow[item] = Math.Round( (double)dt.Rows[j][3]+ (double)newrow[item],2);
                }
                for (int j = columncount-2; j >0; j--)
                {
                    newrow[columncount - 1] = (double)newrow[j] + (double)newrow[columncount - 1];
                }
                if ((double)newrow[columncount-1] < 1)
                    break;
                newrow[columncount - 1] = Math.Round((double)newrow[columncount - 1], 2);
                newrow["month"] = i-1;
                //if (i%100>12)
                //{
                //    int j,k;
                //    j = i % 100 % 12;
                //    k = i / 100 + i % 100 / 12;
                //    newrow["month"] = k*100+j;
                //}
                
                re.Rows.Add(newrow);
                
            }
            //添加总额统计列
            re.Columns.Add("sumofall", typedouble.GetType());
            int rowcount = re.Rows.Count-1;
            int sumord = re.Columns["sumofmonth"].Ordinal;
            re.Rows[rowcount]["sumofall"] = re.Rows[rowcount][sumord];
            for (int i = re.Rows.Count-2; i >=0; i--)
            {
                re.Rows[i]["sumofall"] = (double)re.Rows[i][sumord]+ (double)re.Rows[i+1]["sumofall"];
            }
            if (chartdebt.Series.Count == 0)
            {

            }
            else if (chartdebt.Series.Count >= 1)
            {

                chartdebt.Series.Clear();
            }
            foreach (DataColumn item in re.Columns)
            {
                if (item.ColumnName != "month"&&item.ColumnName!="sumofall")
                {
                    Series dataTable1Series = new Series(item.ColumnName);
                    dataTable1Series.Points.DataBind(re.AsEnumerable(), "month", item.ColumnName, "");
                    dataTable1Series.XValueType = ChartValueType.Auto; //设置X轴类型为时间
                    dataTable1Series.ChartType = SeriesChartType.Line;  //设置Y轴为饼状图
                    dataTable1Series["PieLabelStyle"] = "Outside";
                    dataTable1Series.LegendText = item.ColumnName.Translate();
                    dataTable1Series.ToolTip = "#VALX"+" " +item.ColumnName.Translate()+" "+"#VALY";
                    dataTable1Series.Label = "";
                    dataTable1Series.MarkerStyle = (MarkerStyle)(item.Ordinal  );
                    dataTable1Series.MarkerSize = 10;  
                    dataTable1Series["PointWidth"] = "1";
                    dataTable1Series.IsValueShownAsLabel = false;
                    chartdebt.Series.Add(dataTable1Series);
                }

            }
            chartsum.Series.Clear();
            Series dataTable1Series1 = new Series("sumofall".Translate());
            dataTable1Series1.Points.DataBind(re.AsEnumerable(), "month", "sumofall", "");
            dataTable1Series1.XValueType = ChartValueType.Auto; //设置X轴类型为时间
            dataTable1Series1.ChartType = SeriesChartType.Line;  //设置Y轴为折线
            dataTable1Series1.Color = Color.Red;
            dataTable1Series1.MarkerStyle = (MarkerStyle)(5);
            dataTable1Series1.MarkerSize = 10;
            dataTable1Series1.ToolTip = "#VALX" + " " + "sumofall".Translate() + " " + "#VALY";
            dataTable1Series1.LegendText = "sumofall";
            dataTable1Series1.YAxisType =AxisType.Primary;
            chartsum.Legends[0].Docking = Docking.Top;
            chartsum.Series.Add(dataTable1Series1);

            chartsum.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
            chartsum.ChartAreas[0].AxisY.Enabled = AxisEnabled.True;
            chartsum.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            chartsum.ChartAreas[0].AxisY.MaximumAutoSize = 100;
            
            chartsum.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            chartsum.ChartAreas[0].BorderWidth = 10;
            chartsum.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chartsum.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
            
            chartsum.ChartAreas[0].AxisY.Minimum = 0;
         
            chartsum.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            chartsum.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            chartsum.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartsum.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            
            chartsum.ChartAreas[0].AxisY.Title = "sum\noff\nall";

            chartdebt.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            chartdebt.ChartAreas[0].BorderWidth = 10;
            chartdebt.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;          
            chartdebt.ChartAreas[0].AxisY.MaximumAutoSize=100;            
            chartdebt.ChartAreas[0].AxisY.Minimum = 0;
            chartdebt.ChartAreas[0].AxisY.Title = "s\nu\nm";           
            chartdebt.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            chartdebt.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            chartdebt.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartdebt.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartdebt.Legends[0].Docking = Docking.Top;
          
            return re;
        }


    }
}
