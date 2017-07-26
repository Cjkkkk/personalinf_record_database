using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using personalinf;
using System.Data.SQLite;

namespace personalinf
{
    public partial class Form2 : Form
    {
        SQLiteConnection conn;
        public Form2()
        {
            InitializeComponent();
   
        }

        private void button1_Click(object sender, EventArgs e)
        {

            conn = new SQLiteConnection(@"Data Source=C: \Users\Administrator\我的文档\Visual Studio 2017\Projects\personalinf\personalinf\db\personalinf.sqlite;");
            conn.Open();
            findrecord(textBox1.Text);
            conn.Close();


        }
        public void findrecord(string name)
        {
            string sql = "SELECT * FROM information WHERE Name =@Name";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.Parameters.Add(new SQLiteParameter("Name", name));
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string message = "名字 ：" + reader["Name"] + "\n" + "血型 ：" + reader["Bloodtype"] + "\n";
                message = message + "生日 ：" + reader["Birthday"] + "\n" + "性别 ：" + reader["Sex"] + "\n";
                message = message + "兴趣爱好 ：" + reader["Hobby"] + "\n" + "籍贯 ：" + reader["Home"] + "\n";
                message = message + "自我介绍 ：" + reader["Selfintr"] + "\n" ;
                Form3 result = new Form3(message,reader["Pic"].ToString(),this);
                result.Show();
                
               
               
            }
            else
            {
                MessageBox.Show("数据库中没有" + name + "的个人信息");
            }
        }
    }
}
