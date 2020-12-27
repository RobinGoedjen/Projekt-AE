using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLibrary
{
    public class Map
    {
        private const uint height = 100;
        private const uint width = 100;

        public String filename { get;  }
        public List<List<int>> worldMap { get; set; }

    public override string ToString()
        {
            return filename.Split('.')[0];
        }

        public Map(String fileName)
        {
            this.filename = fileName;
        }
    }
}
