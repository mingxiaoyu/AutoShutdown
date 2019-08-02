using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoShutdown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label2.Text = "";
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon1.Visible = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void tool退出StripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                myMenu.Show();
            }

            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string command = string.Empty;
            var date = DateTime.Now;
            if (radioButton1.Checked)
            {
                command = $"at  {dateTimePicker1.Value.ToString("HH:mm")}  shutdown -s ";
                date = dateTimePicker1.Value;
            }
            else
            {
                command = $"shutdown  -s  -t  {numericUpDown1.Value * 60} ";
                date = DateTime.Now.AddMinutes((double)numericUpDown1.Value);
            }
            label2.Text = $"将于{date.ToString("HH:mm") }关机";
            ipcmd(command);

            WindowState = FormWindowState.Minimized;
            btnOK.Enabled = false;
        }

        public void ipcmd(object p)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c " + p;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            process.StartInfo = startInfo;
            startInfo.Verb = "RunAs";
            process.Start();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            ipcmd("shutdown  -a ");
            label2.Text = "";
            btnOK.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                RadioControl(true);
            }
            else
            {
                RadioControl(false);
            }
        }

        private void RadioControl(bool enabled)
        {

            dateTimePicker1.Enabled = enabled;
            numericUpDown1.Enabled = !enabled;
        }
    }
}
