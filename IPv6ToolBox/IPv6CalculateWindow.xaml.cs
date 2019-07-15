using IPv6ToolBox.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    /// IPv6CalculateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IPv6CalculateWindow : Window
    {
        public static string ipv6 = string.Empty;

        public string Ipv6 { get; set; }

        public IPv6CalculateWindow()
        {
            InitializeComponent();
        }
        public IPv6CalculateWindow(string ipv6)
        {
            InitializeComponent();

            Init();

        }



        private void Init()
        {
            cityComboBox.ItemsSource = AddressResource.city2cityIndexDictionary;
            cityComboBox.SelectedValue = "Value";
            cityComboBox.DisplayMemberPath = "Key";
            cityComboBox.SelectedIndex = Properties.Settings.Default.IDCIPv6CalculateCityComboBoxSelectedIndex;

            AddressPrefixComboBoxIDCAddressPrefixComboBox.ItemsSource = AddressResource.AddressPrefixDictionary;
            AddressPrefixComboBoxIDCAddressPrefixComboBox.SelectedValue = "Value";
            AddressPrefixComboBoxIDCAddressPrefixComboBox.DisplayMemberPath = "Key";
            AddressPrefixComboBoxIDCAddressPrefixComboBox.SelectedIndex = 0;


            IDCAddressPrefixComboBox.ItemsSource = AddressResource.IDCAddressPrefixDictionary;
            IDCAddressPrefixComboBox.SelectedValue = "Value";
            IDCAddressPrefixComboBox.DisplayMemberPath = "Key";
            IDCAddressPrefixComboBox.SelectedIndex = 0;

            ZXAddressPrefixComboBox.ItemsSource = AddressResource.ZXAddressPrefixDictionary;
            ZXAddressPrefixComboBox.SelectedValue = "Value";
            ZXAddressPrefixComboBox.DisplayMemberPath = "Key";
            ZXAddressPrefixComboBox.SelectedIndex = 0;

        }

        private void cityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            Debug.WriteLine(Convert.ToString((sender as ComboBox).SelectedIndex, 16).ToLower());

            if ((bool)IDCRadioButton.IsChecked)
            {
                //IDC
                Properties.Settings.Default.IDCIPv6CalculateCityComboBoxSelectedIndex = (sender as ComboBox).SelectedIndex;

                string cityCode = Convert.ToString((sender as ComboBox).SelectedIndex, 16).ToLower();
                if (cityCode == "c" || cityCode == "d")
                {
                    IDCNumComboBox.ItemsSource = AddressResource.cityCodeDictionary[cityCode];
                    IDCNumComboBox.SelectedValue = "Key";
                    IDCNumComboBox.DisplayMemberPath = "Value";
                    IDCNumComboBox.SelectedIndex = 0;
                    IDCNumComboBox.Visibility = Visibility.Visible;
                    IDCNumTextBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    IDCNumComboBox.Visibility = Visibility.Collapsed;
                    IDCNumTextBox.Visibility = Visibility.Visible;
                }

            }
            else
            {
                //专线
                Properties.Settings.Default.ZXIPv6CalculateCityComboBoxSelectedIndex = (sender as ComboBox).SelectedIndex;
                ZXQXAddressPrefixComboBox.ItemsSource = AddressResource.zxcityCodeDictionary[Convert.ToString((sender as ComboBox).SelectedIndex, 16).ToLower()];
                ZXQXAddressPrefixComboBox.SelectedValue = "Key";
                ZXQXAddressPrefixComboBox.DisplayMemberPath = "Value";
                ZXQXAddressPrefixComboBox.SelectedIndex = 0;
            }

            Properties.Settings.Default.Save();




        }

        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton.Content.ToString() == "IDC")
            {
                cityComboBox.ItemsSource = AddressResource.city2cityIndexDictionary;
                cityComboBox.SelectedValue = "Value";
                cityComboBox.DisplayMemberPath = "Key";
                cityComboBox.SelectedIndex = Properties.Settings.Default.IDCIPv6CalculateCityComboBoxSelectedIndex;

                IDCGrid.Visibility = Visibility.Visible;
                ZXGrid.Visibility = Visibility.Collapsed;

            }
            else
            {
                cityComboBox.ItemsSource = AddressResource.zxcity2cityIndexDictionary;
                cityComboBox.SelectedValue = "Value";
                cityComboBox.DisplayMemberPath = "Key";
                cityComboBox.SelectedIndex = Properties.Settings.Default.ZXIPv6CalculateCityComboBoxSelectedIndex;

                IDCGrid.Visibility = Visibility.Collapsed;
                ZXGrid.Visibility = Visibility.Visible;
            }
        }

        private void buildButton_Click(object sender, RoutedEventArgs e)
        {
            AddressTextBlock.Text = string.Empty;

            string AddressPrx = string.Empty;
            Debug.WriteLine("前缀:" + AddressPrx);

            string qxorIDCNum = string.Empty;
            if ((bool)IDCRadioButton.IsChecked)
            {
                //IDC
                AddressPrx = AddressResource.IDCAddressPrefixDictionary.Keys.ToArray()[IDCAddressPrefixComboBox.SelectedIndex].Substring(0, 10);

                string cityIndex = Convert.ToString(cityComboBox.SelectedIndex, 16).ToLower();
                Debug.WriteLine("城域网:" + cityIndex);

                string IDCNum = string.Empty;
                if (cityIndex == "c" || cityIndex == "d")
                {
                    IDCNum = AddressResource.cityCodeDictionary[cityIndex].Keys.ToArray()[IDCNumComboBox.SelectedIndex];
                }
                else
                {
                    IDCNum = IDCNumTextBox.Text;
                }
                Debug.WriteLine("机房编号:" + IDCNum);
                qxorIDCNum = cityIndex + IDCNum;
            }
            else
            {
                //专线
                AddressPrx = AddressResource.ZXAddressPrefixDictionary.Keys.ToArray()[IDCAddressPrefixComboBox.SelectedIndex].Substring(0, 10);


                string cityIndex = Convert.ToString(cityComboBox.SelectedIndex, 16).ToLower();
                qxorIDCNum = AddressResource.zxcityCodeDictionary[cityIndex].Keys.ToArray()[ZXQXAddressPrefixComboBox.SelectedIndex];
            }

            Debug.WriteLine("区县码或IDC信息:" + qxorIDCNum);

            string customNum = customNumTextBox.Text;
            int pre = AddressResource.AddressPrefixDictionary.Keys.ToArray()[AddressPrefixComboBoxIDCAddressPrefixComboBox.SelectedIndex];
            string mn = GetMN(customNum, pre);
            Debug.WriteLine(mn);
            if (mn == "error")
            {
                MessageBox.Show("信息输入有误，请检查...");
                return;
            }

            Ipv6 = AddressPrx + qxorIDCNum + mn + "::/" + pre;
            Ipv6 = Utils.IPv62ShortAddress(Utils.IPv62FullAddress(Ipv6));
            Debug.WriteLine(Ipv6);
            AddressTextBlock.Text = Ipv6;
        }

        private string GetMN(string customNum, int pre)

        {
            int num;
            try
            {
                num = int.Parse(customNum);
            }
            catch (Exception)
            {
                return "error";
            }
            int p, c;
            if (num <= 0)
            {
                return "error";
            }
            switch (pre)
            {
                case 64:
                    if (num < 65536)
                        return "ff:" + Convert.ToString(num, 16).ToLower();
                    break;
                case 60:
                    if (num < 4096)
                        return "fe:" + Convert.ToString(num, 16).ToLower() + "0";
                    break;
                case 56:
                    p = 0xfc;
                    c = num / 256;
                    if (c < 2)
                        return Convert.ToString((p + c), 16).ToLower() + ":" + Convert.ToString(num % 256, 16).ToLower() + "00";
                    break;
                case 52:
                    p = 0xf8;
                    c = num / 16;
                    if (c < 4)
                        return Convert.ToString((p + c), 16).ToLower() + ":" + Convert.ToString(num % 16, 16).ToLower() + "000";
                    break;
                case 48:
                    p = 0x8;
                    c = num / 8;
                    if (c < 8)
                        return Convert.ToString((p + c), 16).ToLower() + Convert.ToString(num % 8, 16).ToLower() + ":0000";
                    break;
                case 44:
                    if (num < 8)
                        return (0x1 + num - 1).ToString() + "0:0000";
                    break;
                default:
                    return "error";
            }
            return "error";
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AddressTextBlock.Text))
            {
                this.DialogResult = true;
                this.Close();
            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
