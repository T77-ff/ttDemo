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
using System.Data;
using System.IO;

namespace Enquire
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strConn = "SERVER=.;DATABASE=Book;USER ID=sa;PASSWORD=123456";
            SqlConnection sqlconn = new SqlConnection(strConn);
            sqlconn.Open();

            string sqlText = string.Format("SELECT * FROM users WHERE username='{0}' AND password='{1}'",
                    txtName.Text, txtPassword.Text
                    );

            SqlCommand sqlcmd = new SqlCommand(sqlText, sqlconn);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            string s = null;
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    s = dr["lvl"].ToString();
                }
                if (s.Equals("0"))
                {
                    this.Hide();
                    Form1 form1 = new Form1();
                    sqlconn.Close();
                    return;
                }
                else
                {

                    this.Hide();
                    User user = new User();
                    sqlconn.Close();
                    return;

                }
            }
            else
            {
                MessageBox.Show("用户名或密码错误！");
                sqlconn.Close();
            }
            
        }
    }
}
