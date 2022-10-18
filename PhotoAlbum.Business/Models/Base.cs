using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Business.Models
{
    public class Base
    {
        public bool HasErrors => !string.IsNullOrWhiteSpace(Message);
        public string Message { get; set; }
    }
}
