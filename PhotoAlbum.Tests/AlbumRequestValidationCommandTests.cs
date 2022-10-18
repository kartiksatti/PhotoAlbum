using PhotoAlbum.Business;
using PhotoAlbum.Business.Commands;
using PhotoAlbum.Business.Models;

namespace PhotoAlbum.Tests
{
    public class AlbumRequestValidationCommandTests
    {

        AlbumRequestValidationCommand _albumRequestValidationCommand;
        public AlbumRequestValidationCommandTests() {
            _albumRequestValidationCommand = new AlbumRequestValidationCommand();
        }

        [Theory]
        [InlineData(null, "album.com")]        
        [InlineData("","album.com")]
        [InlineData(" ", "album.com")]
        public void AlbumRequestValidationCommand_Execute_Should_Fail_NullOrEmpty(string val,string url)
        {
          
            var result= _albumRequestValidationCommand.Execute(new Album() { StrId = val }, url);

            Assert.True(result.HasErrors);
            Assert.True(result.Message == "Album Id Cannot Be Empty");
        }       
       
        [Theory]
        [InlineData("-1", "album.com")]
        [InlineData("0", "album.com")]
        public void AlbumRequestValidationCommand_Execute_Should_Fail_NegativesAndZero(string val, string url)
        {        
            var result = _albumRequestValidationCommand.Execute(new Album() { StrId = val }, url);

            Assert.True(result.HasErrors);
            Assert.True(result.Message == "Album Id Should be greater than 0");
        }

        [Fact]
        public void AlbumRequestValidationCommand_Execute_Should_Fail_For_NonInteger()
        {
            var result = _albumRequestValidationCommand.Execute(new Album() { StrId = "W" }, "album.com");

            Assert.True(result.HasErrors);
            Assert.True(result.Message == "Album Id Should be an integer");
        }

        [Fact]
        public void AlbumRequestValidationCommand_Execute_Should_Pass_For_Int()
        {
            var result = _albumRequestValidationCommand.Execute(new Album() { StrId = "10" }, "album.com");

            Assert.True(result.Id == 10);
            Assert.False(result.HasErrors);
            Assert.True(result.Message == null);
        }

        [Theory]
        [InlineData(null )]
        [InlineData("" )]
        [InlineData(" ")]
        public void AlbumRequestValidationCommand_Execute_Should_Fail_for_EmptyUrl( string url)
        {
            var result = _albumRequestValidationCommand.Execute(new Album() { StrId = "10" }, url);

            Assert.False(result.Id.HasValue );
            Assert.True(result.HasErrors);
            Assert.True(result.Message == "Album url not set up");
        }


    }
}