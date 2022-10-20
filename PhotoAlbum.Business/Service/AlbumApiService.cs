using System.Net;

namespace PhotoAlbum.Business.Service
{
    public class AlbumApiService
    {
        public virtual string Get(string url)
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
                     return reader.ReadToEnd();
                    }
                }
            }
        }

    }
}
