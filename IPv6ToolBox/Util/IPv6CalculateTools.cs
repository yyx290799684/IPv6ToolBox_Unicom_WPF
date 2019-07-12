using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPv6ToolBox.Util
{
    class IPv6CalculateTools
    {
        string ipv6Templete = "{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}/{8}";
        string ipv6InfoTemplete = "{0}地址段,归属于{1},位于{2},用户序号为{3},用户共有{4}台设备,每台设备{5}个上联方向";

        private int customDevicesCount;
        private int customLineCount;
        private string speed;

        string ipv6net = string.Empty;//掩码
        List<string> ipv6StringList = new List<string>();
        string carrieroperatorIndex = "2408"; //运营商大段2408
        string ipIndex = string.Empty; //IP地址段序号
        string cityIndex = string.Empty; //城域网标识
        string engineRoomIndex = string.Empty; //机房序号标识
        string markTagIndex = string.Empty; //位长起始标识
        string customIndex = string.Empty; //接入客户序号
        string quxianCode = string.Empty; //区县码，对应AddressResource.zhuanxianCityCodeDictionary

        public IPv6CalculateTools(string ipv6YWFull, int customDevicesCount, int customLineCount, string speed)
        {
            this.customDevicesCount = customDevicesCount;
            this.customLineCount = customLineCount;
            this.speed = speed;

            Address2List(ipv6YWFull);
            ipIndex = AddressResource.IPindexDictionary[ipv6StringList[1]];
            cityIndex = ipv6StringList[2].First().ToString();
            engineRoomIndex = ipv6StringList[2].Substring(1, 1).ToString();

            if (int.Parse(ipv6net) >= 60)
            {
                markTagIndex = ipv6StringList[2].Substring(2, 2).ToString();
                customIndex = ipv6StringList[3].TrimStart('0');
            }
            else if (int.Parse(ipv6net) >= 56)
            {
                markTagIndex = ipv6StringList[2].Substring(2, 2).ToString();
                customIndex = ipv6StringList[3].Substring(0, 2).TrimStart('0');
            }
            else if (int.Parse(ipv6net) >= 48)
            {
                markTagIndex = ipv6StringList[2].Substring(2, 1).ToString() + "0";
                customIndex = ipv6StringList[2].Substring(3, 1).ToString();
            }
            else if (int.Parse(ipv6net) >= 44)
            {
                markTagIndex = "00";
                customIndex = ipv6StringList[2].Substring(2, 1).ToString();
            }

            if (ipv6StringList[1][1] == '6')
            {
                quxianCode = ipv6StringList[2].Substring(0, 2);
            }
        }

        private void Address2List(string ipv6YWFull)
        {
            ipv6net = ipv6YWFull.Split('/').Last();
            ipv6StringList = ipv6YWFull.Split('/').First().Split(':').ToList();
        }

        /// <summary>
        /// 获取对应用户LoopBack地址
        /// </summary>
        /// <returns></returns>
        public List<string> GetIPv6LoopBack()
        {
            List<string> loopbackList = new List<string>();
            for (int i = 0; i < customDevicesCount; i++)
            {
                if (Convert.ToInt32(ipIndex, 16) <= 4)
                {
                    //IDC
                    loopbackList.Add(string.Format(ipv6Templete, carrieroperatorIndex, "8000", "b00" + cityIndex, "0000", customIndex, "0000", ipIndex + engineRoomIndex + markTagIndex, "f" + (i + 1).ToString("X") + "00", "128"));
                }
                else
                {
                    loopbackList.Add(string.Format(ipv6Templete, carrieroperatorIndex, "8000", "b00" + AddressResource.city2cityIndexDictionary[AddressResource.Qu2ShiDictionary[AddressResource.zhuanxianCityCodeDictionary[quxianCode]]], "0000", customIndex, "0000", ipIndex + AddressResource.QXCode2QXIndexDictionary[quxianCode] + markTagIndex, "f" + (i + 1).ToString("X") + "00", "128"));
                }
            }
            return loopbackList;
        }

        /// <summary>
        /// 获取对应用户互联地址
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetIPv6InternetAddress()
        {
            List<string[]> addressList = new List<string[]>();
            for (int j = 0; j < customDevicesCount; j++)
            {
                for (int i = 0; i < customLineCount; i++)
                {
                    if (Convert.ToInt32(ipIndex, 16) <= 4)
                    {
                        //IDC
                        addressList.Add(new string[2] {
                        string.Format(ipv6Templete, carrieroperatorIndex, "8000", "b00" + cityIndex, "0001", customIndex, ipIndex + engineRoomIndex + markTagIndex, "f" + (j + 1).ToString("X")  + i + AddressResource.speedIndexDictionary[speed], "0102", "127") ,
                        string.Format(ipv6Templete, carrieroperatorIndex, "8000", "b00" + cityIndex, "0001", customIndex, ipIndex + engineRoomIndex + markTagIndex, "f" + (j + 1).ToString("X")  + i + AddressResource.speedIndexDictionary[speed], "0103", "127") });
                    }
                    else
                    {
                        addressList.Add(new string[2] {
                        string.Format(ipv6Templete, carrieroperatorIndex, "8000", "b00" + AddressResource.city2cityIndexDictionary[AddressResource.Qu2ShiDictionary[AddressResource.zhuanxianCityCodeDictionary[quxianCode]]], "0001", customIndex, ipIndex + AddressResource.QXCode2QXIndexDictionary[quxianCode] + markTagIndex, "f" + (j + 1).ToString("X") + i + AddressResource.speedIndexDictionary[speed], "0102", "127") ,
                        string.Format(ipv6Templete, carrieroperatorIndex, "8000", "b00" + AddressResource.city2cityIndexDictionary[AddressResource.Qu2ShiDictionary[AddressResource.zhuanxianCityCodeDictionary[quxianCode]]], "0001", customIndex, ipIndex + AddressResource.QXCode2QXIndexDictionary[quxianCode] + markTagIndex, "f" + (j + 1).ToString("X") + i + AddressResource.speedIndexDictionary[speed], "0103", "127") });
                    }
                }
            }

            return addressList;
        }

        /// <summary>
        /// 获取对应平联地址
        /// </summary>
        /// <returns></returns>
        public List<string> GetIPv6PInternetAddress()
        {
            List<string> addressList = new List<string>();
            addressList.Add(string.Format(ipv6Templete, carrieroperatorIndex, "8000", "b00" + AddressResource.city2cityIndexDictionary[AddressResource.Qu2ShiDictionary[AddressResource.zhuanxianCityCodeDictionary[quxianCode]]], "0001", customIndex, ipIndex + engineRoomIndex + markTagIndex, "f1" + "f" + AddressResource.speedIndexDictionary[speed], "0102", "127"));
            addressList.Add(string.Format(ipv6Templete, carrieroperatorIndex, "8000", "b00" + AddressResource.city2cityIndexDictionary[AddressResource.Qu2ShiDictionary[AddressResource.zhuanxianCityCodeDictionary[quxianCode]]], "0001", customIndex, ipIndex + engineRoomIndex + markTagIndex, "f1" + "e" + AddressResource.speedIndexDictionary[speed], "0102", "127"));
            return addressList;
        }

        public string GetIPv6Info()
        {
            string info = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(customIndex))
                {
                    info = "Error:用户序号从1开始，请确认";
                    return info;
                }
                if (Convert.ToInt32(ipIndex, 16) <= 4)
                {
                    if (cityIndex == "c" || cityIndex == "d")
                    {
                        info = string.Format(ipv6InfoTemplete, "IDC", AddressResource.cityIndexDictionary[cityIndex], AddressResource.cityCodeDictionary[cityIndex][engineRoomIndex], Int32.Parse(customIndex, System.Globalization.NumberStyles.HexNumber), customDevicesCount, customLineCount);
                    }
                    else
                    {
                        info = string.Format(ipv6InfoTemplete, "IDC", AddressResource.cityIndexDictionary[cityIndex], "第" + Int32.Parse(engineRoomIndex, System.Globalization.NumberStyles.HexNumber) + "个机房", Int32.Parse(customIndex, System.Globalization.NumberStyles.HexNumber), customDevicesCount, customLineCount);
                    }
                }
                else
                {
                    string qx = AddressResource.zhuanxianCityCodeDictionary[quxianCode];
                    info = string.Format(ipv6InfoTemplete, "专线", AddressResource.Qu2ShiDictionary[qx], qx, Int32.Parse(customIndex, System.Globalization.NumberStyles.HexNumber), customDevicesCount, customLineCount);
                }
            }
            catch (Exception)
            {
                info = "暂无法分析该段地址";
            }

            return info;
        }
    }
}
