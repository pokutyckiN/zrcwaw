using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace zrcwaw_l2
{
    public static class AwsC2InstanceManager
    {
        private static readonly IAmazonEC2 _client;

        static AwsC2InstanceManager()
        {
            _client = new AmazonEC2Client(RegionEndpoint.USEast1);
        }

        public static async Task<List<Instance>> DescribeInstances()
        {
            var instances = new List<Instance>();
            var response = await _client.DescribeInstancesAsync();

            foreach (var reservation in response.Reservations)
            {
                foreach (var instance in reservation.Instances)
                {
                    instances.Add(instance);
                }
            }

            return instances;
        }

        public static async Task<StartInstancesResponse> StartInstance(string instanceId)
        {
            var ids = new List<string> { instanceId };

            var request = new StartInstancesRequest(ids);

            return await _client.StartInstancesAsync(request);
        }

        public static async Task<StopInstancesResponse> StopInstance(string instanceId)
        {
            var ids = new List<string> { instanceId };
            
            var request = new StopInstancesRequest(ids);
            
            return await _client.StopInstancesAsync(request);
        }
    }
}
