using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MapLibrary;

namespace Raycasting
{
    static class SpriteManager
    {
        public readonly static List<Sprite> sprites = new List<Sprite>();
        private readonly static Dictionary<SpriteName, int> spriteTextureIDs = new Dictionary<SpriteName, int>();
        private readonly static Dictionary<SpriteName, String> spritePaths = new Dictionary<SpriteName, String>();

        static SpriteManager()
        {
            string currDirectory = Directory.GetCurrentDirectory() + @"\Sprites\";
            spritePaths.Add(SpriteName.Barrel, currDirectory + "barrel.png");
            spritePaths.Add(SpriteName.Pillar, currDirectory +"pillar.png");
        }
        public static String getSpritePath(SpriteName name)
        {
            return spritePaths[name];
        }

        public static int getSpriteTextureID(SpriteName name)
        {
            return spriteTextureIDs[name];
        }

        public static void addSpriteTextureID(SpriteName name, int ID)
        {
            spriteTextureIDs[name] = ID;
        }

        public static void loadSpritesFromMap(Map map)
        {
            foreach (var sprite in map.sprites)
            {
                sprites.Add(new Sprite(new OpenTK.Vector2(sprite.position.Y, sprite.position.X), sprite.name));
            }
        }
    }
}
