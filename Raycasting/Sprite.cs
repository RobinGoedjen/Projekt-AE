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
        public Vector2 position { get; }
        private SpriteName texture { get; }
        public Vector2 drawStart;
        public Vector2 drawEnd;
        public bool visible;
        public float firstTextureX;
        public float lastTextureX;

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
            return (int)(other.distanceToPlayer - this.distanceToPlayer);   
        }

        public void transformDrawToScrren(int screenWidth, int scrrenHeight)
        {
            this.drawStart = scaleVecotrToScreen(this.drawStart, screenWidth, scrrenHeight);
            this.drawEnd = scaleVecotrToScreen(this.drawEnd, screenWidth, scrrenHeight);
        }
         
        private Vector2 scaleVecotrToScreen(Vector2 input, int width, int height)
        {
            float newX = (float)input.X / width * 2f - 1f;
            float newY = (float)input.Y / height * 2f - 1f;           
            return new Vector2(newX, newY);     
        }
        
    }
}
