using Newtonsoft.Json;
using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Business.Models;
using PhotoAlbum.Business.Service;

namespace PhotoAlbum.Business.Commands
{
    public class AlbumCommand : IExecute <Album>
    {
        AlbumApiService _albumApi;
        public AlbumCommand(AlbumApiService albumApi) {
            _albumApi = albumApi;
        }            

        public virtual Album Execute(Album album, string url)
        {

            var images = JsonConvert.DeserializeObject<List<AlbumImage>>(_albumApi.Get(string.Concat(url, album.Id)));
            if(images == null || !images.Any())
            {
                album.Message = $"Album is empty!";
                return album;
            }
            
            album.Images.AddRange(images);
            return album;
        }
    }
}