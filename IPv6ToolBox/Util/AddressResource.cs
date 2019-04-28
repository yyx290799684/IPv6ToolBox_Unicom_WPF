﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPv6ToolBox.Util
{
    public class IPindex
    {
        public string ip { get; set; }
        public string index { get; set; }
    }
    public class AddressResource
    {
        public static Dictionary<string, string> IPindexDictionary = new Dictionary<string, string> {
            { "8740", "1" },
            { "8741", "2" },
            { "8742", "3" },
            { "8743", "4" },
            { "8640", "5" },
            { "8641", "6" },
            { "8642", "7" },
            { "8643", "8" }
        };

        public static Dictionary<string, string> SpecialAddressDictionary = new Dictionary<string, string> {
            { "2408:8888::8/128", "中国联通DNS" },
            { "2408:8899::8/128", "中国联通DNS" },
        };

        public static List<string> netmaskList = new List<string>(new string[] {
            "44",
            "48",
            "52",
            "56",
            "60",
            "64",
            "128",
        });

        public static Dictionary<string, string> cityIndexDictionary = new Dictionary<string, string> {
            { "0", "省公司" },
            { "1", "杭州" },
            { "2", "宁波" },
            { "3", "温州" },
            { "4", "金华" },
            { "5", "台州" },
            { "6", "绍兴" },
            { "7", "嘉兴" },
            { "8", "湖州" },
            { "9", "衢州" },
            { "a", "丽水" },
            { "b", "舟山" },
            { "c", "杭州大客户域" },
            { "d", "宁波大客户域" },
            { "e", "预留" },
            { "f", "预留" },
        };

        public static Dictionary<string, string> city2cityIndexDictionary = new Dictionary<string, string> {
            { "省公司","0"},
            { "杭州","1"},
            { "宁波","2"},
            { "温州","3"},
            { "金华","4"},
            { "台州","5"},
            { "绍兴","6"},
            { "嘉兴","7"},
            { "湖州","8"},
            { "衢州","9"},
            { "丽水","a"},
            { "舟山","b"},
            { "杭州大客户域","c"},
            { "宁波大客户域","d"},
        };

        public static Dictionary<string, string> speedIndexDictionary = new Dictionary<string, string> {
            { "100G", "1" },
            { "10G", "2" },
            { "1G", "3" },
            { "100M", "4" },
            { "40G", "5" },
            { "2.5G", "6" },
            { "115M", "7" },
        };

        /// <summary>
        /// 区县对到地市
        /// </summary>
        public static Dictionary<string, string> Qu2ShiDictionary = new Dictionary<string, string> {
            { "上城区","杭州"},
            { "下城区","杭州"},
            { "江干区","杭州"},
            { "拱墅区","杭州"},
            { "西湖区","杭州"},
            { "滨江区","杭州"},
            { "萧山区","杭州"},
            { "余杭区","杭州"},
            { "建德市","杭州"},
            { "富阳市","杭州"},
            { "临安市","杭州"},
            { "桐庐县","杭州"},
            { "淳安县","杭州"},
            { "海曙区","宁波"},
            { "江东区","宁波"},
            { "江北区","宁波"},
            { "北仑区","宁波"},
            { "镇海区","宁波"},
            { "鄞州区","宁波"},
            { "余姚市","宁波"},
            { "慈溪市","宁波"},
            { "奉化市","宁波"},
            { "象山县","宁波"},
            { "宁海县","宁波"},
            { "鹿城区","温州"},
            { "龙湾区","温州"},
            { "瓯海区","温州"},
            { "瑞安市","温州"},
            { "乐清市","温州"},
            { "洞头县","温州"},
            { "永嘉县","温州"},
            { "平阳县","温州"},
            { "苍南县","温州"},
            { "文成县","温州"},
            { "泰顺县","温州"},
            { "南湖区","嘉兴"},
            { "秀洲区","嘉兴"},
            { "海宁市","嘉兴"},
            { "平湖市","嘉兴"},
            { "桐乡市","嘉兴"},
            { "嘉善县","嘉兴"},
            { "海盐县","嘉兴"},
            { "吴兴区","湖州"},
            { "南浔区","湖州"},
            { "德清县","湖州"},
            { "长兴县","湖州"},
            { "安吉县","湖州"},
            { "越城区","绍兴"},
            { "诸暨市","绍兴"},
            { "上虞市","绍兴"},
            { "嵊州市","绍兴"},
            { "绍兴县","绍兴"},
            { "新昌县","绍兴"},
            { "婺城区","金华"},
            { "金东区","金华"},
            { "兰溪市","金华"},
            { "义乌市","金华"},
            { "东阳市","金华"},
            { "永康市","金华"},
            { "武义县","金华"},
            { "浦江县","金华"},
            { "磐安县","金华"},
            { "柯城区","衢州"},
            { "衢江区","衢州"},
            { "江山市","衢州"},
            { "常山县","衢州"},
            { "开化县","衢州"},
            { "龙游县","衢州"},
            { "定海区","舟山"},
            { "普陀区","舟山"},
            { "岱山县","舟山"},
            { "嵊泗县","舟山"},
            { "椒江区","台州"},
            { "黄岩区","台州"},
            { "路桥区","台州"},
            { "温岭市","台州"},
            { "临海市","台州"},
            { "玉环县","台州"},
            { "三门县","台州"},
            { "天台县","台州"},
            { "仙居县","台州"},
            { "莲都区","丽水"},
            { "龙泉市","丽水"},
            { "青田县","丽水"},
            { "缙云县","丽水"},
            { "遂昌县","丽水"},
            { "松阳县","丽水"},
            { "云和县","丽水"},
            { "庆元县","丽水"},
            { "景宁县","丽水"},
        };

        /// <summary>
        /// 区县码对应到区县码序号
        /// </summary>
        public static Dictionary<string, string> QXCode2QXIndexDictionary = new Dictionary<string, string> {
            { "00", "1" }, { "01", "1" },
            { "02", "2" }, { "03", "2" },
            { "04", "3" }, { "05", "3" },
            { "06", "4" }, { "07", "4" },
            { "08", "5" }, { "09", "5" },
            { "0a", "6" }, { "0b", "6" },
            { "0c", "7" }, { "0d", "7" },
            { "0e", "8" }, { "0f", "8" },
            { "10", "9" }, { "11", "9" },
            { "12", "a" }, { "13", "a" },
            { "14", "b" }, { "15", "b" },
            { "16", "c" }, { "17", "c" },
            { "18", "d" }, { "19", "d" },
            { "1a", "1" }, { "1b", "1" },
            { "1c", "2" }, { "1d", "2" },
            { "1e", "3" }, { "1f", "3" },
            { "20", "4" }, { "21", "4" },
            { "22", "5" }, { "23", "5" },
            { "24", "6" }, { "25", "6" },
            { "26", "7" }, { "27", "7" },
            { "28", "8" }, { "29", "8" },
            { "2a", "9" }, { "2b", "9" },
            { "2c", "a" }, { "2d", "a" },
            { "2e", "b" }, { "2f", "b" },
            { "30", "1" }, { "31", "1" },
            { "32", "2" }, { "33", "2" },
            { "34", "3" }, { "35", "3" },
            { "36", "4" }, { "37", "4" },
            { "38", "5" }, { "39", "5" },
            { "3a", "6" }, { "3b", "6" },
            { "3c", "7" }, { "3d", "7" },
            { "3e", "8" }, { "3f", "8" },
            { "40", "9" }, { "41", "9" },
            { "42", "a" }, { "43", "a" },
            { "44", "b" }, { "45", "b" },
            { "46", "1" }, { "47", "1" },
            { "48", "2" }, { "49", "2" },
            { "4a", "3" }, { "4b", "3" },
            { "4c", "4" }, { "4d", "4" },
            { "4e", "5" }, { "4f", "5" },
            { "50", "6" }, { "51", "6" },
            { "52", "7" }, { "53", "7" },
            { "54", "1" }, { "55", "1" },
            { "56", "2" }, { "57", "2" },
            { "58", "3" }, { "59", "3" },
            { "5a", "4" }, { "5b", "4" },
            { "5c", "5" }, { "5d", "5" },
            { "5e", "1" }, { "5f", "1" },
            { "60", "2" }, { "61", "2" },
            { "62", "3" }, { "63", "3" },
            { "64", "4" }, { "65", "4" },
            { "66", "5" }, { "67", "5" },
            { "68", "6" }, { "69", "6" },
            { "6a", "1" }, { "6b", "1" },
            { "6c", "2" }, { "6d", "2" },
            { "6e", "3" }, { "6f", "3" },
            { "70", "4" }, { "71", "4" },
            { "72", "5" }, { "73", "5" },
            { "74", "6" }, { "75", "6" },
            { "76", "7" }, { "77", "7" },
            { "78", "8" }, { "79", "8" },
            { "7a", "9" }, { "7b", "9" },
            { "7c", "1" }, { "7d", "1" },
            { "7e", "2" }, { "7f", "2" },
            { "80", "3" }, { "81", "3" },
            { "82", "4" }, { "83", "4" },
            { "84", "5" }, { "85", "5" },
            { "86", "6" }, { "87", "6" },
            { "88", "1" }, { "89", "1" },
            { "8a", "2" }, { "8b", "2" },
            { "8c", "3" }, { "8d", "3" },
            { "8e", "4" }, { "8f", "4" },
            { "90", "1" }, { "91", "1" },
            { "92", "2" }, { "93", "2" },
            { "94", "3" }, { "95", "3" },
            { "96", "4" }, { "97", "4" },
            { "98", "5" }, { "99", "5" },
            { "9a", "6" }, { "9b", "6" },
            { "9c", "7" }, { "9d", "7" },
            { "9e", "8" }, { "9f", "8" },
            { "a0", "9" }, { "a1", "9" },
            { "a2", "1" }, { "a3", "1" },
            { "a4", "2" }, { "a5", "2" },
            { "a6", "3" }, { "a7", "3" },
            { "a8", "4" }, { "a9", "4" },
            { "aa", "5" }, { "ab", "5" },
            { "ac", "6" }, { "ad", "6" },
            { "ae", "7" }, { "af", "7" },
            { "b0", "8" }, { "b1", "8" },
            { "b2", "9" }, { "b3", "9" },
        };

        /// <summary>
        /// 区县码对应区县名
        /// </summary>
        public static Dictionary<string, string> zhuanxianCityCodeDictionary = new Dictionary<string, string> {
            { "00", "上城区" }, { "01", "上城区" },
            { "02", "下城区" }, { "03", "下城区" },
            { "04", "江干区" }, { "05", "江干区" },
            { "06", "拱墅区" }, { "07", "拱墅区" },
            { "08", "西湖区" }, { "09", "西湖区" },
            { "0a", "滨江区" }, { "0b", "滨江区" },
            { "0c", "萧山区" }, { "0d", "萧山区" },
            { "0e", "余杭区" }, { "0f", "余杭区" },
            { "10", "建德市" }, { "11", "建德市" },
            { "12", "富阳市" }, { "13", "富阳市" },
            { "14", "临安市" }, { "15", "临安市" },
            { "16", "桐庐县" }, { "17", "桐庐县" },
            { "18", "淳安县" }, { "19", "淳安县" },
            { "1a", "海曙区" }, { "1b", "海曙区" },
            { "1c", "江东区" }, { "1d", "江东区" },
            { "1e", "江北区" }, { "1f", "江北区" },
            { "20", "北仑区" }, { "21", "北仑区" },
            { "22", "镇海区" }, { "23", "镇海区" },
            { "24", "鄞州区" }, { "25", "鄞州区" },
            { "26", "余姚市" }, { "27", "余姚市" },
            { "28", "慈溪市" }, { "29", "慈溪市" },
            { "2a", "奉化市" }, { "2b", "奉化市" },
            { "2c", "象山县" }, { "2d", "象山县" },
            { "2e", "宁海县" }, { "2f", "宁海县" },
            { "30", "鹿城区" }, { "31", "鹿城区" },
            { "32", "龙湾区" }, { "33", "龙湾区" },
            { "34", "瓯海区" }, { "35", "瓯海区" },
            { "36", "瑞安市" }, { "37", "瑞安市" },
            { "38", "乐清市" }, { "39", "乐清市" },
            { "3a", "洞头县" }, { "3b", "洞头县" },
            { "3c", "永嘉县" }, { "3d", "永嘉县" },
            { "3e", "平阳县" }, { "3f", "平阳县" },
            { "40", "苍南县" }, { "41", "苍南县" },
            { "42", "文成县" }, { "43", "文成县" },
            { "44", "泰顺县" }, { "45", "泰顺县" },
            { "46", "南湖区" }, { "47", "南湖区" },
            { "48", "秀洲区" }, { "49", "秀洲区" },
            { "4a", "海宁市" }, { "4b", "海宁市" },
            { "4c", "平湖市" }, { "4d", "平湖市" },
            { "4e", "桐乡市" }, { "4f", "桐乡市" },
            { "50", "嘉善县" }, { "51", "嘉善县" },
            { "52", "海盐县" }, { "53", "海盐县" },
            { "54", "吴兴区" }, { "55", "吴兴区" },
            { "56", "南浔区" }, { "57", "南浔区" },
            { "58", "德清县" }, { "59", "德清县" },
            { "5a", "长兴县" }, { "5b", "长兴县" },
            { "5c", "安吉县" }, { "5d", "安吉县" },
            { "5e", "越城区" }, { "5f", "越城区" },
            { "60", "诸暨市" }, { "61", "诸暨市" },
            { "62", "上虞市" }, { "63", "上虞市" },
            { "64", "嵊州市" }, { "65", "嵊州市" },
            { "66", "绍兴县" }, { "67", "绍兴县" },
            { "68", "新昌县" }, { "69", "新昌县" },
            { "6a", "婺城区" }, { "6b", "婺城区" },
            { "6c", "金东区" }, { "6d", "金东区" },
            { "6e", "兰溪市" }, { "6f", "兰溪市" },
            { "70", "义乌市" }, { "71", "义乌市" },
            { "72", "东阳市" }, { "73", "东阳市" },
            { "74", "永康市" }, { "75", "永康市" },
            { "76", "武义县" }, { "77", "武义县" },
            { "78", "浦江县" }, { "79", "浦江县" },
            { "7a", "磐安县" }, { "7b", "磐安县" },
            { "7c", "柯城区" }, { "7d", "柯城区" },
            { "7e", "衢江区" }, { "7f", "衢江区" },
            { "80", "江山市" }, { "81", "江山市" },
            { "82", "常山县" }, { "83", "常山县" },
            { "84", "开化县" }, { "85", "开化县" },
            { "86", "龙游县" }, { "87", "龙游县" },
            { "88", "定海区" }, { "89", "定海区" },
            { "8a", "普陀区" }, { "8b", "普陀区" },
            { "8c", "岱山县" }, { "8d", "岱山县" },
            { "8e", "嵊泗县" }, { "8f", "嵊泗县" },
            { "90", "椒江区" }, { "91", "椒江区" },
            { "92", "黄岩区" }, { "93", "黄岩区" },
            { "94", "路桥区" }, { "95", "路桥区" },
            { "96", "温岭市" }, { "97", "温岭市" },
            { "98", "临海市" }, { "99", "临海市" },
            { "9a", "玉环县" }, { "9b", "玉环县" },
            { "9c", "三门县" }, { "9d", "三门县" },
            { "9e", "天台县" }, { "9f", "天台县" },
            { "a0", "仙居县" }, { "a1", "仙居县" },
            { "a2", "莲都区" }, { "a3", "莲都区" },
            { "a4", "龙泉市" }, { "a5", "龙泉市" },
            { "a6", "青田县" }, { "a7", "青田县" },
            { "a8", "缙云县" }, { "a9", "缙云县" },
            { "aa", "遂昌县" }, { "ab", "遂昌县" },
            { "ac", "松阳县" }, { "ad", "松阳县" },
            { "ae", "云和县" }, { "af", "云和县" },
            { "b0", "庆元县" }, { "b1", "庆元县" },
            { "b2", "景宁县" }, { "b3", "景宁县" },
        };

        public static Dictionary<string, Dictionary<string, string>> cityCodeDictionary = new Dictionary<string, Dictionary<string, string>>{
            { "0", new Dictionary<string, string>
                {//省公司
                    { "f","联通自有业务"},
                }
            },
            { "1", new Dictionary<string, string>
                {//杭州
                    { "1","上城区"},
                    { "2","下城区"},
                    { "3","江干区"},
                    { "4","拱墅区"},
                    { "5","西湖区"},
                    { "6","滨江区"},
                    { "7","萧山区"},
                    { "8","余杭区"},
                    { "9","建德市"},
                    { "a","富阳市"},
                    { "b","临安市"},
                    { "c","桐庐县"},
                    { "d","淳安县"},
                    { "f","联通自有业务"},
                }
            },
            { "2", new Dictionary<string, string>
                {//宁波
                    { "1","海曙区"},
                    { "2","江东区"},
                    { "3","江北区"},
                    { "4","北仑区"},
                    { "5","镇海区"},
                    { "6","鄞州区"},
                    { "7","余姚市"},
                    { "8","慈溪市"},
                    { "9","奉化市"},
                    { "a","象山县"},
                    { "b","宁海县"},
                    { "f","联通自有业务"},
                }
            },
            { "3", new Dictionary<string, string>
                {//温州
                    { "1","鹿城区"},
                    { "2","龙湾区"},
                    { "3","瓯海区"},
                    { "4","瑞安市"},
                    { "5","乐清市"},
                    { "6","洞头县"},
                    { "7","永嘉县"},
                    { "8","平阳县"},
                    { "9","苍南县"},
                    { "a","文成县"},
                    { "b","泰顺县"},
                    { "f","联通自有业务"},
                }
            },
            { "4", new Dictionary<string, string>
                {//金华
                    { "1","婺城区"},
                    { "2","金东区"},
                    { "3","兰溪市"},
                    { "4","义乌市"},
                    { "5","东阳市"},
                    { "6","永康市"},
                    { "7","武义县"},
                    { "8","浦江县"},
                    { "9","磐安县"},
                    { "f","联通自有业务"},
                }
            },
            { "5", new Dictionary<string, string>
                {//台州
                    { "1","椒江区"},
                    { "2","黄岩区"},
                    { "3","路桥区"},
                    { "4","温岭市"},
                    { "5","临海市"},
                    { "6","玉环县"},
                    { "7","三门县"},
                    { "8","天台县"},
                    { "9","仙居县"},
                    { "f","联通自有业务"},
                }
            },
            { "6", new Dictionary<string, string>
                {//绍兴
                    { "1","越城区"},
                    { "2","诸暨市"},
                    { "3","上虞市"},
                    { "4","嵊州市"},
                    { "5","绍兴县"},
                    { "6","新昌县"},
                    { "f","联通自有业务"},
                }
            },
            { "7", new Dictionary<string, string>
                {//嘉兴
                    { "1","南湖区"},
                    { "2","秀洲区"},
                    { "3","海宁市"},
                    { "4","平湖市"},
                    { "5","桐乡市"},
                    { "6","嘉善县"},
                    { "7","海盐县"},
                    { "f","联通自有业务"},
                }
            },
            { "8", new Dictionary<string, string>
                {//湖州
                    { "1","吴兴区"},
                    { "2","南浔区"},
                    { "3","德清县"},
                    { "4","长兴县"},
                    { "5","安吉县"},
                    { "f","联通自有业务"},
                }
            },
            { "9", new Dictionary<string, string>
                {//衢州
                    { "1","柯城区"},
                    { "2","衢江区"},
                    { "3","江山市"},
                    { "4","常山县"},
                    { "5","开化县"},
                    { "6","龙游县"},
                    { "f","联通自有业务"},
                }
            },
            { "a", new Dictionary<string, string>
                {//丽水
                    { "1","莲都区"},
                    { "2","龙泉市"},
                    { "3","青田县"},
                    { "4","缙云县"},
                    { "5","遂昌县"},
                    { "6","松阳县"},
                    { "7","云和县"},
                    { "8","庆元县"},
                    { "9","景宁县"},
                    { "f","联通自有业务"},
                }
            },
            { "b", new Dictionary<string, string>
                {//舟山
                    { "1","定海区"},
                    { "2","普陀区"},
                    { "3","岱山县"},
                    { "4","嵊泗县"},
                }
            },
            { "c", new Dictionary<string, string>
                {//杭州大客户域
                    { "1","滨江一号楼"},
                    { "2","滨江二号楼"},
                    { "3","谷易"},
                    { "4","通普"},
                    { "5","环北"},
                    { "6","德清"},
                    { "f","联通自有业务"},
                }
            },
            { "d", new Dictionary<string, string>
                {//宁波大客户域
                    { "1","新大楼"},
                    { "2","科通"},
                    { "3","梅墟"},
                    { "f","联通自有业务"},
                }
            },
        };

    }
}
