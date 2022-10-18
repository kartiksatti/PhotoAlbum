using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Business.Interfaces
{
    internal interface IExecute<T>
    {
        public T Execute(T t,string url);
    }

   
}
