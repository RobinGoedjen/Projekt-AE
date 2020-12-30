using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLibrary
{
    public class Map
    {
        public uint height { get; }
        public uint width { get; }
        public List<List<sbyte>> worldMap;

        public Map(uint width, uint height)
        {
            this.width = width;
            this.height = height;
            worldMap = new List<List<sbyte>>();
        }

    }
}
