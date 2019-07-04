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
    public partial class infoUser : Form
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;
        public string Name1
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _pw;
        public string PW
        {
            get { return _pw; }
            set { _pw = value; }
        }
        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        private string _pnum;
        public string Pnum
        {
            get { return _pnum; }
            set { _pnum = value; }
        }
        private string _pow;
        public string Pow
        {
            get { return _pow; }
            set { _pow = value; }
        }
        private string _tag;
        public string _Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        public infoUser()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void infoUser_Load(object sender, EventArgs e)
        {
            if (_Tag == "user") label7.Text = "普通用户";
            else if(Pow == "1") label7.Text = "超级管理员";
            else if (Pow == "0") label7.Text = "管理员";
            label9.Text = Id;
            label10.Text = PW;
            label11.Text = Name1;
            label12.Text = Pnum;
            label5.Text = Gender;

        }
    }
}
