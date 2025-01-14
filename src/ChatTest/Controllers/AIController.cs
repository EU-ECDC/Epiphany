using Azure.AI.OpenAI;
using Azure;
using ChatTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace ChatTest.Controllers
{
    public class AIController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AIModel aiModel;

        public AIController(IConfiguration configuration)
        {
            _configuration = configuration;
            aiModel = new AIModel(configuration);
        }

        [HttpPost]
        [Route("ai/ask")]
        public async Task<ActionResult> Ask([FromBody] AskModel askData)
        {
            var responseData = new
            {
                answer = await aiModel.AskTheMachine(askData.modelType, askData.prompt)
            };

            return Json(responseData);
        }
    }
}
