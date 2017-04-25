using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCapi.DB
{
    public class Links
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Read { get; set; }
        public string Tag { get; set; }
    }
}
