using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapLibrary
{
    public enum SpriteName { Barrel, Pillar, Portal, Portal_Inactive, Coin, Armor, Pillar_brown, Skeleton, Skull, Bone_Pile, Well, Well_blood, Dead_Tree }
    public enum WallKind { None, Shadow, RedWall, GreenWall, BlueWall, LightGreyWall, VioletWall, OrangeWall, DarkWall, LightBlueWall }
    public class Map
    {
        public uint height { get; }
        public uint width { get; }
        public List<List<WallKind>> worldMap;
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
            worldMap = new List<List<WallKind>>();
            sprites = new List<spriteData>();
        }

        public static Color getColorFromGameTexture(WallKind texture)
        {
            switch (texture)
            {
                case WallKind.None:
                    return Color.White;
                case WallKind.RedWall:
                    return Color.FromArgb(145, 9, 30);
                case WallKind.GreenWall:
                    return Color.FromArgb(8, 128, 17);
                case WallKind.BlueWall:
                    return Color.FromArgb(21, 25, 101);
                case WallKind.LightGreyWall:
                    return Color.LightGray;
                case WallKind.VioletWall:
                    return Color.FromArgb(57, 6, 90);
                case WallKind.OrangeWall:
                    return Color.FromArgb(207, 122, 43);
                case WallKind.DarkWall:
                    return Color.FromArgb(57, 62, 70);
                case WallKind.LightBlueWall:
                    return Color.FromArgb(70, 179, 230);
                default:
                    return Color.HotPink;
            }
        }

        public static bool canBeDrawnByUser(SpriteName sprite)
        {
            return sprite != SpriteName.Portal_Inactive;
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
