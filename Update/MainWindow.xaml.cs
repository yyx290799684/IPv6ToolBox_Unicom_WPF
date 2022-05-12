using System;
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

        public MainWindow(StartupEventArgs e)
        {
            InitializeComponent();
            if (e.Args.Length == 1)
            {
                checkUpdate(e.Args[0].ToString());
            }
            else if (e.Args.Length == 2)
            {
                checkUpdate(e.Args[0].ToString(), e.Args[1].ToString());
            }
        }

        private async void checkUpdate(string name = "IPv6ToolBox.exe", string type = "IPv6ToolBox")
        {
            //MessageBox.Show(name);

            string update = string.Empty;
            if (type == "IPv6ToolBox")
            {
                update = await NetWork.getHttpWebRequest("http://60.12.230.82:10086/IPv6/update.txt", PostORGet: 1);
            }
            else if (type == "IP")
            {
                update = await NetWork.getHttpWebRequest("http://60.12.230.82:10086/HJKGKiller/update.txt", PostORGet: 1);
            }
            string dir = System.IO.Directory.GetCurrentDirectory();  //程序所在文件夹路径

            string file = string.Empty;
            if (type == "IPv6ToolBox")
            {
                file = System.IO.Path.Combine(dir, "IPv6ToolBox.exe");  //下载后zip文件的完整路径
            }
            else if (type == "IP")
            {
                file = System.IO.Path.Combine(dir, "HJKGKiller_WPF.exe");  //下载后zip文件的完整路径
            }


            string f = System.IO.Directory.GetCurrentDirectory() + "/" + name;
            try
            {
                if (File.Exists(f))
                {
                    if (type == "IPv6ToolBox")
                    {
                        File.Copy(f, System.IO.Directory.GetCurrentDirectory() + "/IPv6ToolBox.bak");
                    }
                    else if (type == "IP")
                    {
                        File.Copy(f, System.IO.Directory.GetCurrentDirectory() + "/HJKGKiller_WPF.bak");
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
            
            try
            {
                string appname = name.Replace(".exe", "");  //A名字，不要路径，不要.exe
                Process[] processes = Process.GetProcessesByName(appname);
                foreach (var p in processes)
                    p.Kill();
                await Task.Delay(500);

                WebClient client = new WebClient();
                if (type == "IPv6ToolBox")
                {
                    client.DownloadFile("http://60.12.230.82:10086/IPv6/bin/IPv6ToolBox_" + update.Substring(1, 4) + ".exe", file);
                    File.Delete(System.IO.Directory.GetCurrentDirectory() + "/IPv6ToolBox.bak");
                    if (name != "IPv6ToolBox.exe")
                    {
                        File.Delete(f);
                    }
                }
                else if (type == "IP")
                {
                    client.DownloadFile("http://60.12.230.82:10086/HJKGKiller/bin/HJKGKiller_WPF_" + update.Substring(1, 4) + ".exe", file);
                    File.Delete(System.IO.Directory.GetCurrentDirectory() + "/HJKGKiller_WPF.bak");
                    if (name != "HJKGKiller_WPF.exe")
                    {
                        File.Delete(f);
                    }
                }

            }
            catch (Exception e)
            {
                tb.Text = "更新失败...";
                if (type == "IPv6ToolBox")
                {
                    File.Move(System.IO.Directory.GetCurrentDirectory() + "/IPv6ToolBox.bak", f);
                }
                else if (type == "IP")
                {
                    File.Move(System.IO.Directory.GetCurrentDirectory() + "/HJKGKiller_WPF.bak", f);
                }
                MessageBox.Show(e.Message);
            }
            await Task.Delay(500);
            System.Diagnostics.Process.Start(file);
            Application.Current.Shutdown();

        }
    }
}
