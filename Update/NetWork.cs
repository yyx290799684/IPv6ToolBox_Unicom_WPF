using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Update
{
    public class NetWork
    {
        public static async Task<string> getHttpWebRequest(string api, List<KeyValuePair<String, String>> paramList = null, int PostORGet = 0)
        {
            string content = "";
            return await Task.Run(() =>
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    try
                    {
                        HttpClient httpClient = new HttpClient();
                        string uri;
                        uri = api;
                        HttpRequestMessage requst;
                        System.Net.Http.HttpResponseMessage response;
                        if (PostORGet == 0)
                        {
                            requst = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));
                            response = httpClient.PostAsync(new Uri(uri), new FormUrlEncodedContent(paramList)).Result;
                        }
                        else
                        {
                            requst = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));
                            response = httpClient.GetAsync(new Uri(uri)).Result;
                        }
                        if (response.StatusCode == HttpStatusCode.OK)
                            content = response.Content.ReadAsStringAsync().Result;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message + "网络请求异常");
                    }
                }
                else
                {
                }
                //if (content.IndexOf("{") != 0)
                //    return "";
                //else
                return content;

            });
        }

    }
}
