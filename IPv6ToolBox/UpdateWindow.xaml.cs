using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace IPv6ToolBox
{
    /// <summary>
    /// UpdateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateWindow : Window
    {
        public UpdateWindow()
        {
            InitializeComponent();
        }

        public UpdateWindow(string update)
        {
            InitializeComponent();



            updateTextBox.Text = update;
            //var result = MessageBox.Show("程序存在更新，是否下载？", "检查更新", MessageBoxButton.OKCancel);
            //if (result == MessageBoxResult.OK)
            //{
            //    System.Diagnostics.Process.Start("http://60.12.230.82:10086/bin/ConfigBuilder_WPF_" + update + ".exe");
            //}
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Update()
        {
            try
            {
                string dir = System.IO.Directory.GetCurrentDirectory();  //程序所在文件夹路径
                string file = System.IO.Path.Combine(dir, "Update.exe");  //下载后zip文件的完整路径

                WebClient client = new WebClient();
                client.DownloadFile("http://60.12.230.82:10086/IPv6/Update.exe", file);
                Debug.WriteLine(Process.GetCurrentProcess().MainModule.FileName.Split('\\').Last());
                System.Diagnostics.Process.Start(file, Process.GetCurrentProcess().MainModule.FileName.Split('\\').Last());

            }
            catch (Exception)
            {
                MessageBox.Show("自动更新存在问题，请确保目录可读写，并且程序不在系统磁盘或目录", "检查更新");
            }


        }


    }
}
