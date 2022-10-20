using Moq;
using PhotoAlbum.Business;
using PhotoAlbum.Business.Commands;
using PhotoAlbum.Business.Models;
using PhotoAlbum.Business.Service;


namespace PhotoAlbum.Tests
{
    public class AlbumOrchestratorTests
    {
        Mock<AlbumApiService> _albumApiGetServiceMock;
        Mock<AlbumRequestValidationCommand> _albumRequestValidationCommandMock;
        Mock<AlbumCommand> _albumCommandMock;

        public AlbumOrchestratorTests()
        {
            _albumApiGetServiceMock = new Mock<AlbumApiService>();         
            _albumCommandMock = new Mock<AlbumCommand >(_albumApiGetServiceMock.Object);
            _albumRequestValidationCommandMock = new Mock<AlbumRequestValidationCommand>();
        }


        [Fact]
        public void AlbumOrchestrator_Should_Not_Call_AlbumCommand_If_ValidationFails()
        {
            var album = new Album() {
                StrId = "AA",
            };
            var url = "api";
           
            _albumRequestValidationCommandMock.Setup(x => x.Execute(It.IsAny<Album>(), It.IsAny<string>())).Returns(new Album() {Message ="Some Error" });
            
            var orca = new AlbumOrchestrator(_albumRequestValidationCommandMock.Object, _albumCommandMock.Object);
            orca.Execute(album.StrId, url);

            _albumCommandMock.Verify(x => x.Execute(It.IsAny<Album>(), It.IsAny<string>()), Times.Never);

        }

        [Fact]
        public void AlbumOrchestrator_Should_Call_AlbumCommand_If_ValidationSucceeds()
        {
          
            _albumRequestValidationCommandMock.Setup(x => x.Execute(It.IsAny<Album>(), It.IsAny<string>())).Returns(new Album() {Id= 1});

            var orca = new AlbumOrchestrator(_albumRequestValidationCommandMock.Object, _albumCommandMock.Object);
            orca.Execute("1", "");

            _albumCommandMock.Verify(x => x.Execute(It.IsAny<Album>(), "" ), Times.Once);

        }

        [Fact]
        public void AlbumOrchestrator_Should_Fail_If_AlbumCallApi_Call_Fails()
        {

            var album = new Album { };
            var error = "Some Error";
            _albumApiGetServiceMock.Setup(x => x.Get("url")).Throws(new Exception(error));
            _albumRequestValidationCommandMock.Setup(x => x.Execute(It.IsAny<Album>(), It.IsAny<string>())).Returns(new Album() { Id = 1 });

            var orca = new AlbumOrchestrator(_albumRequestValidationCommandMock.Object,new AlbumCommand(_albumApiGetServiceMock.Object));

            album = orca.Execute("1", "");

            Assert.True(album.HasErrors);
            Assert.True(album.Message.Contains("An unhandeled error occured"));
        }


    }
}
