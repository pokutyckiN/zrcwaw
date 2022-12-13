using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zrcwaw_l2.Models;

namespace zrcwaw_l2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3Controller : Controller
    {
        [HttpGet]
        [Route("getBuckets")]
        public async Task<IActionResult> GetBuckets()
        {
            var buckets = await S3Manager.ListBuckets();
            return Json(buckets);
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] BucketData data)
        {
            var putRequest = new PutObjectRequest()
            {
                BucketName = data.BucketName,
                Key = data.Name,
                ContentType = data.Type,
                FilePath = "C:\\Users\\strze\\OneDrive\\Pulpit\\" + data.Name,
            };

            var result = await S3Manager.PutObjectAsync(putRequest);
            
            return Ok(result);
            
        }
    }
}
