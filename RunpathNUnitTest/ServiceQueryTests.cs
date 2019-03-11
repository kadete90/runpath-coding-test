using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using RunpathWebApi.Models;
using RunpathWebApi.Services;

namespace RunpathUnitTests
{
    [TestFixture]
    public class ServiceQueryTests
    {
        private QueryService _queryService;

        [SetUp]
        public void Setup()
        {
            _queryService = new QueryService();
        }

        [Test]
        public async Task GetAllAlbumsTest()
        {
            _queryService.Should().NotBeNull();

            var albums = await _queryService.GetAllAsync<Album>();
            albums.Should().NotBeNull();
            albums.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetSingleAlbumTest()
        {
            _queryService.Should().NotBeNull();

            var id = 1;
            // TODO create some queryBuilder
            var query = $"{nameof(Album.id)}={id}";

            var albums = await _queryService.GetAllAsync<Album>(query);
            albums.Should().NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(1);
        }

        [Test]
        public async Task GetSingleAlbumFailTest()
        {
            _queryService.Should().NotBeNull();

            var id = -1;
            var query = $"{nameof(Album.id)}={id}";

            var albums = await _queryService.GetAllAsync<Album>(query);
            albums.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllAlbumsByUserTest()
        {
            _queryService.Should().NotBeNull();

            var userId = 1;
            var query = $"{nameof(Album.userId)}={userId}";

            var albums = await _queryService.GetAllAsync<Album>(query);
            albums.Should().NotBeNull();
            albums.Should().NotBeEmpty();
            //albums.Should().HaveCount(10);

            albums.Select(a => a.userId.Should().Be(userId));
        }

        [Test]
        public async Task GetAllPhotosTest()
        {
            _queryService.Should().NotBeNull();

            var photos = await _queryService.GetAllAsync<Photo>();
            photos.Should().NotBeNull();
            photos.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetSinglePhotoTest()
        {
            _queryService.Should().NotBeNull();

            var id = 1;
            var query = $"{nameof(Album.id)}={id}";

            var photos = await _queryService.GetAllAsync<Photo>(query);
            photos.Should().NotBeNull();
            photos.Should().NotBeEmpty();
            photos.Should().HaveCount(1);
        }


        [Test]
        public async Task GetSinglePhotoFailTest()
        {
            _queryService.Should().NotBeNull();

            var id = -1;
            var query = $"{nameof(Album.id)}={id}";

            var photo = await _queryService.GetAllAsync<Photo>(query);
            photo.Should().BeEmpty();
        }
    }
}