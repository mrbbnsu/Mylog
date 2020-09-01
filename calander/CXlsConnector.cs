using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Spire;
using System.Data;

namespace calander
{
     public class CXlsConnector
    {
         public Workbook wb = new Workbook();
         string path = Directory.GetCurrentDirectory() + @"\mylog.csv";
        public bool Connect()
        {

           
            if (File.Exists(path))
            {
                wb.LoadFromFile(path);
                return true;
            }
            else wb.SaveToFile(path);
            return false; 
        }
         public bool AddTab(DataTable table)
        {
            Worksheet worksheet =wb.Worksheets.Add(table.TableName);
            
            try
            {
                worksheet.InsertDataTable(table, true, 1, 1);
                wb.SaveToFile(path);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return true;
        }
    }
}
