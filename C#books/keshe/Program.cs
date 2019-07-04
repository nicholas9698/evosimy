using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace keshe
{
    public static class Common
    {
        private  static string _text;
        public  static string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        private static string _con;
        public static string Con
        {
            get { return _con; }
            set { _con = value; }
        }
    }
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Common.Con = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new loginForm());
        }
    }
}
