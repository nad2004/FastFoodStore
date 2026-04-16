using Microsoft.AspNetCore.Mvc;
using Infrastructure.Persistence;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class DbTestController : BaseStoreController
    {
        private readonly AppDbContext _context;

        public DbTestController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckDbConnection()
        {
            try
            {
                // CanConnectAsync tests whether the database exists and can be reached.
                bool canConnect = await _context.Database.CanConnectAsync();

                if (canConnect)
                {
                    return Ok(new 
                    { 
                        Success = true, 
                        Message = "Successfully established a connection to the PostgreSQL database!" 
                    });
                }

                return StatusCode(500, new 
                { 
                    Success = false, 
                    Message = "Could not connect to the PostgreSQL database. Ensure the container is running and credentials are correct." 
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new 
                { 
                    Success = false, 
                    Message = "An exception occurred while verifying the database connection.",
                    Error = ex.Message
                });
            }
        }
    }
}
