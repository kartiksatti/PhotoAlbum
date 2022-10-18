using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Business.Models;

namespace PhotoAlbum.Business.Commands
{
    public class AlbumCommand : IExecute <Album>
    {
        AlbumApiGetCommand _albumApi;
        public AlbumCommand(AlbumApiGetCommand albumApi) {
            _albumApi = albumApi;
        }            

        public virtual Album Execute(Album album, string url)
        {
           album = _albumApi.Execute(album, string.Concat(url, album.Id));
                              
           if (!album.Images.Any())
            album.Message = $"Album is empty!";              
           
            return album;
        }
    }
}