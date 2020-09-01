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
    public partial class FormDayViw : Form
    {
        public FormDayViw()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var date = dateTimePicker1.Value;
            (this.Owner as FormTable).SelectDate = date;
            this.Hide();
            this.Close();
        }
        
        
    }
}
