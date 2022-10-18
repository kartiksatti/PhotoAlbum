using Moq;
using PhotoAlbum.Business.Commands;
using PhotoAlbum.Business.Models;

namespace PhotoAlbum.Tests
{
    public class AlbumCommandTests
    {        
        Mock<AlbumApiGetCommand> _albumApiGetCommand;
        public AlbumCommandTests() {
            _albumApiGetCommand = new Mock<AlbumApiGetCommand>();           
        }

        [Fact]
        public void AlbumCommand_Execute_Should_Fail_If_AlbumCall_ReturnsEmpty() 
        {

            var album = new Album { };
            _albumApiGetCommand.Setup(x => x.Execute(album, "url")).Returns(album);

            var cmd = new AlbumCommand(_albumApiGetCommand.Object);           

            cmd.Execute(album, "url");

            Assert.True(album.HasErrors);
            Assert.True(album.Message== "Album is empty!");

        }


        [Fact]
        public void AlbumCommand_Execute_Should_Fail_If_AlbumCall_Fails_ReturnsEmpty()
        {

            var album = new Album { };
            var error = "Some Error";
            _albumApiGetCommand.Setup(x => x.Execute(album, "url")).Throws(new Exception(error));

            var cmd = new AlbumCommand(_albumApiGetCommand.Object);

            cmd.Execute(album, "url");

            Assert.True(album.HasErrors);
            Assert.True(album.Message== $"An unhandeled error occured : {error}");
        }
        [Fact]
        public void AlbumCommand_Execute_Should_Pass_If_AlbumCall_Passes_ReturnsImages()
        {

            var album = new Album { };
            album.Images = new List<AlbumImage>() { new AlbumImage() { Id = 1, Title = "Image1" } };
            var error = "Some Error";
            _albumApiGetCommand.Setup(x => x.Execute(album, "url"))
                               .Returns(album);
           
            var cmd = new AlbumCommand(_albumApiGetCommand.Object);

            cmd.Execute(album, "url");

            Assert.False(album.HasErrors);
            Assert.True(album.Message == null);
            Assert.True(album.Images.Count ==1);
            Assert.True(album.Images.First().Id == 1);
            Assert.True(album.Images.First().Title == "Image1");
        }
    }
}
