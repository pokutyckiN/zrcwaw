using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zrcwaw_l2.Auth;
using zrcwaw_l2.Models;

namespace zrcwaw_l2
{
    public class Logs : ILogs
    {
        private static readonly IAmazonDynamoDB _client;
        static Logs()
        {
            _client = new AmazonDynamoDBClient(Amazon.RegionEndpoint.USEast1);
        }

        public async Task<LogsData[]> GetLogs()
        {
            var result = await _client.ScanAsync(new ScanRequest
            {
                TableName = "logs"

            });

            if (result != null && result.Items != null)
            {
                var logi = new List<Models.LogsData>();
                foreach (var item in result.Items)
                {
                    item.TryGetValue("id", out var id);
                    item.TryGetValue("url", out var url);
                    item.TryGetValue("ip", out var ip);
                    item.TryGetValue("username", out var username);
                    item.TryGetValue("request", out var request);
                    item.TryGetValue("methodname", out var methodname);
                    item.TryGetValue("date", out var date);

                    logi.Add(new Models.LogsData
                    {
                        Id = id?.S,
                        Url = url?.S,
                        Ip = ip?.S,
                        UserName = username?.S,
                        Request = request.S,
                        MethodName = methodname?.S,
                        Date = date?.S
                    });
                }
                return logi.ToArray();
            }
            return Array.Empty<Models.LogsData>();
        }

        public async Task<bool> InsertLog(Models.LogsData logsData)
        {
            var request = new PutItemRequest
            {
                TableName = "logs",
                Item = new Dictionary<string, AttributeValue>
                {
                    {"id", new AttributeValue(System.Guid.NewGuid().ToString()) },
                    {"url", new AttributeValue(logsData.Url != null ? logsData.Url : "null") },
                    {"ip", new AttributeValue(logsData.Ip != null ? logsData.Ip : "null") },
                    {"username", new AttributeValue(Authorization.CurrentUser != null ? Authorization.CurrentUser.Username : "niezalogowany" ) },
                    {"request", new AttributeValue(logsData.Request != null ? logsData.Request : "null") },
                    {"methodname", new AttributeValue(logsData.MethodName != null ? logsData.MethodName : "null") },
                    {"date", new AttributeValue(DateTime.Now.ToString()) }
                }
    };
            var response = await _client.PutItemAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
