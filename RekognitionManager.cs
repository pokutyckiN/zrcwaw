using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Rekognition;

namespace zrcwaw_l2
{
    public static class RekognitionManager
    {
        private static readonly IAmazonRekognition _client;

        static RekognitionManager()
        {
            _client = new AmazonRekognitionClient(Amazon.RegionEndpoint.USEast1);
        }
        public static async Task<Amazon.Rekognition.Model.DetectLabelsResponse> GetLabels(String bucketName, String fileName)
        {
            var response = await _client.DetectLabelsAsync(new Amazon.Rekognition.Model.DetectLabelsRequest()
            {
                Image = new Amazon.Rekognition.Model.Image()
                {
                    S3Object = new Amazon.Rekognition.Model.S3Object()
                    {
                        Bucket = bucketName,
                        Name = fileName
                    }
                }
            });

            return response;
        }

        public static async Task<Amazon.Rekognition.Model.DetectTextResponse> GetText(String bucketName, String fileName)
        {
            var response = await _client.DetectTextAsync(new Amazon.Rekognition.Model.DetectTextRequest()
            {
                Image = new Amazon.Rekognition.Model.Image()
                {
                    S3Object = new Amazon.Rekognition.Model.S3Object()
                    {
                        Bucket = bucketName,
                        Name = fileName
                    }
                }
            });

            return response;
        }
    }
}
