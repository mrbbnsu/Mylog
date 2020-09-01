#define IsAppliacion 


using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;
using System.Windows.Forms;

namespace calander
{
  public class Test
  {
      public string name { get; set; }
  }
    

  public static class myMethod
  {
      /// <summary>
      /// 获得日期在一个月的日历表中处于第几行第几列，用行和列表示
      /// </summary>
      /// <param name="datetime"></param>
      /// <param name="j"></param>
      /// <returns></returns>
      public static int GetDayAndWeek(this DateTime datetime, out int j)
      {

          int day, week;
          day = (int)datetime.DayOfWeek;
          week = (datetime.Day - 1) / 7;
          if (day - (datetime.Day - 1) % 7 < 0)
          {
              j = week + 1;
          }
          else
          {
              j = week;
          }
          return day;

      }

      /// <summary>
      /// 获得日期在一个月的日历表中处于第几行第几列，用一个数字表示
      /// </summary>
      /// <param name="datetime"></param>
      /// <returns></returns>
      public static int GetDayAndWeek(this DateTime datetime)
      {
          int day, week;
          day = (int)datetime.DayOfWeek;
          week = (datetime.Day - 1) / 7;
          if (day - (datetime.Day - 1) % 7 < 0)
          {
              week = week + 1;
          }
          
          return day+week*7;
      }

      /// <summary>
      /// 计算一个月有几天
      /// </summary>
      /// <param name="datetime"></param>
      /// <returns></returns>
      public static int GetDayOfMonth(this DateTime datetime)
      {
          switch(datetime.Month)
          {
              case 1: 
              case 3: 
              case 5: 
              case 7: 
              case 8: 
              case 10: 
              case 12: return 31; 
              case 2:
                  {
                      if (datetime.Year % 4 == 0)
                          return 29;
                      else return 28;
                      
                  }
              case 4: 
              case 6: 
              case 9: 
              case 11: return 30; 
              default: return 31; 

          }
      }
      public static int GetDayOfMonth(int year,int Month)
      {
          switch (Month)
          {
              case 1:
              case 3:
              case 5:
              case 7:
              case 8:
              case 10:
              case 12: return 31; 
              case 2:
                  {
                      if (year % 4 == 0)
                          return 29;
                      else return 28;
                      
                  }
              case 4:
              case 6:
              case 9:
              case 11: return 30; 
              default: return 31; 

          }
      }



      public static DataTable ToDataTable(DataRow[] rows)
      {
          if (rows == null || rows.Length == 0) return null;
          DataTable tmp = rows[0].Table.Clone(); // 复制DataRow的表结构
          foreach (DataRow row in rows)
          {
              tmp.ImportRow(row); // 将DataRow添加到DataTable中
          }
          return tmp;
      }

      
#if IsAppliacion
      public static void text(this System.Windows.Forms.TextBox textbox,string s)
      {
          textbox.Width = s .Length*7;
          textbox.Text = s;
      }

      public static void distinctadd(this System.Windows.Forms.ComboBox.ObjectCollection items,object obj)
      {
          if (!items.Contains(obj))
              items.Add(obj);
      }
#endif
      
  }

    public static class StringTrans
    {
        public static Dictionary<string, string> Dic = new Dictionary<string, string>();
        public static string Translate(this string key)
        {
            if (Dic.Keys.Contains(key))
                return key = Dic[key];
            return
                key;
        }
        /// <summary>
        /// 英文转中文
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable Translate(this DataTable dt)
        {
            DataTable temp = dt.Copy();
            foreach (DataColumn item in temp.Columns)
            {
                item.ColumnName = item.ColumnName.Translate();
            }
            return temp;
        }
        public static void AddItem(this ToolStripMenuItem toolstrip, List<ToolStripMenuItem> list,string item,EventHandler clickhandler,bool ischeck=false)
        {
            ToolStripMenuItem newitem = new ToolStripMenuItem();
            newitem.Name = item;
            newitem.Text = item.Translate();
            newitem.Tag = toolstrip.Tag;
            newitem.Click += clickhandler;
            list.Add(newitem);
            toolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newitem });
            newitem.Checked = ischeck;
            if (ischeck)
                newitem.PerformClick();
        }
        /// <summary>
        /// 返回当前日期表示的月份
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ThisMonth(this DateTime date)
        {
            return date.AddDays(-date.Day+1);
        }
        /// <summary>
        /// 返回当前日期表示的月份的下一个月
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime NextMonth(this DateTime date)
        {
            return date.ThisMonth().AddMonths(1);
        }
        /// <summary>
        /// 返回当前日期表示的月份的上一个月
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastMonth(this DateTime date)
        {
            return date.ThisMonth().AddMonths(-1);
        }
        public static double ToDouble(this DateTime date)
        {

            return 0;
        }
    }

    public class DataGridCom : IComparer
    {
        public int Compare(object x, object y)
        {
            return Convert.ToInt16(x) - Convert.ToInt16(y);
        }

        
    }
}

