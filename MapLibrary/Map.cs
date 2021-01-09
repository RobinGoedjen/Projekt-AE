using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapLibrary
{
    public enum SpriteName { Barrel, Pillar }
    public class Map
    {
        public uint height { get; }
        public uint width { get; }
        public List<List<sbyte>> worldMap;
        public string name;
        public const byte colorCount = 4;
        public PointF playerStartPosition;
        public ushort playerStartOrientation;
        public List<spriteData> sprites;

        public Map(uint width, uint height)
        {
            this.width = width;
            this.height = height;
            playerStartPosition = new PointF(1f, 1f);
            playerStartOrientation = 0;
            worldMap = new List<List<sbyte>>();
            sprites = new List<spriteData>();
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

    public struct spriteData
    {
        public PointF position;
        public SpriteName name;

        public double getDistanceToPoint(PointF point)
        {
            return Math.Abs(Math.Sqrt(Math.Pow(point.X - this.position.X, 2) + Math.Pow(point.Y - this.position.Y, 2)));
        }
    }
}
