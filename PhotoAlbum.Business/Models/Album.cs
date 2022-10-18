using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Business.Models
{ 
        public class Album : Base
        {
            public Album() {
                Images = new List<AlbumImage>();
                }

            public string StrId { get; set; }             
            public int? Id { get; set; }
            public List<AlbumImage> Images { get; set; }
        }
}
