using Amazon.EC2.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zrcwaw_l2.Models;

namespace zrcwaw_l2.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class AwsC2Controller : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetInstances()
        {
            var instances = await AwsC2InstanceManager.DescribeInstances();
            return Json(instances);
        }

        [HttpPost]
        [Route("start")]
        public async Task<IActionResult> StartInstance([FromBody] InstanceData data)
        {
            var response = await AwsC2InstanceManager.StartInstance(data.InstanceId);
            return Json(response);
        }

        [HttpPost]
        [Route("stop")]
        public async Task<IActionResult> StopInstance([FromBody] InstanceData data)
        {
            var response = await AwsC2InstanceManager.StopInstance(data.InstanceId);
            return Json(response);
        }
    }
}
