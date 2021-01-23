using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using MapLibrary;

namespace Raycasting
{
    public enum GameTexture { Shadow, RedWall, GreenWall, BlueWall, LightGreyWall }  
    static class GameTextureManager
    {
        public static readonly Dictionary<GameTexture, Texture> textureDictionary;

        static GameTextureManager()
        {
            textureDictionary = new Dictionary<GameTexture, Texture>();
            foreach (GameTexture texture in (GameTexture[])Enum.GetValues(typeof(GameTexture)))
            {
                int VAO = GL.GenVertexArray();
                int VBO = GL.GenBuffer();

                textureDictionary.Add(texture, new Texture(texture, colorToVec4(Map.getColorFromTileID((sbyte)texture)), VAO, VBO));
            }

        }

        public static void fillAllVAOs()
        {
            foreach (var texture in textureDictionary)
            {
                var currentTexture = texture.Value;
                GL.BindVertexArray(currentTexture.VAO);
                GL.BindBuffer(BufferTarget.ArrayBuffer, currentTexture.VBO);
                GL.BufferData(BufferTarget.ArrayBuffer, currentTexture.vertices.Count * Vector4.SizeInBytes, currentTexture.vertices.ToArray(), BufferUsageHint.DynamicDraw);
                GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, Vector4.SizeInBytes, 0);
                GL.EnableVertexAttribArray(0);
                GL.BindVertexArray(0);
            }
        }

        public static void clearAllVAOs()
        {
            foreach (var texture in textureDictionary)
            {
                texture.Value.vertices.Clear();
            }
        }


        //TODO SPÄTER WEG
        private static Vector4 colorToVec4(Color color)
        {
            return new Vector4((float)color.R / 255, (float)color.G / 255, (float)color.B / 255, (float)color.A / 255);
        }

        public static Texture getTextureByID(sbyte tileID)
        {
            return textureDictionary[(GameTexture)tileID];
        }
    }
}
