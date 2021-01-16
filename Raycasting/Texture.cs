using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Raycasting
{
    class Texture
    {
        public readonly int VAO;
        public readonly int VBO;
        public readonly List<Vector2> vertices;
        public readonly Vector4 color;
        public readonly GameTexture texture;

        public Texture(GameTexture texture, Vector4 color, int VAO, int VBO)
        {
            this.texture = texture;
            this.color = color;
            this.VAO = VAO;
            this.VBO = VBO;
            vertices = new List<Vector2>();
        }

        public void addOpenVertice(float drawXScaled, float drawYScaled)
        {
            vertices.Add(new Vector2(drawXScaled, drawYScaled));
            vertices.Add(new Vector2(drawXScaled, -drawYScaled));
        }

        public void addCloseVertice(float drawXScaled, float drawYScaled)
        {
            addOpenVertice(drawXScaled, -drawYScaled);
        }
    }
}
