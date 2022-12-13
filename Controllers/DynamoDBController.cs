using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zrcwaw_l2.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class DynamoDBController : Controller
    {
        private readonly IDynamoDBCRUD _dBCRUD;

        public DynamoDBController(IDynamoDBCRUD dBCRUD)
        {
            _dBCRUD = dBCRUD; 
        }

        public IActionResult Index()
        {
            return View();
        }
   [HttpPost]
    [Route("createclient")]
    public async Task <IActionResult> Create([FromBody]Models.Client client)
        {
           var response = await _dBCRUD.CreateClient(client);
            return Json(response);
        }

        [HttpGet]
        [Route("getclients")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _dBCRUD.GetClients();
            return Json(response);
        }

        [HttpPut]
        [Route("updateclient/{Id}")]
        public Task<bool> Update(string Id, [FromBody]Models.Client client)
        {
            var response = _dBCRUD.Update(Id, client);
            return response;
        }


        [HttpDelete]
        [Route("deleteclient/{Id}")]
        public Task<bool> Delete(string Id)
        {
            var response = _dBCRUD.Delete(Id);
            return response;
        }
    }
}
