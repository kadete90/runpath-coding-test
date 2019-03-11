using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunpathWebApi.Models;
using RunpathWebApi.Services;

namespace RunpathWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IQueryService _queryService;

        public PhotosController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        // GET api/photos
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Photo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var photos = await _queryService.GetAllAsync<Photo>();

            return photos.Any() 
                ? (IActionResult) Ok(photos) 
                : NotFound();
        }

        // GET api/photos/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Photo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var photos = await _queryService.GetAllAsync<Album>($"id={id}");

            return photos.Any() 
                ? Ok(photos) 
                : (IActionResult) NotFound();
        }

        // GET api/photos/user/5
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<Photo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var albumsByUser = await _queryService.GetAllAsync<Album>($"userId={userId}");

            if (albumsByUser == null || !albumsByUser.Any())
            {
                return NotFound();
            }

            var idsQuery = "albumId=" + string.Join("&albumId=", albumsByUser.Select(a => a.id));

            var photos = await _queryService.GetAllAsync<Photo>(idsQuery);

            return photos.Any()
                ? (IActionResult)Ok(photos)
                : NotFound();

        }
    }
}
