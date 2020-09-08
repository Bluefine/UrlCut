using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlCut.Models
{
    public class URL
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Link { get; set; }
        public string Token { get; set; }
        public int Clicked { get; set; }
        public DateTime Created { get; set; }
    }
}
