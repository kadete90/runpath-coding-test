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
    public class AlbumsController : ControllerBase
    {
        private readonly IQueryService _queryService;

        public AlbumsController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        // GET api/albums
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Album>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var albums = await _queryService.GetAllAsync<Album>();

            return albums.Any() 
                ? (IActionResult) Ok(albums) 
                : NotFound();
        }

        // GET api/albums/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Album), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var albums = await _queryService.GetAllAsync<Album>($"id={id}");

            return albums.Any()
                ? (IActionResult) Ok(albums) 
                : NotFound();
        }

        // GET api/albums/user/5
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(Album), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var albumsByUserId = await _queryService.GetAllAsync<Album>($"userId={userId}");

            /* // Different inefficient approach
            var fetchAllAlbums = await _queryService.GetAllAsync<Album>($"{_albumsPrefix}");
            var albumsByUserId = fetchAllAlbums.Where(a => a.userId == userId);
            */

            return albumsByUserId != null
                ? (IActionResult)Ok(albumsByUserId)
                : NotFound();
        }
    }
}
