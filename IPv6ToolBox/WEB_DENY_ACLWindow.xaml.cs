using IPv6ToolBox.Util;
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
    /// WEB_DENY_ACLWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WEB_DENY_ACLWindow : Window
    {
        string ipv4tmp = " rule {0} deny tcp destination {1} {2} destination-port eq {3}\r\n";
        string ipv6tmp = " rule {0} deny tcp destination {1} destination-port eq {2}\r\n";

        public WEB_DENY_ACLWindow()
        {
            InitializeComponent();
        }

        private void buildButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                retTextBox.Text = string.Empty;
                if (string.IsNullOrEmpty(indexTextBox.Text) || string.IsNullOrEmpty(ipTextBox.Text))
                {
                    MessageBox.Show("Empty");
                    return;
                }
                int index = int.Parse(indexTextBox.Text);
                string ips = ipTextBox.Text.Replace("\"", "").Replace("\r", "");
                var iplist = ips.Split('\n');
                if (ipv4RadioButton.IsChecked == true)
                {
                    foreach (var ip in iplist)
                    {
                        if (string.IsNullOrEmpty(ip))
                        {
                            continue;
                        }
                        var iparray = ip.Split('/');
                        string ipa = string.Empty;
                        string ipm = string.Empty;
                        string ipmr = string.Empty;
                        if (iparray.Length == 2)
                        {
                            ipa = iparray[0];
                            ipm = iparray[1];
                        }
                        else
                        {
                            ipa = iparray[0];
                            ipm = "32";
                        }
                        ipmr = AddressResource.WildcardMaskDictionary[ipm];
                        retTextBox.Text += string.Format(ipv4tmp, index++, ipa, ipmr, "www");
                        retTextBox.Text += string.Format(ipv4tmp, index++, ipa, ipmr, "8080");
                        retTextBox.Text += string.Format(ipv4tmp, index++, ipa, ipmr, "443");
                    }
                }
                else
                {
                    foreach (var ip in iplist)
                    {
                        if (string.IsNullOrEmpty(ip))
                        {
                            continue;
                        }
                        retTextBox.Text += string.Format(ipv6tmp, index++, ip, "www");
                        retTextBox.Text += string.Format(ipv6tmp, index++, ip, "8080");
                        retTextBox.Text += string.Format(ipv6tmp, index++, ip, "443");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }
    }
}
