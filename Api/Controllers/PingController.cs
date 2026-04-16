using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class PingController : BaseStoreController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new 
            { 
                Message = "Pong! The API is running successfully.", 
                Status = "Healthy" 
            });
        }
    }
}
