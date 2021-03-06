﻿using IPv6ToolBox.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace IPv6ToolBox
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string ipv6YW = string.Empty;
        string ipv6YWFull = string.Empty;
        List<string> ipv6LoopbackList = new List<string>();
        List<string[]> ipv6InternetAddressList = new List<string[]>();
        string ipv6Info = string.Empty;

        int customDevicesCount = 0;
        int customLineCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            checkUpdateAsync();
        }

        private async Task checkUpdateAsync(int needBack = 0)
        {
            var update = await NetWork.getHttpWebRequest("http://60.12.230.82:10086/IPv6/update.txt", PostORGet: 1);
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Replace(".", "");

            try
            {
                if (int.Parse(update.Substring(1, 4)) > int.Parse(version))
                {
                    UpdateWindow updateWindow = new UpdateWindow(update);
                    updateWindow.ShowInTaskbar = false;
                    updateWindow.Owner = this;
                    updateWindow.Show();
                }
                else if (needBack == 1)
                {
                    MessageBox.Show("暂无更新");
                }

                string fileToBeDeleted = System.IO.Directory.GetCurrentDirectory() + "/Update.exe";
                if (File.Exists(fileToBeDeleted))
                {
                    File.Delete(fileToBeDeleted);
                }
            }
            catch (Exception e)
            {
                if (needBack == 1)
                {
                    MessageBox.Show("更新失败->" + e.Message);
                }
            }

        }

        private void buildButton_Click(object sender, RoutedEventArgs e)
        {
            OutLoopbackTextBox.Text = string.Empty;
            //OutNetAddressTextBox.Text = string.Empty;
            OutNetAddressRichTextBox.Document.Blocks.Clear();

            OutInfoTextBox.Text = string.Empty;



            ipv6YW = ipv6YWTextBox.Text.Trim().ToLower().Replace("：", ":");

            if (ipv6YW.IndexOf("/") == -1)
            {
                MessageBox.Show("请输入掩码...");
                return;
            }

            if (isSpecialAddress())
            {
                return;
            }

            if (!CheckData())
            {
                MessageBox.Show("信息输入有误，请检查...");
                return;
            }



            ipv6YWFull = Utils.IPv62FullAddress(ipv6YWTextBox.Text);
            try
            {
                IPv6CalculateTools ipv6CalculateTools = new IPv6CalculateTools(ipv6YWFull, customDevicesCount, customLineCount, (SpeedComboBox.SelectedValue as ComboBoxItem).Content.ToString());
                ipv6LoopbackList = ipv6CalculateTools.GetIPv6LoopBack();
                ipv6InternetAddressList = ipv6CalculateTools.GetIPv6InternetAddress();
                ipv6Info = ipv6CalculateTools.GetIPv6Info();
                ShowAddressAsync();
            }
            catch (Exception)
            {
                MessageBox.Show("信息输入有误，请检查...");
                return;
            }
        }

        private bool isSpecialAddress()
        {
            try
            {
                OutInfoTextBox.Text = AddressResource.SpecialAddressDictionary[ipv6YW];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task ShowAddressAsync()
        {
            StringBuilder loopbackStringBuilder = new StringBuilder();
            for (int i = 0; i < ipv6LoopbackList.Count; i++)
            {
                loopbackStringBuilder.AppendLine("第" + (i + 1) + "个Loopback：" + Utils.IPv62ShortAddress(ipv6LoopbackList[i]));
            }
            OutLoopbackTextBox.Text = loopbackStringBuilder.ToString();

            StringBuilder internetStringBuilder = new StringBuilder();
            if (ipv6LoopbackList.Count > 1)
            {
                for (int i = 0, j = 0; j < ipv6LoopbackList.Count; j++)
                {
                    for (int k = 0; k < customLineCount; k++)
                    {
                        internetStringBuilder.AppendLine("第" + (j + 1) + "台设备" + "第" + (k + 1) + "个方向互联地址：联通->" + Utils.IPv62ShortAddress(ipv6InternetAddressList[i][0]) + "  用户->" + Utils.IPv62ShortAddress(ipv6InternetAddressList[i][1]));
                        i++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < ipv6InternetAddressList.Count; i++)
                {
                    internetStringBuilder.AppendLine("第" + (i + 1) + "个方向互联地址：联通->" + Utils.IPv62ShortAddress(ipv6InternetAddressList[i][0]) + "  用户->" + Utils.IPv62ShortAddress(ipv6InternetAddressList[i][1]));
                }
            }
            OutNetAddressRichTextBox.AppendText(internetStringBuilder.ToString());
            await Task.Delay(1);
            TextRange textRange = new TextRange(OutNetAddressRichTextBox.Document.ContentStart, OutNetAddressRichTextBox.Document.ContentEnd);
            textRange.ClearAllProperties();
            TextPointer position = OutNetAddressRichTextBox.Document.ContentStart;

            while (position != null)
            {
                //向前搜索,需要内容为Text
                Debug.WriteLine(position.GetPointerContext(LogicalDirection.Forward));
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    //拿出Run的Text
                    string text = position.GetTextInRun(LogicalDirection.Forward);
                    //可能包含多个keyword,做遍历查找

                    int temp = 0;

                    List<string> useKeywords = new List<string>(new string[] {
                        "102/127",
                        "103/127",
                    });

                    //Debug.WriteLine("text -> " + text + " ->从" + temp + "开始搜索 -> " + "搜索 -> " + useKeywords[i] + " -> index -> " + index);
                    List<TextRange> hintTextRangeList = new List<TextRange>();
                    for (int i = 0; i < useKeywords.Count; i++)
                    {
                        int index = -1;
                        index = text.IndexOf(useKeywords[i], temp);

                        if (index != -1)
                        {
                            TextPointer start = position.GetPositionAtOffset(index);
                            TextPointer end = start.GetPositionAtOffset(1);
                            TextRange range = new TextRange(start, end);
                            hintTextRangeList.Add(range);
                        }
                    }
                    foreach (var item in hintTextRangeList)
                    {
                        item.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                    }
                }
                try
                {
                    position = position.GetNextContextPosition(LogicalDirection.Forward);
                }
                catch (Exception)
                {
                    return;
                }

            }

            //OutNetAddressTextBox.Text = internetStringBuilder.ToString();
            OutInfoTextBox.Text = ipv6Info;
        }

        private bool CheckData()
        {
            if (ipv6YW == string.Empty || customDevicesCountTextBox.Text == string.Empty)
            {
                return false;
            }
            if (!int.TryParse(customDevicesCountTextBox.Text, out customDevicesCount))
            {
                customDevicesCount = 0;
                return false;
            }
            if (!int.TryParse((customLineCountComboBox.SelectedItem as ComboBoxItem).Content.ToString(), out customLineCount))
            {
                customLineCount = 0;
                return false;
            }
            if (AddressResource.netmaskList.IndexOf(ipv6YW.Split('/').Last()) == -1)
            {
                OutInfoTextBox.Text = "掩码长度非法";
                return false;
            }
            if (ipv6YW.Length < 4)
            {
                OutInfoTextBox.Text = "该地址非浙江联通IPv6地址段";
                return false;
            }
            if (ipv6YW.Substring(0, 4) != "2408")
            {
                OutInfoTextBox.Text = "该地址非浙江联通IPv6地址段";
                return false;
            }
            return Utils.isIPv6Address(ipv6YWTextBox.Text.Trim());
        }

        private void updateTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            checkUpdateAsync(1);
        }

        private void githubTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/yyx290799684/IPv6ToolBox_Unicom_WPF");
        }

        private void ipv6YWTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string ipv6 = string.Empty;
            IPv6CalculateWindow ipv6CalculateWindow = new IPv6CalculateWindow(ipv6);
            ipv6CalculateWindow.ShowInTaskbar = false;
            ipv6CalculateWindow.Owner = this;
            if (ipv6CalculateWindow.ShowDialog() == true)
            {
                Debug.WriteLine(ipv6CalculateWindow.Ipv6);
                ipv6YWTextBox.Text = ipv6CalculateWindow.Ipv6;
            }



        }
    }
}
