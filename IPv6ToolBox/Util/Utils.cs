using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPv6ToolBox.Util
{
    public class Utils
    {
        public static string IPv62FullAddress(string ipv6)
        {
            ipv6 = ipv6.ToLower().Replace("：", ":");
            var ipv6net = ipv6.Split('/');
            if (ipv6net[0].IndexOf("::") == -1)
            {
                ipv6net[0] += "::";
            }
            var ipv6List = ipv6net[0].Split(':').ToList();
            for (int i = 0; i < ipv6List.Count(); i++)
            {
                if (ipv6List[i].Length < 4)
                {
                    int n = ipv6List[i].Length;
                    for (int j = 0; j < 4 - n; j++)
                    {
                        ipv6List[i] = '0' + ipv6List[i];
                    }
                }
            }
            if (ipv6List.Count() != 8)
            {
                int n = ipv6List.Count();
                for (int i = 0; i < 8 - n; i++)
                {
                    ipv6List.Insert(ipv6List.IndexOf("0000"), "0000");
                }
            }

            StringBuilder ipv6Full = new StringBuilder();
            foreach (var item in ipv6List)
            {
                ipv6Full.Append(item + ":");
            }
            return ipv6Full.ToString().Remove(ipv6Full.Length - 1, 1) + '/' + ipv6net[1];
        }

        public static string IPv62ShortAddress(string ipv6Full)
        {
            var ipv6net = ipv6Full.Split('/');
            return IPAddress.Parse(ipv6net[0]).ToString() + "/" + ipv6net[1];
        }

        public static bool isIPv6Address(string ipv6Full)
        {
            var ipv6net = ipv6Full.Split('/');
            if (ipv6net[0].IndexOf("::") == -1)
            {
                ipv6net[0] += "::";
            }
            IPAddress add;
            return IPAddress.TryParse(ipv6net[0], out add);
        }
    }
}
