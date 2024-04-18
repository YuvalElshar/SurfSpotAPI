using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurfSpotAPI.DAL;
using SurfSpotAPI.Models;

namespace SurfSpotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurfSpotsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<SurfSpot[]> Get()
        {
            try
            {
                SurfSpotsDataService ds = new SurfSpotsDataService();
                return Ok(SurfSpotsDB.SurfSpotsList.ToArray());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
