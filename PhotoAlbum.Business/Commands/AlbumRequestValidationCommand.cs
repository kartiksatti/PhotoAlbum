using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Business.Models;

namespace PhotoAlbum.Business.Commands
{
    public class AlbumRequestValidationCommand : IExecute <Album>
    {

        public AlbumRequestValidationCommand() {         
        }             
      
        private Album SetValidationAndReturn(Album retAlbum, string message)
        {
            retAlbum.Message = message;
            return retAlbum;
        }

        public virtual Album Execute(Album album, string url)
        {

            if (string.IsNullOrWhiteSpace(url))
            {
                return SetValidationAndReturn(album, "Album url not set up");
            }

            if (string.IsNullOrWhiteSpace(album.StrId))
            {
                return SetValidationAndReturn(album, "Album Id Cannot Be Empty");
            }

            if (!int.TryParse(album.StrId, out _))
            {
                return SetValidationAndReturn(album, "Album Id Should be an integer");
            }

            if (int.TryParse(album.StrId, out int albumId) && albumId <= 0)
            {
                return SetValidationAndReturn(album, "Album Id Should be greater than 0");
            }

            album.Id = albumId;
            return album;
        }
    }
}