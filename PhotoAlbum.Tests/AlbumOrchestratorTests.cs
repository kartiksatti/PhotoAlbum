using Moq;
using PhotoAlbum.Business;
using PhotoAlbum.Business.Commands;
using PhotoAlbum.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Tests
{
    public class AlbumOrchestratorTests
    {
        Mock<AlbumApiGetCommand> _albumApiGetCommandMock;
        Mock<AlbumRequestValidationCommand> _albumRequestValidationCommandMock;
        Mock<AlbumCommand> _albumCommandMock;

        public AlbumOrchestratorTests()
        {
            _albumApiGetCommandMock = new Mock<AlbumApiGetCommand>();
         
            _albumCommandMock = new Mock<AlbumCommand >(_albumApiGetCommandMock.Object);
        }


        [Fact]
        public void AlbumOrchestrator_Should_Not_Call_AlbumCommand_If_ValidationFails()
        {
            var album = new Album() {
                StrId = "AA",
            };
            var url = "api";
            _albumRequestValidationCommandMock = new Mock<AlbumRequestValidationCommand>();
            _albumRequestValidationCommandMock.Setup(x => x.Execute(It.IsAny<Album>(), It.IsAny<string>())).Returns(new Album() {Message ="Some Error" });
            
            var orca = new AlbumOrchestrator(_albumRequestValidationCommandMock.Object, _albumCommandMock.Object);
            orca.Execute(album.StrId, url);

            _albumCommandMock.Verify(x => x.Execute(It.IsAny<Album>(), It.IsAny<string>()), Times.Never);

        }

        [Fact]
        public void AlbumOrchestrator_Should_Call_AlbumCommand_If_ValidationSucceeds()
        {
            _albumRequestValidationCommandMock = new Mock<AlbumRequestValidationCommand>();
            _albumRequestValidationCommandMock.Setup(x => x.Execute(It.IsAny<Album>(), It.IsAny<string>())).Returns(new Album() {Id= 1});

            var orca = new AlbumOrchestrator(_albumRequestValidationCommandMock.Object, _albumCommandMock.Object);
            orca.Execute("1", "");

            _albumCommandMock.Verify(x => x.Execute(It.IsAny<Album>(), "" ), Times.Once);

        }

    }
}
