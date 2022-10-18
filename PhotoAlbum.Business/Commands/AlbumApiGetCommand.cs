using Newtonsoft.Json;
using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Business.Models;
using System.Net;

namespace PhotoAlbum.Business.Commands
{   
    public class AlbumApiGetCommand : IExecute<Album>
    {
        public virtual Album Execute(Album album, string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                    {
                        album.Images = JsonConvert.DeserializeObject<List<AlbumImage>>(reader.ReadToEnd());

                        return album;
                    }
                }
            }
        }  

    }
}
