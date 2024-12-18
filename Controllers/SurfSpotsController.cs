﻿using Microsoft.AspNetCore.Cors;
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
                List<SurfSpot> surfSpots = ds.getSurfSpots();

                return Ok(surfSpots.ToArray());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<SurfSpot> Post([FromBody] SurfSpot spot)
        {
            try
            {
                SurfSpotsDataService ds = new SurfSpotsDataService();
                SurfSpot s = ds.AddSurfSpot(spot);

                if (s != null)
                    return Ok(s);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
