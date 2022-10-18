using PhotoAlbum.Business.Commands;
using PhotoAlbum.Business.Models;

namespace PhotoAlbum.Business
{
    public class AlbumOrchestrator 
    {

        public AlbumOrchestrator(AlbumRequestValidationCommand albumValidatorCommand,AlbumCommand albumCommand) {
            _albumCommand = albumCommand;
            _albumValidatorCommand = albumValidatorCommand;
        }

        private AlbumCommand _albumCommand;
        private AlbumRequestValidationCommand _albumValidatorCommand;

        public virtual Album Execute(string albumId,string url)
        {
            var album = new Album();

            try {
                album.StrId = albumId;
                album = _albumValidatorCommand.Execute(album, url);

                if (album.HasErrors) return album;

                _albumCommand.Execute(album, url);
            }
            catch (Exception ex)
            {
                album.Message = $"An unhandeled error occured : {ex.Message}";
            }
           

            return album;
        }
    }
}
