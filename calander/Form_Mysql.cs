// Copyright (C) 2004-2005 MySQL AB
//
// MySQL Connector/NET is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most 
// MySQL Connectors. There are special exceptions to the terms and 
// conditions of the GPLv2 as it is applied to this software, see the 
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License 
// for more details.
//
// You should have received a copy of the GNU General Public License along 
// with this program; if not, write to the Free Software Foundation, Inc., 
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;
namespace TableEditor
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FormMysql : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox tables;
		private System.Windows.Forms.Button updateBtn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ComboBox databaseList;
		private System.Windows.Forms.Label label5;

		private MySqlConnection		conn;
		private DataTable			data;
		private MySqlDataAdapter	da;
        private System.Windows.Forms.DataGrid dataGrid;
		private MySqlCommandBuilder	cb;

		public FormMysql()
		{
			
			InitializeComponent();
            string myPassword;
            bool MysqlConnectionInfo =false;
            do
            {
                myPassword = Interaction.InputBox("«Î ‰»Î√‹¬Î", "µ«¬º", "Password", -1, -1);
                if (myPassword == "") break;
                string connStr = String.Format("server={0};user id={1}; password={2}; database=mysql; pooling=false",
                "", "root", myPassword);
                try
                {
                    conn = new MySqlConnection(connStr);
                    conn.Open();
                    GetDatabases();
                    MysqlConnectionInfo =true;
                }
                catch (MySqlException ex)
                {
                    MysqlConnectionInfo = false;
                    MessageBox.Show("Error connecting to the server: " + ex.Message);
                }
            } while (!MysqlConnectionInfo);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label4 = new System.Windows.Forms.Label();
            this.tables = new System.Windows.Forms.ComboBox();
            this.dataGrid = new System.Windows.Forms.DataGrid();
            this.updateBtn = new System.Windows.Forms.Button();
            this.databaseList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tables";
            // 
            // tables
            // 
            this.tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tables.Location = new System.Drawing.Point(96, 112);
            this.tables.Name = "tables";
            this.tables.Size = new System.Drawing.Size(355, 20);
            this.tables.TabIndex = 7;
            this.tables.SelectedIndexChanged += new System.EventHandler(this.tables_SelectedIndexChanged);
            // 
            // dataGrid
            // 
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.DataMember = "";
            this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid.Location = new System.Drawing.Point(10, 146);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(780, 302);
            this.dataGrid.TabIndex = 8;
            this.dataGrid.Navigate += new System.Windows.Forms.NavigateEventHandler(this.dataGrid_Navigate);
            // 
            // updateBtn
            // 
            this.updateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateBtn.Location = new System.Drawing.Point(700, 112);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(90, 25);
            this.updateBtn.TabIndex = 9;
            this.updateBtn.Text = "Update";
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // databaseList
            // 
            this.databaseList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.databaseList.Location = new System.Drawing.Point(96, 86);
            this.databaseList.Name = "databaseList";
            this.databaseList.Size = new System.Drawing.Size(355, 20);
            this.databaseList.TabIndex = 11;
            this.databaseList.SelectedIndexChanged += new System.EventHandler(this.databaseList_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Databases";
            // 
            // FormMysql
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(801, 453);
            this.Controls.Add(this.databaseList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.tables);
            this.Controls.Add(this.label4);
            this.Name = "FormMysql";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMysql_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		

		private void GetDatabases() 
		{
			MySqlDataReader reader = null;

			MySqlCommand cmd = new MySqlCommand("SHOW DATABASES", conn);
			try 
			{
				reader = cmd.ExecuteReader();
				databaseList.Items.Clear();
				while (reader.Read()) 
				{
					databaseList.Items.Add( reader.GetString(0) );
				}
			}
			catch (MySqlException ex) 
			{
				MessageBox.Show("Failed to populate database list: " + ex.Message );
			}
			finally 
			{
				if (reader != null) reader.Close();
			}
		}

		private void databaseList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			MySqlDataReader reader = null;

			conn.ChangeDatabase( databaseList.SelectedItem.ToString() );

			MySqlCommand cmd = new MySqlCommand("SHOW TABLES", conn);
            
			try 
			{
				reader = cmd.ExecuteReader();
				tables.Items.Clear();
				while (reader.Read()) 
				{
					tables.Items.Add( reader.GetString(0) );
				}
			}
			catch (MySqlException ex) 
			{
				MessageBox.Show("Failed to populate table list: " + ex.Message );
			}
			finally 
			{
				if (reader != null) reader.Close();
			}
		}

		private void tables_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			data = new DataTable();
			
			da = new MySqlDataAdapter("SELECT * FROM " + tables.SelectedItem.ToString(), conn );
			cb = new MySqlCommandBuilder( da );
            

			da.Fill( data );

			dataGrid.DataSource = data;
		}

		private void updateBtn_Click(object sender, System.EventArgs e)
		{
			DataTable changes = data.GetChanges();
			da.Update( changes );
			data.AcceptChanges();
		}

        private void server_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormMysql_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void dataGrid_Navigate(object sender, NavigateEventArgs ne)
        {

        }
	}
}
