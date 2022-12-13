using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using zrcwaw_l2.Models;

namespace zrcwaw_l2.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class PollyController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Speak([FromBody] SpeechData data)
        {
            var voice = await Polly.Speak(data);
            MemoryStream stream = new();
            await voice.CopyToAsync(stream);

            return File(stream.ToArray(), "audio/mpeg");
        }
    }
}
