using OpenTK;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    class Sprite : IComparable<Sprite>
    {
        public enum SpriteName { barrel }
        private Vector2 position { get; }
        private SpriteName texture { get; }

        public readonly static List<Sprite> sprites = new List<Sprite>();
        private readonly static Dictionary<SpriteName, String> spriteDict = new Dictionary<SpriteName, String>(); //Hier nochma typ ändern? TODO

        private float distanceToPlayer { get; set; }
        public Sprite(Vector2 position, SpriteName texture)
        {
            this.position = position;
            this.texture = texture;
        }

        static Sprite()
        {
            Sprite.registerSprite(SpriteName.barrel, Directory.GetCurrentDirectory() + "/Sprites/barrel.png");
        }

        private static void registerSprite(SpriteName name, String path)
        {
            spriteDict.Add(name, path);
        }

        public static String getSprite(SpriteName name)
        {
            return spriteDict[name];
        }

        public void updateDistanceToPlayer(Vector2 playerPosition)
        {
            distanceToPlayer = (float)(Math.Pow(playerPosition.X - position.X, 2f) + Math.Pow(playerPosition.Y - position.Y, 2f)); 
        }

        public int CompareTo(Sprite other)
        {
            return (int)(this.distanceToPlayer - other.distanceToPlayer);   
        }

         
        
    }
}
