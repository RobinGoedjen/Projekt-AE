using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapLibrary
{
    public class Map
    {
        public uint height { get; }
        public uint width { get; }
        public List<List<sbyte>> worldMap;
        public string name;
        public const byte colorCount = 4;
        public PointF playerStartPosition;

        public Map(uint width, uint height)
        {
            this.width = width;
            this.height = height;
            playerStartPosition = new PointF(1f, 1f);
            worldMap = new List<List<sbyte>>();
        }

        public static Color getColorFromTileID(sbyte tileID)
        {
            switch (tileID)
            {
                case 0:
                    return Color.White;
                case 1:
                    return Color.Red;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Blue;
                case 4:
                    return Color.LightGray;
                default:
                    return Color.Gray;
            }
        }
    }
}
