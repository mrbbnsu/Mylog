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
    public partial class FormModify : Form
    {
        List<ToolStripMenuItem> TableList = new List<ToolStripMenuItem>();
        List<CmdAndAdp> ALltables = new List<CmdAndAdp>();
        DataTable SelectTable = new DataTable();
        public FormModify()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void AddSelectMenu(List<CmdAndAdp> list)
        {
            ALltables = list;
            foreach (var item in list)
            {
                ToolStripMenuItem newitem = new ToolStripMenuItem();
                newitem.Name = item.tablenames;
                newitem.Text = item.tablenames;
                newitem.Click += ItemClick;
                TableList.Add(newitem);
                SelectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newitem });
               
            }
        }
        private void ItemClick(object sender, EventArgs e)
        {
            var tab =sender as ToolStripMenuItem;
            TableList.ForEach(i => i.Checked = false);
            TableList.Find(i => i.Name == tab.Name).Checked = true;
            //只修改最近30天的数据
            DataTable temp = ALltables.Find(i => i.tablenames == tab.Name).table;
            //temp.DefaultView.Sort = "datadate desc";
            temp = temp.DefaultView.ToTable();
            DataTable t = temp.Clone();
            DateTime date = DateTime.Today;
            foreach (DataRow  item in temp.Rows)
            {
                var d = (DateTime)item["datadate"];
                if (d >= date.AddDays(-30))
                    t.ImportRow(item);
                else
                    break;
            }
            SelectTable = t;
            //SelectTable.DefaultView.Sort = "datadate desc";
            SelectTable = SelectTable.DefaultView.ToTable();
            dataGridView1.DataSource = SelectTable;
            SelectTable.AcceptChanges();
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
        }


        private void SelectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonmod_Click(object sender, EventArgs e)
        {
            //只能修改而不能删除 
            var change = SelectTable.GetChanges();
            string tablename = change.TableName;
            DataTable temp = ALltables.Find(i => i.tablenames == tablename).table;//找到对应表格
            for (int k = 0; k < change.Rows.Count; k++)
                for (int i = temp.Rows.Count-1; i>0; i--)
                {

                    if (temp.Rows[i][0].Equals(change.Rows[k][0]))
                    {
                        temp.Rows.RemoveAt(i);
                        temp.ImportRow(change.Rows[k]);
                    }
                }
            var tempchange = temp.GetChanges();
            ALltables.Find((i => i.tablenames == tablename)).da.Update(change);
            ALltables.Find((i => i.tablenames == tablename)).table.AcceptChanges();
           // temp.AcceptChanges();
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                int datacount = (int)dataGridView1.SelectedRows[i].Cells[0].Value;
                string tablename = SelectTable.TableName;
                DataTable temp = ALltables.Find(k => k.tablenames == tablename).table;//找到对应表格
                for (int k = temp.Rows.Count-1; k >0 ; k--)
                {
                    if((int)temp.Rows[k][0]== datacount)
                    {
                        temp.Rows[k].Delete();
                       
                    }
                    var change = temp.GetChanges();
                    ALltables.Find((j => j.tablenames == tablename)).da.Update(change);
                    ALltables.Find((j => j.tablenames == tablename)).table.AcceptChanges();
                    break;
                }
            }
           

        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (MessageBox.Show("是否删除", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                var deletenum = e.RowIndex;
                int datacount = (int)SelectTable.Rows[deletenum][0];
                var change = SelectTable.GetChanges();
                string tablename = SelectTable.TableName;
                DataTable temp = ALltables.Find(i => i.tablenames == tablename).table;//找到对应表格
                ALltables.Find((i => i.tablenames == tablename)).da.Update(change);
                ALltables.Find((i => i.tablenames == tablename)).table.AcceptChanges();

            }


        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

           
           

        }
    }
}
