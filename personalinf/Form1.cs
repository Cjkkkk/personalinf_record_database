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
using System.Data.SQLite;
using personalinf.db;


namespace personalinf
{
    public partial class Form1 : Form
    {
        personalinfo person = new personalinfo();
        int savestate = 0;
        SQLiteConnection conn;
       
        public Form1()
        {
            InitializeComponent();
        }
        //上传头像
        private void button1_Click(object sender, EventArgs e)
        {
            person.Pic = "";
            OpenFileDialog filedia = new OpenFileDialog();
            filedia.InitialDirectory = "C:\\";
            if (filedia.ShowDialog() == DialogResult.OK)
            {
                person.Pic = filedia.FileName;
                pictureBox1.Image = Image.FromFile(person.Pic);
            }

        }
        //修改字体
        private void button3_Click(object sender, EventArgs e)
        {
            
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowDialog();
            listBox1.Font = fontDlg.Font;
        }
        //修改颜色
        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ColorDialog colordig = new ColorDialog();
            colordig.ShowDialog();
            listBox1.ForeColor = colordig.Color;
        }

       
        //菜单修改文件
        private void 打开文字对话框ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            person.Pic = "";
            OpenFileDialog filedia = new OpenFileDialog();
            filedia.InitialDirectory = "C:\\";
            if (filedia.ShowDialog() == DialogResult.OK)
            {
                person.Pic = filedia.FileName;
                pictureBox1.Image = Image.FromFile(person.Pic);
            }
        }
        //菜单修改字体
        private void 打开字体对话框ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FontDialog fontDlg = new FontDialog();
            fontDlg.ShowDialog();
            listBox1.Font = fontDlg.Font;
        }
        //菜单修改颜色
        private void 设置颜色对话框ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ColorDialog colordig = new ColorDialog();
            colordig.ShowDialog();
            listBox1.ForeColor = colordig.Color;
            
        }



        //status内容修改
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
           toolStripStatusLabel1.Text = "欢迎 " + textBox1.Text;
        }

        //提交个人信息按钮事件

        private void button2_Click(object sender, EventArgs e)
        {
           
        
      
      
            
            if(radioButton1.Checked == true)
            {
                person.Sex = "男孩";
            }
            else
            {
                person.Sex = "女孩";
            }
            //检查是否选择了兴趣爱好
            foreach(Control i in groupBox1.Controls)
            {
                if(i is CheckBox)
                {
                    if (((CheckBox)i).Checked)
                    {
                        
                        person.Hobby = person.Hobby + i.Text+",";
                    }
                }
            }
         
  
            //检查是否选择了户籍
            foreach(TreeNode i in treeView1.Nodes)
            {
                if (i.IsSelected) {
                    person.Home = i.Text;
                }
            }
          
            //

            //检查剩余信息是否填写
            person.Name = textBox1.Text;
            person.Birthday = dateTimePicker1.Text;
            person.Selfintr = richTextBox1.Text;
            person.Bloodtype = comboBox1.Text;
           
            if (person.Hobby == string.Empty)
            {
                MessageBox.Show("请选择一个兴趣爱好");
            }
            else if (person.Home == string.Empty)
            {
                MessageBox.Show("请选择户籍");
            }
            else if (person.Name == string.Empty)
            {
                MessageBox.Show("请填写姓名");
            }
            else if (person.Birthday == string.Empty)
            {
                MessageBox.Show("请填写生日");
            }
            else if (person.Selfintr == string.Empty)
            {
                MessageBox.Show("请填写个人简介");
            }
            else if (person.Bloodtype == string.Empty)
            {
                MessageBox.Show("请选择血型");
            }
            else if (person.Pic == string.Empty)
            {
                MessageBox.Show("请上传照片");
            }
            else
            {
                MessageBox.Show("填写完毕");
                //数据库操作
                connectToDatabase();
               if (whether_already_exist(person.Name)){
                    filldata();
                }
                // printHighscores();      
                conn.Close();
                if (savestate == 0)
                {
                    savestate = 1;
                }
                
            }
          
        }
      
        //链接数据库
        public void connectToDatabase()
        {
            conn = new SQLiteConnection(@"Data Source=C: \Users\Administrator\我的文档\Visual Studio 2017\Projects\personalinf\personalinf\db\personalinf.sqlite;");
            conn.Open();
        }
        //插入数据
        void filldata()
        {

      
            SQLiteCommand command = conn.CreateCommand();

            command.CommandText = "INSERT INTO information(Name,Bloodtype,Birthday,Hobby,Sex,Selfintr,Pic,Home) VALUES(@Name1,@Bloodtype1,@Birthday1,@Hobby1,@Sex1,@Selfintr1,@Pic1,@Home1)";
            command.Parameters.Add(new SQLiteParameter("Name1",person.Name));
            command.Parameters.Add(new SQLiteParameter("Bloodtype1", person.Bloodtype));
            command.Parameters.Add(new SQLiteParameter("Birthday1", person.Birthday));
            command.Parameters.Add(new SQLiteParameter("Hobby1", person.Hobby));
            command.Parameters.Add(new SQLiteParameter("Sex1", person.Sex));
            command.Parameters.Add(new SQLiteParameter("Selfintr1", person.Selfintr));
            command.Parameters.Add(new SQLiteParameter("Pic1", person.Pic));
            command.Parameters.Add(new SQLiteParameter("Home1", person.Home));

            int i = command.ExecuteNonQuery();
            if (i == 1)
             {
             MessageBox.Show("成功添加"+person.Name+"的个人信息");
             }


        }
        //查询数据
        void printHighscores()
        {
            string sql = "select * from information order by Name desc";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            MessageBox.Show("Name: " + reader["Name"] + "\thome: " + reader["Home"]);
           
        }

         bool whether_already_exist(string name)
        {
            string sql = "SELECT * FROM information WHERE Name =@Name";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.Parameters.Add(new SQLiteParameter("Name", name));
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("数据库中已经有" + name+"的个人信息，不要重复录入");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        public void clearwindows()
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Close();

        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(savestate == 0)
            {
               
                if (MessageBox.Show("你有信息未保存，仍然要新建文件吗？", "关闭查询", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    clearwindows();
                }
               
               

            }
            else
            {
                clearwindows();
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       
    }
}
