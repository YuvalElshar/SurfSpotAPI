using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurfSpotAPI.DAL;
using SurfSpotAPI.Models;

namespace SurfSpotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<User[]> Get()
        {
            try
            {
                UsersDataService ds = new UsersDataService();
                List<User> users = ds.GetUsers();
                return Ok(users.ToArray());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
                UsersDataService ds = new UsersDataService();
                User u = ds.AddUser(user);

                if (u != null)
                    return Ok(u);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
