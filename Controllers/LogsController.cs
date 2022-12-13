using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zrcwaw_l2.Controllers
{
    [Route("api/logs/[controller]")]
    [ApiController]
    public class LogsController : Controller
    {
        private readonly ILogs _ilogs;

        public LogsController(ILogs ilogs)
        {
            _ilogs = ilogs;
        }

        [HttpGet]
        [Route("getAllLogs")]
        public async Task<IActionResult> GetAllLogs()
        {
            var response = await _ilogs.GetLogs();
            return Json(response);
        }

        [HttpPost]
        [Route("insertLog")]
        public async Task<IActionResult> InsertLog([FromBody] Models.LogsData logsData)
        {
            var response = await _ilogs.InsertLog(logsData);
            return Json(response);
        }
    }
}
