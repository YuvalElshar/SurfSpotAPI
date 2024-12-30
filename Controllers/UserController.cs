using Microsoft.AspNetCore.Mvc;
using SurfSpotAPI.DAL;
using SurfSpotAPI.Models;

namespace SurfSpotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Existing GET method to retrieve all users
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

        // Existing POST method to add a user
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

        // Existing Login method for user authentication
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                UsersDataService ds = new UsersDataService();
                int result = ds.ValidateUser(request.Email, request.Password);

                switch (result)
                {
                    case 0:
                        return Ok("Login successful");
                    case -1:
                        return NotFound("User not found");
                    case -2:
                        return Unauthorized("Invalid password");
                    default:
                        return BadRequest("Unexpected error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Like a SurfSpot endpoint
        [HttpPost("like/{userId}/{surfSpotId}")]
        public IActionResult LikeSpot(int userId, int surfSpotId)
        {
            try
            {
                UsersDataService ds = new UsersDataService();
                bool liked = ds.LikeSurfSpot(userId, surfSpotId);

                if (liked)
                    return Ok("Surf spot liked successfully.");
                else
                    return Conflict("User has already liked this surf spot.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Unlike a SurfSpot endpoint
        [HttpPost("unlike/{userId}/{surfSpotId}")]
        public IActionResult UnlikeSpot(int userId, int surfSpotId)
        {
            try
            {
                UsersDataService ds = new UsersDataService();
                bool unliked = ds.UnlikeSurfSpot(userId, surfSpotId);

                if (unliked)
                    return Ok("Surf spot unliked successfully.");
                else
                    return NotFound("User has not liked this surf spot.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Get all liked spots for a specific user
        [HttpGet("likedspots/{userId}")]
        public IActionResult GetLikedSpots(int userId)
        {
            try
            {
                UsersDataService ds = new UsersDataService();
                var likedSpots = ds.GetLikedSpots(userId);

                if (likedSpots == null || likedSpots.Count == 0)
                    return NotFound("No liked spots found.");

                return Ok(likedSpots);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    // LoginRequest model
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
