using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOBot
{
    public partial class Visual : Form
    {
        public Visual()
        {
            InitializeComponent();
        }

        private void btnBox_Click(object sender, EventArgs e)
        {
            BoxCollection box = new BoxCollection();
            box.SearchBox();
        }
    }
}
