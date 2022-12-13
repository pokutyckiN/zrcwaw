using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zrcwaw_l2.Models;

namespace zrcwaw_l2.Controllers
{
    [Route("api/rek/[controller]")]
    [ApiController]
    public class RekognitionController : Controller
    {
        [HttpPost]
        [Route("getLabels")]
        public async Task<IActionResult> GetLabels([FromBody] LabelData data)
        {
            var labels = await RekognitionManager.GetLabels(data.BucketName, data.FileName);
            return Json(labels);
        }

        [HttpGet]
        [Route("getFiles")]
        public async Task<IActionResult> GetFiles()
        {
            var buckets = await S3Manager.ListBuckets();
            var objects = (Amazon.S3.Model.ListObjectsResponse)null;

            foreach (var bucket in buckets.Buckets)
            {
                objects = await S3Manager.ListObjects(bucket.BucketName);
            }
            return Json(objects);
        }

        [HttpPost]
        [Route("getText")]
        public async Task<IActionResult> GetText([FromBody] LabelData data)
        {
            var text = await RekognitionManager.GetText(data.BucketName, data.FileName);
            return Json(text);
        }
    }
}
