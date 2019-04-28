﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Update
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            checkUpdate();

        }

        private async void checkUpdate()
        {
            var update = await NetWork.getHttpWebRequest("http://60.12.230.82:10086/IPv6/update.txt", PostORGet: 1);
            string dir = System.IO.Directory.GetCurrentDirectory();  //程序所在文件夹路径
            string file = System.IO.Path.Combine(dir, "IPv6ToolBox.exe");  //下载后zip文件的完整路径

            string f = System.IO.Directory.GetCurrentDirectory() + "/IPv6ToolBox.exe";
            if (File.Exists(f))
            {
                File.Copy(f, System.IO.Directory.GetCurrentDirectory() + "/IPv6ToolBox.bak");
            }
            try
            {
                string appname = "IPv6ToolBox";  //A名字，不要路径，不要.exe
                Process[] processes = Process.GetProcessesByName(appname);
                foreach (var p in processes)
                    p.Kill();
                await Task.Delay(500);

                WebClient client = new WebClient();
                client.DownloadFile("http://60.12.230.82:10086/IPv6/bin/IPv6ToolBox_" + update.Substring(1, 4) + ".exe", file);
                File.Delete(System.IO.Directory.GetCurrentDirectory() + "/IPv6ToolBox.bak");
            }
            catch (Exception e)
            {
                tb.Text = "更新失败...";
                File.Move(System.IO.Directory.GetCurrentDirectory() + "/IPv6ToolBox.bak", f);
                MessageBox.Show(e.Message);
            }
            await Task.Delay(500);
            System.Diagnostics.Process.Start(file);
            Application.Current.Shutdown();

        }
    }
}
