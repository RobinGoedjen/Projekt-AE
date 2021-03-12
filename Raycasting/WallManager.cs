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
using System.Drawing.Imaging;

namespace Raycasting
{
    static class WallManager
    {
        public static readonly Dictionary<WallKind, Wall> textureDictionary;

        static WallManager()
        {
            textureDictionary = new Dictionary<WallKind, Wall>();
            foreach (WallKind texture in (WallKind[])Enum.GetValues(typeof(WallKind)))
            {
                if (texture == WallKind.None)
                    continue;

                textureDictionary.Add(texture, new Wall(texture, colorToVec4(Map.getColorFromGameTexture(texture))));
            }

        }

        public static void fillAllVAOs()
        {
            foreach (var texture in textureDictionary)
            {
                var currentTexture = texture.Value;
                fillVAO(currentTexture);
            }
        }

        public static void fillVAO(Texture text)
        {
            GL.BindVertexArray(text.VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, text.VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, text.vertices.Count * Vector4.SizeInBytes, text.vertices.ToArray(), BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, Vector4.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);
        }

        public static void clearAllVAOs()
        {
            foreach (var texture in textureDictionary)
            {
                texture.Value.vertices.Clear();
            }
        }

        public static Vector4 colorToVec4(Color color)
        {
            return new Vector4((float)color.R / 255, (float)color.G / 255, (float)color.B / 255, (float)color.A / 255);
        }

        public static Wall getTextureByGameTexture(WallKind texture)
        {
            return textureDictionary[(WallKind)texture];
        }

        public static int generateTexture(string texturePath)
        {
            int newTextureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, newTextureID);


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            Bitmap bmp = new Bitmap(texturePath);
            bmp.MakeTransparent(Color.FromArgb(255, 255, 0, 220));
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            bmp.UnlockBits(data);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            return newTextureID;
        }
    }
}
