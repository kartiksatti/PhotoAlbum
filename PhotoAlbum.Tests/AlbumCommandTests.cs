using Moq;
using Newtonsoft.Json;
using PhotoAlbum.Business.Commands;
using PhotoAlbum.Business.Models;
using PhotoAlbum.Business.Service;

namespace PhotoAlbum.Tests
{
    public class AlbumCommandTests
    {        
        Mock<AlbumApiService> _albumApiService;
        public AlbumCommandTests() {
            _albumApiService = new Mock<AlbumApiService>();           
        }

        [Fact]
        public void AlbumCommand_Execute_Should_Fail_If_AlbumCall_ReturnsEmpty() 
        {

            var album = new Album { };
            _albumApiService.Setup(x => x.Get(It.IsAny<string>())).Returns("");

            var cmd = new AlbumCommand(_albumApiService.Object);           

            cmd.Execute(album, "url");

            Assert.True(album.HasErrors);
            Assert.True(album.Message== "Album is empty!");

        }
        [Fact]
        public void AlbumCommand_Execute_Should_Pass_If_AlbumCall_Passes_ReturnsImages()
        {

            var album = new Album { };
           
            var error = "Some Error";
            _albumApiService.Setup(x => x.Get("url"))
                               .Returns(@"[{""albumId"": 1,""id"": 1, ""title"":""accusamus beatae ad facilis cum similique qui sunt""}]");
           
            var cmd = new AlbumCommand(_albumApiService.Object);

            cmd.Execute(album, "url");

            Assert.False(album.HasErrors);
            Assert.True(album.Message == null);
            Assert.True(album.Images.Count ==1);
            Assert.True(album.Images.First().Id == 1);
            Assert.True(album.Images.First().Title == "accusamus beatae ad facilis cum similique qui sunt");
        }
    }
}
