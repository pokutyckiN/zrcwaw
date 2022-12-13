using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
namespace zrcwaw_l2
{
    public class DynamoDBCRUD : IDynamoDBCRUD
    {
        private static readonly IAmazonDynamoDB _client;

        static DynamoDBCRUD()
        {
            _client = new AmazonDynamoDBClient(Amazon.RegionEndpoint.USEast1);
        }

        public async Task<bool> CreateClient(Models.Client client)
        {
            var request = new PutItemRequest
            {
                TableName = "clientTable",
                Item = new Dictionary<string, AttributeValue>
                {
                    { "Name", new AttributeValue(client.Name) },

                    {"Surname", new AttributeValue(client.Surname) },

                    {"ClientId", new AttributeValue(client.ClientId) }
                }

            };
            var response = await _client.PutItemAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }


        public async Task<Models.Client[]> GetClients()
        {
            
            var result = await _client.ScanAsync(new ScanRequest
            {
                TableName = "clientTable"

            });

            if(result!=null && result.Items!=null)
            {
                var clients = new List<Models.Client>();
                foreach(var item in result.Items)
                {
                    item.TryGetValue("Name", out var name);
                    item.TryGetValue("Surname", out var surname);
                    item.TryGetValue("ClientId", out var id);

                    clients.Add(new Models.Client
                    {
                        Name=name?.S,
                        Surname=surname?.S,
                        ClientId=id?.S
                    });
                }
                return clients.ToArray();
            }
            return Array.Empty<Models.Client>();
        }


        public async Task<Models.Client> GetOneClient(string Id)
        {


            var dictionary = new Dictionary<string, AttributeValue>
            {
                { "ClientId", new AttributeValue(Id)}

            };
            var request = new GetItemRequest("clientTable", dictionary);
            
            var result = await _client.GetItemAsync(request);
            result.Item.TryGetValue("Name", out var name);
            result.Item.TryGetValue("Surname", out var surname);
            var client = new Models.Client()
            {
                Name = name?.S,
                Surname = surname?.S

            };

            return client;
        }

        


        public async Task<bool> Update(string Id, Models.Client client)
        {
            

            var request = new PutItemRequest
            {
                TableName = "clientTable",
                Item = new Dictionary<string, AttributeValue>
                {
                    { "Name", new AttributeValue(client.Name) },

                    {"Surname", new AttributeValue(client.Surname) },

                    {"ClientId", new AttributeValue(Id) }



                }
            };

            var response = await _client.PutItemAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }



        public async Task<bool> Delete(string Id)
        {
            

            var request = new DeleteItemRequest
            {
                TableName = "clientTable",
                Key = new Dictionary<string, AttributeValue>() { { "ClientId", new AttributeValue {S = Id} } },
            };

            var response = await _client.DeleteItemAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }


    }
}
