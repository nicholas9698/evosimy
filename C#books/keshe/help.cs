using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace keshe
{
    public partial class help : Form
    {
        public help()
        {
            InitializeComponent();
        }

        private void help_Load(object sender, EventArgs e)
        {

            label1.Text = "root管理员联系方式";
            label2.Text = "手机：15552235129";
            label3.Text = "qq：1439698007";
            label4.Text = "网址：https://www.evosimy.com";
            label5.Text = "github：https://github.com/nicholas9698/evosimy";

        }
    }
}
