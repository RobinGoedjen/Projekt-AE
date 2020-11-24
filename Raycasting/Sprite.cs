using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    class Sprite
    {
        public enum SpriteName { barrel }
        private Vector2 position { get; }

        private SpriteName texture { get; }

        public readonly static List<Sprite> allSprites = new List<Sprite>();
        private readonly static Dictionary<SpriteName, String> spriteDict = new Dictionary<SpriteName, String>(); //Hier nochma typ ändern?

        public static void registerSprite(SpriteName name, String path)
        {
            spriteDict.Add(name, path);
        }

        public static String getSprite(SpriteName name)
        {
            return spriteDict[name];
        }

        public Sprite(Vector2 position, SpriteName texture)
        {
            this.position = position;
            this.texture = texture;
        }

         
        
    }
}
