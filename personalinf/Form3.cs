using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace personalinf
{
    public partial class Form3 : Form
    {
        Form2 fmtrue;
        string output;
        public Form3(string message,string pic,Form2 fm2)//传入窗口2假对象，用来关闭
        {
            InitializeComponent();
            richTextBox1.Text = message;
            output = message;
            pictureBox1.Image = Image.FromFile(pic);
            fmtrue = fm2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            fmtrue.Close();
            Form2 fm = new Form2();
            fm.Show();


        }

        private string ShowSaveFileDialog()
        {
            string localFilePath = "";
            string fileNameExt = "";
            //string localFilePath, fileNameExt, newFileName, FilePath; 
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "文档（*.txt）|*.txt";

            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;

            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString(); //获得文件路径 
                fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径

                //获取文件路径，不带文件名 
                //FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\")); 

                //给文件名前加上时间 
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt; 

                //在文件名里加字符 
                //saveFileDialog1.FileName.Insert(1,"dameng"); 

                //System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();//输出文件 

                ////fs输出带文字或图片的文件，就看需求了 
            }
          
           
               
            return localFilePath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            fmtrue.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
             byte[] myByte = System.Text.Encoding.UTF8.GetBytes(output);
            string path = ShowSaveFileDialog();
           // MessageBox.Show(a);
            using (FileStream fsWrite = new FileStream(path, FileMode.Append))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
                MessageBox.Show("导出成功，地址为" + path);
            };
        }
    }
}
