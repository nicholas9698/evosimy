using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace keshe
{
    public partial class delBookClass : Form
    {
        private IList<string> _list;
        public IList<string> _List
        {
            get { return _list; }
            set { _list = value; }
        }
        private IList<string> _Slist;
        public IList<string> _SList
        {
            get { return _Slist; }
            set { _Slist = value; }
        }
        public delBookClass()
        {
            InitializeComponent();
        }

        private void delBookClass_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            SqlCommand cmd = new SqlCommand("Select * from bookclass", con);
            con.Open();
            if (cmd.ExecuteScalar() == null)
            {
                MessageBox.Show("无图书分类，请先添加图书分类");
                con.Close();
                this.Close();
            }
            else
            {
                IList<string> list1 = new List<string>();
                IList<string> list2 = new List<string>();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list1.Add(dr[0].ToString());
                    list2.Add(dr[1].ToString());
                }
                dr.Close();
                comboBox1.DataSource = list2;
                _List = list1;
                _SList = list2;
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            SqlCommand cmd = new SqlCommand("delete from bookclass where bcNum="+_List[comboBox1.SelectedIndex], con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            this.Close();
        }
    }
}
