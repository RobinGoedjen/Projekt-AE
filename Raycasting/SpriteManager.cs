using System;
using System.Collections.Generic;
using System.IO;
using MapLibrary;

namespace Raycasting
{
    static class SpriteManager
    {
        public static ushort totalCoins = 0;
        public readonly static List<Sprite> sprites = new List<Sprite>();
        private readonly static Dictionary<SpriteName, int> spriteTextureIDs = new Dictionary<SpriteName, int>();
        private readonly static Dictionary<SpriteName, String> spritePaths = new Dictionary<SpriteName, String>();

        static SpriteManager()
        {
            string currDirectory = Directory.GetCurrentDirectory() + @"\Sprites\";
            spritePaths.Add(SpriteName.Barrel, currDirectory + "barrel.png");
            spritePaths.Add(SpriteName.Pillar, currDirectory + "pillar.png");
            spritePaths.Add(SpriteName.Portal, currDirectory + "portal.png");
            spritePaths.Add(SpriteName.Portal_Inactive, currDirectory + "portal_inactive.png");
            spritePaths.Add(SpriteName.Coin, currDirectory + "coin.png");
            spritePaths.Add(SpriteName.Armor, currDirectory + "armor.png");
            spritePaths.Add(SpriteName.Pillar_brown, currDirectory + "brown_pillar.png");
            spritePaths.Add(SpriteName.Skeleton, currDirectory + "skeleton.png");
            spritePaths.Add(SpriteName.Skull, currDirectory + "skull.png");
            spritePaths.Add(SpriteName.Bone_Pile, currDirectory + "skulls.png");
            spritePaths.Add(SpriteName.Well, currDirectory + "well.png");
            spritePaths.Add(SpriteName.Well_blood, currDirectory + "well_blood.png");
            spritePaths.Add(SpriteName.Dead_Tree, currDirectory + "dead_tree.png");
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
                sprites.Add(new Sprite(new OpenTK.Vector2(sprite.position.Y, sprite.position.X), sprite.name == SpriteName.Portal ? SpriteName.Portal_Inactive : sprite.name));
                if (sprite.name == SpriteName.Coin)
                    totalCoins++;
            }
        }
    }
}
