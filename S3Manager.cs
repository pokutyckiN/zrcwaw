using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zrcwaw_l2
{
    public static class S3Manager
    {
        private static readonly IAmazonS3 _client;

        static S3Manager()
        {
            _client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
        }

        public static Task<ListBucketsResponse> ListBuckets()
        {
            var response = _client.ListBucketsAsync();
            return response;
        }
        public static Task<PutObjectResponse> PutObjectAsync(PutObjectRequest putRequest)
        {
            var result = _client.PutObjectAsync(putRequest);
            return result;
        }
        public static Task<ListObjectsResponse> ListObjects(String bucketName)
        {
            var response = _client.ListObjectsAsync(bucketName);
            return response;
        }
    }
}
