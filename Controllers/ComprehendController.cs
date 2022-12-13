using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zrcwaw_l2.Models;

namespace zrcwaw_l2.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class ComprehendController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> DetectLanguage([FromBody] TextToTranslate text)
        {
            var response = await Comprehend.DetectLanguage(text.Text);
            return Json(response);
        }
    }
}
